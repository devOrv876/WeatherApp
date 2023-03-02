using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Api.HttpClients;
using WeatherApp.Api.HttpClients.Interfaces;
using WeatherApp.Api.Middleware;
using WeatherApp.Api.Services;
using WeatherApp.Api.Services.Interfaces;
using WeatherApp.Infrastructure;
using WeatherApp.Infrastructure.Interfaces;
using WeatherApp.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(log =>
{
    log.LoggingFields = HttpLoggingFields.All;
    log.RequestHeaders.Add("wea-tha");
    log.ResponseHeaders.Add("weahter-header");
    log.MediaTypeOptions.AddText("application/json");
    log.RequestBodyLogLimit = 4096;
    log.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(c =>
{
    c.Filters.Add(typeof(ValidateModelStateFilter));
});

//TypedHttpClient
builder.Services.AddHttpClient<IOpenWeatherHttpClient, OpenWeatherHttpClient>(x =>
{
    x.BaseAddress = new Uri("https://api.openweathermap.org/data/3.0/");
});

builder.Services.AddResponseCaching();

//Services
builder.Services.AddTransient<IOpenWeatherApiService, OpenWeatherApiService>();

builder.Services.AddTransient<IFavouriteWeatherLocationsService, FavouriteWeatherLocationsService>();

builder.Services.AddTransient<IFavouriteLocationsRepository, FavouriteLocationsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "WeatherApp", Version = "v1" }));

//DBContext
var sqlConnectionString = builder.Configuration.GetConnectionString("WeatherApp");

builder.Services.AddDbContext<WeatherAppDBContext>(options => options.UseSqlServer(sqlConnectionString)); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ApiTrafficMiddleware>();

app.Run();
