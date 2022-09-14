using System.Text;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace YPermitin.TinyDevTools.Client.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var configHttpClient = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            var configHttpResponse = await configHttpClient.GetAsync("appsettings.json");
            string configAsJsonString = await configHttpResponse.Content.ReadAsStringAsync();
            var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(configAsJsonString))).Build();
            string apiUrl = configuration.GetValue("Api:Url", builder.HostEnvironment.BaseAddress);
            string fiasApiUrl = configuration.GetValue("FIASApi:Url", builder.HostEnvironment.BaseAddress);

            #region HTTPClientSettings // Регистрируем HTTPClient для разных сервисов

            // 1. Клиент для API по умолчанию
            builder.Services.AddHttpClient("api", (s, h) =>
            {
                h.BaseAddress = new Uri(apiUrl);
            });
            // 2. Клиент для API ФИАС
            builder.Services.AddHttpClient("fiasapi", (s, h) =>
            {
                h.BaseAddress = new Uri(fiasApiUrl);
            });

            // Для обратной совместимости оставляем возможность внедрения HttpClient,
            // при этом будет использоваться клиент для API по умолчанию
            builder.Services.AddTransient<HttpClient>(s =>
            {
                var factory = s.GetService<IHttpClientFactory>();
                return factory?.CreateClient("api");
            });

            #endregion

            builder.Services.AddScoped<Services.ClipboardService>();

            await builder.Build().RunAsync();
        }
    }
}