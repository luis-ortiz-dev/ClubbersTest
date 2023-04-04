using System;
using PruebaXamarinLuisOrtiz.ViewModels;

namespace PruebaXamarinLuisOrtiz.Helpers
{
	public class GlobalConstants
	{
		public const string TasksRoute = nameof(TaskPageViewModel);
		public const string CurrencyRoute = nameof(CurrencyPageViewModel);
		public const string TaskDetailRoute = nameof(TaskDetailPageViewModel);
        public const string CurrencyDetailRoute = nameof(CurrencyDetailPageViewModel);

        public const string DatabaseName = "PruebaXamarinDB";
		public const string ApiUrl = "https://api.currencyapi.com/v3/";
		public const string HeaderName = "apikey";
		public const string ApiKey = "PcACFZBUjRFJXWA5MlMqR3fPRyD7ivfXEDaXihgu";
    }
}

