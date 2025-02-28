using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public class Boss
    {
        public required string Name { get; set; }
        public required int Kills { get; set; }

        public required int Rank { get; set; }

        public static Boss FromString(string name, string bossString)
        {
            if (!bossString.Contains(',') && bossString.Where(s => s == ',').Count() != 1)
            {
                throw new ArgumentException("Boss string must contain a comma");
            }

            var parts = bossString.Split(",");
            bool isValidKills = int.TryParse(parts[1], out int kills) && kills > 0;
            bool isValidRank = int.TryParse(parts[0], out int rank) && rank > 0;

            if (!isValidKills)
            {
                throw new FormatException("Kills must be a valid integer");
            }

            return new Boss
            {
                Name = name,
                Rank = int.Parse(parts[0]),
                Kills = int.Parse(parts[1]),
            };
        }
    }
}
