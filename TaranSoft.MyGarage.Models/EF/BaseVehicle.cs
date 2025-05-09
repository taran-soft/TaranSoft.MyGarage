using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Vehicle")]
    public class Vehicle : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public string? Model { get; set; }
        public string? Trim { get; set; }
        public string? Engine { get; set; }
        public string? Year { get; set; }

        // Foreign key
        public long ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
