using Asp.Versioning;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Dto;
using DotnetComp.Results;
using DotnetComp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetComp.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("player")]
    public class PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
        : ControllerBase
    {
        private readonly ILogger<PlayerController> logger = logger;
        private readonly IPlayerService playerService = playerService;

        /// <summary>
        ///  Gets the player based on playername
        /// </summary>
        /// <param name="playerName"> Name of the player</param>
        [HttpGet("{playerName}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(string playerName)
        {
            if (playerName == null)
            {
                return BadRequest("Please provide a playername");
            }

            if (playerName.Length < 3 || playerName.Length > 100)
            {
                return BadRequest(
                    "The length of the player's name cannot be less than 3 and greater than 100"
                );
            }

            var result = await playerService.GetOrCreatePlayerAsync(playerName);
            logger.LogDebug("Retrieving player {playerName}", playerName);

            return result.Match(
                onSuccess: () =>
                {
                    var dto = PlayerDTO.FromDomain(Player.ToDomain(result.Value));
                    return Ok(dto);
                },
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => NotFound("Player not found"),
                        _ => StatusCode(500, "An unexpected error occurred"),
                    };
                }
            );
        }

        [HttpGet("{playerName}/all")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerAll(string playerName)
        {
            if (playerName == null)
            {
                return BadRequest("Please provide a playername");
            }

            if (playerName.Length < 3 || playerName.Length > 100)
            {
                return BadRequest(
                    "The length of the player's name cannot be less than 3 and greater than 100"
                );
            }

            var result = await playerService.GetByPlayerNameDetailed(playerName);
            logger.LogDebug("Retrieving player {playerName}", playerName);

            return result.Match(
                onSuccess: () =>
                {
                    var dto = PlayerDTO.FromDomain(Player.ToDomain(result.Value));
                    return Ok(dto);
                },
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => NotFound("Player not found"),
                        _ => StatusCode(500, "An unexpected error occurred"),
                    };
                }
            );
        }

        /// <summary>
        /// Updates the player's experience for the current date
        /// </summary>
        /// <param name="playerNameDTO"> Name of the player</param>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePlayerExperiences(
            [FromBody] PlayerNameDTO playerNameDTO
        )
        {
            var result = await playerService.AddExperienceEntryForTodaysDateAsync(
                playerNameDTO.PlayerName
            );

            result.Match(
                onSuccess: () =>
                {
                    logger.LogInformation("Experience entry added for {playerName}", playerNameDTO);
                    return Created();
                },
                onFailure: (error) =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => NotFound("Player not found"),
                        ErrorType.Failure => StatusCode(500, error.Description),
                        _ => StatusCode(500, "An unexpected error occurred"),
                    };
                }
            );
            return Ok();
        }
    }
}
