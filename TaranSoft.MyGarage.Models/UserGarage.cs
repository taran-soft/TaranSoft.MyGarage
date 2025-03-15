namespace TaranSoft.MyGarage.Data.Models;

public class UserGarage
{
    public User UserData { get; set; }
    
    public IEnumerable<Car> Cars { get; set; }

}