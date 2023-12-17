using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllAsync();
        Task<NewsDto> GetNewsAsync(int newsId);
        Task<IEnumerable<NewsDto>> GetNewsBySportKindAsync(int sportKindId);
        Task AddNewsAsync(NewsDto news);
        void UpdateNews(NewsDto news);
        void DeleteNews(int newsId);
    }
}
