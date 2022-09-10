namespace YPermitin.TinyDevTools.Client.Helpers
{
    public static class StringHelper
    {
        public static string Base64Encode(string sourceValue)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sourceValue);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
