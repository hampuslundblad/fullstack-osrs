using DotnetComp.Data;
using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Repositories
{
    public interface IGroupRepository
    {
        Task UpdateAsync(GroupEntity groupEntity);
    }

    public class GroupRepository(DatabaseContext dbContext) : IGroupRepository
    {
        private readonly DatabaseContext dbContext = dbContext;

        public async Task UpdateAsync(GroupEntity groupEntity)
        {
            dbContext.Entry(groupEntity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
