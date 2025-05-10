using AutoMapper;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Contracts.VehicleDto;
using TaranSoft.MyGarage.Controllers.Request;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;

namespace MyGarage;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<UpdateUserRequest, UserDto>();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.User, UserDto>().ReverseMap();

        
        // Dto mappings
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Motorcycle, MotocycleDto>().ReverseMap();
        CreateMap<Trailer, TrailerDto>().ReverseMap();
        
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.UserGarage, GarageDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Manufacturer, ManufacturerDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Country, CountryDto>().ReverseMap();
    }
}