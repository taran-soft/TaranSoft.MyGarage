using System.ComponentModel.DataAnnotations.Schema;
using GarageSpace.Data.Models.EF;

namespace GarageSpace.Data.Models.MongoDB;

public class AddressInfo
{
    public Country? Country { get; set; }
    public string? City { get; set; }
}
