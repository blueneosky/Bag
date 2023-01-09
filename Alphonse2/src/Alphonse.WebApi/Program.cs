using Alphonse.WebApi.Setup;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions{
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

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if(app.Environment.IsDevelopment() || settings.ForceSwagger)
{
    app.Logger.LogInformation("Swagger added");
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    // app.UseExceptionHandler("/error-development");
}
else
{
    // app.UseExceptionHandler("/error");
}

// app.UseHttpsRedirection();   // no http required (strictly on localhost)

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("WebApp Ready");
app.Run();
app.Logger.LogInformation("WebApp Shutdown");
