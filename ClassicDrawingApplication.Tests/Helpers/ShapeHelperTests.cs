using System.Collections.Generic;
using ClassicDrawingApplication.Helpers;
using ClassicDrawingApplication.Models;
using FluentAssertions;
using Xunit;

namespace ClassicDrawingApplication.Tests.Helpers
{
    public class ToolsServiceTests
    {
        [Theory]
        [InlineData("rectangle")]
        [InlineData("Rectangle")]
        [InlineData("Triangle")]
        [InlineData("triangle")]
        [InlineData("circle")]
        [InlineData("Circle")]
        public void IsValidShape_Should_Be_True(string shape)
        {
            var sut = new ShapeHelper();

            var result = sut.IsValidShapeType(shape);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("square")]
        [InlineData("star")]
        [InlineData("cross")]
        public void IsValidShape_Should_Be_False(string shape)
        {
            var sut = new ShapeHelper();

            var result = sut.IsValidShapeType(shape);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("red")]
        [InlineData("Red")]
        [InlineData("blue")]
        [InlineData("Blue")]
        [InlineData("yellow")]
        [InlineData("Yellow")]
        public void IsValidColour_Should_Be_True(string colour)
        {
            var sut = new ShapeHelper();

            var result = sut.IsValidColour(colour);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Orange")]
        [InlineData("Green")]
        [InlineData("Pink")]
        public void IsValidColour_Should_Be_False(string colour)
        {
            var sut = new ShapeHelper();

            var result = sut.IsValidColour(colour);

            result.Should().BeFalse();
        }

        [Fact]
        public void HasValidShapeCoordinates_Should_Be_True()
        {
            // top left x should be lower than bottom right x
            // bottom right y should be lower than top left y

            var shape = new Shape()
            {
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 1
            };

            var sut = new ShapeHelper();

            var result = sut.HasValidShapeCoordinates(shape);

            result.Should().BeTrue();
        }

        [Fact]
        public void HasValidShapeCoordinates_Should_Be_False()
        {
            // top left x should be lower than bottom right x
            // bottom right y should be lower than top left y

            var shape = new Shape()
            {
                TopLeftPointX = 5,
                TopLeftPointY = 1,
                BottomRightPointX = 1,
                BottomRightPointY = 5
            };

            var sut = new ShapeHelper();

            var result = sut.HasValidShapeCoordinates(shape);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(5, 1, 4)]
        [InlineData(10, 1, 9)]
        public void CalculateShapeHeight_Should_Be_Expected_Height(int topLeftY, int bottomRightY, int expectedHeight)
        {
            var shape = new Shape()
            {
                TopLeftPointY = topLeftY,
                BottomRightPointY = bottomRightY
            };

            var sut = new ShapeHelper();

            var result = sut.CalculateShapeHeight(shape);

            result.Should().Be(expectedHeight);
        }

        [Theory]
        [InlineData(1, 5, 4)]
        [InlineData(1, 10, 9)]
        public void CalculateShapeLength_Should_Be_Expected_Length(int topLeftX, int bottomRightX, int expectedLength)
        {
            var shape = new Shape()
            {
                TopLeftPointX = topLeftX,
                BottomRightPointX = bottomRightX
            };

            var sut = new ShapeHelper();

            var result = sut.CalculateShapeLength(shape);

            result.Should().Be(expectedLength);
        }

        [Theory]
        [InlineData(6, 1, 2.5)]
        [InlineData(4, 1, 1.5)]
        public void CalculateShapeRadius_Should_Be_Expected_Radius(int topLeftY, int bottomRightY, double expectedRadius)
        {
            var shape = new Shape()
            {
                TopLeftPointY = topLeftY,
                BottomRightPointY = bottomRightY
            };

            var sut = new ShapeHelper();

            var result = sut.CalculateShapeRadius(shape);

            result.Should().Be(expectedRadius);
        }

        [Fact]
        public void SelectShape_Should_Select_CorrectShape()
        {
            var shapes = new List<Shape>
            {
                new Shape()
                {
                    Id = 1,
                    ShapeType = "Rectangle",
                    Colour = "Red",
                    TopLeftPointX = 1,
                    TopLeftPointY = 5,
                    BottomRightPointX = 5,
                    BottomRightPointY = 1
                },
                new Shape()
                {
                    Id = 2,
                    ShapeType = "Triangle",
                    Colour = "Red",
                    TopLeftPointX = 10,
                    TopLeftPointY = 15,
                    BottomRightPointX = 10,
                    BottomRightPointY = 15
                }
            };

            var sut = new ShapeHelper();

            var result = sut.SelectShape(shapes, 3, 4);

            result.Id.Should().Be(1);
        }
    }
}
