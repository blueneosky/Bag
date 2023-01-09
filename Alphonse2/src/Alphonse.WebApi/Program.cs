using System.Text;
using Alphonse.WebApi.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // otherwize, CurrentDirectory is used
    ContentRootPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
});

builder.ConfigureNLog();

builder.ConfigureOptions();

builder.ConfigureKestrel();

builder.ConfigureDbContext();

builder.ConfigureServices();

builder.ConfigureValidators();

// Add authentication
builder.ConfigureAuthentication();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ConfigureAuthentication(builder);
});

var app = builder.Build();

app.Logger.LogInformation("WebApp Starting...");

try
{
    app.SetupAlphonseData();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Unexpected error!");
    throw;
}

// Configure the HTTP request pipeline.
var settings = app.Services.GetService<IOptions<AlphonseSettings>>()!.Value;
if (app.Environment.IsDevelopment() || settings.ForceSwagger)
{
    app.Logger.LogInformation("Swagger added");
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseExceptionHandler("/error-development");
}
else
{
    // app.UseExceptionHandler("/error");
}

// app.UseHttpsRedirection();   // no https required (strictly on localhost - opened through reverse proxy)

if (!settings.WithoutAuthorization)
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapControllers()
    .WhenFalse(settings.WithoutAuthorization, b => b.RequireAuthorization());

app.Logger.LogInformation("WebApp Ready");
app.Run();
app.Logger.LogInformation("WebApp Shutdown");
