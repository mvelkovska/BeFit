using System;
using System.Collections.Generic;
using System.Text;
using TryBeingFit.Domain.Models;
using System.Linq;

namespace TryBeingFit.Domain.Database
{
    public class Database<T> : IDatabase<T> where T : BaseEntity
    {
        private List<T> _ourList { get; set; }
        
        public int Id { get; set; }

        public Database()
        {
            _ourList = new List<T>();
            Id = 0;
        }

        public List<T> GetAll()
        {
            return _ourList;
        }

        public T GetById(int Id)
        {
            T theEntity = _ourList.FirstOrDefault(x => x.Id == Id);

            if (theEntity == null)
            {
                throw new Exception("Id not found! We cannot find your entity!");
            }

            else return theEntity;
        }

        public int Insert(T entity)
        {
            Id++;
            entity.Id = Id;
            _ourList.Add(entity);
            return entity.Id;
           
        }

        public void RemoveById(int id)
        {
            T theEntity = _ourList.FirstOrDefault(x => x.Id == Id);
            

            if (theEntity == null)
            {
                throw new Exception("Id not found! We cannot find it to remove it!");
            }

            else
                _ourList.Remove(theEntity);
        }

        public void Update(T entity)
        {
            T theEntity = _ourList.FirstOrDefault(x => x.Id == Id);

            if (theEntity == null)
            {
                throw new Exception("Id not found! We cannot find your entity to update it!");
            }

            entity = theEntity;
        }
    }
    }
