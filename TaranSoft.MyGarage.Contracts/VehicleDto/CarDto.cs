namespace TaranSoft.MyGarage.Contracts.VehicleDto
{
    public class CarDto : MotorVehicleDto
    {
        public string Name { get; set; }
        public string? Trim { get; set; }
        public string? Body { get; set; }
        public int NumberOfDoors { get; set; }
    }
}
