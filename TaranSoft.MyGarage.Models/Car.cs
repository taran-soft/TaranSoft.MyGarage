using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Data.Models
{
    [Table("Cars")]
    public class Car : BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        public Manufacturer ManufacturerId { get; set; }
        public string Model { get; set; }

        public string Year { get; set; }

        public EF.User CreatedBy { get; set; }
        public EF.User Owner { get; set; }

        public Guid ImageId { get; set; }
    }
}
