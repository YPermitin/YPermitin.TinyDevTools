using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using YPermitin.TinyDevTools.API.Extensions;
using YPermitin.TinyDevTools.API.Infrastructure;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

ServiceDeployType serviceDeployTypeDefault = ServiceDeployType.Unknown;
if (OperatingSystem.IsWindows())
{
    serviceDeployTypeDefault = ServiceDeployType.IIS;
}
else if (OperatingSystem.IsLinux())
{
    serviceDeployTypeDefault = ServiceDeployType.Kestrel;
}
string serviceDeployTypeAsString = configuration.GetValue("DeployType", string.Empty);
var serviceDeployType = serviceDeployTypeAsString.ToEnum(serviceDeployTypeDefault);

string logPath = Path.Combine("Logs", "log-.txt");
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    if (serviceDeployType == ServiceDeployType.IIS)
    {
        builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
        builder.WebHost.UseIISIntegration();
    }
    else if (serviceDeployType == ServiceDeployType.Kestrel)
    {
        builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
        builder.WebHost.UseKestrel();
    }
    else
    {
        throw new Exception($"Unknown service deploy type in config file: {serviceDeployType}");
    }

    builder.Host.UseSerilog((_, lc) => lc
        .ReadFrom.Configuration(configuration)
        .WriteTo.Console()
        .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7));
    var services = builder.Services;

    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    services.AddControllersExtension();
    services.AddMVCExtension();
    services.AddHttpContextAccessor();
    services.AddCors();
    services.AddSwaggerExtension();

    var app = builder.Build();
    IWebHostEnvironment env = app.Environment;
    var logger = app.Services.GetService<ILogger<Program>>();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseExceptionPage(env);
    app.ConfigureExceptionHandler(logger);
    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseSwagger();
    app.UseSwaggerExtension(typeof(Program));

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseCors();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
