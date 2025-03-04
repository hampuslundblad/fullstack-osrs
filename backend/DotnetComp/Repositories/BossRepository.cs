using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Data;
using DotnetComp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Repositories
{
    public interface IBossRepository
    {
        Task<List<PlayerBossKillEntity>> GetPlayerBossKillsByPlayerId(int playerId);
        Task<List<PlayerBossRankEntity>> GetPlayerBossRankingsByPlayerId(int playerId);

        Task<List<BossEntity>> GetAllBosses();
    }

    public class BossRepository(DatabaseContext context)
    {
        private readonly DatabaseContext _context = context;

        public async Task<List<BossEntity>> GetAllBosses()
        {
            return await _context.Bosses.ToListAsync();
        }

        public async Task<List<PlayerBossKillEntity>> GetPlayerBossKillsByPlayerId(int playerId)
        {
            return await _context.PlayerBossKills.Where(x => x.PlayerId == playerId).ToListAsync();
        }

        public async Task<List<PlayerBossRankEntity>> GetPlayerBossRankingsByPlayerId(int playerId)
        {
            return await _context.PlayerBossRanks.Where(x => x.PlayerId == playerId).ToListAsync();
        }

        public async Task UpdatePlayerBossRanking(PlayerBossRankEntity playerBossRankEntity)
        {
            _context.Entry(playerBossRankEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayerBossKill(PlayerBossKillEntity playerBossKillEntity)
        {
            _context.Entry(playerBossKillEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CreatePlayerBossRanking(PlayerBossRankEntity playerBossRankEntity)
        {
            await _context.PlayerBossRanks.AddAsync(playerBossRankEntity);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePlayerBossKill(PlayerBossKillEntity playerBossKillEntity)
        {
            await _context.PlayerBossKills.AddAsync(playerBossKillEntity);
            await _context.SaveChangesAsync();
        }
    }
}
