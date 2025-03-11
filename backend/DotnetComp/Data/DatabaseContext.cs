using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public required DbSet<UserEntity> Users { get; set; }
        public required DbSet<AuthProviderEntity> AuthProviders { get; set; }
        public required DbSet<GroupEntity> Groups { get; set; }
        public required DbSet<PlayerEntity> Players { get; set; }

        public required DbSet<PlayerBossStat> PlayerBossStats { get; set; }

        public required DbSet<BossEntity> Bosses { get; set; }
    }
}
