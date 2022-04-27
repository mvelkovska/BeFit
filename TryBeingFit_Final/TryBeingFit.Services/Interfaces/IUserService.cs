using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;


namespace TryBeingFit.Services.Interfaces
{
    public interface IUserService<T> where T : User
    {
        void ChangePassword(int userId, string oldPassword, string newPassword);
         
        T Register(T user);

        T Login(string username, string password);

        T GetById(int id);

        T ChangeInfo(int userId, string firstName, string lastName);

        void RemoveById(int id);
    }
}
