namespace WeatherApp.Api.Models
{
    public class WeatherModel
    {
        public string LocationName { get; set; }
        public TemperatureModel Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime Sunset { get; set; }
        public DateTime Sunrise { get; set; }
    }
}
