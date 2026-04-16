using System;
using System.Collections.Generic;

namespace FarmingManagementSystem.BL
{
    public class ReportBL
    {
        private EmployeeBL employeeBL;
        private CropBL cropBL;

        public ReportBL()
        {
            employeeBL = new EmployeeBL();
            cropBL = new CropBL();
        }

        public void LoadData()
        {
            try
            {
                employeeBL.LoadEmployees();
                cropBL.LoadCrops();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load report data: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetEmployeeReport()
        {
            try
            {
                return employeeBL.GetEmployeeCountByRole();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate employee report: " + ex.Message);
            }
        }

        public Dictionary<string, double> GetSalaryReport()
        {
            try
            {
                return employeeBL.GetSalarySummaryByRole();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate salary report: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetCropTypeReport()
        {
            try
            {
                return cropBL.GetCropCountByType();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate crop type report: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetCropStatusReport()
        {
            try
            {
                return cropBL.GetCropStatusCount();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate crop status report: " + ex.Message);
            }
        }

        public int GetTotalEmployees()
        {
            try
            {
                return employeeBL.GetTotalEmployeeCount();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get total employees: " + ex.Message);
            }
        }
    }
}