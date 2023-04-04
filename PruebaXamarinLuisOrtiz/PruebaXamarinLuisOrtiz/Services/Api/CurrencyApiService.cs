using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PruebaXamarinLuisOrtiz.Helpers;
using PruebaXamarinLuisOrtiz.Models;

namespace PruebaXamarinLuisOrtiz.Services.Api
{
	public class CurrencyApiService : ICurrencyApiService
	{
		private readonly IApiClientService _apiClientService;

        private DateTime _lastQueryDone;

		public CurrencyApiService(IApiClientService apiClientService)
		{
			_apiClientService = apiClientService;
		}

        public async Task<List<CurrencyModel>> GetLatestCurrencyExchange()
        {
            try
            {
                var response = await _apiClientService.GetAsync(GlobalConstants.ApiUrl + "latest");

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }
                else
                {
                    var newResponse = JsonConvert.DeserializeObject<LatestExchangeResponseModel>(response);

                    _lastQueryDone = newResponse.Meta.Values.FirstOrDefault();

                    return newResponse.Data.Values.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while requesting data from API");
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<CurrencyDetailModel> GetCurrencyDetails(string currency)
        {
            try
            {
                var response = await _apiClientService.GetAsync($"{GlobalConstants.ApiUrl}currencies?currencies={currency}");

                if (string.IsNullOrEmpty(response))
                {
                    return null;
                }
                else
                {
                    var newResponse = JsonConvert.DeserializeObject<CurrencyResponseModel>(response);

                    return newResponse.data.Values.ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception while requesting data from API");
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}

