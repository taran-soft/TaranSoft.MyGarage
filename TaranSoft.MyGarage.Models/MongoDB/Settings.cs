namespace TaranSoft.MyGarage.Data.Models.MongoDB;

public class Settings
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Environment { get; set; } = null!;
}