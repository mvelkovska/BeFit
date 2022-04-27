using System;
using TryBeingFit.Domain.Enums;
using TryBeingFit.Domain.Models;
using TryBeingFit.Domain.Interfaces;
using TryBeingFit.Services.Implementations;
using TryBeingFit.Services.Interfaces;
using TryBeingFit.Services.Helpers;
using System.Linq;
using System.Collections.Generic;


namespace TryBeingFit.App
{
    class Program
    {
        public static ITrainingService<LiveTraining> _liveTrainingService = new TrainingService<LiveTraining>();

        public static ITrainingService<VideoTraining> _videoTrainingService = new TrainingService<VideoTraining>();

        public static IUserService<Trainer> _trainerService = new UserService<Trainer>();

        public static IUserService<StandardUser> _standardUserService = new UserService<StandardUser>();

        public static IUserService<PremiumUser> _premiumUserService = new UserService<PremiumUser>();

        public static IUIService _uiService = new UIService();

        public static User _curUser;

        public static void Seed()
        {
            _standardUserService.Register(new StandardUser()
            {
                FirstName = "Bob",
                LastName = "Bobsky",
                Username = "bob.bobsky",
                Password = "bob.bobsky1"
            });
            _premiumUserService.Register(new PremiumUser()
            {
                FirstName = "Anne",
                LastName = "Bobsky",
                Username = "anne.bobsky",
                Password = "anne.bobsky2"
            });
            Trainer paul = new Trainer()
            {
                FirstName = "Paul",
                LastName = "Paulsky",
                Username = "paul.paulsky",
                Password = "paul.paulsky3",
                YearsOfExperience = 3
            };
            Trainer registeredTrainer = _trainerService.Register(paul);
            _videoTrainingService.AddTraining(new VideoTraining()
            {
                Title = "Abs workout",
                Description = "Abs workout made easy",
                Difficulty = TrainingDifficulty.Easy,
                Link = "someLink",
                Rating = 3,
                Time = 15.55
            });
            _videoTrainingService.AddTraining(new VideoTraining()
            {
                Title = "Cardio",
                Description = "Dance cardio",
                Difficulty = TrainingDifficulty.Medium,
                Link = "someLink",
                Rating = 5,
                Time = 25
            });

            _liveTrainingService.AddTraining(new LiveTraining()
            {

                Title = "Cardio",
                Description = "Dance cardio",
                Difficulty = TrainingDifficulty.Medium,
                NextSession = DateTime.Now.AddDays(2),
                Trainer = registeredTrainer,
                Rating = 5,
                Time = 25,
            });
        }



        static void Main(string[] args)
        {
            Seed();
            int option = _uiService.LogInMenu();
            
            
            if(option == 1) // Log in the existing user
                 
            {
                int roleChoice = _uiService.RoleMenu();

                Console.WriteLine("Enter username");
                string username = Console.ReadLine();
                if (string.IsNullOrEmpty(username))
                {
                    throw new Exception("You must enter username");
                }
                Console.WriteLine("Enter password");
                string password = Console.ReadLine();
                if (string.IsNullOrEmpty(password))
                {
                    throw new Exception("You must enter password");
                }

                UserRole userRole = (UserRole)roleChoice;

                switch (userRole)
                {
                    case UserRole.Standard:
                        _curUser = _standardUserService.Login(username, password);
                        Console.WriteLine("You are now successfully logged in!");
                        break;

                    case UserRole.Premium:
                        _curUser = _premiumUserService.Login(username, password);
                        Console.WriteLine("You are now successfully logged in!");
                        break;

                    case UserRole.Trainer:
                        _curUser = _trainerService.Login(username, password);
                        Console.WriteLine("You are now successfully logged in!");
                        break;
 }
            }

            else //Register the new user
            {
                StandardUser _standardUser = _uiService.FillNewUserData();

                _curUser = _standardUserService.Register(_standardUser);

                if(_curUser != null)
                {
                    Console.WriteLine("You are successfully registered");
                }
            }

            int optionFromTheMainMenu = _uiService.MainMenu(_curUser.UserRole);

            string convertToStringOption = _uiService.MenuItems[optionFromTheMainMenu - 1];

            Console.WriteLine(convertToStringOption);

            switch (convertToStringOption)
            {
                case "Account":
                    int accountChoice = _uiService.AccountMenu();

                    if (accountChoice == 1) //Change info
                    {
                        Console.WriteLine("Enter first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter last name:");
                        string lastName = Console.ReadLine();

                        switch (_curUser.UserRole)
                        {

                            case UserRole.Standard:
                                _curUser = _standardUserService.ChangeInfo(_curUser.Id, firstName, lastName);
                                Console.WriteLine("Successfully changed information");
                                break;

                            case UserRole.Premium:
                                _curUser = _premiumUserService.ChangeInfo(_curUser.Id, firstName, lastName);
                                Console.WriteLine("Successfully changed information");
                                break;


                            case UserRole.Trainer:
                                _curUser = _trainerService.ChangeInfo(_curUser.Id, firstName, lastName);
                                Console.WriteLine("Successfully changed information");
                                break;

                        }


                    }
                    else // Change password
                    {
                        Console.WriteLine("Enter your old password:");
                        string oldPassword = Console.ReadLine();

                        Console.WriteLine("Enter your new password:");
                        string newPassword = Console.ReadLine();

                        switch (_curUser.UserRole)
                        {

                            case UserRole.Standard:
                                _standardUserService.ChangePassword(_curUser.Id, oldPassword, newPassword);
                                Console.WriteLine("Successfully changed password");
                                break;

                            case UserRole.Premium:
                                _premiumUserService.ChangePassword(_curUser.Id, oldPassword, newPassword);
                                Console.WriteLine("Successfully changed password");
                                break;


                            case UserRole.Trainer:
                                _trainerService.ChangePassword(_curUser.Id, oldPassword, newPassword);
                                Console.WriteLine("Successfully changed password");
                                break;

                        }
                    }
                    break;

                case "Log Out":
                    _curUser = null;
                    break;

                case "Train":
                    int trainOption = 1;

                    if(_curUser.UserRole == UserRole.Premium)
                    {
                        _uiService.TrainMenu();
                    }
                   
                    if(trainOption == 1) //Video training
                    {
                        List<VideoTraining> listOfVideoTrainings = new List<VideoTraining>();
                        int chooseTraining = _uiService.TrainMenuItems(listOfVideoTrainings);

                        VideoTraining chosenVideoTraining = listOfVideoTrainings[chooseTraining - 1];

                        Console.WriteLine("You chose:");
                        Console.WriteLine($"{chosenVideoTraining.GetInfo()}");
                    }

                    else //Live training
                    {
                        List<LiveTraining> listOfLiveTrainings = new List<LiveTraining>();
                        int chooseTraining = _uiService.TrainMenuItems(listOfLiveTrainings);

                        LiveTraining chosenLiveTraining = listOfLiveTrainings[chooseTraining - 1];

                        Console.WriteLine("You chose:");
                        Console.WriteLine($"{chosenLiveTraining.GetInfo()}");
                    }

                    break;


                case "Reschedule training":
                    List<LiveTraining> liveTrainingsForCurrentUser = _liveTrainingService.GetAllTrainings()
                        .Where(x => x.Trainer.Id == _curUser.Id).ToList();
                    if (liveTrainingsForCurrentUser.Count == 0)
                    {
                        Console.WriteLine("There are no live trainings!");
                    }
                    else
                    {
                        int trainingItem = _uiService.TrainMenuItems(liveTrainingsForCurrentUser);
                        Console.WriteLine("Enter number of days");
                        int days = ValidationHelper.ValidateNumber(Console.ReadLine(), 10);
                        _trainerService.GetById(_curUser.Id)
                            .Reschedule(liveTrainingsForCurrentUser[trainingItem - 1], days);
                    }
                        break;

                case "Upgrade to premium":

                    _standardUserService.RemoveById(_curUser.Id);
                    PremiumUser newPremiumUser = new PremiumUser();
                    _curUser = _premiumUserService.Register(newPremiumUser);

                    newPremiumUser.FirstName = _curUser.FirstName;
                    newPremiumUser.LastName = _curUser.LastName;
                    newPremiumUser.Username = _curUser.Username;
                    newPremiumUser.Password = _curUser.Password;




                    break;
            }






            Console.ReadLine();
        }
    }
}
