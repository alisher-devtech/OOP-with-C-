using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class CropManagementUI
    {
        private CropBL cropBL;

        public CropManagementUI(CropBL cBL)
        {
            cropBL = cBL;
        }

        public void Show()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int option = 0;

            while (option != 5)
            {
                Console.SetCursorPosition(62, 10);                 ConsoleHelper.PrintColoredText("=======Crop Management=======", ConsoleColor.Yellow);
                Console.SetCursorPosition(70, 11);                 Console.Write("1. Add Crop");
                Console.SetCursorPosition(70, 12);                 Console.Write("2. View Crop");
                Console.SetCursorPosition(70, 13);                 Console.Write("3. Update Crop");
                Console.SetCursorPosition(70, 14);                 Console.Write("4. Delete Crop");
                Console.SetCursorPosition(70, 15);                 Console.Write("5. Back");

                Console.SetCursorPosition(70, 16);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                option = ConsoleHelper.GetSafeInt(1, 5, 83, 16); 
                if (option == 1)
                    AddCrop();
                else if (option == 2)
                    ViewCrops(true);
                else if (option == 3)
                    UpdateCrop();
                else if (option == 4)
                    DeleteCrop();
                else if (option == 5)
                {
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    break;
                }
            }
        }

        private void AddCrop()
        {
            ConsoleHelper.Pause();
            ConsoleHelper.ClearInsideBoundary();
            int choice = 1;

            while (choice != 0)
            {
                try
                {
                    Console.SetCursorPosition(70, 10);                     Console.Write("Crop name: ");
                    string name = ConsoleHelper.GetSafeString(81, 10, "Crop name", 2, 50); 
                    Console.SetCursorPosition(70, 11);                     Console.Write("Crop type: ");
                    string[] validTypes = { "Vegetable", "Fruit", "Grain" };
                    string type = ConsoleHelper.GetValidRole(81, 11, validTypes); 
                    Console.SetCursorPosition(70, 12);                     Console.Write("Crop quantity (kg): ");
                    int quantity = ConsoleHelper.GetSafeInt(0, 999999, 90, 12); 
                    Console.SetCursorPosition(70, 13);                     Console.Write("Crop price per kg: ");
                    int price = ConsoleHelper.GetSafeInt(1, 999999, 89, 13); 
                    Console.SetCursorPosition(70, 14);                     Console.Write("Crop status: ");
                    string[] validStatus = { "Harvested", "Growing" };
                    string status = ConsoleHelper.GetValidRole(83, 14, validStatus); 
                    if (cropBL.AddCrop(name, type, price, quantity, status))
                    {
                        ConsoleHelper.ShowSuccess(70, 16, "Crop added successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 16, "Failed to add crop!");                     }

                    Console.SetCursorPosition(70, 18);                     Console.Write("Add more crops? (1 = Yes, 0 = No): ");
                    choice = ConsoleHelper.GetSafeInt(0, 1, 105, 18); 
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
        }

        public void ViewCrops(bool showStatus)
        {
            try
            {
                cropBL.LoadCrops();
                List<Crop> crops = cropBL.GetAllCrops();

                if (crops.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No crops found in the system!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                int tx = 50, ty = 11;                 Console.SetCursorPosition(tx, 10); 
                if (showStatus)
                    Console.Write("{0,-12} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                        "Crop Id", "Crop Name", "Crop Type", "Crop Quantity", "Crop per Kg", "Crop Status");
                else
                    Console.Write("{0,-12} {1,-15} {2,-15} {3,-15} {4,-15}",
                        "Crop Id", "Crop Name", "Crop Type", "Crop Quantity", "Crop per Kg");

                foreach (Crop crop in crops)
                {
                    if (ty > 32) break; 
                    Console.SetCursorPosition(tx, ty);
                    if (showStatus)
                        Console.Write("{0,-12} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}",
                            crop.CropId, crop.CropName, crop.CropType, crop.CropQuantity, crop.CropPrice, crop.CropStatus);
                    else
                        Console.Write("{0,-12} {1,-15} {2,-15} {3,-15} {4,-15}",
                            crop.CropId, crop.CropName, crop.CropType, crop.CropQuantity, crop.CropPrice);
                    ty++;
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 15, "Error loading crops: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void UpdateCrop()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter crop id to update: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 95, 14); 
                Console.SetCursorPosition(70, 15);                 Console.Write("Crop Status: ");
                string[] validStatus = { "Harvested", "Growing" };
                string status = ConsoleHelper.GetValidRole(83, 15, validStatus); 
                cropBL.UpdateCropStatus(id, status);
                ConsoleHelper.ShowSuccess(70, 17, "Crop update successful!"); 
                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 19, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void DeleteCrop()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter Crop id to delete: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 95, 14); 
                Console.SetCursorPosition(70, 15);                 Console.Write("Are you sure? (1=Yes, 0=No): ");
                int confirm = ConsoleHelper.GetSafeInt(0, 1, 99, 15); 
                if (confirm == 1)
                {
                    if (cropBL.DeleteCrop(id))
                    {
                        ConsoleHelper.ShowSuccess(70, 17, "Deleted Successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 17, "Deletion not Successful! ID not found.");                     }
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