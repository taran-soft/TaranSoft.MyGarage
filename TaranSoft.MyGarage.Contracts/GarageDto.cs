using TaranSoft.MyGarage.Contracts.VehicleDto;

namespace TaranSoft.MyGarage.Contracts
{
    public class GarageDto : BaseEntityDto
    {
        public UserDto Owner { get; set; }

        public IList<CarDto> Cars { get; set; }
        public IList<MotocycleDto> Motorcycles { get; set; }
        public IList<TrailerDto> Trailers { get; set; }

    }
}
