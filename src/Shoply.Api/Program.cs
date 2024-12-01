using Shoply.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext();
builder.Services.ConfigureCors();
builder.Services.ConfigureController();
builder.Services.ConfigureMapper();

builder.Host.ConfigureDependencyInjection();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseStaticFiles();
app.ApplyCors();
app.ApplyController();

app.Run();