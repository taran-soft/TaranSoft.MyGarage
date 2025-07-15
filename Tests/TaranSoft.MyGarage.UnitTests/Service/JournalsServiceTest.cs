using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using TaranSoft.MyGarage.Contracts.Dto;
using TaranSoft.MyGarage.Data.Models.EF.CarJournal;
using TaranSoft.MyGarage.Repository.Interfaces.EF;
using TaranSoft.MyGarage.Services;
using Xunit;

namespace TaranSoft.MyGarage.UnitTests.Service
{
    public class JournalsServiceTest
    {
        private readonly IEFJournalsRepository _repositoryMock;
        private readonly IMapper _mapperMock;
        private readonly JournalsService _service;

        public JournalsServiceTest()
        {
            _repositoryMock = Substitute.For<IEFJournalsRepository>();
            _mapperMock = Substitute.For<IMapper>();
            _service = new JournalsService(_repositoryMock, _mapperMock);
        }

        [Fact]
        public async Task GetByVehicleId_ReturnsMappedJournals()
        {
            // Arrange
            var vehicleId = 42L;
            var journals = new List<Journal> { new Journal { VehicleId = vehicleId, Title = "Test Journal" } };
            var journalDtos = new List<JournalDto> { new JournalDto { Title = "Test Journal" } };
            _repositoryMock.GetByVehicleId(vehicleId).Returns(Task.FromResult((ICollection<Journal>)journals));
            _mapperMock.Map<ICollection<JournalDto>>(journals).Returns(journalDtos);

            // Act
            var result = await _service.GetByVehicleId(vehicleId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test Journal", result.First().Title);
        }

        [Fact]
        public async Task GetByVehicleId_ReturnsEmptyList_WhenNoJournals()
        {
            // Arrange
            var vehicleId = 99L;
            var journals = new List<Journal>();
            var journalDtos = new List<JournalDto>();
            _repositoryMock.GetByVehicleId(vehicleId).Returns(Task.FromResult((ICollection<Journal>)journals));
            _mapperMock.Map<ICollection<JournalDto>>(journals).Returns(journalDtos);

            // Act
            var result = await _service.GetByVehicleId(vehicleId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
} 