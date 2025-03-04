using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Results;

namespace DotnetComp.Services
{
    public interface IBossService
    {
        Task<Result<List<PlayerBoss>>> GetBossesForPlayer(int playerId);
    }

    public class BossService(ILogger<BossService> logger, IBossRepository bossRepository)
        : IBossService
    {
        private readonly ILogger<BossService> _logger = logger;
        private readonly IBossRepository _bossRepository = bossRepository;

        public async Task<Result<List<PlayerBoss>>> GetBossesForPlayer(int playerId)
        {
            try
            {
                List<PlayerBossKillEntity> bossKillEntities =
                    await _bossRepository.GetPlayerBossKillsByPlayerId(playerId);

                List<PlayerBossRankEntity> bossRankEntities =
                    await _bossRepository.GetPlayerBossRankingsByPlayerId(playerId);

                List<BossEntity> bossEntities = await _bossRepository.GetAllBosses();

                List<PlayerBoss> bosses = [];

                foreach (BossEntity bossEntity in bossEntities)
                {
                    List<PlayerBossKillEntity> playerBossKillEntitiesForBoss = bossKillEntities
                        .Where(x => x.BossId == bossEntity.BossId)
                        .ToList();

                    List<PlayerBossRankEntity> playerBossRankEntitiesForBoss = bossRankEntities
                        .Where(x => x.BossId == bossEntity.BossId)
                        .ToList();

                    bosses.Add(
                        PlayerBoss.ToDomain(bossKillEntity, bossRankEntity, bossEntity.Name)
                    );
                }
                return Result<List<PlayerBoss>>.Success(bosses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get bosses for player");
                return Result<List<PlayerBoss>>.Failure(
                    BossServiceError.ServiceError("Unable to get bosses for player")
                );
            }
        }
    }
}
