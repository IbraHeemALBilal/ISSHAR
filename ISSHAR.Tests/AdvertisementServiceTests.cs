using AutoMapper;
using ISSHAR.Application.DTOs.AdvertisementDTOs;
using ISSHAR.Application.Profiles;
using ISSHAR.Application.Services;
using ISSHAR.Application.Survices;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace ISSHAR.Tests
{
    public class AdvertisementServiceTests
    {
        private readonly Mock<IAdvertisementRepository> _mockAdvertisementRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<IAdvertisementService>> _mockLogger;

        private readonly AdvertisementService _advertisementService;

        public AdvertisementServiceTests()
        {
            _mockAdvertisementRepository = new Mock<IAdvertisementRepository>();
            var mapperConfig = new MapperConfiguration(ct =>
            {
                ct.AddProfile<AdvertisementProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockLogger = new Mock<ILogger<IAdvertisementService>>();
            _advertisementService = new AdvertisementService(_mockAdvertisementRepository.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAdvertisements()
        {
            // Arrange
            var advertisements = new List<Advertisement>
            {
                new Advertisement { AdvertisementId = 1, Title = "Ad 1", Description = "Description 1" },
                new Advertisement { AdvertisementId = 2, Title = "Ad 2", Description = "Description 2" }
            };
            _mockAdvertisementRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(advertisements);

            // Act
            var result = await _advertisementService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(advertisements.Count, result.Count);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnAdvertisement()
        {
            // Arrange
            var advertisement = new Advertisement { AdvertisementId = 1, Title = "Ad 1", Description = "Description 1" };
            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(advertisement);

            // Act
            var result = await _advertisementService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(advertisement.AdvertisementId, result.AdvertisementId);
            Assert.Equal(advertisement.Title, result.Title);
        }
        [Fact]
        public async Task GetByIdAsync_WhenAdvertismentNotExists_SouldReturnNull()
        {
            // Arrange
            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Advertisement)null);

            // Act
            var result = await _advertisementService.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task AddAsync_ValidInput_AddsAdvertisement()
        {
            // Arrange
            var advertisementDto = new AdvertisementDTO();

            // Act
            await _advertisementService.AddAsync(advertisementDto);

            // Assert
            _mockAdvertisementRepository.Verify(repo => repo.AddAsync(It.IsAny<Advertisement>()), Times.Once);
        }



        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenIdExists()
        {
            // Arrange
            var advertisementId = 1;
            var advertisementDto = new AdvertisementDTO();

            var existingAdvertisement = new Advertisement { AdvertisementId = advertisementId };
            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(advertisementId)).ReturnsAsync(existingAdvertisement);

            // Act
            var result = await _advertisementService.UpdateAsync(advertisementId, advertisementDto);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenIdDoesNotExist()
        {
            // Arrange
            var advertisementId = 1;
            var advertisementDto = new AdvertisementDTO();

            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(advertisementId)).ReturnsAsync((Advertisement)null);

            // Act
            var result = await _advertisementService.UpdateAsync(advertisementId, advertisementDto);

            // Assert
            result.Should().Be(false);
        }




        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenAdvertisementDoesNotExist()
        {
            // Arrange
            var advertisementId = 1;
            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(advertisementId)).ReturnsAsync((Advertisement)null);

            // Act
            var result = await _advertisementService.DeleteAsync(advertisementId);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenAdvertisementExists()
        {
            // Arrange
            var advertisementId = 1;
            var existingAdvertisement = new Advertisement { AdvertisementId = advertisementId };

            _mockAdvertisementRepository.Setup(repo => repo.GetByIdAsync(advertisementId)).ReturnsAsync(existingAdvertisement);

            // Act
            var result = await _advertisementService.DeleteAsync(advertisementId);

            // Assert
            result.Should().Be(true);
        }




        [Fact]
        public async Task GetAdsByUserAsync_ShouldReturnAdvertisementDtos()
        {
            // Arrange
            int userId = 1;
            var advertisements = new List<Advertisement>
            {
                new Advertisement { AdvertisementId = 1, Title = "Ad 1", Description = "Description 1", UserId = userId },
                new Advertisement { AdvertisementId = 2, Title = "Ad 2", Description = "Description 2", UserId = userId }
            };
            _mockAdvertisementRepository.Setup(repo => repo.GetAdsByUserAsync(userId)).ReturnsAsync(advertisements);

            // Act
            var result = await _advertisementService.GetAdsByUserAsync(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(advertisements.Count);
            foreach (var advertisementDto in result)
            {
                advertisementDto.UserId.Should().Be(userId);
            }
        }





        [Fact]
        public async Task GetFilteredAdsAsync_ShouldReturnFilteredAdvertisements()
        {
            // Arrange
            var advertisementFilterBody = new AdvertisementFilterBody
            {
                City = "New York",
                ServiceType = "TypeA"
            };
            var filteredAdvertisements = new List<Advertisement>
            {
                new Advertisement { AdvertisementId = 1, Title = "Ad 1", Description = "Description 1", City = "New York", ServiceType = "TypeA" },
                new Advertisement { AdvertisementId = 2, Title = "Ad 2", Description = "Description 2", City = "New York", ServiceType = "TypeA" }
            };
            _mockAdvertisementRepository.Setup(repo => repo.GetFilteredAdsAsync("New York", "TypeA")).ReturnsAsync(filteredAdvertisements);

            // Act
            var result = await _advertisementService.GetFilteredAdsAsync(advertisementFilterBody);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(filteredAdvertisements.Count);
            foreach (var advertisementDto in result)
            {
                advertisementDto.City.Should().Be("New York");
                advertisementDto.ServiceType.Should().Be("TypeA");
            }
        }

        [Fact]
        public async Task GetFilteredAdsAsync_ShouldReturnEmptyList_WhenNoAdvertisementsMatchFilter()
        {
            // Arrange
            var advertisementFilterBody = new AdvertisementFilterBody
            {
                City = "Paris",
                ServiceType = "TypeB"
            };
            _mockAdvertisementRepository.Setup(repo => repo.GetFilteredAdsAsync("Paris", "TypeB")).ReturnsAsync(new List<Advertisement>());

            // Act
            var result = await _advertisementService.GetFilteredAdsAsync(advertisementFilterBody);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetFilteredAdsAsync_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var advertisementFilterBody = new AdvertisementFilterBody
            {
                City = "New York",
                ServiceType = "TypeA"
            };
            _mockAdvertisementRepository.Setup(repo => repo.GetFilteredAdsAsync("New York", "TypeA")).ThrowsAsync(new Exception("Simulated exception"));

            // Act
            Func<Task> act = async () => await _advertisementService.GetFilteredAdsAsync(advertisementFilterBody);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Simulated exception");
        }
    }
}
