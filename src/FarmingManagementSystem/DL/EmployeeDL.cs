using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class EmployeeDL
    {
        private List<Employee> employees;

        public EmployeeDL()
        {
            employees = new List<Employee>();
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public void LoadEmployeesFromDatabase()
        {
            try
            {
                employees.Clear();
                string query = "SELECT id, name, role, salary, joindate FROM employees";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.Id = reader.GetInt32("id");
                        emp.Name = reader.GetString("name");
                        emp.Role = reader.GetString("role");
                        emp.Salary = reader.GetDouble("salary");
                        emp.JoinDate = reader.GetString("joindate");
                        employees.Add(emp);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading employees: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading employees: " + ex.Message);
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new Exception("Employee object cannot be null!");
                }

                employee.Id = employees.Count + 1;

                string query = "INSERT INTO employees (id, name, role, salary, joindate) VALUES (@id, @name, @role, @salary, @joindate)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@id", employee.Id);
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@role", employee.Role);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@joindate", employee.JoinDate);
                });

                employees.Add(employee);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding employee: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding employee: " + ex.Message);
            }
        }

        public void UpdateEmployee(int id, string role, double salary)
        {
            try
            {
                Employee emp = null;
                foreach (Employee e in employees)
                {
                    if (e.Id == id)
                    {
                        emp = e;
                        break;
                    }
                }

                if (emp != null)
                {
                    emp.Role = role;
                    emp.Salary = salary;

                    string query = "UPDATE employees SET role = @role, salary = @salary WHERE id = @id";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@salary", salary);
                        cmd.Parameters.AddWithValue("@id", id);
                    });
                }
                else
                {
                    throw new Exception("Employee not found with ID: " + id);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating employee: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating employee: " + ex.Message);
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                Employee emp = null;
                foreach (Employee e in employees)
                {
                    if (e.Id == id)
                    {
                        emp = e;
                        break;
                    }
                }

                if (emp != null)
                {
                    string query = "DELETE FROM employees WHERE id = @id";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    });

                    employees.Remove(emp);
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while deleting employee: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting employee: " + ex.Message);
            }
        }
    }
}