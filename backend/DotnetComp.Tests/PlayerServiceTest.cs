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
        private readonly PlayerService playerService;

        public PlayerServiceTest()
        {
            mockPlayerRepository = new Mock<IPlayerRepository>();
            mockLogger = new Mock<ILogger<PlayerService>>();
            playerService = new PlayerService(mockLogger.Object, mockPlayerRepository.Object);
        }

        [Fact]
        public async Task GetPlayer_ReturnsPlayer_WhenPlayerExists()
        {
            // Arrange
            var playerName = "testPlayer";
            var player = new PlayerEntity
            {
                PlayerName = playerName,
                TotalExperience = 123,
                ExperienceGainedLast24H = 10,
                ExperienceGainedLastWeek = 10,
            };
            mockPlayerRepository
                .Setup(repo => repo.GetByPlayerName(playerName))
                .ReturnsAsync(player);

            // Act
            var result = await playerService.GetPlayer(playerName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(playerName, result.Value.PlayerName);
        }

        [Fact]
        public async Task GetPlayer_ReturnsNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            var playerName = "nonExistentPlayer";
            _ = mockPlayerRepository
                .Setup(repo => repo.GetByPlayerName(playerName))
                .ReturnsAsync((PlayerEntity?)null);

            // Act
            var result = await playerService.GetPlayer(playerName);

            // Assert
            Assert.NotNull(result.Error);
            Assert.Equal(ErrorType.NotFound, result.Error.ErrorType);
        }

        [Fact]
        public async Task CreatePlayer_ReturnsSuccess_WhenPlayerIsCreated()
        {
            // Arrange
            var playerName = "newPlayer";
            mockPlayerRepository
                .Setup(repo => repo.Create(playerName))
                .ReturnsAsync(new PlayerEntity { PlayerName = playerName });

            // Act
            var result = await playerService.CreatePlayer(playerName);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
