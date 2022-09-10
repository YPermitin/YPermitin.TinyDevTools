using Microsoft.AspNetCore.Mvc;
using YPermitin.TinyDevTools.API.Extensions;
using YPermitin.TinyDevTools.API.Models;

namespace YPermitin.TinyDevTools.API.Controllers
{
    /// <summary>
    /// Работа с информацией о клиенте
    /// </summary>
    [ApiController]
    [Route("api/myip")]
    public class MyIPController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyIPController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor
                                   ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Получение информации о клиенте
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetClientInfo")]
        public IActionResult GetClientInfo()
        {
            var clientInfo = new ClientInfo()
            {
                IP = _httpContextAccessor.GetRequestIP(),
                UserAgent = _httpContextAccessor.GetHeaderValueAs<string>("User-Agent")
            };

            return Ok(clientInfo);
        }
    }
}