using AutoMapper;
using TaranSoft.MyGarage.Contracts;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Services.Interfaces;

namespace TaranSoft.MyGarage.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IMapper _mapper;
        public CarsService(ICarsRepository carsRepository, IMapper mapper) 
        {
            _carsRepository = carsRepository;
            _mapper = mapper;
        }

        public Task<Guid> Create(CarDto car)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CarDto>> GetByUserId(Guid id)
        {
            var cars = await _carsRepository.GetByUserId(id);
            return _mapper.Map<IList<CarDto>>(cars);
        }

        public async Task<IList<CarDto>> Search(int take, int skip)
        {
            var carsList = await _carsRepository.Search(take, skip);
            throw new NotImplementedException();
        }

        public Task Update(Guid id, CarDto carDto)
        {
            throw new NotImplementedException();
        }
    }
}
