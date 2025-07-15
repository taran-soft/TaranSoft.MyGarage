using AutoMapper;
using TaranSoft.MyGarage.Contracts.Dto;
using TaranSoft.MyGarage.Contracts.Dto.Vehicle;
using TaranSoft.MyGarage.Controllers.Request;
using TaranSoft.MyGarage.Data.Models.EF.CarJournal;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;

namespace MyGarage;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<UpdateUserRequest, UserDto>();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.User, UserDto>().ReverseMap();

        
        // Vehicle mappings
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Motorcycle, MotocycleDto>().ReverseMap();
        CreateMap<Trailer, TrailerDto>().ReverseMap();

        CreateMap<Journal, JournalDto>().ReverseMap();
        CreateMap<JournalRecord, JournalRecordDto>().ReverseMap();

        CreateMap<TaranSoft.MyGarage.Data.Models.EF.UserGarage, GarageDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Manufacturer, ManufacturerDto>().ReverseMap();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Country, CountryDto>().ReverseMap();
    }
}