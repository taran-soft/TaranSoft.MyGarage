using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Cars")]
    public class Car : BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }

        // Foreign key
        public long ManufacturerId { get; set; }
        public Manufacturer Manufacturer {  get; set; }
    }
}
