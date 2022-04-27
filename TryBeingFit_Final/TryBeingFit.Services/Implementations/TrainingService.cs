using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;
using TryBeingFit.Services.Interfaces;
using TryBeingFit.Domain.Database;

namespace TryBeingFit.Services.Implementations
{
    public class TrainingService<T> : ITrainingService<T> where T : Training
         
        
    { 
        private IDatabase<T> _database; 

        public TrainingService()
        {
            _database = new Database<T>(); 
        }


        public List<T> GetAllTrainings()
            
        {
            return _database.GetAll();
        }

        public T GetOneTraining(int id)
        {
            return _database.GetById(id);
        }

        public void AddTraining(T entity)
        {
            if (string.IsNullOrEmpty(entity.Title))
            {
                throw new Exception("The title cannot be empty!");
            }
            else
            {
                _database.Insert(entity);
            }
        }
    }
}
