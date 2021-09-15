using System;

namespace ClassicDrawingApplication.Helpers
{
    public static class CommandHelper
    {
        public static bool IsValidCommand(string input, int expectedArrayCount)
        {
            var actualArrayCount = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
            return actualArrayCount == expectedArrayCount;
        }

        public static string[] GetSanitizedInputSections(string input)
        {
            var sanitizedInput = input.Replace(",", "");
            return sanitizedInput.Split();
        }
    }
}
