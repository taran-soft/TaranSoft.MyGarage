using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Motorcycles")]
    public class Motorcycle : Vehicle
    {
        // Foreign key
        public Guid GarageId { get; set; }
        public UserGarage Garage { get; set; }
    }
}