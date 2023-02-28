using WeatherApp.Api.HttpClients;
using WeatherApp.Api.Services;
using WeatherApp.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TypedHttpClient
builder.Services.AddHttpClient<IOpenWeatherHttpClient, OpenWeatherHttpClient>();

//Services
builder.Services.AddTransient<IOpenWeatherApiService, OpenWeatherApiService>();

builder.Services.AddTransient<IFavouriteWeatherLocationsService, FavouriteWeatherLocationsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "WeatherApp", Version = "v1" }));

//DBContext
//var sqlConnectionString = builder.Configuration.GetConnectionString("VehicleManagementCN");
//builder.Services.AddDbContext<VehicleManagementDBContext>(options => options.UseSqlServer(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
