namespace TaranSoft.MyGarage.Contracts.Request;

public class LoginModel : IUserCredentials
{
    public string Email { get; set; }
    public string Password { get; set; }
}