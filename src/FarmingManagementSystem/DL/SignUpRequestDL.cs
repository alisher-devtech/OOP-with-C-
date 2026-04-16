using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class SignUpRequestDL
    {
        private List<User> requests;

        public SignUpRequestDL()
        {
            requests = new List<User>();
        }

        public List<User> GetAllRequests()
        {
            return requests;
        }

        public void LoadRequestsFromDatabase()
        {
            try
            {
                requests.Clear();
                string query = "SELECT username, password, role FROM signuprequests";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        User request = new User();
                        request.Username = reader.GetString("username");
                        request.Password = reader.GetString("password");
                        request.Role = reader.GetString("role");
                        requests.Add(request);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading signup requests: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading signup requests: " + ex.Message);
            }
        }

        public void AddRequest(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("User request object cannot be null!");
                }

                string query = "INSERT INTO signuprequests (username, password, role) VALUES (@username, @password, @role)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                });

                requests.Add(user);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding signup request: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding signup request: " + ex.Message);
            }
        }

        public bool RemoveRequest(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new Exception("Username cannot be empty!");
                }

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
                    string query = "DELETE FROM signuprequests WHERE username = @username";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                    });

                    requests.Remove(request);
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while removing signup request: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing signup request: " + ex.Message);
            }
        }
    }
}