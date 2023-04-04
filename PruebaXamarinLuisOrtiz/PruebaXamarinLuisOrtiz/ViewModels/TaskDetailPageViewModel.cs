using System.Windows.Input;
using PruebaXamarinLuisOrtiz.Models;
using PruebaXamarinLuisOrtiz.Services.DataBase;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.ViewModels
{
    public class TaskDetailPageViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private bool _isUpdate;
        private bool _isSaveButtonEnabled;
        private bool _isCompleted;
        private TaskModel _currentTask;
        private string _title;
        private string _description;

        public bool IsUpdate
        {
            get => _isUpdate;
            set => SetProperty(ref _isUpdate, value);
        }

        public bool IsSaveButtonEnabled
        {
            get => _isSaveButtonEnabled;
            set => SetProperty(ref _isSaveButtonEnabled, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                IsSaveButtonEnabled = ValidateFields();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
                IsSaveButtonEnabled = ValidateFields();
            }
        }

        public ICommand SaveTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public TaskDetailPageViewModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override void InitializeCommands()
        {
            base.InitializeCommands();
            SaveTaskCommand = new Command(ExecuteSaveTaskCommand);
            DeleteTaskCommand = new Command(ExecuteDeleteTaskCommand);
        }

        public override void OnNavigating<TParam>(TParam parameter)
        {
            base.OnNavigating(parameter);
            _currentTask = new TaskModel();

            if (parameter != null
                && parameter is TaskModel task)
            {
                IsUpdate = true;
                IsSaveButtonEnabled = true;
                Title = task.Title;
                Description = task.Description;
                IsCompleted = task.IsCompleted;
                _currentTask = new TaskModel()
                {
                    _id = task._id,
                };
                return;
            }

            IsUpdate = false;
            IsSaveButtonEnabled = false;
            Title = string.Empty;
            Description = string.Empty;
        }

        private void ExecuteSaveTaskCommand()
        {
            if (!ValidateFields())
            {
                return;
            }

            UpdateCurrentTaskModel();

            if (IsUpdate)
            {
                _databaseService.UpdateTask(_currentTask);
            }
            else
            {
                _databaseService.SaveTask(_currentTask);
            }

            GoBackCommand.Execute(null);
        }

        private void ExecuteDeleteTaskCommand()
        {
            UpdateCurrentTaskModel();

            _databaseService.DeleteTask(_currentTask);

            GoBackCommand.Execute(null);
        }

        private void UpdateCurrentTaskModel()
        {
            _currentTask.Title = Title;
            _currentTask.Description = Description;
            _currentTask.IsCompleted = IsCompleted;
        }

        private bool ValidateFields()
        {
            if (!string.IsNullOrEmpty(Title) &&
                !string.IsNullOrEmpty(Description))
            {
                return true;
            }

            return false;
        }

    }
}

