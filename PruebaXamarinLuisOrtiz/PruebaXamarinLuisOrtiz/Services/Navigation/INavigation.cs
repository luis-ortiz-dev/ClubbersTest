using System;
using System.Threading.Tasks;
using PruebaXamarinLuisOrtiz.ViewModels.Base;

namespace PruebaXamarinLuisOrtiz.Services.Navigation
{
    public interface INavigationService
    {
        /// <summary>
        /// Registers the ViewModel-View Mapping.
        /// </summary>
        /// <param name="viewModel">Used to specify the ViewModel.</param>
        /// <param name="view">Used to specify the View.</param>
        void RegisterViewModelMapping(Type viewModel, Type view);

        /// <summary>
        /// Registers the ViewModel-View Mapping.
        /// </summary>
        /// <typeparam name="TViewModel">Used to specify the ViewModel.</typeparam>
        /// <typeparam name="TView">Used to specify the View.</typeparam>
        void RegisterViewModelMapping<TViewModel, TView>();

        /// <summary>
        /// Sets the Root Navigation from the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel for the Root.</param>
        /// <param name="parameter">The parameters for the RootPage.</param>
        void RootNavigation(Type viewModel, params object[] parameter);

        /// <summary>
        /// Sets the Root Navigation from the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type.</typeparam>
        /// <param name="parameter">The parameters for the RootPage.</param>
        void RootNavigation<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Creates a <see cref="T:Xamarin.Forms.NavigationPage" /> from the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel for the NavigationPage.</param>
        /// <param name="parameter">The parameters for the NavigationPage.</param>
        void CreateNavigation(Type viewModel, params object[] parameter);

        /// <summary>
        /// Creates a <see cref="T:Xamarin.Forms.NavigationPage" /> from the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel Type.</typeparam>
        /// <param name="parameter">The parameters for the NavigationPage.</param>
        void CreateNavigation<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="TPage">TPage.</typeparam>
        /// <typeparam name="TParam">TParam.</typeparam>
        /// <param name="route">Route.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        Task ShellGoToAsync<TPage, TParam>(string route = null, TParam parameter = default, bool animated = true);

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <typeparam name="TParam">TParam.</typeparam>
        /// <param name="route">Route.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        Task ShellGoToAsync<TParam>(string route, TParam parameter = default, bool animated = true);

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="route">Route.</param>
        /// <param name="animated">Animated.</param>
        /// <returns>The Task.</returns>
        Task ShellGoToAsync(string route, bool animated = true);

        /// <summary>
        /// Asynchronously removes the most recent <see cref="T:Xamarin.Forms.Page" /> from the navigation stack.
        /// </summary>
        /// <returns>The <see cref="T:Xamarin.Forms.Page" /> that had been at the top of the navigation stack.</returns>
        Task PopAsync();

        /// <summary>
        /// Asynchronously removes the most recent <see cref="T:Xamarin.Forms.Page" /> from the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>The <see cref="T:Xamarin.Forms.Page" /> that had been at the top of the navigation stack.</returns>
        Task PopAsync(bool animated);

        /// <summary>
        /// Asynchronously dismisses the most recent modally presented <see cref="T:Xamarin.Forms.Page" />.
        /// </summary>
        /// <returns>An awaitable Task&lt;Page&gt;, indicating the PopModalAsync completion. The Task.Result is the Page that has been popped.</returns>
        Task PopModalAsync();

        /// <summary>
        /// Asynchronously dismisses the most recent modally presented <see cref="T:Xamarin.Forms.Page" />, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>An awaitable Task&lt;Page&gt;, indicating the PopModalAsync completion. The Task.Result is the Page that has been popped.</returns>
        Task PopModalAsync(bool animated);

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page" /> off the navigation stack.
        /// </summary>
        /// <returns>A task representing the asynchronous dismiss operation.</returns>
        Task PopToRootAsync();

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page" /> off the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Whether to animate the pop.</param>
        /// <returns>A task representing the asynchronous dismiss operation.</returns>
        Task PopToRootAsync(bool animated);

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync(Type viewModel);

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync(Type viewModel, bool animated);

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync(Type viewModel, params object[] parameter);

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModel">The ViewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync(Type viewModel, bool animated, params object[] parameter);

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync<TViewModel>()
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync<TViewModel>(bool animated)
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushAsync<TViewModel>(bool animated, params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushAsync<TViewModel, TResult>()
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushAsync<TViewModel, TResult>(bool animated)
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushAsync<TViewModel, TResult>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Asynchronously adds a <see cref="T:Xamarin.Forms.Page" /> to the top of the navigation stack.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushAsync<TViewModel, TResult>(bool animated, params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        Task PushModalAsync(Type viewModel);

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        Task PushModalAsync(Type viewModel, bool animated);

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        Task PushModalAsync(Type viewModel, params object[] parameter);

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <param name="viewModel">The viewModel to push.</param>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>An awaitable Task, indicating the PushModal completion.</returns>
        Task PushModalAsync(Type viewModel, bool animated, params object[] parameter);

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushModalAsync<TViewModel>()
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushModalAsync<TViewModel>(bool animated)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushModalAsync<TViewModel>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task PushModalAsync<TViewModel>(bool animated, params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushModalAsync<TViewModel, TResult>()
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushModalAsync<TViewModel, TResult>(bool animated)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushModalAsync<TViewModel, TResult>(params object[] parameter)
            where TViewModel : IViewModel;

        /// <summary>
        /// Presents a <see cref="T:Xamarin.Forms.Page" /> modally, with optional animation.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel Type.</typeparam>
        /// <typeparam name="TResult">Result Type.</typeparam>
        /// <param name="animated">Whether to animate the push.</param>
        /// <param name="parameter">The parameters for the push.</param>
        /// <returns>A task that represents the asynchronous push operation.</returns>
        Task<TResult> PushModalAsync<TViewModel, TResult>(bool animated, params object[] parameter)
            where TViewModel : IViewModel;
    }

    public interface INavigationAwaitable<T>
    {
        TaskCompletionSource<T> AwaitNavigationTask { get; set; }
    }
}


