using System.ComponentModel.DataAnnotations;

namespace DotnetComp.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        public required AuthProviderEntity AuthProvider { get; set; }

        [Required]
        [MinLength(3), MaxLength(64)]
        public required string Username { get; set; }

        public ICollection<GroupEntity> Groups { get; set; } = [];
    }
}
