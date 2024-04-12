using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Realizations;
using SportsHub.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;
using MockQueryable.Moq;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class NutritionServiceTests
    {
        private Mock<IRepositoryWrapper> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private INutritionService _nutritionService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _nutritionService = new NutritionService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllNutritions_ReturnsAllNutritions()
        {
            // Arrange
            var nutritionsList = new List<Nutrition>
            {
                new Nutrition { Id = 1, Name = "Nutrition 1" },
                new Nutrition { Id = 2, Name = "Nutrition 2" }
            };
            var nutritionDtoList = new List<NutritionDto>
            {
                new NutritionDto { Id = 1, Name = "Nutrition 1" },
                new NutritionDto { Id = 2, Name = "Nutrition 2" }
            };

            _mockRepository.Setup(repo => repo.Nutrition.GetAll(
                It.IsAny<Func<IQueryable<Nutrition>, IIncludableQueryable<Nutrition, object>>>(),
                It.IsAny<Expression<Func<Nutrition, bool>>>(),
                It.IsAny<Func<IQueryable<Nutrition>, IOrderedQueryable<Nutrition>>>()))
                .Returns(nutritionsList.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<List<Nutrition>, List<NutritionDto>>(nutritionsList)).Returns(nutritionDtoList);

            // Act
            var result = await _nutritionService.GetAllNutritions();

            // Assert
            Assert.AreEqual(nutritionDtoList, result);
        }

        [Test]
        public async Task GetNutritionByTrainingProgramOfUserAsync_ValidUserId_ReturnsNutrition()
        {
            // Arrange
            var userId = "1";
            var user = new User { Id = userId, TrainingProgramId = 1 };
            var program = new TrainingProgram { Id = 1, NutritionId = 1 };
            var nutrition = new Nutrition { Id = 1, Name = "Nutrition 1" };
            var nutritionDto = new NutritionDto { Id = 1, Name = "Nutrition 1" };

            _mockRepository.Setup(repo => repo.User.GetAll(
                It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new List<User>() { user }.AsQueryable().BuildMock());
            _mockRepository.Setup(repo => repo.TrainingProgram.GetAll(
                It.IsAny<Func<IQueryable<TrainingProgram>, IIncludableQueryable<TrainingProgram, object>>>(),
                It.IsAny<Expression<Func<TrainingProgram, bool>>>(),
                It.IsAny<Func<IQueryable<TrainingProgram>, IOrderedQueryable<TrainingProgram>>>()))
                .Returns(new List<TrainingProgram>() { program} .AsQueryable().BuildMock());
            _mockRepository.Setup(repo => repo.Nutrition.GetAll(
                It.IsAny<Func<IQueryable<Nutrition>, IIncludableQueryable<Nutrition, object>>>(),
                It.IsAny<Expression<Func<Nutrition, bool>>>(),
                It.IsAny<Func<IQueryable<Nutrition>, IOrderedQueryable<Nutrition>>>()))
                .Returns(new List<Nutrition>() { nutrition }.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<Nutrition, NutritionDto>(nutrition)).Returns(nutritionDto);

            // Act
            var result = await _nutritionService.GetNutritionByTrainingProgramOfUserAsync(userId);

            // Assert
            Assert.AreEqual(nutritionDto, result);
        }

        [Test]
        public async Task GetNutritionAsync_ValidNutritionId_ReturnsNutrition()
        {
            // Arrange
            var nutritionId = 1;
            var nutrition = new Nutrition { Id = nutritionId, Name = "Nutrition 1" };
            var nutritionDto = new NutritionDto { Id = nutritionId, Name = "Nutrition 1" };

            _mockRepository.Setup(repo => repo.Nutrition.GetAll(
                It.IsAny<Func<IQueryable<Nutrition>, IIncludableQueryable<Nutrition, object>>>(),
                It.IsAny<Expression<Func<Nutrition, bool>>>(),
                It.IsAny<Func<IQueryable<Nutrition>, IOrderedQueryable<Nutrition>>>()))
                .Returns(new List<Nutrition>() { nutrition }.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<Nutrition, NutritionDto>(nutrition)).Returns(nutritionDto);

            // Act
            var result = await _nutritionService.GetNutritionAsync(nutritionId);

            // Assert
            Assert.AreEqual(nutritionDto, result);
        }

        [Test]
        public async Task AddNutritionAsync_ValidNutritionDto_AddsNutrition()
        {
            // Arrange
            var nutritionDto = new NutritionDto { Id = 1, Name = "Nutrition 1" };
            var nutrition = new Nutrition { Id = 1, Name = "Nutrition 1" };

            _mockMapper.Setup(m => m.Map<NutritionDto, Nutrition>(nutritionDto)).Returns(nutrition);
            _mockRepository.Setup(m => m.Nutrition.CreateAsync(nutrition)).Verifiable();

            // Act
            await _nutritionService.AddNutritionAsync(nutritionDto);

            // Assert
            _mockRepository.Verify(r => r.Nutrition.CreateAsync(nutrition), Times.Once);
        }

        [Test]
        public void UpdateNutrition_ValidNutritionDto_UpdatesNutrition()
        {
            // Arrange
            var nutritionDto = new NutritionDto { Id = 1, Name = "Nutrition 1" };
            var nutrition = new Nutrition { Id = 1, Name = "Nutrition 1" };

            _mockMapper.Setup(m => m.Map<NutritionDto, Nutrition>(nutritionDto)).Returns(nutrition);
            _mockRepository.Setup(r => r.Nutrition.Update(nutrition)).Verifiable();

            // Act
            _nutritionService.UpdateNutrition(nutritionDto);

            // Assert
            _mockRepository.Verify(r => r.Nutrition.Update(nutrition), Times.Once);
        }

        [Test]
        public void DeleteNutrition_ValidNutritionId_DeletesNutrition()
        {
            // Arrange
            var nutritionId = 1;
            var nutrition = new Nutrition { Id = nutritionId, Name = "Nutrition 1" };

            _mockRepository.Setup(repo => repo.Nutrition.GetAll(
                It.IsAny<Func<IQueryable<Nutrition>, IIncludableQueryable<Nutrition, object>>>(),
                It.IsAny<Expression<Func<Nutrition, bool>>>(),
                It.IsAny<Func<IQueryable<Nutrition>, IOrderedQueryable<Nutrition>>>()))
                .Returns(new List<Nutrition>() { nutrition }.AsQueryable().BuildMock());

            // Act
            _nutritionService.DeleteNutrition(nutritionId);

            // Assert
            _mockRepository.Verify(r => r.Nutrition.Delete(nutrition), Times.Once);
        }
    }
}
