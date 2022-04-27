using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Services.Interfaces
{
    public interface ITrainingService<T> where T: Training
    {
        List<T> GetAllTrainings();

        T GetOneTraining(int id);

        void AddTraining(T entity);


    }
}
