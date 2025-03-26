using DotnetComp.Data;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Repositories
{
    public interface IBossRepository
    {
        Task Create(BossEntity bossEntity);

        Task Delete(BossEntity bossEntity);
    }

    public class BossRepository(DatabaseContext dbContext) : IBossRepository
    {
        private readonly DatabaseContext dbContext = dbContext;

        public async Task Create(BossEntity bossEntity)
        {
            dbContext.Bosses.Add(bossEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(BossEntity bossEntity)
        {
            dbContext.Bosses.Remove(bossEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
