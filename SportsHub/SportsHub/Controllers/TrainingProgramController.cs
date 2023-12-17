using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TrainingProgramController : ControllerBase
    {
        private ITrainingProgramService _trainingProgramService;
        private ILoggerService<TrainingProgramController> _loggerService;
        

        public TrainingProgramController(ITrainingProgramService trainingProgramService, ILoggerService<TrainingProgramController> loggerService)
        {
            this._trainingProgramService = trainingProgramService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get recommended training programs for specific user based on his sport kind
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns recommended training programs for specific user based on his sport kind</returns>
        [HttpGet("recommended/{userId}")]
        public async Task<IActionResult> GetRecommendations(string userId)
        {
            try
            {
                var programs = await _trainingProgramService.GetProgramsByUserSportKind(userId);
                return Ok(programs);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
        /// <summary>
        /// Get recommended training programs for specific user based on his old likes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns>Returns recommended training programs for specific user based on his likes</returns>
        [HttpGet("suggestions")]
        public async Task<IActionResult> GetSuggestions(string userId, int count)
        {
            try
            {
                var programs = await _trainingProgramService.GetRecommendations(userId, count);
                if (programs == null)
                {
                    return NotFound();
                }
                return Ok(programs);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates recommended training programs for specific user based on his old likes and gets new ones
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns>Returns new recommended training programs for specific user based on his likes</returns>
        [HttpGet("newsuggestions")]
        public async Task<IActionResult> GetNewSuggestions(string userId, int count)
        {
            try
            {
                var programs = await _trainingProgramService.GetNewRecommendations(userId, count);
                if (programs == null)
                {
                    return NotFound();
                }
                return Ok(programs);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Likes user training program
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trainingProgramId"></param>
        /// <param name="rating"></param>
        /// <returns>Returns OK if liked successfully</returns>
        [HttpGet("like")]
        public async Task<IActionResult> Like([Required] string userId, [Required] int trainingProgramId, [Range(0, 5)] int rating)
        {
            try
            {
                var status = await _trainingProgramService.LikeTrainingProgram(userId, trainingProgramId, rating);
                return Ok(status);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets rating of user training program
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trainingProgramId"></param>
        /// <returns>Returns rating of user training program</returns>
        [HttpGet("getrating")]
        public async Task<IActionResult> GetRating([Required] string userId, [Required] int trainingProgramId)
        {
            try
            {
                var result = await _trainingProgramService.GetRatingAsync(userId, trainingProgramId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get training program for specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var program = await _trainingProgramService.Get(userId);
                return Ok(program);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get specific training program
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var program = await _trainingProgramService.Get(id);
                return Ok(program);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all training programs
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var program = await _trainingProgramService.GetAllPrograms();
                return Ok(program);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Add new training program
        /// </summary>
        /// <param name="trainingProgram"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("new")]
        public async Task<IActionResult> Add(TrainingProgramDto trainingProgram)
        {
            try
            {
                await _trainingProgramService.AddTrainingProgram(trainingProgram);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Set up user's training program
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="trainingProgramId"></param>
        /// <returns></returns>
        [HttpPost("Follow/{userId}")]
        public async Task<IActionResult> Follow(string userId, [FromBody]int trainingProgramId)
        {
            try
            {
                await _trainingProgramService.SetTrainingProgramForUser(userId, trainingProgramId);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete user's training program
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Unfollow/{userId}")]
        public async Task<IActionResult> Unfollow(string userId)
        {
            try
            {
                await _trainingProgramService.DeleteTrainingProgramForUser(userId);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit existing training program
        /// </summary>
        /// <param name="trainingProgram"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, [FromBody]TrainingProgramDto trainingProgram)
        {
            try
            {
                _trainingProgramService.EditTrainingProgram(trainingProgram);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
        /// <summary>
        /// Delete specific training program
        /// </summary>
        /// <param name="trainingProgramId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public IActionResult Delete(int trainingProgramId)
        {
            try
            {
                _trainingProgramService.DeleteTrainingProgram(trainingProgramId);
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
