using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Services.Interfaces;
using TryBeingFit.Domain.Models;
using TryBeingFit.Services.Helpers;
using TryBeingFit.Domain.Database;
using System.Linq;

namespace TryBeingFit.Services.Implementations
{
    public class UserService<T> : IUserService<T> where T : User
    {
        private IDatabase<T> _database;

        public UserService()
        {
            _database = new Database<T>(); // But we can create an instance of a class that implements the interface , then assign that instance to a variable of the interface type.
        }

       
        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            T user = _database.GetById(userId);

            if(user != null)
            {
                if(user.Password != oldPassword)
                {
                    throw new Exception("The passwords don't match");
                }

                else
                {
                    if (!ValidationHelper.ValidatePassword(newPassword))
                    {
                        throw new Exception("Invalid password!");
                    }

                    else
                    {
                        user.Password = newPassword;
                        _database.Update(user);
                    }
                }

            }
        }

        public T GetById(int id)
        {
            return _database.GetById(id);
        }

        public T Login(string username, string password)
        {
            T userToLogin = _database.GetAll().FirstOrDefault(x => x.Password == password && x.Username == username);

            if(userToLogin == null)
            {
                MessageHelper.PrintMessage($"User with username {username} does not exist!", ConsoleColor.Red);
                return null;

            }

            else
            {

                return userToLogin;
            }
        }

        public T Register(T user)
        {
            if (!(ValidationHelper.ValidateUsername(user.Username)) || !(ValidationHelper.ValidateName(user.FirstName)) || (!(ValidationHelper.ValidateName(user.LastName))) || !(ValidationHelper.ValidatePassword(user.Password)))
            {
                MessageHelper.PrintMessage("Some of the entered parameters are wrong!", ConsoleColor.Red);
                return null; //objektot e null
            }

            else
            {
                int idd = _database.Insert(user);
                return _database.GetById(idd);
            }
            
        }

        public T ChangeInfo(int userId, string firstName, string lastName)
        {
            T user = GetById(userId);
            if (!ValidationHelper.ValidateName(firstName) || !ValidationHelper.ValidateName(lastName))
            {
                MessageHelper.PrintMessage("Invalid user data", ConsoleColor.Red);
                return null;
            }

            else
            {
                user.FirstName = firstName;
                user.LastName = lastName;

                _database.Update(user);
                MessageHelper.PrintMessage("Successfully updated user!", ConsoleColor.Green);
                return _database.GetById(userId);
            }
        }

        public void RemoveById(int id)
        {
            _database.RemoveById(id);
        }

    }
}
