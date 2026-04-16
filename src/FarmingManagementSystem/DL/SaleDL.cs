using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class SaleDL
    {
        private List<Sale> sales;

        public SaleDL()
        {
            sales = new List<Sale>();
        }

        public List<Sale> GetAllSales()
        {
            return sales;
        }

        public void LoadSalesFromDatabase()
        {
            try
            {
                sales.Clear();
                string query = "SELECT saledate, saleamount FROM sales";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        Sale sale = new Sale();
                        sale.SaleDate = reader.GetString("saledate");
                        sale.SaleAmount = reader.GetInt32("saleamount");
                        sales.Add(sale);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading sales: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading sales: " + ex.Message);
            }
        }

        public void AddOrUpdateSale(string date, int amount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    throw new Exception("Sale date cannot be empty!");
                }

                if (amount <= 0)
                {
                    throw new Exception("Sale amount must be greater than 0!");
                }

                Sale existingSale = null;
                foreach (Sale s in sales)
                {
                    if (s.SaleDate == date)
                    {
                        existingSale = s;
                        break;
                    }
                }

                if (existingSale != null)
                {
                    existingSale.SaleAmount += amount;

                    string query = "UPDATE sales SET saleamount = @saleamount WHERE saledate = @saledate";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@saleamount", existingSale.SaleAmount);
                        cmd.Parameters.AddWithValue("@saledate", date);
                    });
                }
                else
                {
                    Sale newSale = new Sale(date, amount);

                    string query = "INSERT INTO sales (saledate, saleamount) VALUES (@saledate, @saleamount)";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@saledate", date);
                        cmd.Parameters.AddWithValue("@saleamount", amount);
                    });

                    sales.Add(newSale);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating sale: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating sale: " + ex.Message);
            }
        }

        public Sale FindSaleByDate(string date)
        {
            try
            {
                foreach (Sale sale in sales)
                {
                    if (sale.SaleDate == date)
                    {
                        return sale;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding sale: " + ex.Message);
            }
        }
    }
}