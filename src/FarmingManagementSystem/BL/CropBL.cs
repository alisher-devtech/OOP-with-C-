using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class CropBL
    {
        private CropDL cropDL;

        public CropBL()
        {
            cropDL = new CropDL();
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

        public List<Crop> GetAllCrops()
        {
            try
            {
                return cropDL.GetAllCrops();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get crops: " + ex.Message);
            }
        }

        public bool AddCrop(string name, string type, int price, int quantity, string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception("Crop name cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(type))
                {
                    throw new Exception("Crop type cannot be empty!");
                }

                if (price <= 0)
                {
                    throw new Exception("Price must be greater than 0!");
                }

                if (quantity < 0)
                {
                    throw new Exception("Quantity cannot be negative!");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Crop status cannot be empty!");
                }

                if (type != "Vegetable" && type != "Fruit" && type != "Grain")
                {
                    throw new Exception("Invalid type! Choose Vegetable, Fruit, or Grain.");
                }

                if (status != "Harvested" && status != "Growing")
                {
                    throw new Exception("Invalid status! Choose Harvested or Growing.");
                }

                Crop crop = new Crop(0, name, type, price, quantity, status);
                cropDL.AddCrop(crop);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add crop: " + ex.Message);
            }
        }

        public bool UpdateCropStatus(int cropId, string status)
        {
            try
            {
                if (cropId <= 0)
                {
                    throw new Exception("Invalid crop ID!");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Status cannot be empty!");
                }

                if (status != "Harvested" && status != "Growing")
                {
                    throw new Exception("Invalid status! Choose Harvested or Growing.");
                }

                cropDL.UpdateCrop(cropId, status);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update crop: " + ex.Message);
            }
        }

        public bool DeleteCrop(int cropId)
        {
            try
            {
                if (cropId <= 0)
                {
                    throw new Exception("Invalid crop ID!");
                }

                return cropDL.DeleteCrop(cropId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete crop: " + ex.Message);
            }
        }

        public Crop FindCropById(int cropId)
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
                throw new Exception("Failed to find crop: " + ex.Message);
            }
        }

        public Crop SearchCropByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return null;
                }

                return cropDL.FindCropByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to search crop: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetCropCountByType()
        {
            try
            {
                List<Crop> crops = cropDL.GetAllCrops();
                Dictionary<string, int> counts = new Dictionary<string, int>();
                counts["Vegetable"] = 0;
                counts["Fruit"] = 0;
                counts["Grain"] = 0;
                counts["Total"] = 0;

                foreach (Crop crop in crops)
                {
                    if (crop.CropType == "Vegetable")
                        counts["Vegetable"]++;
                    else if (crop.CropType == "Fruit")
                        counts["Fruit"]++;
                    else if (crop.CropType == "Grain")
                        counts["Grain"]++;

                    counts["Total"]++;
                }

                return counts;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get crop counts: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetCropStatusCount()
        {
            try
            {
                List<Crop> crops = cropDL.GetAllCrops();
                Dictionary<string, int> counts = new Dictionary<string, int>();
                counts["Harvested"] = 0;
                counts["Growing"] = 0;

                foreach (Crop crop in crops)
                {
                    if (crop.CropStatus == "Harvested")
                        counts["Harvested"]++;
                    else if (crop.CropStatus == "Growing")
                        counts["Growing"]++;
                }

                return counts;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get crop status: " + ex.Message);
            }
        }
    }
}