namespace YPermitin.TinyDevTools.API.Models
{
    /// <summary>
    /// Информация о клиенте
    /// </summary>
    public class ClientInfo
    {
        /// <summary>
        /// IP-адрес клиента
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Информация о браузере
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Заголовки клиентского запроса
        /// </summary>
        public List<ClientInfoRequestHeader> ClientRequestHeaders { get; set; }
    }
}
