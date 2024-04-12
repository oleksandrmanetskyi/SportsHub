using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class AccountsServiceTests
    {
        private Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private Mock<IMapper> _mapperMock;
        private AccountsService _accountsService;

        private readonly List<User> users = new() { new() { Id = "1" }, new() { Id = "2" } };

        [SetUp]
        public void Setup()
        {
            _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            _mapperMock = new Mock<IMapper>();
            _accountsService = new AccountsService(_mapperMock.Object, null, _repositoryWrapperMock.Object);

            _repositoryWrapperMock.Setup(repo => repo.User.GetAll(
                It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(users.AsQueryable().BuildMock());
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfUserDtos()
        {
            // Arrange
            _mapperMock.Setup(mapper => mapper.Map<List<User>, List<UserDto>>(It.IsAny<List<User>>()))
                .Returns(users.Select(u => new UserDto()).ToList());

            // Act
            var result = await _accountsService.GetAllAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(users.Count));
                Assert.That(result.All(u => u.GetType() == typeof(UserDto)), Is.True);
            });
        }

        [Test]
        public async Task GetUserAsync_ReturnsUserDto()
        {
            // Arrange
            var users = new List<User> { new() { Id = "1" } };
            _mapperMock.Setup(mapper => mapper.Map<User, UserDto>(It.IsAny<User>()))
                .Returns(new UserDto());

            // Act
            var result = await _accountsService.GetUserAsync("1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<UserDto>());
        }

        [Test]
        public void UpdateUser_CallsRepositoryUpdate()
        {
            // Arrange
            var userDto = new UserDto();
            _mapperMock.Setup(mapper => mapper.Map<UserDto, User>(It.IsAny<UserDto>()))
                .Returns(new User());

            // Act
            _accountsService.UpdateUser(userDto);

            // Assert
            _repositoryWrapperMock.Verify(repo => repo.User.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_CallsRepositoryDelete()
        {
            // Arrange

            // Act
            await _accountsService.DeleteUserAsync("1");

            // Assert
            _repositoryWrapperMock.Verify(repo => repo.User.Delete(It.IsAny<User>()), Times.Once);
        }
    }
}
