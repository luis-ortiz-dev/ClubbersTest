using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PruebaXamarinLuisOrtiz.Helpers;
using Xamarin.Essentials;

namespace PruebaXamarinLuisOrtiz.Services.Api
{
	public class ApiClientService : IApiClientService
	{
        private static HttpClient _httpClient;
        protected HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(GlobalConstants.ApiUrl),
                        Timeout = TimeSpan.FromSeconds(20)
                    };
                }

                return _httpClient;
            }
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage httpClientResponse = null;

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    throw new Exception("No internet connection");
                }

                if (!HttpClient.DefaultRequestHeaders.Contains(GlobalConstants.HeaderName))
                {
                    HttpClient.DefaultRequestHeaders.Add
                        (GlobalConstants.HeaderName, GlobalConstants.ApiKey);
                }

                httpClientResponse = await HttpClient.GetAsync(url).ConfigureAwait(false);

                HttpClient.DefaultRequestHeaders.Remove(GlobalConstants.HeaderName);

                if (httpClientResponse.IsSuccessStatusCode)
                {
                    return await httpClientResponse.Content.ReadAsStringAsync();
                }

                if (httpClientResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                if (httpClientResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return await GetAsync(url);
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

