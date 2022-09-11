using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using YPermitin.TinyDevTools.API.Extensions;
using YPermitin.TinyDevTools.API.Models;

namespace YPermitin.TinyDevTools.API.Controllers
{
    /// <summary>
    /// Работа с информацией о клиенте
    /// </summary>
    [ApiExplorerSettings(GroupName = "Информация о клиенте")]
    [ApiController]
    [Route("myip")]
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
        /// <returns>Объект с информацией о клиенте</returns>
        /// <response code="200">Возвращает объект с информацией о текущем клиенте</response>
        /// <response code="400">При ошибке распознавания данных клиента</response>
        [HttpGet(Name = "GetClientInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetClientInfo()
        {
            // Формируем список клиентских заголовков, кром:
            //  * User-Agent - т.к. этот заголовок выводится в отдельное поле.
            var clientHeaders = _httpContextAccessor.GetAllHeaders()
                .Where(e => e.Key != "User-Agent")
                .Select(e => new ClientInfoRequestHeader()
                {
                    Key = e.Key,
                    Value = e.Value
                })
                .OrderBy(e => e.Key)
                .ToList();

            var clientInfo = new ClientInfo()
            {
                IP = _httpContextAccessor.GetRequestIP(),
                UserAgent = _httpContextAccessor.GetHeaderValueAs<string>("User-Agent"),
                ClientRequestHeaders = clientHeaders
            };

            return Ok(clientInfo);
        }
    }
}