using System.Net;
using DotnetComp.Clients;
using DotnetComp.Models.Domain;
using DotnetComp.Services;
using DotnetComp.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

namespace DotnetComp.Tests
{
    public class HiscoreServiceTest
    {
        [Fact]
        public async Task GetPlayerHiscoreDataAsync_ReturnsPlayerHiScore_WhenSuccessfull()
        {
            // Arrange
            var playerName = "testName";
            var playerHiscoreString = Constants.playerHiScoreString;
            var mockLogger = new Mock<ILogger<HiscoreService>>();
            var mockClient = new Mock<IRunescapeClient>();
            mockClient
                .Setup(client => client.GetPlayerHiscoreAsync(playerName))
                .ReturnsAsync(
                    new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(playerHiscoreString),
                    }
                );

            var service = new HiscoreService(mockLogger.Object, mockClient.Object);

            // Acts
            var result = await service.GetPlayerHiscoreDataAsync(playerName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(playerName, result.Value.Name);
            Assert.Equal(359002, result.Value.Rank);
            Assert.Equal(1990, result.Value.TotalLevel);
            Assert.Equal(122848819, result.Value.TotalExperience);
            Assert.Equal(92, result.Value.Skills.First().Level);

            Assert.Equal("Rifts closed", result.Value.Minigames.First().Name);
            Assert.Equal(14, result.Value.Minigames.First().Score);

            Assert.Equal("Artio", result.Value.Bosses.First().Name);
            Assert.Equal(81, result.Value.Bosses.First().Kills);
            Assert.Equal(525504, result.Value.Bosses.First().Rank);

            Assert.Equal(1, result.Value.ClueScrolls.First().Completed);
            Assert.Equal(1866371, result.Value.ClueScrolls.First().Rank);
            Assert.Equal(ClueScrollType.Beginner, result.Value.ClueScrolls.First().Type);
        }
    }
}
