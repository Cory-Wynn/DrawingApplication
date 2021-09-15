using System;
using System.Collections.Generic;
using System.Linq;
using ClassicDrawingApplication.Constants;
using ClassicDrawingApplication.Interfaces;
using ClassicDrawingApplication.Models;

namespace ClassicDrawingApplication.Helpers
{
    public class ShapeHelper : IShapeHelper
    {
        public bool IsValidShapeType(string shape)
        {
            return ShapeConstants.Shapes.Contains(shape, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsValidColour(string Colour)
        {
            return ColourConstants.Colours.Contains(Colour, StringComparer.OrdinalIgnoreCase);
        }

        public bool HasValidShapeCoordinates(Shape shape)
        {
            return shape.BottomRightPointX > shape.TopLeftPointX && shape.TopLeftPointY > shape.BottomRightPointY;
        }

        public int CalculateShapeHeight(Shape shape)
        {
            return Math.Abs(shape.TopLeftPointY - shape.BottomRightPointY);
        }

        public int CalculateShapeLength(Shape shape)
        {
            return Math.Abs(shape.TopLeftPointX - shape.BottomRightPointX);
        }

        public double CalculateShapeRadius(Shape shape)
        {
            var centreY = (Convert.ToDouble(shape.TopLeftPointY) + Convert.ToDouble(shape.BottomRightPointY)) / 2;
            var edgeY = shape.BottomRightPointY; 
            var radius = centreY - edgeY;

            return radius;
        }

        public Shape SelectShape(IEnumerable<Shape> shapes, int xCoordinate, int yCoordinate)
        {
            return shapes.FirstOrDefault(s => xCoordinate >= s.TopLeftPointX && xCoordinate <= s.BottomRightPointX
                                           && yCoordinate <= s.TopLeftPointY && yCoordinate >= s.BottomRightPointY);
        }
    }
}
