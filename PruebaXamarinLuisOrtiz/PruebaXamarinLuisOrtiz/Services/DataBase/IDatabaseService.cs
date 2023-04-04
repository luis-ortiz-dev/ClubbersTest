using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PruebaXamarinLuisOrtiz.Models;

namespace PruebaXamarinLuisOrtiz.Services.DataBase
{
    public interface IDatabaseService
    {
        //Task
        bool SaveTask(TaskModel task);

        bool UpdateTask(TaskModel task);

        List<TaskModel> GetAllTasks();

        bool DeleteTask(TaskModel task);
    }
}

