using AutoMapper;
using TaranSoft.MyGarage.Contracts.VehicleDto;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;
using TaranSoft.MyGarage.Repository.Interfaces;
using TaranSoft.MyGarage.Services.Interfaces;

namespace TaranSoft.MyGarage.Services
{
    public class CarsService : ICarsService
    {
        private readonly IEFCarsRepository _carsRepository;
        private readonly IMapper _mapper;
        public CarsService(IEFCarsRepository carsRepository, IMapper mapper) 
        {
            _carsRepository = carsRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Create(CarDto car)
        {
            var carEntity = _mapper.Map<Car>(car);

            var createdEntity = await _carsRepository.CreateAsync(carEntity);

            return Guid.NewGuid();

        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CarDto>> GetByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CarDto>> Search(int take, int skip)
        {
            var carsList = await _carsRepository.Search(take, skip);

            return _mapper.Map<IList<CarDto>>(carsList);
        }

        public Task Update(Guid id, CarDto carDto)
        {
            throw new NotImplementedException();
        }
    }
}
