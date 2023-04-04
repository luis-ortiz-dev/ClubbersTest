using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PruebaXamarinLuisOrtiz.Models
{
	public class LatestExchangeResponseModel
	{
		[JsonProperty("meta")]
        public Dictionary<string, DateTime> Meta { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, CurrencyModel> Data { get; set; }
    }
}

