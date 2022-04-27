using System;
using System.Collections.Generic;
using System.Text;

namespace TryBeingFit.Services.Helpers
{
    public static class MessageHelper
    {
        public static void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
