using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Manufacturers")]
    public class Manufacturer
    {
        public long Id { get; set; }
        public ManufacturerEnum ManufacturerName { get; set; }

        // Navigation property
        public ICollection<Car> Cars { get; set; }
    }
}
