using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;

namespace DotnetComp.Models.Domain
{
    public class Group
    {
        public required string GroupName { get; set; }
        public List<Player> Players { get; set; } = [];

        public static Group ToDomain(GroupEntity groupEntity)
        {
            Group group =
                new()
                {
                    GroupName = groupEntity.GroupName,
                    Players = groupEntity.Players.Select(p => Player.ToDomain(p)).ToList(),
                };
            return group;
        }
    }
}
