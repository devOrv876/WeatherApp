namespace WeatherApp.Api.Services.Interfaces
{
    public interface IFavouriteWeatherLocationsService
    {
        Task<List<FavouriteLocation>> GetFavouriteLocations();
        
        Task AddToFavourites(string latitude, string longititude, string city);

        Task RemoveFromFavourites(string latitude, string longititude, string city);
    }
}
