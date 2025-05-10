namespace TaranSoft.MyGarage.Contracts
{
    public class UserDto : BaseEntityDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public GarageDto Garage { get; set; }
    }
}
