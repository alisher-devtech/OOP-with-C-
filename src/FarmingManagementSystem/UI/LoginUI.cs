using System;
using FarmingManagementSystem.BL;
using FarmingManagementSystem.Utilities;

namespace FarmingManagementSystem.UI
{
    public class LoginUI
    {
        private UserBL userBL;
        private SignUpRequestBL requestBL;

        public LoginUI()
        {
            userBL = new UserBL();
            requestBL = new SignUpRequestBL();
        }

        public void Show()
        {
            try
            {
                userBL.LoadUsers();
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(60, 12);                 ConsoleHelper.ShowError(60, 12, "Database connection error: " + ex.Message);                 ConsoleHelper.Pause();
                return;
            }

            int option = 0;

            while (option != 3)
            {
                ConsoleHelper.ClearInsideBoundary();
                ConsoleHelper.PrintMenuHeader("Login Menu", "");

                Console.SetCursorPosition(70, 12);                 Console.Write("1. Sign In");
                Console.SetCursorPosition(70, 13);                 Console.Write("2. Sign Up");
                Console.SetCursorPosition(70, 14);                 Console.Write("3. Exit");
                Console.SetCursorPosition(70, 15);                 ConsoleHelper.PrintColoredText("Enter choice: ", ConsoleColor.Yellow);

                option = ConsoleHelper.GetSafeInt(1, 3, 83, 15);                 ConsoleHelper.ClearInsideBoundary();

                if (option == 1)
                {
                    HandleSignIn();
                }
                else if (option == 2)
                {
                    HandleSignUp();
                }
            }
        }

        private void HandleSignIn()
        {
            try
            {
                ConsoleHelper.PrintMenuHeader("Login Menu", "SignIn");

                Console.SetCursorPosition(70, 12);                 Console.Write("Enter User Name: ");
                string username = ConsoleHelper.GetSafeString(87, 12, "Username", 1, 50); 
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter User Password: ");
                string password = ConsoleHelper.GetSafeString(91, 14, "Password", 1, 50); 
                username = username.Trim();
                password = password.Trim();

                string role = userBL.SignIn(username, password);

                if (role != "Undefined")
                {
                    ConsoleHelper.ShowSuccess(70, 18, "Login Successful! Welcome " + username);                     System.Threading.Thread.Sleep(1000);
                    RouteToMenu(role, username);
                }
                else
                {
                    ConsoleHelper.ShowError(70, 18, "Invalid username or password!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Login Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void HandleSignUp()
        {
            try
            {
                ConsoleHelper.ClearInsideBoundary();
                ConsoleHelper.PrintMenuHeader("Login Menu", "SignUp");

                Console.SetCursorPosition(70, 12);                 Console.Write("Enter User Name: ");
                string username = ConsoleHelper.GetSafeString(87, 12, "Username", 3, 50); 
                Console.SetCursorPosition(70, 14);                 Console.Write("Enter User Password: ");
                string password = ConsoleHelper.GetSafeString(91, 14, "Password", 3, 50); 
                Console.SetCursorPosition(70, 16);                 Console.Write("Enter User Role: ");
                string[] validRoles = { "Admin", "Labour", "Supervisor", "Manager", "Customer" };
                string role = ConsoleHelper.GetValidRole(87, 16, validRoles); 
                username = username.Trim();
                password = password.Trim();
                role = role.Trim();

                userBL.LoadUsers();
                requestBL.LoadRequests();

                if (userBL.UserExists(username))
                {
                    ConsoleHelper.ShowError(70, 19, "SignUp Unsuccessful - Username already exists!");                     ConsoleHelper.Pause();
                    ConsoleHelper.ClearInsideBoundary();
                    return;
                }

                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    if (requestBL.RequestExists(username))
                    {
                        ConsoleHelper.ShowError(70, 19, "A signup request for this username is already pending.");
                    }
                    else if (requestBL.AddRequest(username, password, role))
                    {
                        ConsoleHelper.ShowSuccess(70, 19, "Request sent to Admin. Wait for approval!");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 19, "SignUp request failed! Please try again.");
                    }
                }
                else
                {
                    if (userBL.SignUp(username, password, role))
                    {
                        ConsoleHelper.ShowSuccess(70, 19, "SignUp Successful! You can now login.");                     }
                    else
                    {
                        ConsoleHelper.ShowError(70, 19, "SignUp failed! Username may already exist.");
                    }
                }

                ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 21, "SignUp Error: " + ex.Message);                 ConsoleHelper.Pause();
                ConsoleHelper.ClearInsideBoundary();
            }
        }

        private void RouteToMenu(string role, string username)
        {
            try
            {
                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    AdminUI adminUI = new AdminUI(username);
                    adminUI.Show();
                }
                else if (role.Equals("Labour", StringComparison.OrdinalIgnoreCase))
                {
                    LabourUI labourUI = new LabourUI();
                    labourUI.Show();
                }
                else if (role.Equals("Supervisor", StringComparison.OrdinalIgnoreCase))
                {
                    SupervisorUI supervisorUI = new SupervisorUI();
                    supervisorUI.Show();
                }
                else if (role.Equals("Manager", StringComparison.OrdinalIgnoreCase))
                {
                    ManagerUI managerUI = new ManagerUI();
                    managerUI.Show();
                }
                else if (role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    CustomerUI customerUI = new CustomerUI();
                    customerUI.Show();
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(70, 20, "Navigation Error: " + ex.Message);                 ConsoleHelper.Pause();
            }
        }
    }
}