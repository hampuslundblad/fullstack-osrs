using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Requests
{
    public class CreateGroupRequest
    {
        [Required]
        [MinLength(3), MaxLength(64)]
        public required string GroupName { get; set; }
    }
}
