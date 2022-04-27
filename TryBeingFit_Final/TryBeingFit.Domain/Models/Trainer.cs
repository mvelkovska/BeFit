using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Interfaces;
using TryBeingFit.Domain.Enums;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Domain.Models
{
    public class Trainer : User,ITrainer
    {
        public int YearsOfExperience { get; set; }
        public Trainer()
        {
            UserRole = UserRole.Trainer;
        }
        public override string GetInfo()
        {
            return $"{FirstName} {LastName} - {YearsOfExperience} years of experience";
        }

        public void Reschedule(LiveTraining liveTraining, int days)
        {
            liveTraining.NextSession = liveTraining.NextSession.AddDays(days);
        }
    }
} 
 