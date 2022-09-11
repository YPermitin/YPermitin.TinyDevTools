using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using YPermitin.TinyDevTools.API.Extensions;
using YPermitin.TinyDevTools.API.Models;

namespace YPermitin.TinyDevTools.API.Controllers
{
    /// <summary>
    /// ������ � ����������� � �������
    /// </summary>
    [ApiExplorerSettings(GroupName = "���������� � �������")]
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
        /// ��������� ���������� � �������
        /// </summary>
        /// <returns>������ � ����������� � �������</returns>
        /// <response code="200">���������� ������ � ����������� � ������� �������</response>
        /// <response code="400">��� ������ ������������� ������ �������</response>
        [HttpGet(Name = "GetClientInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
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