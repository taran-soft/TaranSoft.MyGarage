using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaranSoft.MyGarage.Data.Models.EF;


[Table("UsersGarage")]
public class UserGarage : BaseEntity
{
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
    public ICollection<Motorcycle> Motocycles { get; set; } = new List<Motorcycle>();


}