using System;

namespace FarmingManagementSystem.Utilities
{
    public static class ConsoleHelper
    {
        public static void ClearScreen()
        {
            Console.Clear();
            DrawBoundary();
            ShowHeader();
        }

        public static void DrawBoundary()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 0);
            Console.Write(new string('=', Console.WindowWidth - 1));             Console.SetCursorPosition(0, 38);             Console.Write(new string('=', Console.WindowWidth - 1));             Console.ResetColor();
        }

        public static void ShowHeader()
        {
            Console.SetCursorPosition(10, 1);
            Console.Write(" _____                    _               __  __                                                   _     ____            _                 ");
            Console.SetCursorPosition(10, 2);
            Console.Write("|  ___|_ _ _ __ _ __ ___ (_)_ __   __ _  |  \\/  | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_  / ___| _   _ ___| |_ ___ _ __ ___  ");
            Console.SetCursorPosition(10, 3);
            Console.Write("| |_ / _` | '__| '_ ` _ \\| | '_ \\ / _` | | |\\/| |/ _` | '_ \\ / _` |/ _` |/ _ \\ '_ ` _ \\ / _ \\ '_ \\| __| \\___ \\| | | / __| __/ _ \\ '_ ` _ \\ ");
            Console.SetCursorPosition(10, 4);
            Console.Write("|  _| (_| | |  | | | | | | | | | | (_| | | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_   ___) | |_| \\__ \\ ||  __/ | | | | |");
            Console.SetCursorPosition(10, 5);
            Console.Write("|_|  \\__,_|_|  |_| |_| |_|_|_| |_|\\__, | |_|  |_|\\__,_|_| |_|\\__,_|\\__, |\\___|_| |_| |_|\\___|_| |_|\\__| |____/ \\__, |___/\\__\\___|_| |_| |_|");
            Console.SetCursorPosition(10, 6);
            Console.Write("                                  |___/                            |___/                                       |___/                       ");
        }

        public static void Pause()
        {
            Console.SetCursorPosition(67, 35);             Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        public static void PrintMenuHeader(string mainMenu, string subMenu)
        {
            Console.SetCursorPosition(70, 9);             if (string.IsNullOrEmpty(subMenu))
                Console.Write(mainMenu);
            else
                Console.Write($"{mainMenu} > {subMenu}");
            Console.SetCursorPosition(70, 10);             PrintColoredText("---------------------------", ConsoleColor.Yellow);
        }

        public static void PrintColoredText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static int GetSafeInt(int min, int max, int x, int y)
        {
            int value;
            while (true)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("    ");
                Console.SetCursorPosition(x, y);

                string input = Console.ReadLine();

                Console.SetCursorPosition(x - 15, y + 2);
                Console.Write(new string(' ', 70));

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText("Input cannot be empty! Please enter a number.", ConsoleColor.Red);
                    continue;
                }

                if (int.TryParse(input, out value))
                {
                    if (value >= min && value <= max)
                    {
                        Console.SetCursorPosition(x - 15, y + 2);
                        Console.Write(new string(' ', 70));
                        return value;
                    }
                    else
                    {
                        Console.SetCursorPosition(x - 15, y + 2);
                        PrintColoredText($"Out of range! Enter {min}-{max}.", ConsoleColor.Red);
                    }
                }
                else
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText("Invalid! Enter numbers only.", ConsoleColor.Red);
                }
            }
        }

        public static double GetSafeDouble(int x, int y, string fieldName)
        {
            double value;
            while (true)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', 20));
                Console.SetCursorPosition(x, y);

                string input = Console.ReadLine();

                Console.SetCursorPosition(x - 15, y + 2);
                Console.Write(new string(' ', 70));

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText($"{fieldName} cannot be empty!", ConsoleColor.Red);
                    continue;
                }

                if (double.TryParse(input, out value))
                {
                    if (value > 0)
                    {
                        Console.SetCursorPosition(x - 15, y + 2);
                        Console.Write(new string(' ', 70));
                        return value;
                    }
                    else
                    {
                        Console.SetCursorPosition(x - 15, y + 2);
                        PrintColoredText($"{fieldName} must be greater than 0!", ConsoleColor.Red);
                    }
                }
                else
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText("Invalid! Enter valid decimal number.", ConsoleColor.Red);
                }
            }
        }

        public static string GetSafeString(int x, int y, string fieldName, int minLength = 1, int maxLength = 100)
        {
            while (true)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(x, y);

                string input = Console.ReadLine();

                Console.SetCursorPosition(x - 15, y + 2);
                Console.Write(new string(' ', 70));

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText($"{fieldName} cannot be empty!", ConsoleColor.Red);
                    continue;
                }

                if (input.Length < minLength)
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText($"{fieldName} must be at least {minLength} characters!", ConsoleColor.Red);
                    continue;
                }

                if (input.Length > maxLength)
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText($"{fieldName} cannot exceed {maxLength} characters!", ConsoleColor.Red);
                    continue;
                }

                Console.SetCursorPosition(x - 15, y + 2);
                Console.Write(new string(' ', 70));
                return input.Trim();
            }
        }

        public static string GetValidRole(int x, int y, string[] validRoles)
        {
            while (true)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(x, y);

                string input = Console.ReadLine();

                Console.SetCursorPosition(x - 15, y + 2);
                Console.Write(new string(' ', 70));

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.SetCursorPosition(x - 15, y + 2);
                    PrintColoredText("Role cannot be empty!", ConsoleColor.Red);
                    continue;
                }

                foreach (string role in validRoles)
                {
                    if (input.Trim().Equals(role, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.SetCursorPosition(x - 15, y + 2);
                        Console.Write(new string(' ', 70));
                        return role;
                    }
                }

                Console.SetCursorPosition(x - 15, y + 2);
                PrintColoredText($"Invalid! Choose from: {string.Join(", ", validRoles)}", ConsoleColor.Red);
            }
        }

        public static void ClearInsideBoundary()
        {
            for (int y = 7; y < 38; y++)             {
                Console.SetCursorPosition(50, y);
                Console.Write(new string(' ', Console.WindowWidth - 80));
            }
        }

        public static void ShowError(int x, int y, string message)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);             Console.ResetColor();
        }

        public static void ShowSuccess(int x, int y, string message)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);             Console.ResetColor();
        }
    }
}