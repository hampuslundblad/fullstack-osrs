using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Requests
{
    public class AddPlayerToGroupRequest
    {
        [Required]
        public required List<string> PlayerNames { get; set; }
    }
}