using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class SignUpRequestBL
    {
        private SignUpRequestDL requestDL;
        private UserDL userDL;

        public SignUpRequestBL()
        {
            requestDL = new SignUpRequestDL();
            userDL = new UserDL();
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

        public void LoadUsers()
        {
            try
            {
                userDL.LoadUsersFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load users: " + ex.Message);
            }
        }

        public List<User> GetAllRequests()
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

        public bool AddRequest(string username, string password, string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
                {
                    throw new Exception("All fields are required!");
                }

                string normalizedUsername = username.Trim();
                string normalizedPassword = password.Trim();
                string normalizedRole = role.Trim();

                if (RequestExists(normalizedUsername))
                {
                    throw new Exception("A signup request for this username is already pending.");
                }

                User request = new User(normalizedUsername, normalizedPassword, normalizedRole);
                requestDL.AddRequest(request);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add request: " + ex.Message);
            }
        }

        public bool RequestExists(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return false;
                }

                string normalizedUsername = username.Trim();
                List<User> requests = requestDL.GetAllRequests();

                foreach (User request in requests)
                {
                    if (string.Equals(request.Username, normalizedUsername, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to check request existence: " + ex.Message);
            }
        }

        public bool ApproveRequest(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception("Username cannot be empty!");
                }

                List<User> requests = requestDL.GetAllRequests();
                User request = null;

                foreach (User r in requests)
                {
                    if (r.Username == username)
                    {
                        request = r;
                        break;
                    }
                }

                if (request != null)
                {
                    userDL.SaveUserToDatabase(request);
                    requestDL.RemoveRequest(username);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to approve request: " + ex.Message);
            }
        }

        public bool RejectRequest(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception("Username cannot be empty!");
                }

                return requestDL.RemoveRequest(username);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to reject request: " + ex.Message);
            }
        }
    }
}