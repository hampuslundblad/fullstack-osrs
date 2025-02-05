using System.Net;
using Asp.Versioning;
using DotnetComp.Errors;
using DotnetComp.Models.Dto;
using DotnetComp.Results;
using DotnetComp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetComp.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("user")]
    public class UserController(ILogger<UserController> logger, IUserService userService)
        : ControllerBase
    {
        private readonly ILogger<UserController> logger = logger;

        private readonly IUserService userService = userService;

        /// <summary>
        ///  Gets the user, including groups
        /// </summary>
        ///
        [Authorize]
        [HttpGet("")]
        //username

        public async Task<ActionResult> GetOrCreateUser()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");

            var userAuthId = userIdClaim!.Value;

            var result = await userService.FindOrCreateUserAsync(userAuthId);
            return result.Match(
                onSuccess: () => Ok(result.Value),
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => BadRequest("User not found"),
                        ErrorType.Failure => StatusCode(500, "Internal server error"),
                        _ => StatusCode(500, "unkown error"),
                    };
                }
            );
            
        }

        [Authorize]
        [HttpGet("group/{groupName}")]
        public async Task<IActionResult> GetGroup(string groupName)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");

            var userAuthId = userIdClaim!.Value;

            if (groupName == null)
            {
                return BadRequest("group name cannot be null");
            }
            if (groupName.Length < 3 || groupName.Length > 32)
            {
                return BadRequest("Group name is too short or too long");
            }
            var groupResult = await userService.GetGroupAsync(userAuthId, groupName);

            return groupResult.Match(
                onSuccess: () => Ok(groupResult.Value),
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => BadRequest("User or group not found"),
                        ErrorType.Failure => StatusCode(500, "Internal server error"),
                        _ => StatusCode(500, "unkown error"),
                    };
                }
            );
        }

        /// <summary>
        ///  Creates a new group on an existing user
        /// </summary>
        /// <param name="groupName"> Name of the new group</param>
        [Authorize]
        [HttpPut("group/{groupName}")]
        public async Task<IActionResult> CreateGroupOnUser(string groupName)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");

            var userAuthId = userIdClaim!.Value;

            if (groupName == null)
            {
                return BadRequest("group name cannot be null");
            }
            if (groupName.Length < 3 || groupName.Length > 32)
            {
                return BadRequest("Group name is too short or too long");
            }

            var result = await userService.CreateGroupOnUserAsync(userIdClaim.Value, groupName);
            return result.Match<IActionResult>(
                onSuccess: () => NoContent(),
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => BadRequest("User not found"),
                        ErrorType.Conflict => Conflict("Group already exists"),
                        _ => StatusCode(500, "Internal service error"),
                    };
                }
            );
        }

        [Authorize]
        [HttpPut("group/{groupName}/player/{playerName}")]
        public async Task<IActionResult> AddPlayerToGroup(string groupName, string playerName)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");

            var userAuthId = userIdClaim!.Value;

            var result = await userService.AddPlayerOnGroupAsync(userAuthId, groupName, playerName);
            return result.Match(
                onSuccess: () => Created(),
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.Failure => StatusCode(500, "Internal service error"),
                        ErrorType.NotFound => BadRequest("Either player or group was not found"),
                        ErrorType.Conflict => Conflict("Player already exists on group"),
                        _ => StatusCode(500, "unkown error"),
                    };
                }
            );
        }

        [Authorize]
        [HttpDelete("group/{groupName}/player/{playerName}")]
        public async Task<IActionResult> RemovePlayerFromGroup(string groupName, string playerName)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");
            var userAuthId = userIdClaim!.Value;
            var result = await userService.RemovePlayerFromGroup(userAuthId, groupName, playerName);
            return result.Match(
                onSuccess: () => StatusCode(204, "Player has been removed from the group"),
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.Failure => StatusCode(500, "Internal service error"),
                        ErrorType.NotFound => BadRequest(
                            "Either the player or group was not found"
                        ),
                        _ => StatusCode(500, "unkown error"),
                    };
                }
            );
        }
    }
}
