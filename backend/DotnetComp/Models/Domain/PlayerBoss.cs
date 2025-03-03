using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public class PlayerBoss
    {
        public required Boss Boss { get; set; }
        public required DateTime DateTime { get; set; }
    }
}
