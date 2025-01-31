using DotnetComp.Models.Domain;

namespace DotnetComp.Models.Dto
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public required List<Group> Groups { get; set; }

        public static UserDTO FromDomain(User user)
        {
            UserDTO userDto = new() { Username = user.Username, Groups = user.Groups };
            return userDto;
        }
    }
}
