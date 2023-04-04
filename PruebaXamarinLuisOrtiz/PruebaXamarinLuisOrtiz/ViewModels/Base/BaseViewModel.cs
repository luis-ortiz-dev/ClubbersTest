using System;
using PruebaXamarinLuisOrtiz.Services.Navigation;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PruebaXamarinLuisOrtiz.ViewModels.Base
{
    public class BaseViewModel : ObservableObject, IViewModel
    {
        public INavigationService _navigationService;

        private bool _isBusy;

        public CancellationTokenSource CancellationTokenSource { get; set; }

        /// <summary>
        /// Gets or sets a value containing the result of a task.
        /// </summary>
        public object TaskCompletionSource { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether if the OnAppearing method has been called.
        /// </summary>
        public bool HasAppeared { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the ViewModel is blocking main thread.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand GoBackCommand { get; set; }

        public BaseViewModel()
        {
            CancellationTokenSource = new CancellationTokenSource();
            InitializeCommands();
        }

        /// <summary>
        /// Initializes the properties or variables.
        /// </summary>
        public virtual void Initialize()
        {
            _navigationService ??= ServiceLocatorProvider.Instance.Current.Resolve<INavigationService>();
        }

        /// <summary>
        /// Initializes the Commands.
        /// </summary>
        public virtual void InitializeCommands()
        {
            GoBackCommand = new AsyncCommand(() => ExecuteGoBackCommand());
        }

        /// <summary>
        /// Implement this method to initializes the ViewModel data.
        /// </summary>
        /// <param name="args">Used to pass an optional parameters.</param>
        public virtual void OnInitialize(params object[] args)
        {
        }

        /// <summary>
        /// Register a TaskCompletionSource for this ViewModel.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="tcs">The task completion source.</param>
        public void OnPageResult<T>(TaskCompletionSource<T> tcs)
        {
            TaskCompletionSource = tcs;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the Page becoming visible.
        /// </summary>
        public virtual void OnAppearing()
        {
            HasAppeared = true;
        }

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the Page disappears.
        /// </summary>
        public virtual void OnDisappearing()
        {
        }

        /// <summary>
        /// When overriden, allows the appliaction developer to customize behavior when navigates to this page. It runs after Ctor and after OnAppearing.
        /// </summary>
        /// <param name="parameter">The parameter received.</param>
        /// <typeparam name="TParam">Pass any type of object as parameter.</typeparam>
        public virtual void OnNavigating<TParam>(TParam parameter)
        {
        }

        /// <summary>
        /// When overriden, allows the appliaction developer to customize behavior when navigates back to this page. It runs after Ctor and after OnAppearing.
        /// </summary>
        /// <param name="parameter">The parameter received while navigating back.</param>
        /// /// <typeparam name="TParam">Pass any type of object as parameter.</typeparam>
        public virtual void OnNavigatingBack<TParam>(TParam parameter)
        {
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>Whether or not the back navigation was handled by the override.</returns>
        public virtual bool OnBackButtonPressed() => true;

        private async Task ExecuteGoBackCommand()
        {
            await _navigationService.ShellGoToAsync("..");
        }
    }
}

