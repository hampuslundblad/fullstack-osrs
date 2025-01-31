using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;

namespace DotnetComp.Models.Domain
{
    public class AuthProvider
    {
        public required string Name { get; set; }

        // This the id that's provided by the auth provider to identify the user.
        public required string UserId { get; set; }
    }
}
