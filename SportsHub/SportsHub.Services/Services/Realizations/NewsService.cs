using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class NewsService: INewsService
    {
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public NewsService(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NewsDto>> GetAllAsync()
        {
            var news = await repository.News.GetAll().ToListAsync();
            return mapper.Map<List<News>, List<NewsDto>>(news);
        }

        public async Task<NewsDto> GetNewsAsync(int newsId)
        {
            var news = await repository.News.GetAll(filter: x => x.Id == newsId).FirstAsync();
            return mapper.Map<News, NewsDto>(news);
        }

        public async Task<IEnumerable<NewsDto>> GetNewsBySportKindAsync(int sportKindId)
        {
            var news = await repository.News.GetAll(filter: x => x.SportKindId == sportKindId).ToListAsync();
            return mapper.Map<List<News>, List<NewsDto>>(news);
        }

        public async Task AddNewsAsync(NewsDto news)
        {
            var newNews = mapper.Map<NewsDto, News>(news);
            await repository.News.CreateAsync(newNews);
        }

        public void UpdateNews(NewsDto news)
        {
            var newNews = mapper.Map<NewsDto, News>(news);
            repository.News.Update(newNews);
        }

        public void DeleteNews(int newsId)
        {
            var news = repository.News.GetAll(filter: x => x.Id == newsId).First();
            repository.News.Delete(news);
        }
    }
}
