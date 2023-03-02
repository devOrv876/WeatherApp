using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Infrastructure.Models;

namespace WeatherApp.Infrastructure
{
    public class WeatherAppDBContext: DbContext
    {
        public WeatherAppDBContext(DbContextOptions<WeatherAppDBContext> options):base(options){}

        
        public DbSet<FavouriteLocationDto> FavouriteLocations { get; set; }
    }
}
