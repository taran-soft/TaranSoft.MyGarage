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


var origins = appSettings.AllowedCORSOrignis;

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

//AddAuthentication(builder, appSettings);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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
    //builder.Services.AddScoped<IUserRepository, TaranSoft.MyGarage.Repository.EntityFramework.UserRepository>();

    //using var context = new MainDbContext();

    //if (!context.Manufacturers.Any())
    //{
    //    var toyota = new Manufacturer { ManufacturerName = ManufacturerEnum.Toyota };
    //    var tesla = new Manufacturer { ManufacturerName = ManufacturerEnum.Tesla };

    //    context.Manufacturers.AddRange(toyota, tesla);
    //    context.SaveChanges();

    //    context.Cars.Add(new TaranSoft.MyGarage.Data.Models.EF.Car
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Model S",
    //        ManufacturerId = tesla.Id
    //    });

    //    context.Cars.Add(new TaranSoft.MyGarage.Data.Models.EF.Car
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Corolla Sedan",
    //        ManufacturerId = toyota.Id
    //    });

    //    context.SaveChanges();
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