using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Aplication.Models;
using WeatherApp.Applcation.HttpClients.Interfaces;

namespace WeatherApp.Application.HttpClients
{
    public class OpenWeatherHttpClient: HttpClientBase, IOpenWeatherHttpClient
    {
        private readonly ILogger<OpenWeatherHttpClient> _logger;
        private const string APIKEY = "112a15bc6fdd7f12cac09a3f9d471d77";


        public OpenWeatherHttpClient(HttpClient httpClient, ILogger<OpenWeatherHttpClient> logger):base(httpClient)
        {
            _logger = logger;
        }


        public async Task<OpenWeatherResponseModel> GetWeatherForLocationAsync(double latitude,double longitude)
        {
            try
            {
                string requeststring = $"onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={APIKEY}";

                var response = await SendRequest(requeststring);

                var content = await response.Content.ReadAsStringAsync();

                var weatherData = JsonConvert.DeserializeObject<OpenWeatherResponseModel>(content);

                return weatherData;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }

        }


    }
}
