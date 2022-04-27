using System;
using System.Collections.Generic;
using TryBeingFit.Domain.Enums;
using System.Text;

namespace TryBeingFit.Domain.Models
{
    public class StandardUser : User
    {
        public List<VideoTraining> VideoTrainings { get; set; }

        public StandardUser()
        {
            VideoTrainings = new List<VideoTraining>();
            UserRole = UserRole.Standard;
        }

        public override string GetInfo()
        {
            string result = $"{FirstName} {LastName}";
            result += "\n Video trainings: \n";
            foreach (VideoTraining videoTraining in VideoTrainings)
            {
                result += $" {videoTraining.Title} \n";
            }
            return result;
        }
    }
}
