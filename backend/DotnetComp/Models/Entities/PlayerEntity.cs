using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetComp.Models.Entities
{
    public class PlayerEntity
    {
        [Key]
        public int PlayerId { get; set; }

        [MinLength(3), MaxLength(128)]
        public required string PlayerName { get; set; }

        public required int TotalExperience { get; set; }

        public required int TotalLevel { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = [];

        public ICollection<PlayerExperienceEntity> PlayerExperiences { get; set; } = [];

        public ICollection<PlayerBossStat> PlayerBossStats { get; set; } = [];
    }
}
