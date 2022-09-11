namespace YPermitin.TinyDevTools.API.Models
{
    /// <summary>
    /// Заголовок клиентского запроса
    /// </summary>
    public class ClientInfoRequestHeader
    {
        /// <summary>
        /// Ключ заголовка
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Значение заголовка
        /// </summary>
        public string Value { get; set; }
    }
}
