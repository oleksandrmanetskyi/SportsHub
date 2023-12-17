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
    [AllowAnonymous]
    public class SportKindController : ControllerBase
    {
        private readonly ISportKindService _sportKindService;
        private readonly ILoggerService<SportKindController> _loggerService;

        public SportKindController(ISportKindService sportKindService, ILoggerService<SportKindController> loggerService)
        {
            _sportKindService = sportKindService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get sport kind of specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var sportKindDto = await _sportKindService.GetSportKindAsync(userId);
                return Ok(sportKindDto);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all sport kinds
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sportKindsDto = await _sportKindService.GetAllSportKindsAsync();
                return Ok(sportKindsDto);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Set sport kind for specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sportKindId"></param>
        /// <returns></returns>
        [HttpPost("set/{userId}-{sportKindId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> SetUserSportKind(string userId, int sportKindId)
        {
            try
            {
                await _sportKindService.SetUpUserKindOfSportAsync(userId, sportKindId);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
        /// <summary>
        /// Edit existing sportKind
        /// </summary>
        /// <param name="sportKind"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer",Roles = "Admin")]
        [HttpPost("edit")]
        public IActionResult EditShop(SportKindDto sportKind)
        {
            try
            {
                _sportKindService.UpdateSportKind(sportKind);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete specific sportKind
        /// </summary>
        /// <param name="sportKindId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("[action]")]
        public IActionResult Delete(int sportKindId)
        {
            try
            {
                _sportKindService.DeleteSportKind(sportKindId);
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
