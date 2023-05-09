using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Api.Models
{
    public class AddFavouriteModel
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
    }
}
