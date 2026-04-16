using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class SupervisorUI
    {
        private TaskBL taskBL;

        public SupervisorUI()
        {
            taskBL = new TaskBL();
        }

        public void Show()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 5)
            {
                ShowMenu();

                Console.SetCursorPosition(70, 16);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                option = ConsoleHelper.GetSafeInt(1, 5, 83, 16); 
                if (option == 1)
                    AssignTask();
                else if (option == 2)
                    ViewTasks();
                else if (option == 3)
                    UpdateTask();
                else if (option == 4)
                    DeleteTask();
            }
        }

        private void ShowMenu()
        {
            Console.SetCursorPosition(64, 10);             ConsoleHelper.PrintColoredText("======SUPERVISOR MENU======", ConsoleColor.Yellow);
            Console.SetCursorPosition(70, 11);             Console.Write("1. Assign Task");
            Console.SetCursorPosition(70, 12);             Console.Write("2. View Task");
            Console.SetCursorPosition(70, 13);             Console.Write("3. Update Task");
            Console.SetCursorPosition(70, 14);             Console.Write("4. Delete Task");
            Console.SetCursorPosition(70, 15);             Console.Write("5. Back");
        }

        private void AssignTask()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int choice = 1;

            while (choice != 0)
            {
                try
                {
                    Console.SetCursorPosition(70, 10);                     Console.Write("Task Name: ");
                    string taskName = ConsoleHelper.GetSafeString(81, 10, "Task name", 3, 200); 
                    Console.SetCursorPosition(70, 11);                     Console.Write("Deadline: ");
                    string deadline = ConsoleHelper.GetSafeString(80, 11, "Deadline", 5, 50); 
                    Console.SetCursorPosition(70, 12);                     Console.Write("Status: ");
                    string[] validStatus = { "Pending", "In Progress", "Completed" };
                    string status = ConsoleHelper.GetValidRole(78, 12, validStatus); 
                    if (taskBL.AddTask(taskName, status, deadline))
                    {
                        ConsoleHelper.ShowSuccess(70, 14, "Task assigned successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 14, "Failed to assign task!");                     }

                    Console.SetCursorPosition(70, 16);                     Console.Write("Assign more tasks? (1 = Yes, 0 = No): ");
                    choice = ConsoleHelper.GetSafeInt(0, 1, 109, 16); 
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(70, 18, "Error: " + ex.Message);                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
        }

        private void ViewTasks()
        {
            try
            {
                taskBL.LoadTasks();
                List<TaskItem> tasks = taskBL.GetAllTasks();

                if (tasks.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No tasks found!");                     ConsoleHelper.Pause();
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

        private void UpdateTask()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter task ID to update: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 95, 14); 
                Console.SetCursorPosition(70, 15);                 Console.Write("Update Status: ");
                string[] validStatus = { "Pending", "In Progress", "Completed" };
                string status = ConsoleHelper.GetValidRole(85, 15, validStatus); 
                Console.SetCursorPosition(70, 16);                 Console.Write("Update Deadline: ");
                string deadline = ConsoleHelper.GetSafeString(87, 16, "Deadline", 5, 50); 
                if (taskBL.UpdateTask(id, status, deadline))
                {
                    ConsoleHelper.ShowSuccess(70, 18, "Updated Successfully!");                 }
                else
                {
                    ConsoleHelper.ShowError(70, 18, "Update failed!");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void DeleteTask()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter task ID to delete: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 95, 14); 
                Console.SetCursorPosition(70, 15);                 Console.Write("Are you sure? (1=Yes, 0=No): ");
                int confirm = ConsoleHelper.GetSafeInt(0, 1, 99, 15); 
                if (confirm == 1)
                {
                    if (taskBL.DeleteTask(id))
                    {
                        ConsoleHelper.ShowSuccess(70, 17, "Deleted Successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 17, "Deletion failed! ID not found.");                     }
                }
                else
                {
                    ConsoleHelper.ShowSuccess(70, 17, "Deletion cancelled.");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 19, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }
    }
}