﻿using AutoMapper;
using TaranSoft.MyGarage.Contracts.Dto.Vehicle;
using TaranSoft.MyGarage.Data.Models.EF.Vehicles;
using TaranSoft.MyGarage.Repository.Interfaces.EF;
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

        public async Task<long> Create(CarDto car)
        {
            var carEntity = _mapper.Map<Car>(car);

            var createdEntity = await _carsRepository.CreateAsync(carEntity);

            return createdEntity.Id;

        }

        public async Task Delete(long id)
        {
            await _carsRepository.DeleteAsync(id);
        }

        public async Task<IList<CarDto>> GetByUserId(long id)
        {
            var allCars = await _carsRepository.ListAllAsync();
            var userCars = allCars.Where(car => car.Garage != null && car.Garage.OwnerId == id).ToList();
            return _mapper.Map<IList<CarDto>>(userCars);
        }

        public async Task<IList<CarDto>> Search(int take, int skip)
        {
            var carsList = await _carsRepository.SearchAsync(take, skip);

            return _mapper.Map<IList<CarDto>>(carsList);
        }

        public async Task Update(long id, CarDto carDto)
        {
            var carEntity = _mapper.Map<Car>(carDto);
            carEntity.Id = id;
            await _carsRepository.UpdateAsync(carEntity);
        }
    }
}
