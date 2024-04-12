using System.Collections.Generic;
using System.Linq;
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
using System;
using MockQueryable.Moq;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class NewsServiceTests
    {
        private Mock<IRepositoryWrapper> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private INewsService _newsService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _newsService = new NewsService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllNews()
        {
            // Arrange
            var newsList = new List<News>
            {
                new News { Id = 1, Title = "News 1" },
                new News { Id = 2, Title = "News 2" }
            };
            var newsDtoList = new List<NewsDto>
            {
                new NewsDto { Id = 1, Title = "News 1" },
                new NewsDto { Id = 2, Title = "News 2" }
            };

            _mockRepository.Setup(repo => repo.News.GetAll(
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>(),
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IOrderedQueryable<News>>>()))
                .Returns(newsList.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<List<News>, List<NewsDto>>(newsList)).Returns(newsDtoList);

            // Act
            var result = await _newsService.GetAllAsync();

            // Assert
            Assert.AreEqual(newsDtoList, result);
        }

        [Test]
        public async Task GetNewsAsync_ValidId_ReturnsNews()
        {
            // Arrange
            var newsId = 1;
            var news = new List<News>() { new() { Id = newsId, Title = "News 1" } };
            var newsDto = new NewsDto { Id = newsId, Title = "News 1" };

            _mockRepository.Setup(repo => repo.News.GetAll(
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>(),
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IOrderedQueryable<News>>>()))
                .Returns(news.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<News, NewsDto>(news[0])).Returns(newsDto);

            // Act
            var result = await _newsService.GetNewsAsync(newsId);

            // Assert
            Assert.AreEqual(newsDto, result);
        }

        [Test]
        public async Task GetNewsBySportKindAsync_ValidSportKindId_ReturnsNewsList()
        {
            // Arrange
            var sportKindId = 1;
            var newsList = new List<News>
            {
                new News { Id = 1, Title = "News 1", SportKindId = sportKindId },
                new News { Id = 2, Title = "News 2", SportKindId = sportKindId }
            };
            var newsDtoList = new List<NewsDto>
            {
                new NewsDto { Id = 1, Title = "News 1", SportKindId = sportKindId },
                new NewsDto { Id = 2, Title = "News 2", SportKindId = sportKindId }
            };

            _mockRepository.Setup(repo => repo.News.GetAll(
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>(),
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IOrderedQueryable<News>>>()))
                .Returns(newsList.AsQueryable().BuildMock());
            _mockMapper.Setup(m => m.Map<List<News>, List<NewsDto>>(newsList)).Returns(newsDtoList);

            // Act
            var result = await _newsService.GetNewsBySportKindAsync(sportKindId);

            // Assert
            Assert.AreEqual(newsDtoList, result);
        }

        [Test]
        public async Task AddNewsAsync_ValidNewsDto_AddsNews()
        {
            // Arrange
            var newsDto = new NewsDto { Id = 1, Title = "News 1" };
            var news = new News { Id = 1, Title = "News 1" };
            _mockMapper.Setup(m => m.Map<NewsDto, News>(newsDto)).Returns(news);
            _mockRepository.Setup(m => m.News.CreateAsync(news)).Verifiable();

            // Act
            await _newsService.AddNewsAsync(newsDto);

            // Assert
            _mockRepository.Verify(r => r.News.CreateAsync(news), Times.Once);
        }

        [Test]
        public void UpdateNews_ValidNewsDto_UpdatesNews()
        {
            // Arrange
            var newsDto = new NewsDto { Id = 1, Title = "News 1" };
            var news = new News { Id = 1, Title = "News 1" };

            _mockMapper.Setup(m => m.Map<NewsDto, News>(newsDto)).Returns(news);
            _mockRepository.Setup(m => m.News.Update(news)).Verifiable();

            // Act
            _newsService.UpdateNews(newsDto);

            // Assert
            _mockRepository.Verify(r => r.News.Update(news), Times.Once);
        }

        [Test]
        public void DeleteNews_ValidNewsId_DeletesNews()
        {
            // Arrange
            var newsId = 1;
            var news = new News { Id = newsId, Title = "News 1" };

            _mockRepository.Setup(repo => repo.News.GetAll(
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>(),
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IOrderedQueryable<News>>>()))
                .Returns(new List<News>{ news }.AsQueryable().BuildMock());
            _mockRepository.Setup(m => m.News.Delete(news)).Verifiable();

            // Act
            _newsService.DeleteNews(newsId);

            // Assert
            _mockRepository.Verify(r => r.News.Delete(news), Times.Once);
        }
    }
}
