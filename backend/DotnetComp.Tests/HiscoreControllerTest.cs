using Moq;

using DotnetComp.Controllers;
using Microsoft.Extensions.Logging;
using DotnetComp.Services;
using DotnetComp.Models.Dto;
using DotnetComp.Results;
using Microsoft.AspNetCore.Mvc;
using DotnetComp.Models.Domain;
using DotnetComp.Errors;


namespace DotnetComp.Tests
{
    public class HiscoreControllerTest
    {
        [Fact]
        public async Task Get_ReturnsPlayerHiscoreDTO_WhenSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HiscoreController>>();
            var mockHiscoreService = new Mock<IHiscoreService>();
            var playerName = "testPlayer";
            var playerHiscore = new PlayerHiscore { Name = playerName, TotalExperience = 1000, Rank = 1, TotalLevel = 99, Skills = [] };
            var serviceResponse = Result<PlayerHiscore>.Success(playerHiscore);

            mockHiscoreService.Setup(service => service.GetPlayerHiscoreDataAsync(playerName))
                             .ReturnsAsync(serviceResponse);

            var controller = new HiscoreController(mockLogger.Object, mockHiscoreService.Object);

            // Act 
            ActionResult<PlayerHiscoreDTO> result = await controller.Get(playerName);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PlayerHiscoreDTO>(okResult.Value);
            Assert.Equal(playerHiscore.Name, returnValue.Name);
            Assert.Equal(playerHiscore.Rank, returnValue.Rank);
            Assert.Equal(playerHiscore.TotalExperience, returnValue.TotalExperience);
        }

        [Fact]
        public async Task Get_ReturnsStatusCode500_WhenErrorOccurs()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HiscoreController>>();
            var mockPlayerService = new Mock<IHiscoreService>();
            var playerName = "testPlayer";
            var serviceResponse = Result<PlayerHiscore>.Failure(PlayerHiscoreError.ServiceError());

            mockPlayerService.Setup(service => service.GetPlayerHiscoreDataAsync(playerName))
                             .ReturnsAsync(serviceResponse);

            var controller = new HiscoreController(mockLogger.Object, mockPlayerService.Object);

            // Act
            var result = await controller.Get(playerName);

            // Assertd
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}