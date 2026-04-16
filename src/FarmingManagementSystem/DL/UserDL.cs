using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class UserDL
    {
        private List<User> users;

        public UserDL()
        {
            users = new List<User>();
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public void LoadUsersFromDatabase()
        {
            try
            {
                users.Clear();
                string query = "SELECT username, password, role FROM users";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Username = reader.GetString("username");
                        user.Password = reader.GetString("password");
                        user.Role = reader.GetString("role");
                        users.Add(user);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading users: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading users: " + ex.Message);
            }
        }

        public void SaveUserToDatabase(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("User object cannot be null!");
                }

                string query = "INSERT INTO users (username, password, role) VALUES (@username, @password, @role)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                });

                users.Add(user);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while saving user: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving user: " + ex.Message);
            }
        }

        public User FindUser(string username, string password)
        {
            try
            {
                string normalizedUsername = username.Trim();
                string normalizedPassword = password.Trim();

                foreach (User user in users)
                {
                    if (string.Equals(user.Username, normalizedUsername, StringComparison.OrdinalIgnoreCase) &&
                        user.Password == normalizedPassword)
                    {
                        return user;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error finding user: " + ex.Message);
            }
        }

        public bool UserExists(string username)
        {
            try
            {
                string normalizedUsername = username.Trim();

                foreach (User user in users)
                {
                    if (string.Equals(user.Username, normalizedUsername, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking user existence: " + ex.Message);
            }
        }
    }
}