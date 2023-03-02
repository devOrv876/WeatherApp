using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Api.Commands
{
    public class UpdateFavouriteCommand
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude{ get; set; }
        [Required]
        public string Name { get; set; } = "";
        
    }
}
