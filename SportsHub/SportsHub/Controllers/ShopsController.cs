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
    public class ShopsController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly ILoggerService<NewsController> _loggerService;

        public ShopsController(IShopService shopService, ILoggerService<NewsController> loggerService)
        {
            _shopService = shopService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get all shops
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var shops = await _shopService.GetAllShops();
                return Ok(shops);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get specific shop
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(int shopId)
        {
            try
            {
                var shop = await _shopService.GetShopAsync(shopId);
                return Ok(shop);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create new shop
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("new")]
        public async Task<IActionResult> AddShop(ShopDto shop)
        {
            try
            {
                await _shopService.AddShopAsync(shop);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit existing shop
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("edit")]
        public IActionResult EditShop(ShopDto shop)
        {
            try
            {
                _shopService.UpdateShop(shop);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete specific shop
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]")]
        public IActionResult Delete(int shopId)
        {
            try
            {
                _shopService.DeleteShop(shopId);
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
