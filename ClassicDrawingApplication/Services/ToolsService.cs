using System;
using System.Collections.Generic;
using System.Linq;
using ClassicDrawingApplication.Constants;
using ClassicDrawingApplication.Helpers;
using ClassicDrawingApplication.Interfaces;
using ClassicDrawingApplication.Models;

namespace ClassicDrawingApplication.Services
{
    public class ToolsService : IToolsService
    {
        private readonly IShapeHelper _shapeHelper;

        public ToolsService(IShapeHelper shapeHelper)
        {
            _shapeHelper = shapeHelper;
        }

        public readonly IList<Shape> Shapes = new List<Shape>();
        public int? SelectedId;
        private int Id = 1;        

        public void Add(string input)
        {            
            var inputSections = CommandHelper.GetSanitizedInputSections(input); // expected input format: Add Rectangle 1, 5, 5, 2

            if (CommandHelper.IsValidCommand(input, 6) && _shapeHelper.IsValidShapeType(inputSections[1]))
            {
                try
                {
                    var shape = new Shape()
                    {
                        Id = Id++,
                        ShapeType = inputSections[1],
                        TopLeftPointX = int.Parse(inputSections[2]),
                        TopLeftPointY = int.Parse(inputSections[3]),
                        BottomRightPointX = int.Parse(inputSections[4]),
                        BottomRightPointY = int.Parse(inputSections[5])
                    };

                    if (_shapeHelper.HasValidShapeCoordinates(shape))
                    {
                        Shapes.Add(shape);
                        Console.WriteLine($"{shape.ShapeType} Added at ([{shape.TopLeftPointX}],[{shape.TopLeftPointY}]) , ([{shape.BottomRightPointX}],[{shape.BottomRightPointY}])");
                    }
                    else
                    {
                        Console.WriteLine("Shape was not added, please check coordinates are valid. Example format: Add Rectangle 1, 5, 5, 2");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Shape was not added, please ensure coordinates are numerical. Example format: Add Rectangle 1, 5, 5, 2");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid Add Command, example: Add Rectangle 1, 5, 5, 2");
            }
        }

        public void Select(string input)
        {
            var inputSections = CommandHelper.GetSanitizedInputSections(input); // expected input format: Select 2, 4

            if (CommandHelper.IsValidCommand(input, 3))
            {
                try
                {
                    var xCoordinate = int.Parse(inputSections[1]);
                    var yCoordinate = int.Parse(inputSections[2]);

                    var Shape = _shapeHelper.SelectShape(Shapes, xCoordinate, yCoordinate);

                    if (Shape != null)
                    {
                        SelectedId = Shape.Id;
                        Console.WriteLine($"{Shape.Id} is selected.");
                    }
                    else
                    {
                        Console.WriteLine("No Shape selected.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please ensure coordinates are numerical. Example format: Select 2, 4");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid Select Command, example: Select 2, 4");
            }
        }

        public void Move(string input)
        {
            var inputSections = CommandHelper.GetSanitizedInputSections(input); // expected input format: Move 1, 3
            var shape = Shapes.FirstOrDefault(s => SelectedId == s.Id);

            if (shape == null)
            {
                Console.WriteLine("No Shape selected");
                return;
            }

            if (CommandHelper.IsValidCommand(input, 3))
            {
                try
                {
                    var horizontalShift = int.Parse(inputSections[1]);
                    var verticalShift = int.Parse(inputSections[2]);

                    shape.TopLeftPointX = shape.TopLeftPointX + horizontalShift;
                    shape.TopLeftPointY = shape.TopLeftPointY + verticalShift;
                    shape.BottomRightPointX = shape.BottomRightPointX + horizontalShift;
                    shape.BottomRightPointY = shape.BottomRightPointY + verticalShift;

                    Console.WriteLine($"{shape.Id} Moved to ([{shape.TopLeftPointX}],[{shape.TopLeftPointY}]) , ([{shape.BottomRightPointX}],[{shape.BottomRightPointY}])");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Shape was not moved, please ensure coordinates are numerical. Example format: Move 1, 3");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid Move Command, example: Move 1, 3");
            }
        }

        public void Resize(string input)
        {
            var inputSections = CommandHelper.GetSanitizedInputSections(input); // expected input format: Resize 1, 10, 10, 3
            var shape = Shapes.FirstOrDefault(s => SelectedId == s.Id);

            if (shape == null)
            {
                Console.WriteLine("No Shape selected");
                return;
            }

            if (CommandHelper.IsValidCommand(input, 5))
            {
                try
                {
                    shape.TopLeftPointX = int.Parse(inputSections[1]);
                    shape.TopLeftPointY = int.Parse(inputSections[2]);
                    shape.BottomRightPointX = int.Parse(inputSections[3]);
                    shape.BottomRightPointY = int.Parse(inputSections[4]);

                    Console.WriteLine($"{shape.Id} Resized to ([{shape.TopLeftPointX}],[{shape.TopLeftPointY}]) , ([{shape.BottomRightPointX}],[{shape.BottomRightPointY}])");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Shape was not resized, please ensure coordinates are numerical. Example format: Resize 1, 10, 10, 3");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid Resize Command, example: Resize 1, 10, 10, 3");
            }
        }

        public void Delete()
        {
            var shape = Shapes.FirstOrDefault(s => SelectedId == s.Id);

            if (shape == null)
            {
                Console.WriteLine("No Shape selected");
                return;
            }

            Shapes.Remove(shape);
            SelectedId = null;
            Console.WriteLine($"{shape.Id} deleted");
        }

        public void SetColour(string input)
        {
            var inputSections = CommandHelper.GetSanitizedInputSections(input); // expected input format: SetColour Red
            var shape = Shapes.FirstOrDefault(s => SelectedId == s.Id);

            if (shape == null)
            {
                Console.WriteLine("No Shape selected");
                return;
            }

            if ( CommandHelper.IsValidCommand(input, 2) && _shapeHelper.IsValidColour(inputSections[1]))
            {
                shape.Colour = inputSections[1];
                Console.WriteLine($"{shape.Id} updated to {shape.Colour}");
            }
            else
            {
                Console.WriteLine("Please enter a valid SetColour Command, example: SetColour Red");
            }
        }

        public void Redraw()
        {
            if (Shapes.Any())
            {
                foreach (var shape in Shapes)
                {
                    var text = $"{shape.Id} - {shape.ShapeType} - ([{shape.TopLeftPointX}],[{shape.TopLeftPointY}]) , ([{shape.BottomRightPointX}],[{shape.BottomRightPointY}]) - Colour {shape.Colour},";

                    if (shape.ShapeType == ShapeConstants.Circle)
                    {
                        var radius = _shapeHelper.CalculateShapeRadius(shape);
                        Console.WriteLine($"{text} Radius {radius}");

                    }
                    else
                    {
                        var length = _shapeHelper.CalculateShapeLength(shape);
                        var height = _shapeHelper.CalculateShapeHeight(shape);
                        Console.WriteLine($"{text} Length {length}, Height {height}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No Shapes have been drawn, please add one");
            }
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}
