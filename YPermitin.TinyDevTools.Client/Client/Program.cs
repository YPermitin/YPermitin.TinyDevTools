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

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });
            builder.Services.AddScoped<Services.ClipboardService>();

            await builder.Build().RunAsync();
        }
    }
}