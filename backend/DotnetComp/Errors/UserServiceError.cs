using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Results;

namespace DotnetComp.Errors
{
    public class UserServiceError
    {
        public static BaseError UserNotFound(string username)
        {
            return BaseError.NotFound("UserService.NotFound", $"Cannot find {username}");
        }

        public static BaseError ErrorWhenGettingUser(string username)
        {
            return BaseError.Failure("UserService.Failure", $"Error when getting user {username}");
        }

        public static BaseError ErrorWhileCreatingUser(string username)
        {
            return BaseError.Failure(
                "UserService.Failure",
                $"Error while creating user {username}"
            );
        }

        public static BaseError ErrorWhileAddinggroupToUser(string username, string groupName)
        {
            return BaseError.Failure(
                "UserService.Failure",
                $"Error while adding {groupName} to {username}"
            );
        }

        public static BaseError ErrorWhileGettingPlayer()
        {
            return BaseError.Failure("UserService.Failure", "Error while getting player");
        }

        public static BaseError GroupNotFound(string groupName)
        {
            return BaseError.NotFound("UserService.Failure", $"Unable to find group {groupName}");
        }

        public static BaseError GroupAlreadyExists(string groupName)
        {
            return BaseError.Conflict("UserService.Failure", $"Group {groupName} already exists.");
        }

        public static BaseError PlayerAlreadyExistsOnGroup(string playerName)
        {
            return BaseError.Conflict(
                "UserService.Failure",
                $"{playerName} already exists on group"
            );
        }

        public static BaseError PlayerNotFoundOnGroup(string playerName)
        {
            return BaseError.NotFound(
                "UserService.NotFound",
                $"{playerName} was not found on the group"
            );
        }
    }
}
