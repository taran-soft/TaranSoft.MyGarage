using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF.Vehicles
{
    [Table("Motorcycles")]
    public class Motorcycle : MotorVehicle
    {
        public bool HasSideCar { get; set; }
        public string? Type { get; set; }
    }
}