namespace FarmingManagementSystem.Models
{
    public class Crop
    {
        private int cropId;
        private string cropName;
        private string cropType;
        private int cropPrice;
        private int cropQuantity;
        private string cropStatus;

        public int CropId
        {
            get { return cropId; }
            set { cropId = value; }
        }

        public string CropName
        {
            get { return cropName; }
            set { cropName = value; }
        }

        public string CropType
        {
            get { return cropType; }
            set { cropType = value; }
        }

        public int CropPrice
        {
            get { return cropPrice; }
            set { cropPrice = value; }
        }

        public int CropQuantity
        {
            get { return cropQuantity; }
            set { cropQuantity = value; }
        }

        public string CropStatus
        {
            get { return cropStatus; }
            set { cropStatus = value; }
        }

        public Crop()
        {
            cropId = 0;
            cropName = "";
            cropType = "";
            cropPrice = 0;
            cropQuantity = 0;
            cropStatus = "";
        }

        public Crop(int cropId, string cropName, string cropType, int cropPrice, int cropQuantity, string cropStatus)
        {
            this.cropId = cropId;
            this.cropName = cropName;
            this.cropType = cropType;
            this.cropPrice = cropPrice;
            this.cropQuantity = cropQuantity;
            this.cropStatus = cropStatus;
        }
    }
}