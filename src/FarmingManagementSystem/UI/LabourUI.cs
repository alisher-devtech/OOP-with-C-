using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class LabourUI
    {
        private TaskBL taskBL;
        private LabourRequestBL requestBL;

        public LabourUI()
        {
            taskBL = new TaskBL();
            requestBL = new LabourRequestBL();
        }

        public void Show()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 5)
            {
                ShowMenu();

                Console.SetCursorPosition(70, 17);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                option = ConsoleHelper.GetSafeInt(1, 5, 83, 17); 
                if (option == 1)
                    ViewTasks();
                else if (option == 2)
                    RequestTaskUpdate();
                else if (option == 3)
                    OtherRequest();
                else if (option == 4)
                    ViewMyRequests();
            }
        }

        private void ShowMenu()
        {
            Console.SetCursorPosition(64, 10);             ConsoleHelper.PrintColoredText("======LABOUR MENU======", ConsoleColor.Yellow);
            Console.SetCursorPosition(70, 11);             Console.Write("1. View task");
            Console.SetCursorPosition(70, 12);             Console.Write("2. Request Task Update");
            Console.SetCursorPosition(70, 13);             Console.Write("3. Other Request");
            Console.SetCursorPosition(70, 14);             Console.Write("4. View My Requests");
            Console.SetCursorPosition(70, 15);             Console.Write("5. Back");
        }

        private void ViewTasks()
        {
            try
            {
                taskBL.LoadTasks();
                List<TaskItem> tasks = taskBL.GetAllTasks();

                if (tasks.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No tasks assigned yet!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                int tx = 56, ty = 11;                 Console.SetCursorPosition(tx, 10);                 Console.Write("{0,-10} {1,-24} {2,-15} {3,-14}", "Task ID", "Task Name", "Deadline", "Status");

                foreach (TaskItem task in tasks)
                {
                    if (ty > 32) break; 
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-10} {1,-24} {2,-15} {3,-14}", task.TaskCropId, task.TaskName, task.TaskDeadline, task.TaskStatus);
                    ty++;
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 15, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void RequestTaskUpdate()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                taskBL.LoadTasks();
                List<TaskItem> tasks = taskBL.GetAllTasks();

                if (tasks.Count == 0)
                {
                    ConsoleHelper.ShowError(55, 12, "No tasks available to update!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                Console.SetCursorPosition(62, 10);                 Console.Write("=== REQUEST TASK STATUS UPDATE ===");
                Console.SetCursorPosition(55, 11);                 Console.Write("Available Tasks:");

                int tx = 55, ty = 14;                 Console.SetCursorPosition(tx, 13);                 Console.Write("{0,-10} {1,-30}", "Task ID", "Task Name");

                foreach (TaskItem task in tasks)
                {
                    if (ty > 26) break; 
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-10} {1,-30}", task.TaskCropId, task.TaskName);
                    ty++;
                }

                Console.SetCursorPosition(55, ty + 1);                 Console.Write("Enter Task ID: ");
                int taskId = ConsoleHelper.GetSafeInt(1, 9999, 70, ty + 1);

                Console.SetCursorPosition(55, ty + 2);
                Console.Write("Requested Status: ");
                string[] validStatus = { "Completed", "In Progress" };
                string newStatus = ConsoleHelper.GetValidRole(73, ty + 2, validStatus);

                Console.SetCursorPosition(55, ty + 3);
                Console.Write("Reason: ");
                string reason = ConsoleHelper.GetSafeString(63, ty + 3, "Reason", 5, 200);

                string description = taskId + "-" + newStatus + "-" + reason;

                if (requestBL.AddRequest("Task Update", description))
                {
                    ConsoleHelper.ShowSuccess(55, ty + 5, "Request submitted successfully!");
                }
                else
                {
                    ConsoleHelper.ShowError(55, ty + 5, "Failed to submit request!");
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(55, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void OtherRequest()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                Console.SetCursorPosition(62, 10);                 ConsoleHelper.PrintColoredText("========= OTHER REQUEST =========", ConsoleColor.Yellow);
                Console.SetCursorPosition(55, 12);                 Console.Write("You can request anything here:");
                Console.SetCursorPosition(55, 13);                 Console.Write("Examples: Need tools, Leave request, Salary issue, etc.");

                Console.SetCursorPosition(55, 15);                 Console.Write("Write your request:");
                Console.SetCursorPosition(55, 16);                 Console.Write("> ");
                string description = ConsoleHelper.GetSafeString(57, 16, "Request", 5, 500); 
                if (requestBL.AddRequest("Other", description))
                {
                    ConsoleHelper.ShowSuccess(55, 18, "Request submitted successfully!");                 }
                else
                {
                    ConsoleHelper.ShowError(55, 18, "Failed to submit request!");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(55, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ViewMyRequests()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                requestBL.LoadRequests();
                List<LabourRequest> requests = requestBL.GetAllRequests();

                Console.SetCursorPosition(68, 10);                 ConsoleHelper.PrintColoredText("========= MY REQUESTS =========", ConsoleColor.Yellow);

                if (requests.Count == 0)
                {
                    ConsoleHelper.ShowError(60, 12, "No requests submitted yet!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                int tx = 50, ty = 13;                 Console.SetCursorPosition(tx, 12);                 Console.Write("{0,-6} {1,-16} {2,-45} {3,-12}", "ID", "Type", "Description", "Status");

                foreach (LabourRequest req in requests)
                {
                    if (ty > 32) break; 
                    Console.SetCursorPosition(tx, ty);
                    string desc = req.RequestDescription.Length > 42 ? req.RequestDescription.Substring(0, 39) + "..." : req.RequestDescription;

                    Console.ForegroundColor = req.RequestStatus == "Pending" ? ConsoleColor.Yellow :
                                             req.RequestStatus == "Approved" ? ConsoleColor.Green : ConsoleColor.Red;

                    Console.Write("{0,-6} {1,-16} {2,-45} {3,-12}", req.RequestId, req.RequestType, desc, req.RequestStatus);
                    Console.ResetColor();
                    ty++;
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(60, 12, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }
    }
}