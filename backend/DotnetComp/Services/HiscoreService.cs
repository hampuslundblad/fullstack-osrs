using System.Linq;
using System.Net;
using DotnetComp.Clients;
using DotnetComp.Errors;
using DotnetComp.Mappers;
using DotnetComp.Models.Domain;
using DotnetComp.Results;
using Microsoft.OpenApi.Any;

namespace DotnetComp.Services
{
    public interface IHiscoreService
    {
        Task<Result<PlayerHiscore>> GetPlayerHiscoreDataAsync(string name);
    }

    public class HiscoreService(ILogger<HiscoreService> logger, IRunescapeClient runescapeClient)
        : IHiscoreService
    {
        private readonly IRunescapeClient runescapeClient = runescapeClient;
        private readonly ILogger<HiscoreService> logger = logger;

        public async Task<Result<PlayerHiscore>> GetPlayerHiscoreDataAsync(string name)
        {
            var response = await runescapeClient.GetPlayerHiscoreAsync(name);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                logger.LogError("Player {name} not found", name);
                return Result<PlayerHiscore>.Failure(PlayerHiscoreError.NotFound());
            }
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(
                    "Failed to fetch player hiscore data {StatusCode}",
                    response.StatusCode
                );
                return Result<PlayerHiscore>.Failure(PlayerHiscoreError.ServiceError());
            }

            try
            {
                logger.LogInformation("Fetching {name} from osrs hiscore", name);
                // The response is difficult, see https://runescape.wiki/w/Application_programming_interface#Old_School_Hiscores for more info.
                var responseString = await response.Content.ReadAsStringAsync();

                var parts = responseString.Split(['\n']);

                var playerRankTotalLevelAndExperience = parts[0].Split(',');

                var playerRank = playerRankTotalLevelAndExperience[0];
                var totalLevel = playerRankTotalLevelAndExperience[1];
                var totalExperience = playerRankTotalLevelAndExperience[2];

                var skillsAsStrings = parts
                    .Skip(1)
                    .Where(s => s.Count(c => c == ',') > 1)
                    .ToArray();
                var skills = SkillMapper.MapStringToSkill(skillsAsStrings);

                return Result<PlayerHiscore>.Success(
                    new PlayerHiscore()
                    {
                        Name = name,
                        TotalExperience = int.Parse(totalExperience),
                        Rank = int.Parse(playerRank),
                        TotalLevel = int.Parse(totalLevel),
                        Skills = skills,
                    }
                );
            }
            catch (Exception e)
            {
                logger.LogError("Failed to parse player hiscore data {Message}", e);
                return Result<PlayerHiscore>.Failure(PlayerHiscoreError.ServiceError());
            }
        }
    }
}
