using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageSpace.Data.Models.EF.Vehicles
{
    [Table("Motorcycles")]
    public class Motorcycle : MotorVehicle
    {
        public bool HasSideCar { get; set; }
        public string? Type { get; set; }
    }
}