using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;
using DotnetComp.Services;

namespace DotnetComp.Models.Domain
{
    public class User
    {
        public required string Username { get; set; }
        public required List<Group> Groups { get; set; } = [];

        public static User ToDomain(UserEntity userEntity)
        {
            User user =
                new()
                {
                    Username = userEntity.Username,
                    Groups = [.. userEntity.Groups.Select(g => Group.ToDomain(g))],
                };
            return user;
        }

        public static implicit operator User(UserService v)
        {
            throw new NotImplementedException();
        }
    }
}
