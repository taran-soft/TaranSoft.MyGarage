using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaranSoft.MyGarage.Data.Models.EF.CarJournal
{
    [Table("JournalRecords")]
    public class JournalRecord : BaseEntity
    {
        [Required]
        [ForeignKey(nameof(CarJournal.Id))]
        public Guid JournalId { get; set; }
    }
}
