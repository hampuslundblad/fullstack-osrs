using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Tests.Domain
{
    public class SkillTest
    {
        [Fact]
        public void FromString_ReturnsSkill_WhenSuccessfull()
        {
            // Arrange
            var skillName = "Agility";
            var rank = "123";
            var experience = "5";
            var level = "99";
            var skillString = $"{rank},{level}, {experience}";

            // Act
            var result = Skill.FromString(skillName, skillString);

            // Assert
            Assert.Equal("Agility", result.Name);
            Assert.Equal(int.Parse(experience), result.Experience);
            Assert.Equal(int.Parse(rank), result.Rank);
        }
    }
}
