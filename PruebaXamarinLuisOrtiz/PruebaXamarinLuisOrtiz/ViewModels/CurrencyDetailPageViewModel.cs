using System;
using PruebaXamarinLuisOrtiz.Models;
using PruebaXamarinLuisOrtiz.Services.Api;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using PruebaXamarinLuisOrtiz.ViewModels.Base;

namespace PruebaXamarinLuisOrtiz.ViewModels
{
	public class CurrencyDetailPageViewModel : BaseViewModel
    {
        private readonly ICurrencyApiService _currencyApiService;

        private string _currencyCode;
        private CurrencyDetailModel _selectedCurrency;

        public CurrencyDetailModel SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value);
        }

        public ICommand GetCurrencyDetailsCommand { get; set; }

        public CurrencyDetailPageViewModel(ICurrencyApiService currencyApiService)
        {
            _currencyApiService = currencyApiService;
        }

        public override void InitializeCommands()
        {
            base.InitializeCommands();
            GetCurrencyDetailsCommand = new AsyncCommand(ExecuteGetCurrencyDetailsCommand);
        }

        public override void OnNavigating<TParam>(TParam parameter)
        {
            base.OnNavigating(parameter);

            if (parameter is string currencyCode)
            {
                _currencyCode = currencyCode;
                GetCurrencyDetailsCommand.Execute(null);
            }
        }

        private async Task ExecuteGetCurrencyDetailsCommand()
        {
            try
            {
                var response = await _currencyApiService.GetCurrencyDetails(_currencyCode);

                if (response == null)
                {
                    Debug.Write("Response of GetCurrencyDetails was null");
                    return;
                }

                SelectedCurrency = response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while requesting data from CurrencyApiService");
                Debug.WriteLine(ex.Message);
            }
        }
    }
}

