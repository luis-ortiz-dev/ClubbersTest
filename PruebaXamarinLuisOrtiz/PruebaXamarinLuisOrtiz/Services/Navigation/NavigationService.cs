using System;
using PruebaXamarinLuisOrtiz.Services.ServiceLocator;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// Constructor.
        /// </summary>
        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Registers the ViewModel-View Mapping.
        /// </summary>
        /// <param name="viewModel">Used to specify the ViewModel.</param>
        /// <param name="view">Used to specify the View.</param>
        public void RegisterViewModelMapping(Type viewModel, Type view)
        {
            if (!_mappings.ContainsKey(viewModel))
            {
                _mappings.Add(viewModel, view);
            }
        }

        /// <summary>
        /// Registers the ViewModel-View Mapping.
        /// </summary>
        /// <typeparam name="TViewModel">Used to specify the ViewModel.</typeparam>
        /// <typeparam name="TView">Used to specify the View.</typeparam>
        public void RegisterViewModelMapping<TViewModel, TView>()
        {
            if (!_mappings.ContainsKey(typeof(TViewModel)))
            {
                _mappings.Add(typeof(TViewModel), typeof(TView));
            }
        }

        /// <summary>
        /// Sets the Root Navigation from the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel for the Root.</param>
        /// <param name="parameter">The parameters for the RootPage.</param>
        public void RootNavigation(Type viewModel, params object[] parameter)
        {
            Application.Current.MainPage = CreateAndBindPage(viewModel, parameter);
        }

        /// <summary>
        /// Sets the Root Navigation from the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type.</typeparam>
        /// <param name="parameter">The parameters for the RootPage.</param>
        public void RootNavigation<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel
        {
            Application.Current.MainPage = CreateAndBindPage(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="TPage">TPage.</typeparam>
        /// <typeparam name="TParam">TParam.</typeparam>
        /// <param name="route">Route.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        public Task ShellGoToAsync<TPage, TParam>(string route = null, TParam parameter = default, bool animated = true)
        {
            return ShellGoToAsync($"{route}{typeof(TPage).Name}", parameter, animated);
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        public Task ShellGoToAsync(string route, bool animated = true)
        {
            return ShellGoToAsync<object>(route, null, animated);
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="TParam">TParam.</typeparam>
        /// <param name="route">Route.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        public async Task ShellGoToAsync<TParam>(string route, TParam parameter = default, bool animated = true)
        {
            await Shell.Current.GoToAsync($"{route}", animated);

            if (Shell.Current.CurrentPage == null)
            {
                await Task.Delay(100);
            }

            var currentPage = Shell.Current.CurrentPage;
            if (currentPage != null && currentPage.BindingContext is BaseViewModel bvm)
            {
                if (route.Contains(".."))
                {
                    bvm.OnNavigatingBack(parameter);
                }
                else if (currentPage.BindingContext is BaseViewModel vm)
                {
                    vm.OnNavigating(parameter);
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="T:Xamarin.Forms.NavigationPage" /> from the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel for the NavigationPage.</param>
        /// <param name="parameter">The parameters for the NavigationPage.</param>
        public void CreateNavigation(Type viewModel, params object[] parameter)
        {
            Application.Current.MainPage = new NavigationPage(CreateAndBindPage(viewModel, parameter));
        }

        /// <summary>
        /// Creates a <see cref="T:Xamarin.Forms.NavigationPage" /> from the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type.</typeparam>
        /// <param name="parameter">The parameters for the NavigationPage.</param>
        public void CreateNavigation<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel
        {
            Application.Current.MainPage = new NavigationPage(CreateAndBindPage(typeof(TViewModel), parameter));
        }

        /// <summary>
        /// Asynchronously removes the most recent <see cref="T:Xamarin.Forms.Page" /> from the navigation stack.
        /// </summary>
        /// <returns>The <see cref="T:Xamarin.Forms.Page" /> that had been at the top of the navigation stack.</returns>
        public Task PopAsync()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Asynchronously removes the most recent <see cref="T:Xamarin.Forms.Page" /> from the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>The <see cref="T:Xamarin.Forms.Page" /> that had been at the top of the navigation stack.</returns>
        public Task PopAsync(bool animated)
        {
            return Application.Current.MainPage.Navigation.PopAsync(animated);
        }

        /// <summary>
        /// Asynchronously dismisses the most recent modally presented <see cref="T:Xamarin.Forms.Page" />.
        /// </summary>
        /// <returns>An awaitable Task&lt;Page&gt;, indicating the PopModalAsync completion. The Task.Result is the Page that has been popped.</returns>
        public Task PopModalAsync()
        {
            return Application.Current.MainPage.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Asynchronously dismisses the most recent modally presented <see cref="T:Xamarin.Forms.Page" />, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>An awaitable Task&lt;Page&gt;, indicating the PopModalAsync completion. The Task.Result is the Page that has been popped.</returns>
        public Task PopModalAsync(bool animated)
        {
            return Application.Current.MainPage.Navigation.PopModalAsync(animated);
        }

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page" /> off the navigation stack.
        /// </summary>
        /// <returns>A task representing the asynchronous dismiss operation.</returns>
        public Task PopToRootAsync()
        {
            return Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page" /> off the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>A task representing the asynchronous dismiss operation.</returns>
        public Task PopToRootAsync(bool animated)
        {
            return Application.Current.MainPage.Navigation.PopToRootAsync(animated);
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync(Type viewModel)
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(viewModel));
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync(Type viewModel, bool animated)
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(viewModel), animated);
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync(Type viewModel, params object[] parameter)
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(viewModel, parameter));
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync(Type viewModel, bool animated, params object[] parameter)
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(viewModel, parameter), animated);
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(typeof(TViewModel)));
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync<TViewModel>(bool animated)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(typeof(TViewModel)), animated);
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(typeof(TViewModel), parameter));
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushAsync<TViewModel>(bool animated, params object[] parameter)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushAsync(CreateAndBindPage(typeof(TViewModel), parameter), animated);
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushAsync<TViewModel, TResult>()
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), tcs: taskCompletionSource));
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushAsync<TViewModel, TResult>(bool animated)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), tcs: taskCompletionSource), animated);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushAsync<TViewModel, TResult>(params object[] parameter)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter, taskCompletionSource));
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushAsync<TViewModel, TResult>(bool animated, params object[] parameter)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter, taskCompletionSource), animated);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        public Task PushModalAsync(Type viewModel)
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(viewModel));
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        public Task PushModalAsync(Type viewModel, bool animated)
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(viewModel), animated);
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        public Task PushModalAsync(Type viewModel, params object[] parameter)
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(viewModel, parameter));
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        public Task PushModalAsync(Type viewModel, bool animated, params object[] parameter)
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(viewModel, parameter), animated);
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushModalAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel)));
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushModalAsync<TViewModel>(bool animated)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel)), animated);
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushModalAsync<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter));
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task PushModalAsync<TViewModel>(bool animated, params object[] parameter)
            where TViewModel : IViewModel
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter), animated);
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushModalAsync<TViewModel, TResult>()
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), tcs: taskCompletionSource));
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushModalAsync<TViewModel, TResult>(bool animated)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), tcs: taskCompletionSource), animated);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushModalAsync<TViewModel, TResult>(params object[] parameter)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter, taskCompletionSource));
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        public Task<TResult> PushModalAsync<TViewModel, TResult>(bool animated, params object[] parameter)
            where TViewModel : IViewModel
        {
            TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
            Application.Current.MainPage.Navigation.PushModalAsync(CreateAndBindPage(typeof(TViewModel), parameter, taskCompletionSource), animated);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Gets the Page Type from the ViewModel Type.
        /// </summary>
        /// <param name="viewModelType">ViewModel Type.</param>
        /// <returns>Returns a object that represents a <see cref="Type"/>.</returns>
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation _mappings");
            }

            return _mappings[viewModelType];
        }

        /// <summary>
        /// Creates and bind the page.
        /// </summary>
        /// <param name="viewModelType">ViewModel Type.</param>
        /// <param name="parameter">Parameters.</param>
        /// <returns>Returns a object that represents a <see cref="Page"/>.</returns>
        private Page CreateAndBindPage(Type viewModelType, object[] parameter = null)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            var viewModel = ServiceLocatorProvider.Instance.Current.Resolve(viewModelType);
            if (viewModel != null)
            {
                if (viewModel is IViewModel vm)
                {
                    vm.Initialize();

                    if (parameter != null)
                    {
                        vm.OnInitialize(parameter);
                    }
                }

                page.BindingContext = viewModel;
            }

            return page;
        }

        /// <summary>
        /// Creates and bind the page.
        /// </summary>
        /// <param name="viewModelType">ViewModel Type.</param>
        /// <param name="parameter">Parameters.</param>
        /// <param name="tcs">TaskCompletionSource.</param>
        /// <returns>Returns a object that represents a <see cref="Page"/>.</returns>
        private Page CreateAndBindPage<TResult>(Type viewModelType, object[] parameter = null, TaskCompletionSource<TResult> tcs = null)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            var viewModel = ServiceLocatorProvider.Instance.Current.Resolve(viewModelType);
            if (viewModel != null)
            {
                if (viewModel is IViewModel vm)
                {
                    vm.Initialize();

                    if (parameter != null)
                    {
                        vm.OnInitialize(parameter);
                    }

                    if (tcs != null)
                    {
                        vm.OnPageResult(tcs);
                    }
                }

                page.BindingContext = viewModel;
            }

            return page;
        }
    }
}

