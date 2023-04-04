using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PruebaXamarinLuisOrtiz.Helpers;
using PruebaXamarinLuisOrtiz.Models;
using PruebaXamarinLuisOrtiz.Services.Api;
using PruebaXamarinLuisOrtiz.ViewModels.Base;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PruebaXamarinLuisOrtiz.ViewModels
{
	public class CurrencyPageViewModel : BaseViewModel
	{
		private readonly ICurrencyApiService _currencyApiService;

        private CurrencyModel _selectedCurrency;
        private ObservableCollection<CurrencyModel> _currencyList;

        public CurrencyModel SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        public ObservableCollection<CurrencyModel> CurrencyList
        {
            get => _currencyList;
            set => SetProperty(ref _currencyList, value);
        }

		public ICommand GetLatestExchangeCommand { get; set; }
        public ICommand GoToCurrencyDetailsCommand { get; set; }

		public CurrencyPageViewModel(ICurrencyApiService currencyApiService)
		{
			_currencyApiService = currencyApiService;
		}

        public override void InitializeCommands()
        {
            base.InitializeCommands();
            GetLatestExchangeCommand = new AsyncCommand(ExecuteGetLatestExchangeCommand);
            GoToCurrencyDetailsCommand = new AsyncCommand(ExecuteGoToCurrencyDetailsCommand);
        }

        public override void Initialize()
        {
            base.Initialize();
			GetLatestExchangeCommand.Execute(null);
        }

        private async Task ExecuteGetLatestExchangeCommand()
        {
            try
            {
                if (CurrencyList != null &&
                    CurrencyList.Any())
                {
                    CurrencyList.Clear();
                }

                var response = await _currencyApiService.GetLatestCurrencyExchange();

                if (response == null ||
                    !response.Any())
                {
                    Debug.WriteLine("Response of GetLatestCurrencyExchange was null or empty");
                    return;
                }

                CurrencyList = new ObservableCollection<CurrencyModel>(response.Take(10).ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while requesting data from CurrencyApiService");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteGoToCurrencyDetailsCommand()
        {
            if (SelectedCurrency == null)
            {
                return;
            }

            try
            {
                await _navigationService.ShellGoToAsync($"{GlobalConstants.CurrencyDetailRoute}", SelectedCurrency.Code);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error navigating to {GlobalConstants.CurrencyDetailRoute}");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}

