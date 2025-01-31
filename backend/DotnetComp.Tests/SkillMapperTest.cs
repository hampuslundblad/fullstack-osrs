using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Mappers;
using DotnetComp.Tests.Helpers;
using Xunit;

namespace DotnetComp.Tests
{
    public class SkillMapperTest
    {
        [Fact]
        public void MapStringToSkill_ReturnsSkills_WhenSuccessfull()
        {
            // Arrange 
            var playerHiscoreOnlySkills = Constants.playerHiScoreStringOnlySkills.Split('\n');
            // Act 

            var result = SkillMapper.MapStringToSkill(playerHiscoreOnlySkills);


            // Assert
            var firstSkill = result.First();

            Assert.Equal("Attack", firstSkill.Name);
            Assert.Equal(92, firstSkill.Level);
            Assert.Equal(560019, firstSkill.Rank);
            Assert.Equal(6682446, firstSkill.Experience);

            var lastSkill = result.Last();
            Assert.Equal("Construction", result.Last().Name);
            Assert.Equal(83, result.Last().Level);
            Assert.Equal(496307, lastSkill.Rank);
            Assert.Equal(2689958, lastSkill.Experience);


        }
    }
}