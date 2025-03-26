using System.ComponentModel.DataAnnotations;

namespace DotnetComp.Models.Entities
{
    public class GroupEntity
    {
        [Key]
        public int GroupId { get; set; }

        [MinLength(3), MaxLength(32)]
        public required string GroupName { get; set; }
        public ICollection<PlayerEntity> Players { get; set; } = [];

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
