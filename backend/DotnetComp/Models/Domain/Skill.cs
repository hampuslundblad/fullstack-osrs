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

    }
}