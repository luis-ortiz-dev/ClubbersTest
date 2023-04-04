using System;
using LiteDB;
using PruebaXamarinLuisOrtiz.Models;

namespace PruebaXamarinLuisOrtiz.Services.DataBase
{
    public class DatabaseCollectionService : IDatabaseCollectionService
    {
        private LiteDatabase DBInstance => DatabaseProvider.Instance.Current;

        public DatabaseCollectionService()
        {
        }

        public ILiteCollection<TaskModel> TaskCollection => DBInstance.GetCollection<TaskModel>();

        public void DropCollections()
        {
            foreach (var name in DBInstance.GetCollectionNames())
            {
                DBInstance.DropCollection(name);
            }
        }
    }
}

