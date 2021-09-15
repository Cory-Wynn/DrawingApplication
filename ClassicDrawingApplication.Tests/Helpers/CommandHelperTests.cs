using System.Collections.Generic;
using ClassicDrawingApplication.Helpers;
using FluentAssertions;
using Xunit;

namespace ClassicDrawingApplication.Tests.Helpers
{
    public class CommandHelperTests
    {
        [Theory]
        [InlineData("Add Rectangle 1, 5, 5, 3", 6)]
        [InlineData("Select 2, 4", 3)]
        [InlineData("Move 1, 3", 3)]
        [InlineData("Resize 1, 10, 10, 3", 5)]
        [InlineData("SetColour Red", 2)]
        public void IsValidCommand_Should_Be_True(string input, int expectedArrayCount)
        {
            var result = CommandHelper.IsValidCommand(input, expectedArrayCount);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("AA", 2)]
        [InlineData("1 2 3 4 5", 3)]
        public void IsValidCommand_Should_Be_False(string input, int expectedArrayCount)
        {
            var result = CommandHelper.IsValidCommand(input, expectedArrayCount);

            result.Should().BeFalse();
        }

        [Fact]
        public void GetSanitizedInputSections_Should_Split_And_Remove_Commas()
        {
            var expectedResult = new [] {"Add", "Rectangle", "1", "5", "5", "3"};

            IEnumerable<string> result = CommandHelper.GetSanitizedInputSections("Add Rectangle 1, 5, 5, 3");

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
