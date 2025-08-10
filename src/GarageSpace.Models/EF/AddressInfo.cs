using System.ComponentModel.DataAnnotations.Schema;

namespace GarageSpace.Data.Models.EF;

[Table("AddressInfo")]
public class AddressInfo : BaseEntity
{
    public int CountryId { get; set; }
    public Country? Country { get; set; }
    public string? City { get; set; }
}