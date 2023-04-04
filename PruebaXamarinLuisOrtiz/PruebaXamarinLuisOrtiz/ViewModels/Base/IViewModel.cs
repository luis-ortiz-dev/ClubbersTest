using System;
using System.Threading.Tasks;

namespace PruebaXamarinLuisOrtiz.ViewModels.Base
{
    public interface IViewModel
    {
        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the Page becoming visible.
        /// </summary>
        void OnAppearing();

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the Page disappears.
        /// </summary>
        void OnDisappearing();

        /// <summary>
        /// When overriden, allows the appliaction developer to customize behavior when navigates to this page. It runs after Ctor and after OnAppearing.
        /// </summary>
        /// <param name="parameter">The parameter received.</param>
        /// <typeparam name="TParam">Pass any type of object as parameter.</typeparam>
        void OnNavigating<TParam>(TParam parameter);

        /// <summary>
        /// When overriden, allows the appliaction developer to customize behavior when navigates back to this page. It runs after Ctor and after OnAppearing.
        /// </summary>
        /// <param name="paremeter">The parameter received while navigating back.</param>
        /// <typeparam name="TParam">Pass any type of object as parameter.</typeparam>
        void OnNavigatingBack<TParam>(TParam paremeter);

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>Whether or not the back navigation was handled by the override.</returns>
        bool OnBackButtonPressed();

        /// <summary>
        /// Initializes the View Model.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes the Commands.
        /// </summary>
        void InitializeCommands();

        /// <summary>
        /// Implement this method to initialize the ViewModel data.
        /// </summary>
        /// <param name="args">Used to pass optional parameters.</param>
        void OnInitialize(params object[] args);

        /// <summary>
        /// Sets the TaskCompletionSource.
        /// </summary>
        /// <typeparam name="T">Used to sepecify the Generic.</typeparam>
        /// <param name="tcs">Used to specify the TaskCompletionSource.</param>
        void OnPageResult<T>(TaskCompletionSource<T> tcs);
    }
}

