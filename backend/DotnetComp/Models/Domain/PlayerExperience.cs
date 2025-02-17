using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;

namespace DotnetComp.Models.Domain
{
    public class PlayerExperience
    {
        public required int Experience { get; set; }
        public required DateTime DateTime { get; set; }

        public static PlayerExperience ToDomain(PlayerExperienceEntity entity)
        {
            return new PlayerExperience
            {
                Experience = entity.Experience,
                DateTime = entity.DateTime,
            };
        }
    }
}
