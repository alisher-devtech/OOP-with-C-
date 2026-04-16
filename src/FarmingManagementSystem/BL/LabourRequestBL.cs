using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class LabourRequestBL
    {
        private LabourRequestDL requestDL;

        public LabourRequestBL()
        {
            requestDL = new LabourRequestDL();
        }

        public void LoadRequests()
        {
            try
            {
                requestDL.LoadRequestsFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load requests: " + ex.Message);
            }
        }

        public List<LabourRequest> GetAllRequests()
        {
            try
            {
                return requestDL.GetAllRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get requests: " + ex.Message);
            }
        }

        public bool AddRequest(string requestType, string description)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new Exception("Request description cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(requestType))
                {
                    throw new Exception("Request type cannot be empty!");
                }

                description = description.Replace(',', '-');

                LabourRequest request = new LabourRequest(0, requestType, description, "Pending");
                requestDL.AddRequest(request);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add request: " + ex.Message);
            }
        }

        public bool UpdateRequestStatus(int requestId, string status)
        {
            try
            {
                if (requestId <= 0)
                {
                    throw new Exception("Invalid request ID!");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Status cannot be empty!");
                }

                requestDL.UpdateRequestStatus(requestId, status);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update request: " + ex.Message);
            }
        }

        public List<LabourRequest> GetPendingRequests()
        {
            try
            {
                List<LabourRequest> allRequests = requestDL.GetAllRequests();
                List<LabourRequest> pendingRequests = new List<LabourRequest>();

                foreach (LabourRequest req in allRequests)
                {
                    if (req.RequestStatus == "Pending")
                    {
                        pendingRequests.Add(req);
                    }
                }

                return pendingRequests;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get pending requests: " + ex.Message);
            }
        }
    }
}