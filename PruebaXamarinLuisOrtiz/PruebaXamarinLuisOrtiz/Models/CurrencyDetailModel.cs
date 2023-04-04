using System;
using Newtonsoft.Json;

namespace PruebaXamarinLuisOrtiz.Models
{
	public class CurrencyDetailModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_plural")]
        public string PluralName { get; set; }

        [JsonProperty("symbol_native")]
        public string SymbolNative { get; set; }

        [JsonProperty("decimal_digits")]
        public string DecimalDigits { get; set; }

        [JsonProperty("rounding")]
        public string Rounding { get; set; }
    }
}