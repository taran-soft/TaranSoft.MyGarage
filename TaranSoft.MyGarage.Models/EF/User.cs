using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Users")]
    public class User : BaseAuditableEntity
    {
        [Required]
        public required string Name { get; set; }

        public string? Surname { get; set; }

        [Required]
        public required string Nickname { get; set; }

        [Required]
        public required string Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid? PhotoId { get; set; }

        public int DriverExperience { get; set; }

        public GenderEnum Gender { get; set; }
    }
}
