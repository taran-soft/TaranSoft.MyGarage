namespace TaranSoft.MyGarage.Contracts
{
    public class CarDto : BaseEntityDto
    {
        public string Name { get; set; }

        public ManufacturerDto Manufacturer { get; set; }
    }
}
