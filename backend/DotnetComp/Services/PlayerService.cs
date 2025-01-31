using System.Linq;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Results;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotnetComp.Services
{
    public interface IPlayerService
    {
        Task<Result<PlayerEntity>> GetOrCreatePlayer(string playerName);
    }

    public class PlayerService(
        ILogger<PlayerService> logger,
        IPlayerRepository playerRepository,
        IHiscoreService hiscoreService
    ) : IPlayerService
    {
        private readonly IPlayerRepository playerRepository = playerRepository;

        private readonly IHiscoreService hiscoreService = hiscoreService;
        private readonly ILogger<PlayerService> logger = logger;

        public async Task<Result<PlayerEntity>> GetOrCreatePlayer(string playerName)
        {
            try
            {
                var playerEntity = await playerRepository.GetByPlayerName(playerName);
                if (playerEntity != null)
                {
                    logger.LogInformation(
                        "{playerName} already exists, returning that",
                        playerName
                    );
                    return Result<PlayerEntity>.Success(playerEntity);
                }

                logger.LogInformation(
                    "Player {playerName} doesn't exist, fetching from osrs hiscore",
                    playerName
                );

                Result<PlayerHiscore> playerHiscoreResult =
                    await hiscoreService.GetPlayerHiscoreDataAsync(playerName);

                if (playerHiscoreResult.IsSuccess)
                {
                    Player player =
                        new()
                        {
                            PlayerName = playerHiscoreResult.Value.Name,
                            TotalExperience = playerHiscoreResult.Value.TotalExperience,
                            TotalLevel = playerHiscoreResult.Value.TotalLevel,
                        };

                    var entity = await playerRepository.Create(Player.FromDomain(player));
                    return Result<PlayerEntity>.Success(entity);
                }
                else
                {
                    return Result<PlayerEntity>.Failure(PlayerHiscoreError.ServiceError());
                }
            }
            catch (Exception e)
            {
                logger.LogError(
                    e,
                    "Unexpected error occurred while retrieving player with name {playerName}",
                    playerName
                );
                return Result<PlayerEntity>.Failure(PlayerServiceErrror.ServiceError());
            }
        }
    }
}
