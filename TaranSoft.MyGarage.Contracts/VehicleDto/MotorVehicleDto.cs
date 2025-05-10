namespace TaranSoft.MyGarage.Contracts.VehicleDto
{
    public class MotorVehicleDto : VehicleDto
    {
        public string? Engine { get; set; }
        public int? HorsePower { get; set; }
        public string? Transmission { get; set; }
    }
}
