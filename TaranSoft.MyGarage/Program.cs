using System.Text;
using MyGarage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyGarage.Common;
using TaranSoft.MyGarage.Services.Interfaces;
using TaranSoft.MyGarage.Services;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Repository.MongoDB.DbContext;
using TaranSoft.MyGarage.Data.Models.MongoDB;
using TaranSoft.MyGarage.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using TaranSoft.MyGarage.Data.Models.EF;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;

var builder = WebApplication.CreateBuilder(args);

var useMongoDB = builder.Configuration.GetValue<bool>("UseMongoDB");
if (useMongoDB)
{
    UseMongoDB(builder);
}
else
{
    UseMsSQL(builder);
}


builder.Services.AddTransient<IPasswordHasher<TaranSoft.MyGarage.Services.Models.User>, PasswordHasher<TaranSoft.MyGarage.Services.Models.User>>();
builder.Services.AddTransient<IIdGenerator, CustomIdGenerator>();


//builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IGarageService, GarageService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Garage API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();


var origins = appSettings?.AllowedCORSOrignis;
if (origins != null) 
{
    builder.Services.AddCors(setup =>
    {
        setup.AddPolicy("policy",
            config =>
            {
                config
                    .WithOrigins(origins)
                    .WithHeaders("Origin", "X-Requested-Width", "Content-Type", "Accept", "Authorization")
                    .WithMethods("GET", "POST", "PUT", "DELETE");
            });
    });

}


//AddAuthentication(builder, appSettings);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MainDbContext>();

    // Make sure DB is created
    context.Database.Migrate();

    // Seed data
    SetupMockData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarGarage API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("policy");

app.Run();

static void UseMongoDB(WebApplicationBuilder builder)
{
    builder.Services.Configure<Settings>(builder.Configuration.GetSection("MongoDB"));
    builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

    builder.Services.AddScoped<ICarsRepository, TaranSoft.MyGarage.Repository.MongoDB.CarsRepository>();
    builder.Services.AddScoped<IUserRepository, TaranSoft.MyGarage.Repository.MongoDB.UserRepository>();
}

static void UseMsSQL(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IEFCarsRepository, TaranSoft.MyGarage.Repository.EntityFramework.CarsRepository>();
    builder.Services.AddScoped<IEFGaragesRepository, TaranSoft.MyGarage.Repository.EntityFramework.GaragesRepository>();
    //builder.Services.AddScoped<IUserRepository, TaranSoft.MyGarage.Repository.EntityFramework.UserRepository>();
    
}

static void AddAuthentication(WebApplicationBuilder builder, AppSettings appSettings)
{
    var key = Encoding.ASCII.GetBytes(appSettings.JWTSecretKey);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

static void SetupMockData(MainDbContext context)
{
    if (context.Garages.Any())
        return; // Already seeded

    // Create Countries
    var country1 = new Country { Name = "United States of America", CountryCode = "USA"};
    var country2 = new Country { Name = "Japan", CountryCode = "JP"};

    
    // Create Manufacturer
    var manufacturerDodge = new Manufacturer { ManufacturerCountry = country1, ManufacturerName = ManufacturerEnum.Dodge, YearCreation = 1986 };
    var manufacturerToyota = new Manufacturer { ManufacturerCountry = country2, ManufacturerName = ManufacturerEnum.Toyota, YearCreation = 1901 };
    var manufacturerSuzuki = new Manufacturer { ManufacturerCountry = country2, ManufacturerName = ManufacturerEnum.Suzuki, YearCreation = 1964 };
    var manufacturerKawasaki = new Manufacturer { ManufacturerCountry = country2, ManufacturerName = ManufacturerEnum.Kawasaki, YearCreation = 1950 };
    var manufacturerLev = new Manufacturer { ManufacturerCountry = country2, ManufacturerName = ManufacturerEnum.Kawasaki, YearCreation = 1950 };

    //Create Users
    var user1 = new TaranSoft.MyGarage.Data.Models.EF.User { Name = "Vladyslav", Surname = "Tar", Nickname = "Vynd", DriverExperience = 10, Email = "vlad@gmail.com", Phone = "1234567890", Gender = TaranSoft.MyGarage.Data.Models.MongoDB.GenderEnum.Male, CreatedAt = DateTime.UtcNow };
    var user2 = new TaranSoft.MyGarage.Data.Models.EF.User { Name = "Serg", Surname = "Postkevich",Nickname = "Geek", DriverExperience = 6, Email = "geek@gmail.com", Phone = "123654787", Gender = TaranSoft.MyGarage.Data.Models.MongoDB.GenderEnum.Male, CreatedAt = DateTime.UtcNow };


    // Create a Common entity
    var garage1 = new TaranSoft.MyGarage.Data.Models.EF.UserGarage { Owner = user1, CreatedAt = DateTime.UtcNow };
    var garage2 = new TaranSoft.MyGarage.Data.Models.EF.UserGarage { Owner = user2, CreatedAt = DateTime.UtcNow };

    // Create some Bs
    var b1 = new TaranSoft.MyGarage.Data.Models.EF.Vehicles.Car 
    {
        Name = "Toyota Tundra",
        Year = "2017",
        Body = "Sedan",
        Manufacturer = manufacturerToyota,
        Garage = garage1,
        LicensePlate = "AI6478KK",
        CreatedAt = DateTime.UtcNow,
        Engine = "5000cc",
        HorsePower = 250,
        NumberOfDoors = 5,
        Transmission = TransmitionTypes.Automatic,
        FuelType = FuelTypes.Disel,
        Trim = "Pickup",
        Weight = 3500,
        VIN = "EU2222222222222222"
    };
    var b2 = new TaranSoft.MyGarage.Data.Models.EF.Vehicles.Car 
    {
        Garage = garage2,
        Name = "Suzuki Jimny",
        Year = "2008",
        Body = "Hatchback",
        Manufacturer = manufacturerSuzuki,
        LicensePlate = "AE2654YB",
        CreatedAt = DateTime.UtcNow,
        Engine = "1600cc",
        HorsePower = 130,
        NumberOfDoors = 5,
        Transmission = TransmitionTypes.Manual,
        FuelType = FuelTypes.Gasoline,
        Trim = "Crossover",
        Weight = 2050,
        VIN = "EU123192378123712371"
    };
    var b3 = new TaranSoft.MyGarage.Data.Models.EF.Vehicles.Car
    {
        Garage = garage1,
        Name = "Dodge Journey",
        Year = "2014",
        Body = "Crossover",
        Manufacturer = manufacturerDodge,
        LicensePlate = "KY1564YA",
        CreatedAt = DateTime.UtcNow,
        Engine = "2500cc",
        HorsePower = 180,
        NumberOfDoors = 4,
        Transmission = TransmitionTypes.CVT,
        FuelType = FuelTypes.LPGAndGasoline,
        Trim = "Crossover",
        Weight = 2050,
        VIN = "EU123192378123712371"
    };

    // Create motos
    var c1 = new Motorcycle { Garage = garage1, Name = "Kawasaki", Year = "2020", Manufacturer = manufacturerKawasaki, LicensePlate = "AE123", Type = "Sport", CreatedAt = DateTime.UtcNow, HorsePower = 50, HasSideCar = true };
    var c2 = new Motorcycle { Garage = garage2 , Name = "Suzuki Bandit 1.8", Year = "2005", Manufacturer = manufacturerSuzuki, LicensePlate = "AE133", Type = "Cross", CreatedAt = DateTime.UtcNow };

    // Create trailers
    var t1 = new Trailer { Garage = garage1, Name = "Custom Trailer", Year = "2006", Manufacturer = manufacturerLev, LicensePlate = "AE8975XM", CreatedAt = DateTime.UtcNow, HasBrakes = true, LengthMeters = 32, LoadCapacityKg = 1000, NumberOfAxles = 2, TrailerType = "Flatbad" };

    // Add everything via Common
    context.Countries.Add(country1);
    context.Countries.Add(country2);

    context.Manufacturers.Add(manufacturerDodge);
    context.Manufacturers.Add(manufacturerToyota);
    context.Manufacturers.Add(manufacturerSuzuki);
    context.Manufacturers.Add(manufacturerKawasaki);
    context.Manufacturers.Add(manufacturerLev);

    context.Users.Add(user1);
    context.Users.Add(user2);

    context.Cars.Add(b1);
    context.Cars.Add(b2);
    context.Cars.Add(b3);

    context.Motorcycles.Add(c1);
    context.Motorcycles.Add(c2);
    
    context.Trailers.Add(t1);

    context.Garages.Add(garage1);
    context.Garages.Add(garage2);

    context.SaveChanges();
}

