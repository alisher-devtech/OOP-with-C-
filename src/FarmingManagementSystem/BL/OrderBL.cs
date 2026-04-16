using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class OrderBL
    {
        private OrderDL orderDL;
        private CropDL cropDL;
        private SaleDL saleDL;

        public OrderBL()
        {
            orderDL = new OrderDL();
            cropDL = new CropDL();
            saleDL = new SaleDL();
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

        public void LoadCrops()
        {
            try
            {
                cropDL.LoadCropsFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load crops: " + ex.Message);
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                return orderDL.GetAllOrders();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get orders: " + ex.Message);
            }
        }

        public bool PlaceOrder(int cropId, int quantity)
        {
            try
            {
                if (cropId <= 0)
                {
                    throw new Exception("Invalid crop ID!");
                }

                if (quantity <= 0)
                {
                    throw new Exception("Quantity must be greater than 0!");
                }

                Crop crop = cropDL.FindCropById(cropId);

                if (crop == null)
                {
                    throw new Exception("Crop not found!");
                }

                if (crop.CropQuantity < quantity)
                {
                    throw new Exception("Insufficient quantity available!");
                }

                int totalPrice = quantity * crop.CropPrice;
                Order order = new Order(0, cropId, quantity, crop.CropPrice, totalPrice, "Pending");
                orderDL.AddOrder(order);

                int newQuantity = crop.CropQuantity - quantity;
                cropDL.UpdateCropQuantity(cropId, newQuantity);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to place order: " + ex.Message);
            }
        }

        public bool UpdateOrderStatus(int orderId, string newStatus)
        {
            try
            {
                if (orderId <= 0)
                {
                    throw new Exception("Invalid order ID!");
                }

                if (string.IsNullOrWhiteSpace(newStatus))
                {
                    throw new Exception("Status cannot be empty!");
                }

                Order order = orderDL.FindOrderById(orderId);
                if (order == null)
                {
                    throw new Exception("Order not found!");
                }

                string oldStatus = order.OrderStatus;
                orderDL.UpdateOrderStatus(orderId, newStatus);

                if ((oldStatus != "Completed" && oldStatus != "Delivered") &&
                    (newStatus == "Completed" || newStatus == "Delivered"))
                {
                    saleDL.LoadSalesFromDatabase();
                    string today = DateTime.Now.ToString("d/M/yyyy");
                    saleDL.AddOrUpdateSale(today, order.TotalPrice);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update order: " + ex.Message);
            }
        }

        public Order FindOrderById(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return null;
                }

                return orderDL.FindOrderById(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to find order: " + ex.Message);
            }
        }

        public Crop GetCropById(int cropId)
        {
            try
            {
                if (cropId <= 0)
                {
                    return null;
                }

                return cropDL.FindCropById(cropId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get crop: " + ex.Message);
            }
        }
    }
}