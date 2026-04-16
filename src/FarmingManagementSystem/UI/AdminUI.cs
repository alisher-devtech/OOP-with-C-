using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class AdminUI
    {
        private EmployeeBL employeeBL;
        private CropBL cropBL;
        private ReportBL reportBL;
        private SignUpRequestBL requestBL;
        private UserBL userBL;
        private string currentAdmin;

        public AdminUI(string adminUsername)
        {
            employeeBL = new EmployeeBL();
            cropBL = new CropBL();
            reportBL = new ReportBL();
            requestBL = new SignUpRequestBL();
            userBL = new UserBL();
            currentAdmin = adminUsername;
        }

        public void Show()
        {
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 6)
            {
                try
                {
                    ConsoleHelper.ClearInsideBoundary();
                    ShowMenu();

                    Console.SetCursorPosition(70, 19); 
                    ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                    option = ConsoleHelper.GetSafeInt(1, 6, 83, 19); 

                    if (option == 1)
                    {
                        EmployeeManagementUI empUI = new EmployeeManagementUI(employeeBL);
                        empUI.Show();
                    }
                    else if (option == 2)
                    {
                        CropManagementUI cropUI = new CropManagementUI(cropBL);
                        cropUI.Show();
                    }
                    else if (option == 3)
                    {
                        ReportUI reportUI = new ReportUI(reportBL);
                        reportUI.Show();
                    }
                    else if (option == 4)
                    {
                        ViewRequests();
                    }
                    else if (option == 5)
                    {
                        ViewAllUsers();
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(70, 25, "Error: " + ex.Message);                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
        }

        private void ShowMenu()
        {
            Console.SetCursorPosition(70, 10);             ConsoleHelper.PrintColoredText("--------Admin Menu---------", ConsoleColor.Yellow);
            Console.SetCursorPosition(70, 11);             Console.Write("1. Employee Management");
            Console.SetCursorPosition(70, 12);             Console.Write("2. Crop Management");
            Console.SetCursorPosition(70, 13);             Console.Write("3. View Reports");
            Console.SetCursorPosition(70, 14);             Console.Write("4. View Requests");
            Console.SetCursorPosition(70, 15);             Console.Write("5. View All Users");
            Console.SetCursorPosition(70, 16);             Console.Write("6. Exit");
            Console.SetCursorPosition(70, 17);             ConsoleHelper.PrintColoredText("---------------------------", ConsoleColor.Yellow);
        }

        private void ViewRequests()
        {
            try
            {
                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();

                requestBL.LoadRequests();
                List<User> requests = requestBL.GetAllRequests();

                if (requests.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No pending requests!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                Console.SetCursorPosition(65, 10);                 ConsoleHelper.PrintColoredText("======== PENDING REQUESTS ========", ConsoleColor.Yellow);

                int tx = 60, ty = 12;                 Console.SetCursorPosition(tx, 11);                 Console.Write("{0,-15} {1,-15} {2,-15}", "Username", "Password", "Role");

                foreach (User req in requests)
                {
                    if (ty > 30) break;                     Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-15} {1,-15} {2,-15}", req.Username, req.Password, req.Role);
                    ty++;
                }

                Console.SetCursorPosition(70, ty + 2);
                Console.Write("Enter username to approve (or type 'back'): ");
                string approveUser = Console.ReadLine();

                if (approveUser.Equals("back", StringComparison.OrdinalIgnoreCase))
                {
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                if (requestBL.ApproveRequest(approveUser))
                {
                    ConsoleHelper.ShowSuccess(70, ty + 4, "Request approved successfully!");
                }
                else
                {
                    ConsoleHelper.ShowError(70, ty + 4, "Username not found in requests!");
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 25, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ViewAllUsers()
        {
            try
            {
                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();

                userBL.LoadUsers();
                List<User> users = userBL.GetAllUsers();

                Console.SetCursorPosition(54, 10);                 ConsoleHelper.PrintColoredText("========== ALL REGISTERED USERS ==========", ConsoleColor.Yellow);

                int tx = 54, ty = 13;                 Console.SetCursorPosition(tx, 12);                 Console.Write("{0,-15} {1,-15} {2,-15}", "Username", "Password", "Role");

                Console.SetCursorPosition(tx, ty - 1);
                Console.Write(new string('-', 45));

                int displayedCount = 0;
                foreach (User user in users)
                {
                    if (user.Username == currentAdmin)
                        continue;

                    if (ty > 30) break; 
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-15} {1,-15} {2,-15}", user.Username, user.Password, user.Role);
                    ty++;
                    displayedCount++;
                }

                if (displayedCount == 0)
                {
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("No other users found!");
                }

                Console.SetCursorPosition(tx, ty + 2);
                Console.Write("Total Users (excluding you): " + displayedCount);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 25, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }
    }
}