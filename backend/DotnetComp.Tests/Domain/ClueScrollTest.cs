using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Models.Domain;

namespace DotnetComp.Tests.Domain
{
    public class ClueScrollTest
    {
        [Fact]
        public void FromString_ReturnsClueScroll_WhenSuccessFull()
        {
            var clueScrollName = "beginner";
            var rank = "123";
            var completed = "5";
            var clueScrollString = $"{rank},{completed}";

            var result = ClueScroll.FromString(clueScrollName, clueScrollString);

            Assert.Equal(ClueScrollType.Beginner, result.Type);
            Assert.Equal(int.Parse(rank), result.Rank);
            Assert.Equal(int.Parse(completed), result.Completed);
        }
    }
}
