using System.Linq;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Results;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotnetComp.Services
{
    public interface IPlayerService
    {
        Task<Result<PlayerEntity>> GetOrCreatePlayerAsync(string playerName);

        Task<Result<PlayerEntity>> GetByPlayerNameDetailed(string playerName);

        Task<BaseResult> AddExperienceEntryForTodaysDateAsync(string playerName);
    }

    public class PlayerService(
        ILogger<PlayerService> logger,
        IPlayerRepository playerRepository,
        IHiscoreService hiscoreService
    ) : IPlayerService
    {
        private readonly IPlayerRepository playerRepository = playerRepository;

        private readonly IHiscoreService hiscoreService = hiscoreService;
        private readonly ILogger<PlayerService> logger = logger;

        public async Task<Result<PlayerEntity>> GetOrCreatePlayerAsync(string playerName)
        {
            try
            {
                var playerEntity = await playerRepository.GetByPlayerName(playerName);
                if (playerEntity != null)
                {
                    logger.LogInformation(
                        "{playerName} already exists, returning that",
                        playerName
                    );
                    return Result<PlayerEntity>.Success(playerEntity);
                }

                logger.LogInformation(
                    "Player {playerName} doesn't exist, fetching from osrs hiscore",
                    playerName
                );

                Result<PlayerHiscore> playerHiscoreResult =
                    await hiscoreService.GetPlayerHiscoreDataAsync(playerName);

                if (playerHiscoreResult.IsSuccess)
                {
                    logger.LogInformation("Creating player {playerName}", playerName);
                    Player player = new()
                    {
                        PlayerName = playerHiscoreResult.Value.Name,
                        TotalExperience = playerHiscoreResult.Value.TotalExperience,
                        TotalLevel = playerHiscoreResult.Value.TotalLevel,
                        ExperienceOverTime = [],
                    };
                    var entity = await playerRepository.Create(Player.FromDomain(player));

                    return Result<PlayerEntity>.Success(entity);
                }
                else
                {
                    return Result<PlayerEntity>.Failure(PlayerHiscoreError.ServiceError());
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    e,
                    "Unexpected error occurred while retrieving player with name {playerName}",
                    playerName
                );
                return Result<PlayerEntity>.Failure(PlayerServiceErrror.ServiceError());
            }
        }

        public async Task<BaseResult> AddExperienceEntryForTodaysDateAsync(string playerName)
        {
            var player = await playerRepository.GetByPlayerNameDetailed(playerName);

            if (player == null)
            {
                logger.LogError("Player {playerName} not found", playerName);
                return BaseResult.Failure(PlayerServiceErrror.NotFound());
            }

            var playerHiscoreResult = await hiscoreService.GetPlayerHiscoreDataAsync(
                player.PlayerName
            );

            if (!playerHiscoreResult.IsSuccess)
            {
                return BaseResult.Failure(
                    PlayerHiscoreError.ServiceError("Error when contacting osrs api")
                );
            }

            var playerHiscore = playerHiscoreResult.Value;

            var totalExperienceEntryAlreadyExists = player.PlayerExperiences.Any(e =>
                e.Experience == playerHiscore.TotalExperience
            );

            if (totalExperienceEntryAlreadyExists)
            {
                logger.LogInformation(
                    "Experience entry for {playerName} already exists, not adding another one",
                    playerName
                );
                return BaseResult.Success();
            }

            var playerExperience = new PlayerExperienceEntity
            {
                PlayerId = player.PlayerId,
                DateTime = DateTime.Now,
                Experience = playerHiscore.TotalExperience,
            };

            try
            {
                player.PlayerExperiences.Add(playerExperience);
                await playerRepository.Update(player);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error when updating player {playerName}", playerName);
                return BaseResult.Failure(PlayerServiceErrror.ServiceError());
            }

            return BaseResult.Success();
        }

        public async Task<Result<PlayerEntity>> GetByPlayerNameDetailed(string playerName)
        {
            try
            {
                var player = await playerRepository.GetByPlayerNameDetailed(playerName);
                if (player == null)
                {
                    return Result<PlayerEntity>.Failure(PlayerServiceErrror.NotFound());
                }
                return Result<PlayerEntity>.Success(player);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error when fetching player {playerName}", playerName);
                return Result<PlayerEntity>.Failure(PlayerServiceErrror.DbError());
            }
        }
    }
}
