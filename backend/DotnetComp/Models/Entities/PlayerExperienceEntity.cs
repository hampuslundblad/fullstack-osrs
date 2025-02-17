using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Models.Entities
{
    public class PlayerExperienceEntity
    {
        [Key]
        public int PlayerExperienceId { get; set; }

        [ForeignKey("PlayerId")]
        public required int PlayerId { get; set; }
        public required int Experience { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
