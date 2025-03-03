using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Tests.Domain
{
    public class MinigameTest
    {
        [Fact]
        public void FromString_ReturnsMinigame_WhenSuccessfull()
        {
            // Arrange
            var minigameName = "Barrows";
            var rank = "123";
            var score = "5";
            var minigameString = $"{rank},{score}";

            // Act
            var result = Minigame.FromString(minigameName, minigameString);

            // Assert
            Assert.Equal("Barrows", result.Name);
            Assert.Equal(int.Parse(score), result.Score);
        }
    }
}
