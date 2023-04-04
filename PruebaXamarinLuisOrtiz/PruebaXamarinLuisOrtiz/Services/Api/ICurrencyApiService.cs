using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PruebaXamarinLuisOrtiz.Models;

namespace PruebaXamarinLuisOrtiz.Services.Api
{
	public interface ICurrencyApiService
	{
		Task<List<CurrencyModel>> GetLatestCurrencyExchange();

		Task<CurrencyDetailModel> GetCurrencyDetails(string currency);
	}
}

