namespace TaranSoft.MyGarage.Services.Models
{
    public class User
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}