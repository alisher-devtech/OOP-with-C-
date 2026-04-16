using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class ReportUI
    {
        private ReportBL reportBL;

        public ReportUI(ReportBL rBL)
        {
            reportBL = rBL;
        }

        public void Show()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 5)
            {
                try
                {
                    Console.SetCursorPosition(64, 10);                     ConsoleHelper.PrintColoredText("=======View Reports=======", ConsoleColor.Yellow);
                    Console.SetCursorPosition(70, 11);                     Console.Write("1. Total Employees");
                    Console.SetCursorPosition(70, 12);                     Console.Write("2. Total Crops");
                    Console.SetCursorPosition(70, 13);                     Console.Write("3. Harvested Vs Growing");
                    Console.SetCursorPosition(70, 14);                     Console.Write("4. Salary Summary");
                    Console.SetCursorPosition(70, 15);                     Console.Write("5. Back");

                    Console.SetCursorPosition(70, 17);                     ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                    option = ConsoleHelper.GetSafeInt(1, 5, 83, 17); 
                    if (option == 1)
                        ShowTotalEmployees();
                    else if (option == 2)
                        ShowTotalCrops();
                    else if (option == 3)
                        ShowCropStatus();
                    else if (option == 4)
                        ShowSalarySummary();
                    else if (option == 5)
                    {
                        ConsoleHelper.Pause();
                        ConsoleHelper.ClearInsideBoundary();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
        }

        private void ShowTotalEmployees()
        {
            try
            {
                reportBL.LoadData();
                Dictionary<string, int> empReport = reportBL.GetEmployeeReport();
                int total = reportBL.GetTotalEmployees();

                Console.SetCursorPosition(70, 21);                 Console.Write("Total Employees: " + total);
                Console.SetCursorPosition(70, 22);                 Console.Write("Labours: " + empReport["Labour"]);
                Console.SetCursorPosition(70, 23);                 Console.Write("Supervisors: " + empReport["Supervisor"]);
                Console.SetCursorPosition(70, 24);                 Console.Write("Managers: " + empReport["Manager"]);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ShowTotalCrops()
        {
            try
            {
                reportBL.LoadData();
                Dictionary<string, int> cropReport = reportBL.GetCropTypeReport();

                Console.SetCursorPosition(70, 21);                 Console.Write("Total Crops: " + cropReport["Total"]);
                Console.SetCursorPosition(70, 22);                 Console.Write("Vegetables: " + cropReport["Vegetable"]);
                Console.SetCursorPosition(70, 23);                 Console.Write("Fruits: " + cropReport["Fruit"]);
                Console.SetCursorPosition(70, 24);                 Console.Write("Grains: " + cropReport["Grain"]);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ShowCropStatus()
        {
            try
            {
                reportBL.LoadData();
                Dictionary<string, int> statusReport = reportBL.GetCropStatusReport();

                Console.SetCursorPosition(70, 21);                 Console.Write("Harvested: " + statusReport["Harvested"]);
                Console.SetCursorPosition(70, 22);                 Console.Write("Growing: " + statusReport["Growing"]);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 24, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ShowSalarySummary()
        {
            try
            {
                reportBL.LoadData();
                Dictionary<string, double> salaryReport = reportBL.GetSalaryReport();

                Console.SetCursorPosition(70, 21);                 Console.Write("Labours' salary:     Rs. " + salaryReport["Labour"]);
                Console.SetCursorPosition(70, 22);                 Console.Write("Supervisors' salary: Rs. " + salaryReport["Supervisor"]);
                Console.SetCursorPosition(70, 23);                 Console.Write("Managers' salary:    Rs. " + salaryReport["Manager"]);
                Console.SetCursorPosition(70, 24);                 Console.Write("Total Salary:        Rs. " + salaryReport["Total"]);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 26, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }
    }
}