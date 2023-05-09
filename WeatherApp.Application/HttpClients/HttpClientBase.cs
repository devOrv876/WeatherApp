namespace WeatherApp.Application.HttpClients
{
    public class HttpClientBase
    {
        private readonly HttpClient _httpClient;
        public HttpClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<HttpResponseMessage> SendRequest(string httpRequest)
        {
            var response = await _httpClient.GetAsync(httpRequest);
            return response.EnsureSuccessStatusCode();
        }
    }
}
