using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Cars")]
    public class Car : Vehicle
    {
        public string? Body { get; set; }
        // Foreign key
        public Guid? GarageId { get; set; }
        public UserGarage? Garage { get; set; }
    }
}
