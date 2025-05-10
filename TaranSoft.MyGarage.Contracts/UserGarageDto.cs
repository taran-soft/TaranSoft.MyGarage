using TaranSoft.MyGarage.Contracts.VehicleDto;

namespace TaranSoft.MyGarage.Contracts
{
    public class UserGarageDto
    {
        public UserDto UserData { get; set; }

        public IEnumerable<CarDto> Cars { get; set; }
    }
}
