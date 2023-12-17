using Microsoft.AspNetCore.Mvc;
using SportsHub.RecommendationSystem.Services;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.RecommendationSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecommendationsController : ControllerBase
    {
        private IRecommendationsService service;

        public RecommendationsController(IRecommendationsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get recommended training programs for specific user based on his old likes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns>Returns recommended training programs for specific user based on his likes</returns>
        [HttpGet]
        public IActionResult Get([Required]string userId, [Required]int suggestionsCount)
        {
            try 
            {
                var res = service.GetRecommendations(userId, suggestionsCount);
                if (res != null && res.Any())
                {
                    return Ok(res);
                }
                return NotFound(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Likes user training program
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trainingProgramId"></param>
        /// <param name="rating"></param>
        /// <returns>Returns OK if liked successfully</returns>
        [HttpGet]
        public async Task<IActionResult> Like([Required]string userId, [Required]int trainingProgramId, [Range(0, 5)]int rating)
        {
            try
            {
                await service.LikeAsync(userId, trainingProgramId, rating);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Gets rating of user training program
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trainingProgramId"></param>
        /// <returns>Returns rating of user training program</returns>
        [HttpGet]
        public async Task<IActionResult> GetRating([Required] string userId, [Required] int trainingProgramId)
        {
            try
            {
                var result = await service.GetRatingAsync(userId, trainingProgramId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
