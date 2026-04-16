using System;
using FarmingManagementSystem.UI;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                                int targetWidth = 160;
                int targetHeight = 50;
                int safeWidth = Math.Min(targetWidth, Console.LargestWindowWidth);
                int safeHeight = Math.Min(targetHeight, Console.LargestWindowHeight);

                Console.SetWindowSize(safeWidth, safeHeight);
                Console.SetBufferSize(safeWidth, safeHeight);

                ConsoleHelper.ClearScreen();

                LoginUI loginUI = new LoginUI();
                loginUI.Show();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n");
                Console.WriteLine("        ╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("        ║                                                        ║");
                Console.WriteLine("        ║   Thank you for using Farming Management System!      ║");
                Console.WriteLine("        ║                                                        ║");
                Console.WriteLine("        ╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\n        Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nFatal Error: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}