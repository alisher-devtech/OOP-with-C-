namespace FarmingManagementSystem.Models
{
    public class Order
    {
        private int orderId;
        private int cropId;
        private int orderQuantity;
        private int pricePerKg;
        private int totalPrice;
        private string orderStatus;

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public int CropId
        {
            get { return cropId; }
            set { cropId = value; }
        }

        public int OrderQuantity
        {
            get { return orderQuantity; }
            set { orderQuantity = value; }
        }

        public int PricePerKg
        {
            get { return pricePerKg; }
            set { pricePerKg = value; }
        }

        public int TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }

        public Order()
        {
            orderId = 0;
            cropId = 0;
            orderQuantity = 0;
            pricePerKg = 0;
            totalPrice = 0;
            orderStatus = "";
        }

        public Order(int orderId, int cropId, int orderQuantity, int pricePerKg, int totalPrice, string orderStatus)
        {
            this.orderId = orderId;
            this.cropId = cropId;
            this.orderQuantity = orderQuantity;
            this.pricePerKg = pricePerKg;
            this.totalPrice = totalPrice;
            this.orderStatus = orderStatus;
        }
    }
}