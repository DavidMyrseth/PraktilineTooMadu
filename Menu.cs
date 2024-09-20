using System;
using System.Diagnostics;
using System.Threading;

namespace PraktilineTööMadu
{
    internal static class Menu
    {
        public static void ShowStartMessage()
        {
            string message = "You play as Hungry At sign -> @";
            string prompt = "Press SPACE to continue...";
            DisplayCenteredMessage(message, -2);
            DisplayCenteredMessage(prompt, 2);
        }

        public static void DisplayCenteredMessage(string message, int yOffset)
        {
            int x = Console.WindowWidth / 2 - message.Length / 2;
            int y = Console.WindowHeight / 2 + yOffset;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(message);
        }

        public static void ShowGameRules()
        {
            Console.Clear();
            string rules = "You play as @. Eat $ to gain 1 point. Eat X to lose 1 point.";
            string prompt = "Press SPACE to start the game...";
            DisplayCenteredMessage(rules, 0);
            DisplayCenteredMessage(prompt, 2);
        }

        public static string GetPlayerName()
        {
            Console.Clear();
            string prompt = "Write your name: ";
            int x = Console.WindowWidth / 2 - prompt.Length / 2;
            int y = Console.WindowHeight / 2 - 2;
            Console.SetCursorPosition(x, y);
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
