using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Domain.Database
{
    public interface IDatabase<T> where T : BaseEntity
    {

        List<T> GetAll(); 

        T GetById(int Id);

        int Insert(T entity);

        void Update(T entity);

        void RemoveById(int id);

        
    }
}
