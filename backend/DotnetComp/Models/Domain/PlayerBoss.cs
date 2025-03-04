using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Entities;

namespace DotnetComp.Models.Domain
{
    public class PlayerBoss
    {
        public required Boss Boss { get; set; }
        public required DateTime DateTime { get; set; }

        public static PlayerBoss ToDomain(
            PlayerBossKillEntity killEntity,
            PlayerBossRankEntity rankEntity,
            string bossName
        )
        {
            return new PlayerBoss
            {
                Boss = new Boss
                {
                    Name = bossName,
                    Rank = rankEntity.Rank,
                    Kills = killEntity.Kills,
                },
                DateTime = killEntity.DateTime,
            };
        }

        public static Tuple<PlayerBossKillEntity, PlayerBossRankEntity> FromDomain(
            PlayerBoss domain
        )
        {
            return new Tuple<PlayerBossKillEntity, PlayerBossRankEntity>(
                new PlayerBossKillEntity { Kills = domain.Boss.Kills, DateTime = domain.DateTime },
                new PlayerBossRankEntity { Rank = domain.Boss.Rank, DateTime = domain.DateTime }
            );
        }
    }
}
