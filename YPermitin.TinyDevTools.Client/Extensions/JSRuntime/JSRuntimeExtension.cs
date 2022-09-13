using Microsoft.JSInterop;

namespace YPermitin.TinyDevTools.Client.Extensions.JSRuntime
{
    public static class JSRuntimeExtension
    {
        public static async Task SaveAs(this IJSRuntime jsRuntime, string fileName, byte[] data)
        {
            await jsRuntime.InvokeAsync<object>(
                "saveAsFile",
                fileName,
                Convert.ToBase64String(data));
        }
    }
}
