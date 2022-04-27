using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Interfaces;

namespace TryBeingFit.Domain.Interfaces
{
    public interface ILiveTraining
    { 
        int HoursToNextSession();
    }
}
