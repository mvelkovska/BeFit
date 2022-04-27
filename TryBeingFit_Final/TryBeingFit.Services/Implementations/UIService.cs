using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Services.Interfaces;
using TryBeingFit.Domain.Enums;
using TryBeingFit.Services.Helpers;
using System.Linq;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Services.Implementations
{
    public class UIService : IUIService
    {
        public List<string> MenuItems { get; set; }

        public int ChooseMenuItem(List<string> menuItems)
        {
            while (true)
            {
                for(int i = 0; i < menuItems.Count; i++)
                {
                    
                    Console.WriteLine($"{i+1}. {menuItems[i]}"); 
                }

                string input = Console.ReadLine();

                int choice = ValidationHelper.ValidateNumber(input, menuItems.Count);

                if(choice == -1)
                {
                    MessageHelper.PrintMessage("You must enter valid number", ConsoleColor.Red);
                    continue;
                }
                return choice;
            }


        }

        public int LogInMenu()
        {
            List<string> theMenu = new List<string>{ "Login", "Register" };
            Console.WriteLine("Choose option: ");
            return ChooseMenuItem(theMenu);
        }

        public int RoleMenu()
        {
            List<string> theMenu = Enum.GetNames(typeof(UserRole)).ToList();
            Console.WriteLine("Choose a role: ");
             return ChooseMenuItem(theMenu);
        }

        public int TrainMenu()
        {
            List<string> theMenu = new List<string> { "Video", "Live" };
            return ChooseMenuItem(theMenu);
        }

        public int AccountMenu()
        {
            List<string> theMenu = new List<string> { "Change info", "Change password" };
            return ChooseMenuItem(theMenu);
        }



        public StandardUser FillNewUserData() //self descriptive name
        {
            StandardUser standardUser = new StandardUser();
            Console.WriteLine("Enter first name");
            string firstName = Console.ReadLine();
            if (string.IsNullOrEmpty(firstName))
            {
                throw new Exception("The name must not be empty!");
            }
            Console.WriteLine("Enter last name");
            string lastName = Console.ReadLine();
            if (string.IsNullOrEmpty(lastName))
            {
                throw new Exception("The last name must not be empty!");
            }

            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("The username must not be empty!");
            }
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("The password must not be empty!");
            }

            standardUser.FirstName = firstName;
            standardUser.LastName = lastName;
            standardUser.Username = username;
            standardUser.Password = password;

            return standardUser;
        }

        public int MainMenu(UserRole userRole)
        {
            MenuItems = new List<string>();
            MenuItems.Add("Account");
            MenuItems.Add("Log Out");
            
            switch (userRole)
            {
                case UserRole.Standard: //Account,LogOut,Train,UpgradeToPremium
                    MenuItems.Add("Train");
                    MenuItems.Add("Upgrade to Premium");
                    break;
                case UserRole.Premium: //Account,Logoout,Train
                    MenuItems.Add("Train");
                    break;
                case UserRole.Trainer: //Account,Logout,RescheduleTraining
                    MenuItems.Add("Reschedule training");
                    break;
            }
            return ChooseMenuItem(MenuItems);
        }

        public int TrainMenuItems<T>(List<T> trainings) where T : Training
        {
            Console.WriteLine("Choose a training:");
            return ChooseEntity(trainings);
        }

        private int ChooseEntity<T>(List<T> entities) where T : Training
        {
            while (true)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {entities[i].GetInfo()}");
                }
                string input = Console.ReadLine();
                int choice = ValidationHelper.ValidateNumber(input, entities.Count);
                if (choice == -1)
                {
                    MessageHelper.PrintMessage("You must enter a valid option", ConsoleColor.Red);
                    continue;
                }
                return choice;
            }
        }
    }
}

