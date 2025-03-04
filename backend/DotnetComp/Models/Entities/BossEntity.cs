using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class BossEntity
    {
        public int BossId { get; set; }

        public required string Name { get; set; }
    }
}
