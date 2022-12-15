using src;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions<LocationSettings>()
    .Bind(builder.Configuration.GetSection(nameof(LocationSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

app.MapControllers();
app.Map("/", () => "OK");

app.Run();
