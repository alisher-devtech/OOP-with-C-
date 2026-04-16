using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class CustomerUI
    {
        private CropBL cropBL;
        private OrderBL orderBL;

        public CustomerUI()
        {
            cropBL = new CropBL();
            orderBL = new OrderBL();
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
                    ViewAvailableCrops();
                else if (option == 2)
                    SearchCrop();
                else if (option == 3)
                    PlaceOrder();
                else if (option == 4)
                    ViewOrderSummary();
            }
        }

        private void ShowMenu()
        {
            Console.SetCursorPosition(70, 10);             ConsoleHelper.PrintColoredText("-------Customer Menu-------", ConsoleColor.Yellow);
            Console.SetCursorPosition(70, 11);             Console.Write("1. View Available Crops");
            Console.SetCursorPosition(70, 12);             Console.Write("2. Search Crop");
            Console.SetCursorPosition(70, 13);             Console.Write("3. Place order");
            Console.SetCursorPosition(70, 14);             Console.Write("4. View Order summary");
            Console.SetCursorPosition(70, 15);             Console.Write("5. Back");
            Console.SetCursorPosition(70, 16);             ConsoleHelper.PrintColoredText("---------------------------", ConsoleColor.Yellow);
        }

        private void ViewAvailableCrops()
        {
            CropManagementUI cropUI = new CropManagementUI(cropBL);
            cropUI.ViewCrops(false);
        }

        private void SearchCrop()
        {
            try
            {
                Console.SetCursorPosition(70, 19);                 Console.Write("Enter the crop name: ");
                string name = ConsoleHelper.GetSafeString(91, 19, "Crop name", 2, 50); 
                cropBL.LoadCrops();
                Crop crop = cropBL.SearchCropByName(name);

                if (crop != null)
                {
                    Console.SetCursorPosition(70, 21);                     Console.Write("--- Crop Details ---");
                    Console.SetCursorPosition(70, 22);                     Console.Write("Crop:     " + crop.CropName);
                    Console.SetCursorPosition(70, 23);                     Console.Write("Type:     " + crop.CropType);
                    Console.SetCursorPosition(70, 24);                     Console.Write("Quantity: " + crop.CropQuantity + " kg");

                    string available = crop.CropStatus == "Growing" ? "Out of Stock" : "In Stock";
                    Console.SetCursorPosition(70, 25);                     Console.Write("Status:   " + available);
                    Console.SetCursorPosition(70, 26);                     Console.Write("Price:    Rs. " + crop.CropPrice + "/kg");
                }
                else
                {
                    ConsoleHelper.ShowError(70, 22, "Crop not found in the system!");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 24, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void PlaceOrder()
        {
            try
            {
                orderBL.LoadCrops();
                int choice = 1;

                while (choice != 0)
                {
                    Console.SetCursorPosition(70, 20);                     Console.Write("Enter crop id to order: ");
                    int cropId = ConsoleHelper.GetSafeInt(1, 9999, 94, 20); 
                    Console.SetCursorPosition(70, 21);                     Console.Write("Enter quantity (kg): ");
                    int quantity = ConsoleHelper.GetSafeInt(1, 999999, 91, 21); 
                    Crop crop = orderBL.GetCropById(cropId);

                    if (crop == null)
                    {
                        ConsoleHelper.ShowError(70, 23, "Invalid Crop ID! Crop not found.");                     }
                    else if (crop.CropStatus == "Growing")
                    {
                        ConsoleHelper.ShowError(70, 23, "Sorry! This crop is still growing and not available.");                     }
                    else if (quantity > crop.CropQuantity)
                    {
                        ConsoleHelper.ShowError(70, 23, "Sorry! Only " + crop.CropQuantity + " kg available.");                     }
                    else
                    {
                        if (orderBL.PlaceOrder(cropId, quantity))
                        {
                            int totalPrice = quantity * crop.CropPrice;
                            ConsoleHelper.ShowSuccess(70, 23, "Order Successful! Total: Rs. " + totalPrice);                         }
                        else
                        {
                            ConsoleHelper.ShowError(70, 23, "Order failed! Please try again.");                         }
                    }

                    Console.SetCursorPosition(70, 25);                     Console.Write("Order another? (1 = Yes, 0 = No): ");
                    choice = ConsoleHelper.GetSafeInt(0, 1, 105, 25); 
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 27, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void ViewOrderSummary()
        {
            try
            {
                orderBL.LoadOrders();
                orderBL.LoadCrops();

                List<Order> orders = orderBL.GetAllOrders();

                if (orders.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No orders found!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                int tx = 56, ty = 11;                 Console.SetCursorPosition(tx, 10);                 Console.Write("{0,-10} {1,-15} {2,-10} {3,-10} {4,-14} {5,-12}",
                    "Order ID", "Crop Name", "Quantity", "Price/kg", "Total Price", "Status");

                foreach (Order order in orders)
                {
                    if (ty > 32) break; 
                    Crop crop = orderBL.GetCropById(order.CropId);
                    if (crop != null)
                    {
                        Console.SetCursorPosition(tx, ty);
                        Console.Write("{0,-10} {1,-15} {2,-10} {3,-10} {4,-14} {5,-12}",
                            order.OrderId, crop.CropName, order.OrderQuantity, order.PricePerKg, order.TotalPrice, order.OrderStatus);
                        ty++;
                    }
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
    }
}