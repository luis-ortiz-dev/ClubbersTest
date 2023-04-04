using System;
using LiteDB;
using System.ComponentModel.Design;
using PruebaXamarinLuisOrtiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace PruebaXamarinLuisOrtiz.Services.DataBase
{
	public class DatabaseService : IDatabaseService
    {
        private readonly IDatabaseCollectionService _databaseCollectionService;

        private ILiteCollection<TaskModel> TaskCollection => _databaseCollectionService.TaskCollection;

        public DatabaseService(IDatabaseCollectionService databaseCollectionService)
		{
            _databaseCollectionService = databaseCollectionService;
		}

        public bool SaveTask(TaskModel task)
        {
            try
            {
                TaskCollection.Insert(task);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while saving tasks");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool UpdateTask(TaskModel task)
        {
            try
            {
                return TaskCollection.Update(task);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while updating tasks");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public List<TaskModel> GetAllTasks()
        {
            try
            {
                return TaskCollection.FindAll().ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while getting tasks");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteTask(TaskModel task)
        {
            try
            {
                return TaskCollection.Delete(task._id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while deleting tasks");
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

