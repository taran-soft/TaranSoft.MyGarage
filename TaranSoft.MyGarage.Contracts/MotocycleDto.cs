namespace TaranSoft.MyGarage.Contracts
{
    public class MotocycleDto : BaseEntityDto
    {
        public string Name { get; set; }

        public ManufacturerDto Manufacturer { get; set; }
    }
}
