using Shoply.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure(builder);
builder.Services.ConfigureContext();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureController();

builder.Host.ConfigureDependencyInjection();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseStaticFiles();
app.ApplyCors();
app.ApplyAuthentication();
app.ApplySwagger();
app.ApplyController();

app.Run();