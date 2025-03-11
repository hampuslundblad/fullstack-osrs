using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class BossEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
