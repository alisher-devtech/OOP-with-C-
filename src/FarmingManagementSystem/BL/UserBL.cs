using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class UserBL
    {
        private UserDL userDL;

        public UserBL()
        {
            userDL = new UserDL();
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

        public string SignIn(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return "Undefined";
                }

                userDL.LoadUsersFromDatabase();

                User user = userDL.FindUser(username, password);
                if (user != null)
                {
                    return user.Role;
                }
                return "Undefined";
            }
            catch (Exception ex)
            {
                throw new Exception("Sign in failed: " + ex.Message);
            }
        }

        public bool SignUp(string username, string password, string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
                {
                    return false;
                }

                string normalizedUsername = username.Trim();
                string normalizedPassword = password.Trim();
                string normalizedRole = role.Trim();

                userDL.LoadUsersFromDatabase();

                if (userDL.UserExists(normalizedUsername))
                {
                    return false;
                }

                User newUser = new User(normalizedUsername, normalizedPassword, normalizedRole);
                userDL.SaveUserToDatabase(newUser);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Sign up failed: " + ex.Message);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return userDL.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get users: " + ex.Message);
            }
        }

        public bool UserExists(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return false;
                }
                return userDL.UserExists(username);
            }
            catch (Exception ex)
            {
                throw new Exception("User check failed: " + ex.Message);
            }
        }
    }
}