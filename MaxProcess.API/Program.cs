using MaxProcess.Persistence;
using MaxProcess.Application;
using MaxProcess.CrossCutting.Security;
using MaxProcess.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
