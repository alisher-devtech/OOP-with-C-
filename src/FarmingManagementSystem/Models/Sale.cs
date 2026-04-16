namespace FarmingManagementSystem.Models
{
    public class Sale
    {
        private string saleDate;
        private int saleAmount;

        public string SaleDate
        {
            get { return saleDate; }
            set { saleDate = value; }
        }

        public int SaleAmount
        {
            get { return saleAmount; }
            set { saleAmount = value; }
        }

        public Sale()
        {
            saleDate = "";
            saleAmount = 0;
        }

        public Sale(string saleDate, int saleAmount)
        {
            this.saleDate = saleDate;
            this.saleAmount = saleAmount;
        }
    }
}