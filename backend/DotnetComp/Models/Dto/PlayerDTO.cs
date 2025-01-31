using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Models.Dto
{
    public class PlayerDTO
    {
        public required string Name { get; set; }
        public required int ExperienceGainedLastWeek { get; set; }
        public required int ExperienceGainedLast24H { get; set; }
        public required int TotalExperience { get; set; }

        public required int TotalLevel { get; set; }

        public static PlayerDTO FromDomain(Player domain)
        {
            return new PlayerDTO
            {
                Name = domain.PlayerName,
                TotalExperience = domain.TotalExperience,
                ExperienceGainedLast24H = domain.ExperienceGainedLast24H,
                ExperienceGainedLastWeek = domain.ExperienceGainedLastWeek,
                TotalLevel = domain.TotalLevel,
            };
        }
    }
}
