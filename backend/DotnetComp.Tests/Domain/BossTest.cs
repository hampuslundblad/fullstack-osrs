using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Tests.Domain
{
    public class BossTest
    {
        // Test the Boss FromString method
        [Fact]
        public void FromString_ReturnsBoss_WhenSuccessfull()
        {
            // Arrange
            var rank = "123";
            var kills = "5";
            var bossString = $"{rank},{kills}";
            var bossName = "Artio";

            // Act
            var result = Boss.FromString(bossName, bossString);

            // Assert
            Assert.Equal("Artio", result.Name);
            Assert.Equal(int.Parse(kills), result.Kills);
            Assert.Equal(int.Parse(rank), result.Rank);
        }

        [Fact]
        public void FromString_ThrowsArgumentException_WhenBossStringDoesNotContainComma()
        {
            // Arrange
            var bossString = "81525504";
            var bossName = "Artio";

            // Act
            void act() => Boss.FromString(bossString, bossName);

            // Assert
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Boss string must contain a comma", exception.Message);
        }

        [Fact]
        public void FromString_ThrowsArgumentException_WhenBossStringContainsNegativeInteger()
        {
            // Arrange
            var bossString = "81,525,504";
            var bossName = "Artio";

            // Act
            Action act = () => Boss.FromString(bossString, bossName);

            // Assert
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Boss string must contain a comma", exception.Message);
        }
    }
}
