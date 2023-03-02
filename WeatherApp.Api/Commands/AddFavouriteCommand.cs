using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Api.Commands
{
    public class AddFavouriteCommand
    {
        [JsonProperty]
        [Required]
        public double Latitude { get; set; }
        [JsonProperty]
        [Required]
        public double Longitude { get; set; }
        [JsonProperty]
        [Required]
        public string Name { get; set; } = "";

        [JsonConstructor]
        public AddFavouriteCommand(double latitude, double longitude, string name)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
        }
    }
}
