using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF;

[Table("AddressInfo")]
public class AddressInfo : BaseEntity
{
    public int CountryId { get; set; }
    public Country? Country { get; set; }
    public string? City { get; set; }
}