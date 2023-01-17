using AuthJWT.Config;
using AuthJWT.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//private readonly IConfiguration configuration;
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//configure jwt authentication
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.AddOurAuthentication(appSettings);
builder.Services.AddOurSwagger();
builder.Services.AddScoped<IUserService, UserService>();
// End configure jwt authentication
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
