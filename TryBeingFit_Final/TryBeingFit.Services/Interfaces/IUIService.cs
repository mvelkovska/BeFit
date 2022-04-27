using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;
using TryBeingFit.Domain.Enums;

namespace TryBeingFit.Services.Interfaces
{
    public interface IUIService
    {

        List<string> MenuItems { get; set; }
        int RoleMenu();
        int LogInMenu();
        int TrainMenu();
        int AccountMenu();
        int ChooseMenuItem(List<string> menuItems);
        StandardUser FillNewUserData();
        int TrainMenuItems<T>(List<T> trainings) where T : Training;
        int MainMenu(UserRole userRole);
    }
}
