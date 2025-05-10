namespace TaranSoft.MyGarage.Contracts.VehicleDto
{
    public class MotocycleDto : MotorVehicleDto
    {
        public bool HasSideCar { get; set; }
        public string? Type { get; set; }
        
    }
}
