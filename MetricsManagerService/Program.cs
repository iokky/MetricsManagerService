using MetricsAgent.Logger;
using MetricsManagerService;
using MetricsManagerService.Repositories;
using MetricsManagerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Polly;

var builder = WebApplication.CreateBuilder(args);

//TODO: Добавть файл конифга логгера зарегить логгера


// Add services to the container.

builder.Services.AddControllers();


/*Add logger*/
builder.Services.AddTransient<IManagerLogger, ManagerLogger>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.MapType<TimeSpan>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    })
);

//Add Db Context
builder.Services.AddDbContext<ServiseDbContext>(options => 
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DB"));
    }, ServiceLifetime.Singleton);

// Add  services
    // HttpClient Use Polly
builder.Services.AddHttpClient<IMerticsAgentClient, MetricsAgentClient>()
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
    sleepDurationProvider: (attemptCount) => TimeSpan.FromMicroseconds(2000),
    onRetry: (exception, sleepDuration, attemptNumber, context) => 
    {
    
    }));

builder.Services.AddScoped<IAgentRepository, AgentsRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
