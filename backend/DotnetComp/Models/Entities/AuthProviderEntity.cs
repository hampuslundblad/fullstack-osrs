using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetComp.Models.Entities
{
    public class AuthProviderEntity
    {
        [Key]
        public int AuthProviderId { get; set; }

        public required string Name { get; set; }

        // This the id that's provided by the auth provider to identify the user.
        public required string AuthProviderUserId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; } = null!;
    }
}
