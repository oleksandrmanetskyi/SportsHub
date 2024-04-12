using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Realizations;
using SportsHub.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class ShopServiceTests
    {
        private Mock<IRepositoryWrapper> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private IShopService _shopService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _shopService = new ShopService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllShops_ReturnsAllShops()
        {
            // Arrange
            var shopsList = new List<Shop>
            {
                new Shop { Id = 1, Name = "Shop 1" },
                new Shop { Id = 2, Name = "Shop 2" }
            };
            var shopDtoList = new List<ShopDto>
            {
                new ShopDto { Id = 1, Name = "Shop 1" },
                new ShopDto { Id = 2, Name = "Shop 2" }
            };

            _mockRepository.Setup(repo => repo.Shop.GetAll(
                It.IsAny<Func<IQueryable<Shop>, IIncludableQueryable<Shop, object>>>(),
                It.IsAny<Expression<Func<Shop, bool>>>(),
                It.IsAny<Func<IQueryable<Shop>, IOrderedQueryable<Shop>>>()
                )).Returns(shopsList.AsQueryable().BuildMock());
            _mockMapper.Setup(mapper => mapper.Map<List<Shop>, List<ShopDto>>(shopsList)).Returns(shopDtoList);

            // Act
            var result = await _shopService.GetAllShops();

            // Assert
            Assert.AreEqual(shopDtoList, result);
        }

        [Test]
        public async Task GetShopAsync_ValidShopId_ReturnsShop()
        {
            // Arrange
            var shopId = 1;
            var shop = new Shop { Id = shopId, Name = "Shop 1" };
            var shopDto = new ShopDto { Id = shopId, Name = "Shop 1" };

            _mockRepository.Setup(repo => repo.Shop.GetAll(
                It.IsAny<Func<IQueryable<Shop>, IIncludableQueryable<Shop, object>>>(),
                It.IsAny<Expression<Func<Shop, bool>>>(),
                It.IsAny<Func<IQueryable<Shop>, IOrderedQueryable<Shop>>>()
                )).Returns(new List<Shop> { shop }.AsQueryable().BuildMock());
            _mockMapper.Setup(mapper => mapper.Map<Shop, ShopDto>(shop)).Returns(shopDto);

            // Act
            var result = await _shopService.GetShopAsync(shopId);

            // Assert
            Assert.AreEqual(shopDto, result);
        }

        [Test]
        public async Task AddShopAsync_ValidShopDto_AddsShop()
        {
            // Arrange
            var shopDto = new ShopDto { Id = 1, Name = "Shop 1" };
            var shop = new Shop { Id = 1, Name = "Shop 1" };

            _mockMapper.Setup(mapper => mapper.Map<ShopDto, Shop>(shopDto)).Returns(shop);
            _mockRepository.Setup(m => m.Shop.CreateAsync(shop)).Verifiable();

            // Act
            await _shopService.AddShopAsync(shopDto);

            // Assert
            _mockRepository.Verify(repo => repo.Shop.CreateAsync(shop), Times.Once);
        }

        [Test]
        public void UpdateShop_ValidShopDto_UpdatesShop()
        {
            // Arrange
            var shopDto = new ShopDto { Id = 1, Name = "Shop 1" };
            var shop = new Shop { Id = 1, Name = "Shop 1" };

            _mockMapper.Setup(mapper => mapper.Map<ShopDto, Shop>(shopDto)).Returns(shop);
            _mockRepository.Setup(m => m.Shop.Update(shop)).Verifiable();

            // Act
            _shopService.UpdateShop(shopDto);

            // Assert
            _mockRepository.Verify(repo => repo.Shop.Update(shop), Times.Once);
        }

        [Test]
        public void DeleteShop_ValidShopId_DeletesShop()
        {
            // Arrange
            var shopId = 1;
            var shop = new Shop { Id = shopId, Name = "Shop 1" };

            _mockRepository.Setup(repo => repo.Shop.GetAll(
                It.IsAny<Func<IQueryable<Shop>, IIncludableQueryable<Shop, object>>>(),
                It.IsAny<Expression<Func<Shop, bool>>>(),
                It.IsAny<Func<IQueryable<Shop>, IOrderedQueryable<Shop>>>()
                )).Returns(new List<Shop> { shop }.AsQueryable().BuildMock());

            // Act
            _shopService.DeleteShop(shopId);

            // Assert
            _mockRepository.Verify(repo => repo.Shop.Delete(shop), Times.Once);
        }
    }
}
