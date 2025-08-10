using GarageSpace.Services.Interfaces;

namespace GarageSpace.Common;

public class CustomIdGenerator : IIdGenerator
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}