using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class OrderDL
    {
        private List<Order> orders;

        public OrderDL()
        {
            orders = new List<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return orders;
        }

        public void LoadOrdersFromDatabase()
        {
            try
            {
                orders.Clear();
                string query = "SELECT orderid, cropid, orderquantity, priceperkg, totalprice, orderstatus FROM orders";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.OrderId = reader.GetInt32("orderid");
                        order.CropId = reader.GetInt32("cropid");
                        order.OrderQuantity = reader.GetInt32("orderquantity");
                        order.PricePerKg = reader.GetInt32("priceperkg");
                        order.TotalPrice = reader.GetInt32("totalprice");
                        order.OrderStatus = reader.GetString("orderstatus");
                        orders.Add(order);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading orders: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading orders: " + ex.Message);
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    throw new Exception("Order object cannot be null!");
                }

                order.OrderId = orders.Count + 1;

                string query = "INSERT INTO orders (orderid, cropid, orderquantity, priceperkg, totalprice, orderstatus) " +
                              "VALUES (@orderid, @cropid, @orderquantity, @priceperkg, @totalprice, @orderstatus)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@orderid", order.OrderId);
                    cmd.Parameters.AddWithValue("@cropid", order.CropId);
                    cmd.Parameters.AddWithValue("@orderquantity", order.OrderQuantity);
                    cmd.Parameters.AddWithValue("@priceperkg", order.PricePerKg);
                    cmd.Parameters.AddWithValue("@totalprice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@orderstatus", order.OrderStatus);
                });

                orders.Add(order);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding order: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order: " + ex.Message);
            }
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                Order order = null;
                foreach (Order o in orders)
                {
                    if (o.OrderId == orderId)
                    {
                        order = o;
                        break;
                    }
                }

                if (order != null)
                {
                    order.OrderStatus = status;

                    string query = "UPDATE orders SET orderstatus = @orderstatus WHERE orderid = @orderid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@orderstatus", status);
                        cmd.Parameters.AddWithValue("@orderid", orderId);
                    });
                }
                else
                {
                    throw new Exception("Order not found with ID: " + orderId);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating order: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order: " + ex.Message);
            }
        }

        public Order FindOrderById(int orderId)
        {
            try
            {
                foreach (Order order in orders)
                {
                    if (order.OrderId == orderId)
                    {
                        return order;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding order: " + ex.Message);
            }
        }
    }
}