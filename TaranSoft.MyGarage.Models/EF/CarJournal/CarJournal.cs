using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaranSoft.MyGarage.Data.Models.EF.CarJournal
{
    [Table("CarJournals")]
    public class CarJournal : BaseEntity
    {
        [Required]
        [ForeignKey("CarId")]
        public Guid CarId { get; set; }

        [ForeignKey("CreatedById")]
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
