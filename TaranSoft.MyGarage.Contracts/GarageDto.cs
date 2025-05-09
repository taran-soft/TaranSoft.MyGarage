namespace TaranSoft.MyGarage.Contracts
{
    public class GarageDto
    {
        public Guid Id { get; set; }
        public UserDto Owner { get; set; }

        public IList<CarDto> Cars { get; set; }
        public IList<MotocycleDto> Motocycles { get; set; }

    }
}
