using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Realizations;
using System.Threading.Tasks;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<SignInManager<User>> _mockSignInManager;
        private Mock<IMapper> _mockMapper;
        private Mock<IRepositoryWrapper> _mockRepoWrapper;

        [SetUp]
        public void SetUp()
        {
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);
            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null,
                null,
                null);
            _mockMapper = new Mock<IMapper>();
            _mockRepoWrapper = new Mock<IRepositoryWrapper>();

            _authService = new AuthService(_mockUserManager.Object, _mockSignInManager.Object, _mockMapper.Object, _mockRepoWrapper.Object);
        }

        [Test]
        public async Task SignInAsync_ValidLogin_ReturnsSignInResult()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password", RememberMe = false };
            var user = new User { Email = "test@example.com" };
            _mockUserManager.Setup(m => m.FindByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, true))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _authService.SignInAsync(loginDto);

            // Assert
            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }

        [Test]
        public async Task SignOutAsync_ValidSignOut_ReturnsNoResult()
        {
            // Arrange & Act
            await _authService.SignOutAsync();

            // Assert
            _mockSignInManager.Verify(m => m.SignOutAsync(), Times.Once);
        }

        [Test]
        public async Task CreateUserAsync_ValidUser_ReturnsIdentityResult()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "password",
                SurName = "Test",
                Name = "User",
                SportKindId = "1",
                Location = "Test Location"
            };
            var user = new User();
            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), registerDto.Password)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.CreateUserAsync(registerDto);

            // Assert
            Assert.That(result, Is.EqualTo(IdentityResult.Success));
        }

        [Test]
        public async Task AddRoleAsync_ValidUser_AddsUserRole()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com" };
            var user = new User();
            _mockUserManager.Setup(m => m.FindByEmailAsync(registerDto.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.AddToRoleAsync(user, "User")).ReturnsAsync(IdentityResult.Success);

            // Act
            await _authService.AddRoleAsync(registerDto);

            // Assert
            _mockUserManager.Verify(m => m.AddToRoleAsync(user, "User"), Times.Once);
        }

        [Test]
        public async Task FindByEmailAsync_ValidEmail_ReturnsUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Email = email };
            var userDto = new UserDto { Email = email };
            _mockUserManager.Setup(m => m.FindByEmailAsync(email)).ReturnsAsync(user);
            _mockMapper.Setup(m => m.Map<User, UserDto>(user)).Returns(userDto);

            // Act
            var result = await _authService.FindByEmailAsync(email);

            // Assert
            Assert.That(result, Is.EqualTo(userDto));
        }
    }
}
