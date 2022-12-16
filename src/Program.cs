using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using src;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddApplicationInsightsTelemetry();
services.AddControllers();
services.AddOptions<LocationSettings>()
    .Bind(builder.Configuration.GetSection(nameof(LocationSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();
    
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        builder.Configuration.Bind(options.GetType().Name, options);   
    });

var app = builder.Build();
app.UseAuthentication();
app.MapControllers();
app.Map("/", () =>
{
    app.Logger.LogInformation("Answering 'OK'");
    return "OK";
});

try
{
    app.Run();
}
catch (OptionsValidationException e)
{
    var sp = builder.Services.BuildServiceProvider();
    var telemetryClient = sp.GetRequiredService<TelemetryClient>();
    telemetryClient.TrackException(e);
    telemetryClient.Flush();
}
