using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILoggerService<NewsController> _loggerService;

        public NewsController(INewsService newsService, ILoggerService<NewsController> loggerService)
        {
            _newsService = newsService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var news = await _newsService.GetAllAsync();
                return Ok(news);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get specific news
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int newsId)
        {
            try
            {
                var news = await _newsService.GetNewsAsync(newsId);
                return Ok(news);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all news for specific sport kind
        /// </summary>
        /// <param name="sportKindId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBySportKind(int sportKindId)
        {
            try
            {
                var news = await _newsService.GetNewsBySportKindAsync(sportKindId);
                return Ok(news);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("new")]
        public async Task<IActionResult> AddNews(NewsDto news)
        {
            try
            {
                await _newsService.AddNewsAsync(news);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit existing news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("edit")]
        public IActionResult EditNews(NewsDto news)
        {
            try
            {
                _newsService.UpdateNews(news);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete specific news
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public IActionResult Delete(int newsId)
        {
            try
            {
                _newsService.DeleteNews(newsId);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
    }
}
