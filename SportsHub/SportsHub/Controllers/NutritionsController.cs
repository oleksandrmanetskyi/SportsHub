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
    public class NutritionsController : ControllerBase
    {
        private readonly INutritionService _nutritionService;
        private readonly ILoggerService<NutritionsController> _loggerService;

        public NutritionsController(INutritionService nutritionService, ILoggerService<NutritionsController> loggerService)
        {
            _nutritionService = nutritionService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get all nutritions
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var nutritions = await _nutritionService.GetAllNutritions();
                return Ok(nutritions);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get nutrition for user by his training program
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var nutrition = await _nutritionService.GetNutritionByTrainingProgramOfUserAsync(userId);
                return Ok(nutrition);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get specific nutrition
        /// </summary>
        /// <param name="nutritionId"></param>
        /// <returns></returns>
        //[Authorize(Roles = "Admin")]
        [HttpGet("{nutritionId}")]
        public async Task<IActionResult> GetNutrition(int nutritionId)
        {
            try
            {
                var nutrition = await _nutritionService.GetNutritionAsync(nutritionId);
                return Ok(nutrition);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create new nutrition
        /// </summary>
        /// <param name="nutrition"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("new")]
        public async Task<IActionResult> AddNutrition(NutritionDto nutrition)
        {
            try
            {
                await _nutritionService.AddNutritionAsync(nutrition);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit existing nutrition
        /// </summary>
        /// <param name="nutrition"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("edit")]
        public IActionResult EditNutrition(NutritionDto nutrition)
        {
            try
            {
                _nutritionService.UpdateNutrition(nutrition);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete specific nutrition
        /// </summary>
        /// <param name="nutritionId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public IActionResult Delete(int nutritionId)
        {
            try
            {
                _nutritionService.DeleteNutrition(nutritionId);
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
