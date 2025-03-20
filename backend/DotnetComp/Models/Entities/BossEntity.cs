using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class BossEntity
    {
        [Key]
        public int BossId { get; set; }

        public required string Name { get; set; }
    }
}
