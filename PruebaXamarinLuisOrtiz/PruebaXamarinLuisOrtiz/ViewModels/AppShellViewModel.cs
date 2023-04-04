using System;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace PruebaXamarinLuisOrtiz.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand NavigateCommand { get; private set; }

        public override void InitializeCommands()
        {
            NavigateCommand = new Command<Type>(NavigateAsync);
            base.InitializeCommands();
        }

        /// <summary>
        /// Navigate to the proper view.
        /// </summary>
        /// <param name="viewModel">Used to specify the ViewModel.</param>
        private void NavigateAsync(Type viewModel)
        {
            _navigationService?.CreateNavigation(viewModel);
        }
    }
}

