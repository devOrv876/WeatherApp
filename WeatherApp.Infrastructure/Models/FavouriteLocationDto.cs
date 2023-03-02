using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Infrastructure.Models
{
    public class FavouriteLocationDto
    {
        public Guid Id { get; set; }

        public string LocationName { get; set; } = "";

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
