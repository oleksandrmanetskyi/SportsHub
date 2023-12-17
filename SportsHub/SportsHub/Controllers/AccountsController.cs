using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly ILoggerService<NewsController> _loggerService;

        public AccountsController(IAccountsService accountsService, ILoggerService<NewsController> loggerService)
        {
            _accountsService = accountsService;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Get all users(accounts), available only for admin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _accountsService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get specific user's account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var user = await _accountsService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Edit existing account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public IActionResult Edit(UserDto user)
        {
            try
            {
                _accountsService.UpdateUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                _loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete specific user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _accountsService.DeleteUserAsync(userId);
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
