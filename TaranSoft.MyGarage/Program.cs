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
using TaranSoft.MyGarage;
using TaranSoft.MyGarage.Repository.Interfaces.EF;

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
builder.Services.AddScoped<IJournalsService, JournalsService>();
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
    MockData.SetupMockData(context);
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

    builder.Services.AddScoped<IEFCarsRepository, CarsRepository>();
    builder.Services.AddScoped<IEFGaragesRepository, GaragesRepository>();
    builder.Services.AddScoped<IEFJournalsRepository, JournalsRepository>();
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

