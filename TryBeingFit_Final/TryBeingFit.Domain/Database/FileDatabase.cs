using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using TryBeingFit.Domain.Models;

namespace TryBeingFit.Domain.Database
{
    public class FileDatabase<T> : IDatabase<T> where T : BaseEntity
    {
        private string _filePath;
        private string _folderPath;
        private int _id;


        public FileDatabase()
        {
            
            _folderPath = @"..\..\Databasee";
            _id = 0;
            _filePath = _folderPath + @$"\{typeof(T).Name}s.json";
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                WriteData(new List<T>());
            }

        }

        public List<T> GetAll()
        {
            return GetEntititesFromDb();
        }

        public T GetById(int Id)
        {
            List<T> theList = GetEntititesFromDb();
            T theEntity = theList.FirstOrDefault(x => x.Id == Id);
            
            if(theEntity == null)
            {
                throw new Exception("We cannot find your entity!");
            }

            else
            {
                return theEntity;
            }
        }

        public int Insert(T entity)
        {
            List<T> theList = GetEntititesFromDb();

            if(theList == null)
            {
                theList = new List<T>();
                _id = 1;
            }

            else
            {
                entity.Id = theList.Count+1;
            }

            _id++;
            WriteData(theList);
            return entity.Id;
        }

        public void RemoveById(int id)
        {
            List<T> theList = GetEntititesFromDb();
            T theEntity = theList.FirstOrDefault(x => x.Id == id);

            if(theEntity == null)
            {
                throw new Exception("That index does not exist");
            }

            else
            {
                theList.Remove(theEntity);
            }
        }

        public void Update(T entity)
        {

            List<T> theList = GetEntititesFromDb();
            T theEntity = theList.FirstOrDefault(x => x.Id == entity.Id);

            if (theEntity == null)
            {
                throw new Exception("Id not found! We cannot find your entity to update it!");
            }

            theList[theList.IndexOf(theEntity)] = entity;
            WriteData(theList);
        }

        private List<T> GetEntititesFromDb()
        {
            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                return JsonConvert.DeserializeObject<List<T>>(streamReader.ReadToEnd());
            }

        }

        private void WriteData(List<T> dbEntities)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(dbEntities));
            }
        }
    }
}
