using System.ComponentModel.DataAnnotations.Schema;
using TaranSoft.MyGarage.Data.Models.MongoDB;

namespace TaranSoft.MyGarage.Data.Models.EF
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }

        public string Phone { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid PhotoId { get; set; }

        public int DriverExperience { get; set; }

        public GenderEnum Gender { get; set; }

    }
}
