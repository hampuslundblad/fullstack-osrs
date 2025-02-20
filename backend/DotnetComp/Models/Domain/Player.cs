using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;

namespace DotnetComp.Models.Domain
{
    public class Player
    {
        public required string PlayerName { get; set; }

        public required int TotalExperience { get; set; }
        public required int TotalLevel { get; set; }

        public required List<PlayerExperience> ExperienceOverTime { get; set; }

        public static Player ToDomain(PlayerEntity playerEntity)
        {
            return new Player
            {
                PlayerName = playerEntity.PlayerName,

                TotalExperience = playerEntity.TotalExperience,
                TotalLevel = playerEntity.TotalLevel,
                ExperienceOverTime = playerEntity
                    .PlayerExperiences.Select(PlayerExperience.ToDomain)
                    .OrderByDescending(pe => pe.DateTime)
                    .ToList(),
            };
        }

        public static PlayerEntity FromDomain(Player player)
        {
            return new PlayerEntity
            {
                PlayerName = player.PlayerName,
                TotalExperience = player.TotalExperience,
                TotalLevel = player.TotalLevel,
            };
        }
    }
}
