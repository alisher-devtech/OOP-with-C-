using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class ManagerUI
    {
        private TaskBL taskBL;
        private OrderBL orderBL;
        private SaleBL saleBL;
        private LabourRequestBL requestBL;

        public ManagerUI()
        {
            taskBL = new TaskBL();
            orderBL = new OrderBL();
            saleBL = new SaleBL();
            requestBL = new LabourRequestBL();
        }

        public void Show()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 9)
            {
                ShowMenu();

                Console.SetCursorPosition(70, 20);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                option = ConsoleHelper.GetSafeInt(1, 9, 83, 20); 
                if (option == 1)
                    AssignTask();
                else if (option == 2)
                    ViewTasks();
                else if (option == 3)
                    UpdateTask();
                else if (option == 4)
                    DeleteTask();
                else if (option == 5)
                    UpdateOrderStatus();
                else if (option == 6)
                    ViewTodaysSales();
                else if (option == 7)
                    ViewLastMonthSales();
                else if (option == 8)
                    HandleLabourRequests();
            }
        }

        private void ShowMenu()
        {
            Console.SetCursorPosition(64, 10);             ConsoleHelper.PrintColoredText("======MANAGER MENU======", ConsoleColor.Yellow);
            Console.SetCursorPosition(70, 11);             Console.Write("1. Assign Task");
            Console.SetCursorPosition(70, 12);             Console.Write("2. View Task");
            Console.SetCursorPosition(70, 13);             Console.Write("3. Update Task");
            Console.SetCursorPosition(70, 14);             Console.Write("4. Delete Task");
            Console.SetCursorPosition(70, 15);             Console.Write("5. Update Order Status");
            Console.SetCursorPosition(70, 16);             Console.Write("6. View Today's Sales");
            Console.SetCursorPosition(70, 17);             Console.Write("7. View Last Month Sales");
            Console.SetCursorPosition(70, 18);             Console.Write("8. Handle Labour Requests");
            Console.SetCursorPosition(70, 19);             Console.Write("9. Back");
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

        private void UpdateOrderStatus()
        {
            try
            {
                Console.SetCursorPosition(70, 16);                 Console.Write("Enter order ID to update: ");
                int orderId = ConsoleHelper.GetSafeInt(1, 9999, 96, 16); 
                Console.SetCursorPosition(70, 17);                 Console.Write("Update status: ");
                string[] validStatus = { "Pending", "Processing", "Completed", "Delivered", "Cancelled" };
                string status = ConsoleHelper.GetValidRole(85, 17, validStatus); 
                if (orderBL.UpdateOrderStatus(orderId, status))
                {
                    ConsoleHelper.ShowSuccess(70, 19, "Updated Successfully!");                 }
                else
                {
                    ConsoleHelper.ShowError(70, 19, "Update failed!");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 21, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ViewTodaysSales()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                saleBL.LoadSales();
                saleBL.LoadOrders();

                Sale todaySale = saleBL.GetTodaysSales();
                int completedOrders = saleBL.GetTodayCompletedOrdersCount();

                string today = DateTime.Now.ToString("d/M/yyyy");

                Console.SetCursorPosition(65, 12);                 ConsoleHelper.PrintColoredText("========== TODAY'S SALES REPORT ==========", ConsoleColor.Yellow);
                Console.SetCursorPosition(70, 14);                 Console.Write("Date: " + today);
                Console.SetCursorPosition(70, 15);                 Console.Write("Total Completed Orders: " + completedOrders);
                Console.SetCursorPosition(70, 16);                 Console.Write("Total Sales Amount: Rs. " + (todaySale != null ? todaySale.SaleAmount : 0));
                Console.SetCursorPosition(65, 17);                 ConsoleHelper.PrintColoredText("===========================================", ConsoleColor.Yellow);

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(65, 19, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ViewLastMonthSales()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                saleBL.LoadSales();
                Dictionary<string, int> monthlySales = saleBL.GetLastMonthSales();
                int monthlyTotal = saleBL.GetMonthlyTotal();

                DateTime now = DateTime.Now;
                int lastMonth = now.Month - 1;
                int lastYear = now.Year;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    lastYear--;
                }

                Console.SetCursorPosition(65, 10);                 ConsoleHelper.PrintColoredText("======== LAST MONTH SALES REPORT ========", ConsoleColor.Yellow);
                Console.SetCursorPosition(70, 12);                 Console.Write("Month: " + lastMonth + "/" + lastYear);

                int tx = 60, ty = 15;                 Console.SetCursorPosition(tx, 14);                 Console.Write("{0,-20} {1,-20}", "Date", "Amount (Rs.)");

                int itemCount = 0;
                foreach (KeyValuePair<string, int> sale in monthlySales)
                {
                    if (ty > 28) break; 
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-20} {1,-20}", sale.Key, sale.Value);
                    ty++;
                    itemCount++;
                }

                Console.SetCursorPosition(tx, ty + 1);
                ConsoleHelper.PrintColoredText("==========================================", ConsoleColor.Yellow);
                Console.SetCursorPosition(70, ty + 2);
                Console.Write("Total Days with Sales: " + monthlySales.Count);
                Console.SetCursorPosition(70, ty + 3);
                Console.Write("Total Monthly Sales: Rs. " + monthlyTotal);

                if (monthlySales.Count > 0)
                {
                    Console.SetCursorPosition(70, ty + 4);
                    Console.Write("Average Daily Sales: Rs. " + (monthlyTotal / monthlySales.Count));
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(65, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void HandleLabourRequests()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();

                requestBL.LoadRequests();
                List<LabourRequest> pendingRequests = requestBL.GetPendingRequests();

                if (pendingRequests.Count == 0)
                {
                    ConsoleHelper.ShowError(55, 12, "No pending requests to handle!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                Console.SetCursorPosition(55, 10);                 ConsoleHelper.PrintColoredText("========= HANDLE LABOUR REQUEST =========", ConsoleColor.Yellow);
                Console.SetCursorPosition(55, 11);                 Console.Write("Pending Requests:");

                int tx = 55, ty = 13;                 Console.SetCursorPosition(tx, 12);                 Console.Write("{0,-6} {1,-14} {2,-50}", "ID", "Type", "Description");

                foreach (LabourRequest req in pendingRequests)
                {
                    if (ty > 24) break; 
                    Console.SetCursorPosition(tx, ty);
                    string desc = req.RequestDescription.Length > 47 ? req.RequestDescription.Substring(0, 44) + "..." : req.RequestDescription;
                    Console.Write("{0,-6} {1,-14} {2,-50}", req.RequestId, req.RequestType, desc);
                    ty++;
                }

                Console.SetCursorPosition(55, ty + 1);
                Console.Write("Enter Request ID to handle: ");
                int reqId = ConsoleHelper.GetSafeInt(1, 9999, 83, ty + 1);

                LabourRequest request = null;
                foreach (LabourRequest req in pendingRequests)
                {
                    if (req.RequestId == reqId)
                    {
                        request = req;
                        break;
                    }
                }

                if (request == null)
                {
                    ConsoleHelper.ShowError(50, ty + 3, "Request ID not found or already processed!");
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                ConsoleHelper.ClearInsideBoundary();
                Console.SetCursorPosition(50, 10);                 ConsoleHelper.PrintColoredText("========= REQUEST DETAILS =========", ConsoleColor.Yellow);
                Console.SetCursorPosition(55, 12);                 Console.Write("Request ID: " + request.RequestId);
                Console.SetCursorPosition(55, 13);                 Console.Write("Type: " + request.RequestType);
                Console.SetCursorPosition(55, 14);                 Console.Write("Description: " + request.RequestDescription);
                Console.SetCursorPosition(55, 15);                 Console.Write("Status: " + request.RequestStatus);

                Console.SetCursorPosition(55, 17);                 Console.Write("Action: 1-Approve  2-Reject  3-Cancel");
                Console.SetCursorPosition(55, 18);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                int action = ConsoleHelper.GetSafeInt(1, 3, 69, 18); 
                if (action == 1)
                {
                    requestBL.UpdateRequestStatus(reqId, "Approved");
                    ConsoleHelper.ShowSuccess(55, 20, "Request APPROVED!");                 }
                else if (action == 2)
                {
                    requestBL.UpdateRequestStatus(reqId, "Rejected");
                    Console.SetCursorPosition(55, 20);                     Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Request REJECTED!");
                    Console.ResetColor();
                }
                else
                {
                    Console.SetCursorPosition(55, 20);                     Console.Write("Action cancelled.");
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
    }
}