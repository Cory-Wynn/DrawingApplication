namespace ClassicDrawingApplication.Models
{
    public class Shape
    {
        public int Id { get; set; }

        public string ShapeType { get; set; }

        public string Colour { get; set; }

        public int TopLeftPointX { get; set; }

        public int TopLeftPointY { get; set; }

        public int BottomRightPointX { get; set; }

        public int BottomRightPointY { get; set; }
    }
}
