using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Models.Dto
{
    public class PlayerExperienceDTO
    {
        public required int Experience { get; set; }
        public required DateTime Time { get; set; }

        public static PlayerExperienceDTO FromDomain(PlayerExperience domain)
        {
            return new PlayerExperienceDTO
            {
                Experience = domain.Experience,
                Time = domain.DateTime,
            };
        }
    }
}
