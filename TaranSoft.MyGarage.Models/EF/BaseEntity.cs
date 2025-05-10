using System.ComponentModel.DataAnnotations;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
