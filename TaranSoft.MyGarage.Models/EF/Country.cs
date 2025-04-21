using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Countries")]
    public class Country
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
