using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Results;

namespace DotnetComp.Services
{
    public interface IUserService
    {
        Task<Result<User>> FindOrCreateUserAsync(string authUserId);
        Task<BaseResult> CreateGroupOnUserAsync(string authUserId, string groupName);
        Task<BaseResult> AddPlayerOnGroupAsync(
            string authUserId,
            string groupName,
            string playerName
        );

        Task<Result<Group>> GetGroupAsync(string authUserId, string groupName);

        Task<BaseResult> RemovePlayerFromGroup(
            string authUserId,
            string groupName,
            string playerName
        );
        Task<BaseResult> SyncPlayerExperienceOnGroup(string userAuthId, string groupName);
    }

    public class UserService(
        ILogger<UserService> logger,
        IUserRepository userRepository,
        IGroupRepository groupRepository,
        IPlayerService playerService
    ) : IUserService
    {
        private readonly ILogger<UserService> logger = logger;
        private readonly IUserRepository userRepository = userRepository;
        private readonly IGroupRepository groupRepository = groupRepository;
        private readonly IPlayerService playerService = playerService;

        public async Task<Result<User>> FindOrCreateUserAsync(string authUserId)
        {
            try
            {
                var user = await userRepository.GetUserIncludingGroupsAndPlayersAsync(authUserId);
                if (user != null)
                {
                    logger.LogInformation("User exists, returning user");
                    return Result<User>.Success(User.ToDomain(user));
                }
            }
            catch
            {
                logger.LogError("Error occured when fetching user");
                return Result<User>.Failure(UserServiceError.ErrorWhenGettingUser(authUserId));
            }

            try
            {
                // Create user
                UserEntity userEntity = new()
                {
                    Username = "basic user",
                    AuthProvider = new AuthProviderEntity()
                    {
                        Name = "github",
                        AuthProviderUserId = authUserId,
                    },
                };

                var user = await userRepository.CreateUserAsync(userEntity);
                logger.LogInformation("User didn't already exist, created a new user.");
                // return user
                return Result<User>.Success(User.ToDomain(user));
            }
            catch
            {
                return Result<User>.Failure(UserServiceError.ErrorWhileCreatingUser(authUserId));
            }
        }

        public async Task<BaseResult> CreateGroupOnUserAsync(string authUserId, string groupName)
        {
            try
            {
                var userEntity = await userRepository.GetUserIncludingGroupsAsync(authUserId);
                if (userEntity == null)
                {
                    logger.LogInformation("User didn't exist");
                    return BaseResult.Failure(UserServiceError.UserNotFound(authUserId));
                }

                var doesGroupAlreadyExist = userEntity.Groups.Any(x => x.GroupName == groupName);

                if (doesGroupAlreadyExist)
                {
                    logger.LogInformation("Group {groupName} already exists", groupName);
                    return BaseResult.Failure(UserServiceError.GroupAlreadyExists(groupName));
                }

                // Create group adn add it to the user
                var groupEntity = new GroupEntity() { GroupName = groupName };
                userEntity.Groups.Add(groupEntity);
                await userRepository.UpdateAsync(userEntity);

                //var _ = await userRepository.AddGroupToUserAsync(authUserId, groupName);

                return BaseResult.Success();
            }
            catch (Exception e)
            {
                logger.LogError("Unkown excepction {e} ", e);
                return BaseResult.Failure(
                    UserServiceError.ErrorWhileAddinggroupToUser(authUserId, groupName)
                );
            }
        }

        public async Task<BaseResult> AddPlayerOnGroupAsync(
            string authUserId,
            string groupName,
            string playerName
        )
        {
            UserEntity? userEntity = await userRepository.GetUserIncludingGroupsAndPlayersAsync(
                authUserId
            );

            if (userEntity == null)
            {
                return BaseResult.Failure(UserServiceError.UserNotFound(authUserId));
            }

            var groupEntity = userEntity.Groups.FirstOrDefault(x => x.GroupName == groupName);

            if (groupEntity == null)
            {
                return BaseResult.Failure(UserServiceError.GroupNotFound(groupName));
            }

            var playerAlreadyExistsOnGroup = groupEntity.Players.Any(p =>
                p.PlayerName == playerName
            );

            if (playerAlreadyExistsOnGroup)
            {
                return BaseResult.Failure(UserServiceError.PlayerAlreadyExistsOnGroup(playerName));
            }

            // Get player
            var playerEntityResult = await playerService.GetOrCreatePlayerAsync(playerName);

            if (!playerEntityResult.IsSuccess)
            {
                return BaseResult.Failure(UserServiceError.ErrorWhileGettingPlayer(playerName));
            }
            groupEntity.Players.Add(playerEntityResult.Value);
            logger.LogInformation("Updating group");
            await groupRepository.UpdateAsync(groupEntity);
            return BaseResult.Success();
        }

        public async Task<Result<Group>> GetGroupAsync(string userAuthId, string groupName)
        {
            var userEntity =
                await userRepository.GetUserIncludingGroupsAndPlayersAndExperienceAsync(userAuthId);
            if (userEntity == null)
            {
                return Result<Group>.Failure(UserServiceError.UserNotFound(userAuthId));
            }

            var groupEntity = userEntity.Groups.FirstOrDefault(g => g.GroupName == groupName);
            if (groupEntity == null)
            {
                return Result<Group>.Failure(UserServiceError.GroupNotFound(groupName));
            }
            return Result<Group>.Success(Group.ToDomain(groupEntity));
        }

        public async Task<BaseResult> RemovePlayerFromGroup(
            string userAuthId,
            string groupName,
            string playerName
        )
        {
            UserEntity? userEntity = await userRepository.GetUserIncludingGroupsAndPlayersAsync(
                userAuthId
            );

            if (userEntity == null)
            {
                return BaseResult.Failure(UserServiceError.UserNotFound(userAuthId));
            }

            var groupEntity = userEntity.Groups.FirstOrDefault(x => x.GroupName == groupName);

            if (groupEntity == null)
            {
                return BaseResult.Failure(UserServiceError.GroupNotFound(groupName));
            }

            var playerEntity = groupEntity.Players.FirstOrDefault(p => p.PlayerName == playerName);

            if (playerEntity == null)
            {
                return BaseResult.Failure(UserServiceError.PlayerNotFoundOnGroup(playerName));
            }

            groupEntity.Players.Remove(playerEntity);
            await groupRepository.UpdateAsync(groupEntity);
            return BaseResult.Success();
        }

        public async Task<BaseResult> SyncPlayerExperienceOnGroup(
            string userAuthId,
            string groupName
        )
        {
            var userEntity = await userRepository.GetUserIncludingGroupsAndPlayersAsync(userAuthId);
            if (userEntity == null)
            {
                return BaseResult.Failure(UserServiceError.UserNotFound(userAuthId));
            }

            var user = User.ToDomain(userEntity);

            var group = user.Groups.FirstOrDefault(g => g.GroupName == groupName);

            if (group == null)
                return BaseResult.Failure(UserServiceError.GroupNotFound(groupName));

            var tasks = new List<Task<BaseResult>>();

            foreach (Player player in group.Players)
            {
                async Task<BaseResult> SyncPlayerExperience()
                {
                    logger.LogInformation(
                        "Adding experience entry for player {playerName}...",
                        player.PlayerName
                    );

                    var playerHiscoreResult =
                        await playerService.AddExperienceEntryForTodaysDateAsync(player.PlayerName);

                    if (!playerHiscoreResult.IsSuccess)
                    {
                        return BaseResult.Failure(
                            UserServiceError.ErrorWhileGettingPlayer(player.PlayerName)
                        );
                    }

                    return BaseResult.Success();
                }

                // Throttle
                Task.Delay(500).Wait();

                tasks.Add(SyncPlayerExperience());
            }

            await Task.WhenAll(tasks);
            if (tasks.Any(t => !t.Result.IsSuccess))
            {
                return BaseResult.Failure(UserServiceError.ErrorWhileGettingPlayer("unknown"));
            }

            return BaseResult.Success();
        }
    };
}
