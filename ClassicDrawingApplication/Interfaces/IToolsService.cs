namespace ClassicDrawingApplication.Interfaces
{
    public interface IToolsService
    {
        void Add(string input);

        void Select(string input);

        void Move(string input);

        void Resize(string input);

        void Delete();

        void SetColour(string input);

        void Redraw();

        string ReadInput();
    }
}
