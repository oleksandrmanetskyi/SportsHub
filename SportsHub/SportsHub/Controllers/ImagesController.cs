using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SportsHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ImagesController : ControllerBase
    {
        private readonly IBlobStorage blobStorage;
        private readonly ILoggerService<ImagesController> loggerService;

        public ImagesController(IBlobStorage blobStorage, ILoggerService<ImagesController> loggerService)
        {
            this.blobStorage = blobStorage;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Uploads an image to blob storage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromBody]ImageUploadRequest request)
        {
            try
            {
                await this.blobStorage.UploadBlobAsync(request.Name, request.Base64);
                return Ok();
            }
            catch (Exception e)
            {
                this.loggerService.LogError(e.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Uploads an image from blob storage
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("download")]
        public async Task<IActionResult> DownloadAsync([FromBody]string name)
        {
            try
            {
                var base64 = await this.blobStorage.DownloadBlobAsync(name);
                return Ok(base64);
            }
            catch (Exception e)
            {
                this.loggerService.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deteles an image from blob storage
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] string name)
        {
            try
            {
                await this.blobStorage.DeleteBlobAsync(name);
                return Ok();
            }
            catch (Exception e)
            {
                this.loggerService.LogError(e.Message);
            }

            return BadRequest();
        }
    }
}
