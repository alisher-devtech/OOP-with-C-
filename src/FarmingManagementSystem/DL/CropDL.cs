using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class CropDL
    {
        private List<Crop> crops;

        public CropDL()
        {
            crops = new List<Crop>();
        }

        public List<Crop> GetAllCrops()
        {
            return crops;
        }

        public void LoadCropsFromDatabase()
        {
            try
            {
                crops.Clear();
                string query = "SELECT cropid, cropname, croptype, cropprice, cropquantity, cropstatus FROM crops";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        Crop crop = new Crop();
                        crop.CropId = reader.GetInt32("cropid");
                        crop.CropName = reader.GetString("cropname");
                        crop.CropType = reader.GetString("croptype");
                        crop.CropPrice = reader.GetInt32("cropprice");
                        crop.CropQuantity = reader.GetInt32("cropquantity");
                        crop.CropStatus = reader.GetString("cropstatus");
                        crops.Add(crop);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading crops: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading crops: " + ex.Message);
            }
        }

        public void AddCrop(Crop crop)
        {
            try
            {
                if (crop == null)
                {
                    throw new Exception("Crop object cannot be null!");
                }

                crop.CropId = crops.Count + 1;

                string query = "INSERT INTO crops (cropid, cropname, croptype, cropprice, cropquantity, cropstatus) " +
                              "VALUES (@cropid, @cropname, @croptype, @cropprice, @cropquantity, @cropstatus)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@cropid", crop.CropId);
                    cmd.Parameters.AddWithValue("@cropname", crop.CropName);
                    cmd.Parameters.AddWithValue("@croptype", crop.CropType);
                    cmd.Parameters.AddWithValue("@cropprice", crop.CropPrice);
                    cmd.Parameters.AddWithValue("@cropquantity", crop.CropQuantity);
                    cmd.Parameters.AddWithValue("@cropstatus", crop.CropStatus);
                });

                crops.Add(crop);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding crop: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding crop: " + ex.Message);
            }
        }

        public void UpdateCrop(int cropId, string cropStatus)
        {
            try
            {
                Crop crop = null;
                foreach (Crop c in crops)
                {
                    if (c.CropId == cropId)
                    {
                        crop = c;
                        break;
                    }
                }

                if (crop != null)
                {
                    crop.CropStatus = cropStatus;

                    string query = "UPDATE crops SET cropstatus = @cropstatus WHERE cropid = @cropid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@cropstatus", cropStatus);
                        cmd.Parameters.AddWithValue("@cropid", cropId);
                    });
                }
                else
                {
                    throw new Exception("Crop not found with ID: " + cropId);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating crop: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating crop: " + ex.Message);
            }
        }

        public void UpdateCropQuantity(int cropId, int newQuantity)
        {
            try
            {
                Crop crop = null;
                foreach (Crop c in crops)
                {
                    if (c.CropId == cropId)
                    {
                        crop = c;
                        break;
                    }
                }

                if (crop != null)
                {
                    crop.CropQuantity = newQuantity;

                    string query = "UPDATE crops SET cropquantity = @cropquantity WHERE cropid = @cropid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@cropquantity", newQuantity);
                        cmd.Parameters.AddWithValue("@cropid", cropId);
                    });
                }
                else
                {
                    throw new Exception("Crop not found with ID: " + cropId);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating crop quantity: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating crop quantity: " + ex.Message);
            }
        }

        public bool DeleteCrop(int cropId)
        {
            try
            {
                Crop crop = null;
                foreach (Crop c in crops)
                {
                    if (c.CropId == cropId)
                    {
                        crop = c;
                        break;
                    }
                }

                if (crop != null)
                {
                    string query = "DELETE FROM crops WHERE cropid = @cropid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@cropid", cropId);
                    });

                    crops.Remove(crop);
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while deleting crop: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting crop: " + ex.Message);
            }
        }

        public Crop FindCropById(int cropId)
        {
            try
            {
                foreach (Crop crop in crops)
                {
                    if (crop.CropId == cropId)
                    {
                        return crop;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding crop: " + ex.Message);
            }
        }

        public Crop FindCropByName(string name)
        {
            try
            {
                foreach (Crop crop in crops)
                {
                    if (crop.CropName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return crop;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding crop by name: " + ex.Message);
            }
        }
    }
}