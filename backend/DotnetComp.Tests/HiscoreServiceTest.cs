using System.Net;
using DotnetComp.Clients;
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
            mockClient.Setup(client => client.GetPlayerHiscoreAsync(playerName)).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(playerHiscoreString) });


            var service = new HiscoreService(mockLogger.Object, mockClient.Object);

            // Act
            var result = await service.GetPlayerHiscoreDataAsync(playerName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(playerName, result.Value.Name);
            Assert.Equal(359002, result.Value.Rank);
            Assert.Equal(1990, result.Value.TotalLevel);
            Assert.Equal(122848819, result.Value.TotalExperience);
            Assert.Equal(92, result.Value.Skills.First().Level);
        }
    }
}