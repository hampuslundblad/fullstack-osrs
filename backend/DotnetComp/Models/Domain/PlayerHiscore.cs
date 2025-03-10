using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public class PlayerHiscore
    {
        public required string Name { get; set; }
        public required int Rank { get; set; }
        public required int TotalExperience { get; set; }
        public required int TotalLevel { get; set; }

        public List<Skill> Skills { get; set; } = [];

        public List<Boss> Bosses { get; set; } = [];
        public List<ClueScroll> ClueScrolls { get; set; } = [];

        public List<Minigame> Minigames { get; set; } = [];
    }
}
