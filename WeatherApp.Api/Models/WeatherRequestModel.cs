using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Api.Models
{
    public class WeatherRequestModel
    {
        [Required(ErrorMessage = "Enter A Valid Latitude")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Enter A Valid Longitude")]
        public double Longitude { get; set; }

    }
}
