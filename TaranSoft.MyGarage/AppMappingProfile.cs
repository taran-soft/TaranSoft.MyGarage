using AutoMapper;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Controllers.Request;

namespace MyGarage;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<UpdateUserRequest, UserDto>();
        CreateMap<TaranSoft.MyGarage.Data.Models.EF.Car, CarDto>().ReverseMap();
    }
}