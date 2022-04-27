using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Domain.Interfaces
{
    public interface ITrainer
    {
        void Reschedule(LiveTraining liveTraining, int days); 
    }
}
