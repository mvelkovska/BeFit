using System;
using System.Collections.Generic;
using TryBeingFit.Domain.Enums;
using System.Text;

namespace TryBeingFit.Domain.Models
{
    public class PremiumUser : User
    { 
        public List<VideoTraining> VideoTrainings { get; set; }

        public LiveTraining LiveTraining { get; set; }
        
        public PremiumUser()
        {
            VideoTrainings = new List<VideoTraining>();
            UserRole = UserRole.Premium;
        }

        public override string GetInfo()
        {
            string liveTrainingMessage = LiveTraining == null ? " no live training" : LiveTraining.Title;
            return $"{FirstName} {LastName}, num. of video trainings {VideoTrainings.Count}, live training: {liveTrainingMessage}";
        }
    }

   

}
