namespace TaranSoft.MyGarage.Contracts
{
    public class MotocycleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ManufacturerDto Manufacturer { get; set; }
    }
}
