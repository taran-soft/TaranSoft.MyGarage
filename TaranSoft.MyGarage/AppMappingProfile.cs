using AutoMapper;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Controllers.Request;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;

namespace MyGarage;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<UpdateUserRequest, UserDto>();
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.User, UserDto>().ReverseMap();
        CreateMap<Motorcycle, MotocycleDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.UserGarage, GarageDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Manufacturer, ManufacturerDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Country, CountryDto>().ReverseMap();
    }
}