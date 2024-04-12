using System;
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
using System.Linq.Expressions;
using MockQueryable.Moq;

namespace SportsHub.Services.Tests.Services.Realizations
{
    [TestFixture]
    public class SportKindServiceTests
    {
        private Mock<IRepositoryWrapper> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private ISportKindService _sportKindService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _sportKindService = new SportKindService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllSportKindsAsync_ReturnsAllSportKinds()
        {
            // Arrange
            var sportKindsList = new List<SportKind>
            {
                new SportKind { Id = 1, Name = "Football" },
                new SportKind { Id = 2, Name = "Basketball" }
            };
            var sportKindDtoList = new List<SportKindDto>
            {
                new SportKindDto { Id = 1, Name = "Football" },
                new SportKindDto { Id = 2, Name = "Basketball" }
            };

            _mockRepository.Setup(repo => repo.SportKind.GetAll(
                It.IsAny<Func<IQueryable<SportKind>, IIncludableQueryable<SportKind, object>>>(),
                It.IsAny<Expression<Func<SportKind, bool>>>(),
                It.IsAny<Func<IQueryable<SportKind>, IOrderedQueryable<SportKind>>>()))
                .Returns(sportKindsList.AsQueryable().BuildMock());
            _mockMapper.Setup(mapper => mapper.Map<List<SportKind>, List<SportKindDto>>(sportKindsList)).Returns(sportKindDtoList);

            // Act
            var result = await _sportKindService.GetAllSportKindsAsync();

            // Assert
            Assert.AreEqual(sportKindDtoList, result);
        }

        [Test]
        public async Task GetSportKindAsync_ValidUserId_ReturnsSportKind()
        {
            // Arrange
            var userId = "1";
            var sportKind = new SportKind { Id = 1, Name = "Football" };
            var user = new User { Id = userId, SportKindId = 1, SportKind = sportKind };
            var sportKindDto = new SportKindDto { Id = 1, Name = "Football" };

            _mockRepository.Setup(repo => repo.User.GetAll(
                It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new List<User>() { user }.AsQueryable().BuildMock());
            _mockRepository.Setup(repo => repo.SportKind.GetAll(
                It.IsAny<Func<IQueryable<SportKind>, IIncludableQueryable<SportKind, object>>>(),
                It.IsAny<Expression<Func<SportKind, bool>>>(),
                It.IsAny<Func<IQueryable<SportKind>, IOrderedQueryable<SportKind>>>()))
                .Returns(new List<SportKind>() { sportKind }.AsQueryable().BuildMock());
            _mockMapper.Setup(mapper => mapper.Map<SportKind, SportKindDto>(sportKind)).Returns(sportKindDto);

            // Act
            var result = await _sportKindService.GetSportKindAsync(userId);

            // Assert
            Assert.AreEqual(sportKindDto, result);
        }

        [Test]
        public async Task SetUpUserKindOfSportAsync_ValidUserIdAndSportKindId_SetsUpUserSportKind()
        {
            // Arrange
            var userId = "1";
            var sportKindId = 1;
            var user = new User { Id = userId, SportKindId = null };

            _mockRepository.Setup(repo => repo.User.GetAll(
                It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new List<User>() { user }.AsQueryable().BuildMock());
            _mockRepository.Setup(m => m.User.Update(user)).Verifiable();

            // Act
            await _sportKindService.SetUpUserKindOfSportAsync(userId, sportKindId);

            // Assert
            Assert.AreEqual(sportKindId, user.SportKindId);
            _mockRepository.Verify(repo => repo.User.Update(user), Times.Once);
        }

        [Test]
        public void UpdateSportKind_ValidSportKindDto_UpdatesSportKind()
        {
            // Arrange
            var sportKindDto = new SportKindDto { Id = 1, Name = "Football" };
            var sportKind = new SportKind { Id = 1, Name = "Football" };

            _mockMapper.Setup(mapper => mapper.Map<SportKindDto, SportKind>(sportKindDto)).Returns(sportKind);
            _mockRepository.Setup(m => m.SportKind.Update(sportKind)).Verifiable();

            // Act
            _sportKindService.UpdateSportKind(sportKindDto);

            // Assert
            _mockRepository.Verify(repo => repo.SportKind.Update(sportKind), Times.Once);
        }

        [Test]
        public void DeleteSportKind_ValidSportKindId_DeletesSportKind()
        {
            // Arrange
            var sportKindId = 1;
            var sportKind = new SportKind { Id = sportKindId, Name = "Football" };

            _mockRepository.Setup(repo => repo.SportKind.GetAll(
                It.IsAny<Func<IQueryable<SportKind>, IIncludableQueryable<SportKind, object>>>(),
                It.IsAny<Expression<Func<SportKind, bool>>>(),
                It.IsAny<Func<IQueryable<SportKind>, IOrderedQueryable<SportKind>>>()))
                .Returns(new List<SportKind>() { sportKind }.AsQueryable().BuildMock());

            // Act
            _sportKindService.DeleteSportKind(sportKindId);

            // Assert
            _mockRepository.Verify(repo => repo.SportKind.Delete(sportKind), Times.Once);
        }
    }
}
