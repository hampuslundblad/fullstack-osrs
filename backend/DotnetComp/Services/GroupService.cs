using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Repositories;
using DotnetComp.Results;

namespace DotnetComp.Services
{
    interface IGroupService
    {
        Task<Result<Group>> GetGroupAsync(string userAuthId, string groupName);
    }

    public class GroupService(IUserRepository userRepository) : IGroupService
    {
        public async Task<Result<Group>> GetGroupAsync(string userAuthId, string groupName)
        {
            var userEntity =
                await userRepository.GetUserIncludingGroupsAndPlayersAndExperienceAsync(userAuthId);
            if (userEntity == null)
            {
                //TODO Fix error
                return Result<Group>.Failure(UserServiceError.UserNotFound(userAuthId));
            }

            var groupEntity = userEntity.Groups.FirstOrDefault(g => g.GroupName == groupName);
            if (groupEntity == null)
            {
                //TODO: Fix error, should be groupserviceerror
                return Result<Group>.Failure(UserServiceError.GroupNotFound(groupName));
            }
            return Result<Group>.Success(Group.ToDomain(groupEntity));
        }
    }
}
