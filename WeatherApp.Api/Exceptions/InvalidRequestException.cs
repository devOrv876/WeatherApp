namespace WeatherApp.Api.Exceptions
{
    public class InvalidRequestException: Exception
    {
        public InvalidRequestException(string message):base(message)
        {
        }
    }
}
