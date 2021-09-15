using System.Collections.Generic;
using System.Linq;
using ClassicDrawingApplication.Interfaces;
using ClassicDrawingApplication.Models;
using ClassicDrawingApplication.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClassicDrawingApplication.Tests.Services
{
    public class ToolsServiceTests
    {
        [Fact]
        public void Add_Should_Add_Shape()
        {
            var input = "Add Rectangle 1, 5, 5, 3";

            var shapeHelperMock = new Mock<IShapeHelper>();
            shapeHelperMock.Setup(sh => sh.IsValidShapeType(It.IsAny<string>())).Returns(true);
            shapeHelperMock.Setup(sh => sh.HasValidShapeCoordinates(It.IsAny<Shape>())).Returns(true);

            var sut = new ToolsService(shapeHelperMock.Object);

            sut.Add(input);

            sut.Shapes.Count.Should().Be(1);
        }

        [Fact]
        public void Add_Should_Not_Add_Shape()
        {
            var input = "Add Rectangle 1, 1, 1, 1";

            var shapeHelperMock = new Mock<IShapeHelper>();
            shapeHelperMock.Setup(sh => sh.IsValidShapeType(It.IsAny<string>())).Returns(true);
            shapeHelperMock.Setup(sh => sh.HasValidShapeCoordinates(It.IsAny<Shape>())).Returns(false);

            var sut = new ToolsService(shapeHelperMock.Object);

            sut.Add(input);

            sut.Shapes.Count.Should().Be(0);
        }

        [Fact]
        public void Select_Should_Select_Shape()
        {
            var input = "Select 2, 4";

            var testShape = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();
            shapeHelperMock.Setup(sh => sh.SelectShape(It.IsAny<List<Shape>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(testShape);

            var sut = new ToolsService(shapeHelperMock.Object);

            sut.Select(input);

            sut.SelectedId.Should().Be(1);
        }

        [Fact]
        public void Select_Should_Not_Select_Shape()
        {
            var input = "Select 2, 4";

            var shapeHelperMock = new Mock<IShapeHelper>();

            var sut = new ToolsService(shapeHelperMock.Object);

            sut.Select(input);

            sut.SelectedId.Should().Be(null);
        }

        [Fact]
        public void Move_Should_Move_Shape()
        {
            var input = "Move 1, 3";

            var testShape = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();

            var sut = new ToolsService(shapeHelperMock.Object) {SelectedId = testShape.Id};
            sut.Shapes.Add(testShape);
            sut.Move(input);

            var result = sut.Shapes.FirstOrDefault(s => testShape.Id == s.Id);
            result.TopLeftPointX.Should().Be(2);
            result.TopLeftPointY.Should().Be(8);
            result.BottomRightPointX.Should().Be(6);
            result.BottomRightPointY.Should().Be(5);
        }

        [Fact]
        public void Resize_Should_Resize_Shape()
        {
            var input = "Resize 1, 10, 10, 3";

            var testShape = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();

            var sut = new ToolsService(shapeHelperMock.Object) { SelectedId = testShape.Id };
            sut.Shapes.Add(testShape);
            sut.Resize(input);

            var result = sut.Shapes.FirstOrDefault(s => testShape.Id == s.Id);
            result.TopLeftPointX.Should().Be(1);
            result.TopLeftPointY.Should().Be(10);
            result.BottomRightPointX.Should().Be(10);
            result.BottomRightPointY.Should().Be(3);
        }

        [Fact]
        public void Delete_Should_Delete_Shape()
        {
            var testShape = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();

            var sut = new ToolsService(shapeHelperMock.Object) { SelectedId = testShape.Id };
            sut.Shapes.Add(testShape);
            sut.Delete();

            sut.Shapes.Count.Should().Be(0);
        }

        [Fact]
        public void SetColour_Should_SetColour_Shape()
        {
            var input = "SetColour Blue";

            var testShape = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();
            shapeHelperMock.Setup(sh => sh.IsValidColour(It.IsAny<string>())).Returns(true);

            var sut = new ToolsService(shapeHelperMock.Object) { SelectedId = testShape.Id };
            sut.Shapes.Add(testShape);
            sut.SetColour(input);

            var result = sut.Shapes.FirstOrDefault(s => testShape.Id == s.Id);
            result.Colour.Should().Be("Blue");
        }

        [Fact]
        public void Redraw_Should_Redraw()
        {
            var rectangle = new Shape()
            {
                Id = 1,
                ShapeType = "Rectangle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var circle = new Shape()
            {
                Id = 2,
                ShapeType = "Circle",
                Colour = "Red",
                TopLeftPointX = 1,
                TopLeftPointY = 5,
                BottomRightPointX = 5,
                BottomRightPointY = 2
            };

            var shapeHelperMock = new Mock<IShapeHelper>();
            shapeHelperMock.Setup(sh => sh.CalculateShapeRadius(It.IsAny<Shape>())).Returns(1);
            shapeHelperMock.Setup(sh => sh.CalculateShapeLength(It.IsAny<Shape>())).Returns(2);
            shapeHelperMock.Setup(sh => sh.CalculateShapeHeight(It.IsAny<Shape>())).Returns(3);

            var sut = new ToolsService(shapeHelperMock.Object);
            sut.Shapes.Add(rectangle);
            sut.Shapes.Add(circle);
            sut.Redraw();

            shapeHelperMock.Verify(sh => sh.CalculateShapeRadius(It.IsAny<Shape>()));
            shapeHelperMock.Verify(sh => sh.CalculateShapeLength(It.IsAny<Shape>()));
            shapeHelperMock.Verify(sh => sh.CalculateShapeHeight(It.IsAny<Shape>()));

        }
    }
}
