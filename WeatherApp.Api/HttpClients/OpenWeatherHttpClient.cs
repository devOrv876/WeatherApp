using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.HttpClients
{
    public class OpenWeatherHttpClient: IOpenWeatherHttpClient
    {
        private readonly HttpClient _httpClient;

        private const string APIKEY = "112a15bc6fdd7f12cac09a3f9d471d77";


        public OpenWeatherHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<OpenWeatherResponseModel> GetWeatherForLocationAsync(string latitude,string longitude)
        {
            try
            {
                string requeststring = $"onecall?lat={latitude}&lon={longitude}&exclude=hourly,daily&appid={APIKEY}";

                var response = await SendRequest(requeststring);

                var content = await response.Content.ReadAsStringAsync();

                var weatherData = JsonConvert.DeserializeObject<OpenWeatherResponseModel>(content);

                return weatherData;
            }
            catch (Exception ex)
            {
                //TODO: Logger ex
                throw;
            }

        }

        private async Task<HttpResponseMessage> SendRequest(string httpRequest)
        {
                var response = await _httpClient.GetAsync(httpRequest);
                return response.EnsureSuccessStatusCode();
        }

    }
}
