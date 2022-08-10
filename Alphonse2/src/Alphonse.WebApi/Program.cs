using Alphonse.WebApi.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<AlphonseSettings>().Bind(builder.Configuration.GetSection("Alphonse"));

builder.ConfigureNLog();

builder.ConfigureKestrel();

builder.ConfigureDbContext();

builder.ConfigureValidators();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.SetupAlphonseData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    // app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // app.UseExceptionHandler("/error");
}

// app.UseHttpsRedirection();   // no http required (strictly on localhost)

app.UseAuthorization();

app.MapControllers();

app.Run();
