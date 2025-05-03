namespace TaranSoft.MyGarage.Data.Models.MongoDB;

public class UserGarage
{
    public User UserData { get; set; }
    
    public IEnumerable<Car> Cars { get; set; }

}