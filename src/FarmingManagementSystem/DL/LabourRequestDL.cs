using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class LabourRequestDL
    {
        private List<LabourRequest> labourRequests;

        public LabourRequestDL()
        {
            labourRequests = new List<LabourRequest>();
        }

        public List<LabourRequest> GetAllRequests()
        {
            return labourRequests;
        }

        public void LoadRequestsFromDatabase()
        {
            try
            {
                labourRequests.Clear();
                string query = "SELECT requestid, requesttype, requestdescription, requeststatus FROM labourrequests";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        LabourRequest request = new LabourRequest();
                        request.RequestId = reader.GetInt32("requestid");
                        request.RequestType = reader.GetString("requesttype");
                        request.RequestDescription = reader.GetString("requestdescription");
                        request.RequestStatus = reader.GetString("requeststatus");
                        labourRequests.Add(request);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading labour requests: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading labour requests: " + ex.Message);
            }
        }

        public void AddRequest(LabourRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Labour request object cannot be null!");
                }

                request.RequestId = labourRequests.Count + 1;

                string query = "INSERT INTO labourrequests (requestid, requesttype, requestdescription, requeststatus) " +
                              "VALUES (@requestid, @requesttype, @requestdescription, @requeststatus)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@requestid", request.RequestId);
                    cmd.Parameters.AddWithValue("@requesttype", request.RequestType);
                    cmd.Parameters.AddWithValue("@requestdescription", request.RequestDescription);
                    cmd.Parameters.AddWithValue("@requeststatus", request.RequestStatus);
                });

                labourRequests.Add(request);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding labour request: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding labour request: " + ex.Message);
            }
        }

        public void UpdateRequestStatus(int requestId, string status)
        {
            try
            {
                LabourRequest request = null;
                foreach (LabourRequest r in labourRequests)
                {
                    if (r.RequestId == requestId)
                    {
                        request = r;
                        break;
                    }
                }

                if (request != null)
                {
                    request.RequestStatus = status;

                    string query = "UPDATE labourrequests SET requeststatus = @requeststatus WHERE requestid = @requestid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@requeststatus", status);
                        cmd.Parameters.AddWithValue("@requestid", requestId);
                    });
                }
                else
                {
                    throw new Exception("Labour request not found with ID: " + requestId);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating labour request: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating labour request: " + ex.Message);
            }
        }
    }
}