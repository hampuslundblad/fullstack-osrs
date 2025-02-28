using System.Linq;
using System.Net;
using DotnetComp.Clients;
using DotnetComp.Errors;
using DotnetComp.Mappers;
using DotnetComp.Models.Domain;
using DotnetComp.Results;
using DotnetComp.Utils;
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

        // The total length of the hiscore list, this can change if Jagex adds more stuff that's tracked by the hiscore
        private readonly int CURRENT_LIST_TOTAL_LENGTH = 109;

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

                if (parts.Length > CURRENT_LIST_TOTAL_LENGTH)
                {
                    logger.LogError(
                        "The hiscore list is longer than expected, expected {expected} but got {actual}",
                        CURRENT_LIST_TOTAL_LENGTH,
                        parts.Length
                    );
                    return Result<PlayerHiscore>.Failure(PlayerHiscoreError.ServiceError());
                }

                var result = new PlayerHiscore()
                {
                    Name = name,
                    Rank = ParseRankFromString(parts[0]),
                    TotalLevel = ParseTotalLevelFromString(parts[0]),
                    TotalExperience = ParseTotalExperienceFromString(parts[0]),
                };

                for (int i = 1; i < parts.Length; i++)
                {
                    HiscoreEntry currentEntry = HiscoreData.HiscoreEntries[i];

                    // If the value is -1 then the player haven't achieved anything in that category
                    if (parts[i].Contains("-1"))
                    {
                        continue;
                    }

                    switch (currentEntry.Type)
                    {
                        case HiscoreEntry.HiscoreEntryType.Skill:
                            result.Skills.Add(Skill.FromString(currentEntry.Name, parts[i]));
                            break;
                        case HiscoreEntry.HiscoreEntryType.Boss:
                            result.Bosses.Add(Boss.FromString(currentEntry.Name, parts[i]));
                            break;
                        case HiscoreEntry.HiscoreEntryType.ClueScroll:
                            if (currentEntry.Name.Contains("Clue Scrolls (all)"))
                            {
                                continue;
                            }
                            result.ClueScrolls.Add(
                                ClueScroll.FromString(currentEntry.Name, parts[i])
                            );
                            break;
                        case HiscoreEntry.HiscoreEntryType.Minigame:
                            result.Minigames.Add(Minigame.FromString(currentEntry.Name, parts[i]));
                            break;
                        case HiscoreEntry.HiscoreEntryType.Event:
                            break;
                        case HiscoreEntry.HiscoreEntryType.CollectionLog:
                            break;
                        case HiscoreEntry.HiscoreEntryType.Other:
                            break;
                    }
                }

                return Result<PlayerHiscore>.Success(result);
            }
            catch (Exception e)
            {
                logger.LogError("Failed to parse player hiscore data {Message}", e);
                return Result<PlayerHiscore>.Failure(PlayerHiscoreError.ServiceError());
            }
        }

        private static int ParseRankFromString(string s)
        {
            int RANK_INDEX = 0;
            return int.Parse(s.Split(',')[RANK_INDEX]);
        }

        private static int ParseTotalLevelFromString(string s)
        {
            int TOTAL_LEVEL_INDEX = 1;
            return int.Parse(s.Split(',')[TOTAL_LEVEL_INDEX]);
        }

        private static int ParseTotalExperienceFromString(string s)
        {
            int TOTAL_EXPERIENCE_INDEX = 2;
            return int.Parse(s.Split(',')[TOTAL_EXPERIENCE_INDEX]);
        }
    }
}
