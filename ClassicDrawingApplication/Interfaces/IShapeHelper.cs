using System.Collections.Generic;
using ClassicDrawingApplication.Models;

namespace ClassicDrawingApplication.Interfaces
{
    public interface IShapeHelper
    {
        bool IsValidShapeType(string shape);

        bool IsValidColour(string Colour);

        bool HasValidShapeCoordinates(Shape shape);

        int CalculateShapeHeight(Shape shape);

        int CalculateShapeLength(Shape shape);

        double CalculateShapeRadius(Shape shape);

        Shape SelectShape(IEnumerable<Shape> shapes, int xCoordinate, int yCoordinate);
    }
}
