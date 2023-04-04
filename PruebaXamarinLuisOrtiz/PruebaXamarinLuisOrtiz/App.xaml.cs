using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PruebaXamarinLuisOrtiz.Services;
using PruebaXamarinLuisOrtiz.Views;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using System.Globalization;
using System.ComponentModel.Design;
using Xamarin.Essentials;
using PruebaXamarinLuisOrtiz.Services.Navigation;
using PruebaXamarinLuisOrtiz.ViewModels;
using PruebaXamarinLuisOrtiz.Services.DataBase;
using PruebaXamarinLuisOrtiz.Services.Api;

namespace PruebaXamarinLuisOrtiz
{
    public partial class App : Application
    {
        private IServiceLocator _serviceLocator;
        private INavigationService _navigationService;

        private static bool _navigationInitialized;

        public App ()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            InitializeDependencyContainer();
            InitializeNavigation();
        }

        private void InitializeDependencyContainer()
        {
            _serviceLocator = ServiceLocatorProvider.Instance.Current;
            RegisterDependencies();
            RegisterViewModels();
            _serviceLocator.Init();
        }

        private void RegisterDependencies()
        {
            _serviceLocator.RegisterSingle<INavigationService, NavigationService>();
            _serviceLocator.RegisterSingle<IDatabaseCollectionService, DatabaseCollectionService>();
            _serviceLocator.RegisterSingle<IDatabaseService, DatabaseService>();
            _serviceLocator.RegisterSingle<IApiClientService, ApiClientService>();
            _serviceLocator.RegisterSingle<ICurrencyApiService, CurrencyApiService>();
        }

        private void RegisterViewModels()
        {
            _serviceLocator.Register<AppShellViewModel>();
            _serviceLocator.Register<TaskPageViewModel>();
            _serviceLocator.Register<CurrencyPageViewModel>();
            _serviceLocator.Register<TaskDetailPageViewModel>();
            _serviceLocator.Register<CurrencyDetailPageViewModel>();
        }

        private void InitializeNavigation()
        {
            _navigationService = _navigationService ?? _serviceLocator.Resolve<INavigationService>();

            lock (typeof(App))
            {
                if (_navigationInitialized)
                {
                    return;
                }

                _navigationInitialized = true;
            }

            RegisterRoutingNavigation();
            BindViewsAndViewModels();
            NavigationToFirstViewModel();
        }

        private void RegisterRoutingNavigation()
        {
            Routing.RegisterRoute(nameof(TaskPageViewModel), typeof(TaskPage));
            Routing.RegisterRoute(nameof(CurrencyPageViewModel), typeof(CurrencyPage));
            Routing.RegisterRoute(nameof(TaskDetailPageViewModel), typeof(TaskDetailPage));
            Routing.RegisterRoute(nameof(CurrencyDetailPageViewModel), typeof(CurrencyDetailPage));
        }

        private void BindViewsAndViewModels()
        {
            // Register all the view models with their corresponding pages
            _navigationService.RegisterViewModelMapping(typeof(AppShellViewModel), typeof(AppShell));
            _navigationService.RegisterViewModelMapping(typeof(TaskPageViewModel), typeof(TaskPage));
            _navigationService.RegisterViewModelMapping(typeof(CurrencyPageViewModel), typeof(CurrencyPage));
            _navigationService.RegisterViewModelMapping(typeof(TaskDetailPageViewModel), typeof(TaskDetailPage));
            _navigationService.RegisterViewModelMapping(typeof(CurrencyDetailPageViewModel), typeof(CurrencyDetailPage));
        }

        private void NavigationToFirstViewModel()
        {
            //if (!_preferencesService.IsFirstTimer)
            //{
            _navigationService.RootNavigation<AppShellViewModel>();
            //}
            //else
            //{
            // TODO:
            //}
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

