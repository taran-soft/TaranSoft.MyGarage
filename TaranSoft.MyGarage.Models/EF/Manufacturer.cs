using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Manufacturers")]
    public class Manufacturer
    {
        public ManufacturerEnum ManufacturerName { get; set; }
        public Country Country { get; set; }
    }
}
