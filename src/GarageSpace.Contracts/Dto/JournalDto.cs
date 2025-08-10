using GarageSpace.Contracts.Dto.Vehicle;

namespace GarageSpace.Contracts.Dto
{
    public class JournalDto : BaseAuditableEntityDto
    {
        public string Title { get; set; }
        public UserDto Owner { get; set; }
        public VehicleDto Vehicle { get; set; }

    }
}
