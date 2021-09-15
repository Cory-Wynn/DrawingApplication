using System.Collections.ObjectModel;

namespace ClassicDrawingApplication.Constants
{
    public static class ColourConstants
    {
        public const string Red = "Red";

        public const string Blue = "Blue";

        public const string Yellow = "Yellow";


        public static readonly ReadOnlyCollection<string> Colours =
            new ReadOnlyCollection<string>(new[]
            {
                Red,
                Blue,
                Yellow
            });
    }
}
