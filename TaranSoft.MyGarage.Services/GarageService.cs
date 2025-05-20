using AutoMapper;
using TaranSoft.MyGarage.Contracts.Dto;
using TaranSoft.MyGarage.Repository.Interfaces.EF;
using TaranSoft.MyGarage.Services.Interfaces;

namespace TaranSoft.MyGarage.Services
{
    public class GarageService : IGarageService
    {
        private readonly IEFGaragesRepository _repository;
        private readonly IMapper _mapper;
        public GarageService(IEFGaragesRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<GarageDto>> Search(int take, int skip)
        {
            var result = await _repository.SearchAsync(take, skip);

            return _mapper.Map<IList<GarageDto>>(result);
        }

        public async Task<GarageDto?> GetGarageByOwner(long ownerId)
        {
            var result = await _repository.GetByOwnerIdAsync(ownerId);
            if (result == null) 
            {
                return null;
            }

            return _mapper.Map<GarageDto>(result);
        }
    }
}
