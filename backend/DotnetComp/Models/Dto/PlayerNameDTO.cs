using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Dto
{
    public record PlayerNameDTO
    {
        public required string PlayerName { get; set; }
    }
}
