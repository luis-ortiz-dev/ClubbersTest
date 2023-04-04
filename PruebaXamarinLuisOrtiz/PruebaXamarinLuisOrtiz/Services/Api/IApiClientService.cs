using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaXamarinLuisOrtiz.Services.Api
{
	public interface IApiClientService
	{
        Task<string> GetAsync(string url);
    }
}

