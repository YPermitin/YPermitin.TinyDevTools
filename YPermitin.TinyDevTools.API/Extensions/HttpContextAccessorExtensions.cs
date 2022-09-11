namespace YPermitin.TinyDevTools.API.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetRequestIP(this IHttpContextAccessor httpContextAccessor, bool tryUseXForwardHeader = true)
        {
            string ip = null;

            // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For

            // X-Forwarded-For (csv list):  Using the First entry in the list seems to work
            // for 99% of cases however it has been suggested that a better (although tedious)
            // approach might be to read each IP from right to left and use the first public IP.
            // http://stackoverflow.com/a/43554000/538763
            //
            if (tryUseXForwardHeader)
                ip = httpContextAccessor.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            if (ip.IsNullOrWhitespace() && httpContextAccessor.HttpContext?.Connection.RemoteIpAddress != null)
                ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ip.IsNullOrWhitespace())
                ip = httpContextAccessor.GetHeaderValueAs<string>("REMOTE_ADDR");

            return ip;
        }

        public static T GetHeaderValueAs<T>(this IHttpContextAccessor httpContextAccessor, string headerName)
        {
            if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(headerName, out var values) ?? false)
            {
                string rawValues = values.ToString();   // writes out as Csv when there are multiple.

                if (!rawValues.IsNullOrWhitespace())
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }

        public static Dictionary<string, string> GetAllHeaders(this IHttpContextAccessor httpContextAccessor)
        {
            Dictionary<string, string> output = httpContextAccessor.HttpContext?.Request.Headers
                .Select(e => new KeyValuePair<string, string>(e.Key, e.Value.ToString()))
                .ToDictionary(k => k.Key, v => v.Value);

            return output;
        }
    }
}
