using System;
using System.Collections.Generic;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Models;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class EmployeeManagementUI
    {
        private EmployeeBL employeeBL;

        public EmployeeManagementUI(EmployeeBL empBL)
        {
            employeeBL = empBL;
        }

        public void Show()
        {
            ConsoleHelper.ClearInsideBoundary();
            int choice = 0;

            while (choice != 5)
            {
                Console.SetCursorPosition(62, 10);                 ConsoleHelper.PrintColoredText("===== EMPLOYEE MANAGEMENT =====", ConsoleColor.Yellow);
                Console.SetCursorPosition(70, 11);                 Console.Write("1. Add Employee");
                Console.SetCursorPosition(70, 12);                 Console.Write("2. View Employee");
                Console.SetCursorPosition(70, 13);                 Console.Write("3. Update Employee");
                Console.SetCursorPosition(70, 14);                 Console.Write("4. Delete Employee");
                Console.SetCursorPosition(70, 15);                 Console.Write("5. Back");

                Console.SetCursorPosition(70, 17);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);
                choice = ConsoleHelper.GetSafeInt(1, 5, 83, 17); 
                if (choice == 1)
                    AddEmployee();
                else if (choice == 2)
                    ViewEmployees();
                else if (choice == 3)
                    UpdateEmployee();
                else if (choice == 4)
                    DeleteEmployee();
                else if (choice == 5)
                {
                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    break;
                }
            }
        }

        private void AddEmployee()
        {
            ConsoleHelper.ClearInsideBoundary();
            bool continueAdding = true;

            while (continueAdding)
            {
                try
                {
                    Console.SetCursorPosition(70, 10);                     Console.Write("Enter Employee's name: ");
                    string name = ConsoleHelper.GetSafeString(93, 10, "Employee name", 2, 50); 
                    Console.SetCursorPosition(70, 11);                     Console.Write("Enter Employee's Role: ");
                    string[] validRoles = { "Labour", "Manager", "Supervisor" };
                    string role = ConsoleHelper.GetValidRole(93, 11, validRoles); 
                    Console.SetCursorPosition(70, 12);                     Console.Write("Enter Employee's salary: ");
                    double salary = ConsoleHelper.GetSafeDouble(96, 12, "Salary"); 
                    Console.SetCursorPosition(70, 13);                     Console.Write("Enter the date of joining: ");
                    string joinDate = ConsoleHelper.GetSafeString(98, 13, "Join date", 5, 20); 
                    if (employeeBL.AddEmployee(name, role, salary, joinDate))
                    {
                        ConsoleHelper.ShowSuccess(70, 15, "Employee added successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 15, "Failed to add employee!");                     }

                    Console.SetCursorPosition(70, 17);                     Console.Write("Add another employee? (1 = Yes, 0 = No): ");
                    int response = ConsoleHelper.GetSafeInt(0, 1, 111, 17);                     continueAdding = (response == 1);

                    ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ShowError(70, 19, "Error: " + ex.Message);                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
        }

        private void ViewEmployees()
        {
            try
            {
                employeeBL.LoadEmployees();
                List<Employee> employees = employeeBL.GetAllEmployees();

                if (employees.Count == 0)
                {
                    ConsoleHelper.ShowError(70, 15, "No employees found!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                int tx = 54, ty = 11;                 Console.SetCursorPosition(54, 10);                 Console.Write("{0,-10} {1,-10} {2,-12} {3,-12} {4,-12}", "ID", "Name", "Role", "Salary", "Join Date");

                foreach (Employee emp in employees)
                {
                    if (ty > 32) break; 
                    Console.SetCursorPosition(tx, ty);
                    Console.Write("{0,-10} {1,-10} {2,-12} {3,-12} {4,-12}", emp.Id, emp.Name, emp.Role, emp.Salary, emp.JoinDate);
                    ty++;
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 15, "Error loading employees: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void UpdateEmployee()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Employee's id no for update: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 99, 14); 
                Console.SetCursorPosition(70, 15);                 Console.Write("Role: ");
                string[] validRoles = { "Labour", "Manager", "Supervisor" };
                string role = ConsoleHelper.GetValidRole(76, 15, validRoles); 
                Console.SetCursorPosition(70, 16);                 Console.Write("Salary: ");
                double salary = ConsoleHelper.GetSafeDouble(78, 16, "Salary"); 
                if (employeeBL.UpdateEmployee(id, role, salary))
                {
                    ConsoleHelper.ShowSuccess(70, 18, "Updated Successfully!");                 }
                else
                {
                    ConsoleHelper.ShowError(70, 18, "Update failed! ID may not exist.");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void DeleteEmployee()
        {
            try
            {
                Console.SetCursorPosition(70, 14);                 Console.Write("Employee ID to delete: ");
                int id = ConsoleHelper.GetSafeInt(1, 9999, 93, 14); 
                Console.SetCursorPosition(70, 16);                 Console.Write("Are you sure? (1=Yes, 0=No): ");
                int confirm = ConsoleHelper.GetSafeInt(0, 1, 99, 16); 
                if (confirm == 1)
                {
                    if (employeeBL.DeleteEmployee(id))
                    {
                        ConsoleHelper.ShowSuccess(70, 18, "Employee deleted Successfully!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 18, "Data not found!");                     }
                }
                else
                {
                    ConsoleHelper.ShowSuccess(70, 18, "Deletion cancelled.");                 }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }
    }
}