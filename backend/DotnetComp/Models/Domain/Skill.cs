using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public record Skill
    {
        public required string Name { get; set; }
        public required int Rank { get; set; }
        public required int Level { get; set; }
        public required int Experience { get; set; }

        /// <summary>
        ///   Converts a string to a skill object
        /// </summary>
        /// <param name="skillName"> Any osrs skill</param>
        /// <param name="skillString"> Should be of the format RANK,LEVEL,EXPERIENCE, example 601561,90,5366032</param>


        public static Skill FromString(string skillName, string skillString)
        {
            int RANK_OFFSET = 0;
            int LEVEL_OFFSET = 1;
            int EXPERIENCE_OFFSET = 2;

            var split = skillString.Split(",");

            if (split.Length != 3)
            {
                throw new FormatException(
                    "The skillString must contain exactly three parts separated by commas."
                );
            }

            bool isValidRank = int.TryParse(split[RANK_OFFSET], out int rank) && rank > 0;
            bool isValidLevel = int.TryParse(split[LEVEL_OFFSET], out int level) && level > 0;
            bool isValidExperience =
                int.TryParse(split[EXPERIENCE_OFFSET], out int experience) && experience > 0;

            if (!isValidRank || !isValidLevel || !isValidExperience)
            {
                throw new FormatException(
                    "Each part of the skillString must be a valid positive integer."
                );
            }

            if (level < 1 || level > 99)
            {
                throw new FormatException("The level must be between 1 and 99.");
            }

            return new Skill
            {
                Name = skillName,
                Rank = rank,
                Level = level,
                Experience = experience,
            };
        }
    }
}
