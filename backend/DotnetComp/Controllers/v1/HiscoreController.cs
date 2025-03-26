using Asp.Versioning;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Dto;
using DotnetComp.Results;
using DotnetComp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetComp.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("hiscore")]
    public class HiscoreController(
        ILogger<HiscoreController> logger,
        IHiscoreService hiscoreService
    ) : ControllerBase
    {
        private readonly ILogger<HiscoreController> logger = logger;
        private readonly IHiscoreService hiscoreService = hiscoreService;

        /// <summary>
        ///  Gets the osrs hiscore for a osrs player.
        /// </summary>
        /// <param name="name"> Name of the osrs player</param>
        [HttpGet("{name}")]
        public async Task<ActionResult<PlayerHiscore>> Get(string name)
        {
            if (name.Length < 3 || name.Length > 100)
            {
                return BadRequest("Player name is either too long or too short");
            }

            logger.LogInformation("Getting hiscore data for {name}", name);
            var response = await hiscoreService.GetPlayerHiscoreDataAsync(name);

            return response.Match(
                onSuccess: () =>
                {
                    var result = response.Value;
                    return Ok(result);
                },
                onFailure: error =>
                {
                    return error.ErrorType switch
                    {
                        ErrorType.NotFound => StatusCode(404, error.Description),
                        ErrorType.Failure => StatusCode(500, error.Description),
                        _ => StatusCode(500, "Something unexpected went wrong"),
                    };
                }
            );
        }
    }
}
