using System;
using System.Threading.Tasks;
using DotnetComp.Errors;
using DotnetComp.Models.Domain;
using DotnetComp.Models.Entities;
using DotnetComp.Repositories;
using DotnetComp.Results;
using DotnetComp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DotnetComp.Tests
{
    public class PlayerServiceTest
    {
        private readonly Mock<IPlayerRepository> mockPlayerRepository;
        private readonly Mock<ILogger<PlayerService>> mockLogger;

        private readonly Mock<IHiscoreService> mockHiscoreService;
        private readonly PlayerService playerService;

        public PlayerServiceTest()
        {
            mockPlayerRepository = new Mock<IPlayerRepository>();
            mockLogger = new Mock<ILogger<PlayerService>>();
            mockHiscoreService = new Mock<IHiscoreService>();
            playerService = new PlayerService(
                mockLogger.Object,
                mockPlayerRepository.Object,
                mockHiscoreService.Object
            );
        }

        [Fact]
        public async Task GetPlayer_ReturnsPlayer_WhenPlayerExists()
        {
            // Arrange
            var playerName = "existingPlayer";
            var player = new PlayerEntity
            {
                PlayerName = playerName,
                TotalExperience = 123,
                TotalLevel = 1234,
                ExperienceGainedLast24H = 10,
                ExperienceGainedLastWeek = 10,
            };
            mockPlayerRepository
                .Setup(repo => repo.GetByPlayerName(playerName))
                .ReturnsAsync(player);

            // Act
            var result = await playerService.GetOrCreatePlayerAsync(playerName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(playerName, result.Value.PlayerName);
        }
    }
}
