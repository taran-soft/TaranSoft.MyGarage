using System.ComponentModel.DataAnnotations;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}
