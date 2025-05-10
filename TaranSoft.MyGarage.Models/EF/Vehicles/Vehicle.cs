using System.ComponentModel.DataAnnotations;

namespace TaranSoft.MyGarage.Data.Models.EF.Vehicles
{
    public abstract class Vehicle : BaseAuditableEntity
    {
        [Required]
        public required string Name { get; set; }

        public string? Model { get; set; }
        public string? Year { get; set; }
        public float? Weight { get; set; }
        public string? VIN { get; set; }

        
        [MaxLength(50)]
        public string LicensePlate { get; set; }

        public long GarageId { get; set; }
        public UserGarage Garage { get; set; }

        // Foreign key
        public long ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
