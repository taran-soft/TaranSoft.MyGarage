using AutoMapper;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Repository.Interfaces;
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
            var result = await _repository.Search(take, skip);

            return _mapper.Map<IList<GarageDto>>(result);
        }
    }
}
