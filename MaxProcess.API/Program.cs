using MaxProcess.Persistence;
using MaxProcess.Application;
using MaxProcess.CrossCutting;
using MaxProcess.API.Configure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp(builder.Configuration);
builder.Services.ConfigureCrossCuttingApp();
builder.Services.ConfigureWebApiApp(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
