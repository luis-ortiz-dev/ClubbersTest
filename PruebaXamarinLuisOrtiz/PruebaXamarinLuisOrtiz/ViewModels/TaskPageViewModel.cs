using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using PruebaXamarinLuisOrtiz.Helpers;
using PruebaXamarinLuisOrtiz.Models;
using PruebaXamarinLuisOrtiz.Services.DataBase;
using PruebaXamarinLuisOrtiz.Services.Navigation;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.ViewModels
{
	public class TaskPageViewModel : BaseViewModel
	{
		private readonly IDatabaseService _databaseService;
		private TaskModel _selectedTask;
		private ObservableCollection<TaskModel> _taskList;

		public TaskModel SelectedTask
		{
			get => _selectedTask;
			set => SetProperty(ref _selectedTask, value);
		}

		public ObservableCollection<TaskModel> TaskList
		{
			get => _taskList;
			set => SetProperty(ref _taskList, value);
		}

		public ICommand GetTasksCommand { get; set; }
		public ICommand CreateTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }

        public TaskPageViewModel(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}

        public override void Initialize()
        {
            base.Initialize();
			TaskList = new ObservableCollection<TaskModel>();
            GetTasksCommand.Execute(null);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void InitializeCommands()
        {
            base.InitializeCommands();
			GetTasksCommand = new Command(ExecuteGetTasksCommand);
            CreateTaskCommand = new AsyncCommand(ExecuteCreateTaskCommand);
            EditTaskCommand = new AsyncCommand(ExecuteEditTaskCommand);
        }

        public override void OnNavigatingBack<TParam>(TParam parameter)
        {
            base.OnNavigatingBack(parameter);

			GetTasksCommand.Execute(null); 
        }

        private void ExecuteGetTasksCommand()
		{
			try
			{
				var taskList = _databaseService.GetAllTasks();

				if (taskList != null)
				{
					TaskList = new ObservableCollection<TaskModel>(taskList);
				}

            }
			catch (Exception ex)
            {
                Debug.WriteLine($"Error getting data from database");
                Debug.WriteLine(ex.Message);
            }
		}

		private async Task ExecuteCreateTaskCommand()
		{
			try
			{
                await _navigationService.ShellGoToAsync($"{GlobalConstants.TaskDetailRoute}");
            }
			catch (Exception ex)
			{
				Debug.WriteLine($"Error navigating to {GlobalConstants.TaskDetailRoute}");
                Debug.WriteLine(ex.Message);
            }
        }


        private async Task ExecuteEditTaskCommand()
		{
			if (SelectedTask == null)
			{
				return;
			}

            try
            {
                await _navigationService.ShellGoToAsync($"{GlobalConstants.TaskDetailRoute}", SelectedTask);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to {GlobalConstants.TaskDetailRoute}");
                Debug.WriteLine(ex.Message);
            }
		}
    }
}

