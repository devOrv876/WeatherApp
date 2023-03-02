namespace WeatherApp.Api.Services.Interfaces
{
    public interface IFavouriteWeatherLocationsService
    {
        Task<List<FavouriteLocation>> GetFavouriteLocationsAsync();
        Task<FavouriteLocation> GetFavouriteLocationByIdAsync(Guid id);
        Task<FavouriteLocation> AddToFavouritesAsync(double latitude, double longititude, string city);
        Task<FavouriteLocation> UpdateFavouriteAsync(Guid id, double latitude, double longititude, string city);
        Task<bool> RemoveFromFavouritesAync(Guid id);
    }
}
