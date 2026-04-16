using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class SaleBL
    {
        private SaleDL saleDL;
        private OrderDL orderDL;

        public SaleBL()
        {
            saleDL = new SaleDL();
            orderDL = new OrderDL();
        }

        public void LoadSales()
        {
            try
            {
                saleDL.LoadSalesFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load sales: " + ex.Message);
            }
        }

        public void LoadOrders()
        {
            try
            {
                orderDL.LoadOrdersFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load orders: " + ex.Message);
            }
        }

        public List<Sale> GetAllSales()
        {
            try
            {
                return saleDL.GetAllSales();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get sales: " + ex.Message);
            }
        }

        public Sale GetTodaysSales()
        {
            try
            {
                string today = DateTime.Now.ToString("d/M/yyyy");
                return saleDL.FindSaleByDate(today);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get today's sales: " + ex.Message);
            }
        }

        public int GetTodayCompletedOrdersCount()
        {
            try
            {
                List<Order> orders = orderDL.GetAllOrders();
                int count = 0;
                foreach (Order order in orders)
                {
                    if (order.OrderStatus == "Completed" || order.OrderStatus == "Delivered")
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get order count: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetLastMonthSales()
        {
            try
            {
                DateTime now = DateTime.Now;
                int lastMonth = now.Month - 1;
                int lastYear = now.Year;

                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    lastYear--;
                }

                List<Sale> allSales = saleDL.GetAllSales();
                Dictionary<string, int> monthlySales = new Dictionary<string, int>();

                foreach (Sale sale in allSales)
                {
                    try
                    {
                        string[] parts = sale.SaleDate.Split('/');
                        if (parts.Length == 3)
                        {
                            int month = int.Parse(parts[1]);
                            int year = int.Parse(parts[2]);

                            if (month == lastMonth && year == lastYear)
                            {
                                monthlySales[sale.SaleDate] = sale.SaleAmount;
                            }
                        }
                    }
                    catch { }
                }

                return monthlySales;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get monthly sales: " + ex.Message);
            }
        }

        public int GetMonthlyTotal()
        {
            try
            {
                Dictionary<string, int> monthlySales = GetLastMonthSales();
                int total = 0;
                foreach (int amount in monthlySales.Values)
                {
                    total += amount;
                }
                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to calculate monthly total: " + ex.Message);
            }
        }
    }
}