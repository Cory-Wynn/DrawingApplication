using System;
using ClassicDrawingApplication.Constants;
using ClassicDrawingApplication.Interfaces;

namespace ClassicDrawingApplication
{
    public class ClassicDrawingApplication : IClassicDrawingApplication
    {
        private readonly IToolsService _toolsService;

        public ClassicDrawingApplication(IToolsService toolsService)
        {
            _toolsService = toolsService;
        }

        public void Run()
        {           
            while (true)
            {
                EnterCommand();
            }
        }

        public void EnterCommand()
        {
            Console.WriteLine("Enter command:");
            var input = _toolsService.ReadInput();

            if (!string.IsNullOrWhiteSpace(input))
            {
                var command = input.Split()[0];

                switch (command)
                {
                    case CommandConstants.Add:
                        _toolsService.Add(input);
                        break;
                    case CommandConstants.Select:
                        _toolsService.Select(input);
                        break;
                    case CommandConstants.Move:
                        _toolsService.Move(input);
                        break;
                    case CommandConstants.Resize:
                        _toolsService.Resize(input);
                        break;
                    case CommandConstants.Delete:
                        _toolsService.Delete();
                        break;
                    case CommandConstants.SetColour:
                        _toolsService.SetColour(input);
                        break;
                    case CommandConstants.Redraw:
                        _toolsService.Redraw();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Input cannot be null or empty");
            }
        }
    }
}
