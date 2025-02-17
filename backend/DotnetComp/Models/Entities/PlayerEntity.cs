using System.ComponentModel.DataAnnotations;

namespace DotnetComp.Models.Entities
{
    public class PlayerEntity
    {
        [Key]
        public int PlayerId { get; set; }

        [MinLength(3), MaxLength(128)]
        public required string PlayerName { get; set; }
        public int ExperienceGainedLast24H { get; set; }
        public int ExperienceGainedLastWeek { get; set; }

        public required int TotalExperience { get; set; }

        public required int TotalLevel { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = [];

        public ICollection<PlayerExperienceEntity> PlayerExperiences { get; set; } = [];
    }
}
