using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Entities
{
    public class PlayerExperience
    {
        public int PlayerExperienceId { get; set; }

        [ForeignKey("PlayerId")]
        public required int PlayerId { get; set; }
        public required int Experience { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
