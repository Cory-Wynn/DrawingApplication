using System.Collections.ObjectModel;

namespace ClassicDrawingApplication.Constants
{
    public static class ShapeConstants
    {
        public const string Rectangle = "Rectangle";

        public const string Triangle = "Triangle";

        public const string Circle= "Circle";


        public static readonly ReadOnlyCollection<string> Shapes =
            new ReadOnlyCollection<string>(new[]
            {
                Rectangle,
                Triangle,
                Circle
            });
    }
}
