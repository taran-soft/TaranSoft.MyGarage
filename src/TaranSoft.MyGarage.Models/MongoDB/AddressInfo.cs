using System.ComponentModel.DataAnnotations.Schema;
using TaranSoft.MyGarage.Data.Models.EF;

namespace TaranSoft.MyGarage.Data.Models.MongoDB;

public class AddressInfo
{
    public Country? Country { get; set; }
    public string? City { get; set; }
}
