using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Data;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Repositories
{
    public interface IPlayerRepository
    {
        Task<PlayerEntity?> GetByPlayerName(string name);

        Task<PlayerEntity> Create(PlayerEntity playerEntity);
    }

    public class PlayerRepository(DatabaseContext dbContext) : IPlayerRepository
    {
        private readonly DatabaseContext dbContext = dbContext;

        public async Task<PlayerEntity?> GetByPlayerName(string playerName)
        {
            return await dbContext
                .Players.Where(p => p.PlayerName == playerName)
                .FirstOrDefaultAsync();
        }

        public async Task<PlayerEntity> Create(PlayerEntity playerEntity)
        {
            var result = await dbContext.Players.AddAsync(playerEntity);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
