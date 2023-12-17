using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PlacesController : ControllerBase
    {
        private IPlacesService _placesService;
        private ILoggerService<PlacesController> _loggerService;

        public PlacesController(IPlacesService placesService, ILoggerService<PlacesController> loggerService)
        {
            _placesService = placesService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get all places for user by his id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var places = await _placesService.GetPlacesByUserId(userId);
                return Ok(places);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
    }
}
