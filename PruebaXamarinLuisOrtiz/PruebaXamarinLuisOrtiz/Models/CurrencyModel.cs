using System;
using Newtonsoft.Json;

namespace PruebaXamarinLuisOrtiz.Models
{
	public class CurrencyModel
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("value")]
		public float Value { get; set; }
	}
}

