namespace YPermitin.TinyDevTools.Client.Models
{
    /// <summary>
    /// Информация о клиенте
    /// </summary>
    public class ClientInfo
    {
        public ClientInfo()
        {
            IP = "<<Неопределен>>";
            UserAgent = "<<Неопределен>>";
            ClientRequestHeaders = new List<ClientInfoRequestHeader>();
        }

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
