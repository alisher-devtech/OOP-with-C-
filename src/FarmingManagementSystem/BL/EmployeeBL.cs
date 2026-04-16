using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class EmployeeBL
    {
        private EmployeeDL employeeDL;

        public EmployeeBL()
        {
            employeeDL = new EmployeeDL();
        }

        public void LoadEmployees()
        {
            try
            {
                employeeDL.LoadEmployeesFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load employees: " + ex.Message);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return employeeDL.GetAllEmployees();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get employees: " + ex.Message);
            }
        }

        public bool AddEmployee(string name, string role, double salary, string joinDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Employee name cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(role))
                {
                    throw new Exception("Employee role cannot be empty!");
                }

                if (salary <= 0)
                {
                    throw new Exception("Salary must be greater than 0!");
                }

                if (string.IsNullOrWhiteSpace(joinDate))
                {
                    throw new Exception("Join date cannot be empty!");
                }

                if (role != "Labour" && role != "Manager" && role != "Supervisor")
                {
                    throw new Exception("Invalid role! Choose Labour, Manager, or Supervisor.");
                }

                Employee emp = new Employee(0, name, role, salary, joinDate);
                employeeDL.AddEmployee(emp);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add employee: " + ex.Message);
            }
        }

        public bool UpdateEmployee(int id, string role, double salary)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Invalid employee ID!");
                }

                if (salary <= 0)
                {
                    throw new Exception("Salary must be greater than 0!");
                }

                if (string.IsNullOrWhiteSpace(role))
                {
                    throw new Exception("Role cannot be empty!");
                }

                if (role != "Labour" && role != "Manager" && role != "Supervisor")
                {
                    throw new Exception("Invalid role! Choose Labour, Manager, or Supervisor.");
                }

                employeeDL.UpdateEmployee(id, role, salary);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update employee: " + ex.Message);
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Invalid employee ID!");
                }

                return employeeDL.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete employee: " + ex.Message);
            }
        }

        public int GetTotalEmployeeCount()
        {
            try
            {
                return employeeDL.GetAllEmployees().Count;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get employee count: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetEmployeeCountByRole()
        {
            try
            {
                List<Employee> employees = employeeDL.GetAllEmployees();
                Dictionary<string, int> counts = new Dictionary<string, int>();
                counts["Labour"] = 0;
                counts["Supervisor"] = 0;
                counts["Manager"] = 0;

                foreach (Employee emp in employees)
                {
                    if (emp.Role == "Labour")
                        counts["Labour"]++;
                    else if (emp.Role == "Supervisor")
                        counts["Supervisor"]++;
                    else if (emp.Role == "Manager")
                        counts["Manager"]++;
                }

                return counts;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get employee counts: " + ex.Message);
            }
        }

        public Dictionary<string, double> GetSalarySummaryByRole()
        {
            try
            {
                List<Employee> employees = employeeDL.GetAllEmployees();
                Dictionary<string, double> summary = new Dictionary<string, double>();
                summary["Labour"] = 0;
                summary["Supervisor"] = 0;
                summary["Manager"] = 0;
                summary["Total"] = 0;

                foreach (Employee emp in employees)
                {
                    if (emp.Role == "Labour")
                    {
                        summary["Labour"] += emp.Salary;
                        summary["Total"] += emp.Salary;
                    }
                    else if (emp.Role == "Supervisor")
                    {
                        summary["Supervisor"] += emp.Salary;
                        summary["Total"] += emp.Salary;
                    }
                    else if (emp.Role == "Manager")
                    {
                        summary["Manager"] += emp.Salary;
                        summary["Total"] += emp.Salary;
                    }
                }

                return summary;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get salary summary: " + ex.Message);
            }
        }
    }
}