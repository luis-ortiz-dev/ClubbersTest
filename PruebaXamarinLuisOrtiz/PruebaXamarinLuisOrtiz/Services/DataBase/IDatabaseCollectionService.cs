using System;
using LiteDB;
using PruebaXamarinLuisOrtiz.Models;

namespace PruebaXamarinLuisOrtiz.Services.DataBase
{
    public interface IDatabaseCollectionService
    {
        ILiteCollection<TaskModel> TaskCollection { get; }

        void DropCollections();
    }
}

