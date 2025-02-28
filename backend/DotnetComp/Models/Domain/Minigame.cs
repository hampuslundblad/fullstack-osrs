using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public class Minigame
    {
        public required string Name { get; set; }
        public required int Score { get; set; }

        // minigameString is in the format "rank,score",
        public static Minigame FromString(string name, string minigameString)
        {
            string[] minigameParts = minigameString.Split(",");
            var score = int.Parse(minigameParts[1]);

            return new Minigame { Name = name, Score = score };
        }
    }
}
