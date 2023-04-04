using System;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.Services.ServiceLocator
{
	public static class PageHelper
	{
        /// <summary>
        /// Gets the View Model.
        /// </summary>
        /// <typeparam name="TViewModel">The view model type.</typeparam>
        /// <param name="contentPage">The page.</param>
        /// <returns>The View model.</returns>
        public static TViewModel GetViewModel<TViewModel>(this ContentPage contentPage)
        {
            TViewModel viewModel = ServiceLocatorProvider.Instance.Current.Resolve<TViewModel>();
            if (viewModel is IViewModel vm)
            {
                vm.Initialize();
            }

            return viewModel;
        }
    }
}

