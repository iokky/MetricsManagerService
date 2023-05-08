using MetricsManagerService.Logger;
using MetricsManagerService.Repositories;
using MetricsManagerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Polly;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using MetricsManagerService.Jobs;
using MetricsManagerService;
using MetricsManagerService.Models;
using MetricsManagerService.Repositories.CPU;
using MetricsManagerService.Repositories.Hdd;
using MetricsManagerService.Repositories.Network;
using MetricsManagerService.Repositories.Ram;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//TODO: Добавть файл конифга логгера зарегить логгера


// Add services to the container.

builder.Services.AddControllers();


/*Add logger*/
builder.Services.AddTransient<IManagerLogger, ManagerLogger>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "MetricsMagagerSerice",
        Description = "Endpoind Our metrics",
        TermsOfService = new Uri("https://example.com/terms"),
        Version="v1",
        Contact = new OpenApiContact 
        {
            Name = "Anton",
            Email = "domeniqueflo@gmail.com"
        }
    });

    options.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });

    // Add ApiDoc File in Root Dir
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.EnableAnnotations(); // Enable Ananation Flagss for Swashbuckle.AspNetCore.Annotations (use for controller anotation)

});

/*Add Db Context*/
//Metrics
builder.Services.AddDbContext<ServiceDbContext>(options => 
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DB"));
    }, ServiceLifetime.Singleton);

//Agent
builder.Services.AddDbContext<ServiceAgentDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("AGent"));
}, ServiceLifetime.Singleton);

/*Add automapper*/
builder.Services.AddAutoMapper(typeof(Program).Assembly);

/*Add  services*/
// HttpClient Use Polly
builder.Services.AddHttpClient<IMerticsAgentClient, MetricsAgentClient>()
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
    sleepDurationProvider: (attemptCount) => TimeSpan.FromMicroseconds(2000),
    onRetry: (exception, sleepDuration, attemptNumber, context) => 
    {
       
    }));

/*Add Quartz*/ //Single
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddHostedService<QuartzHostedService>();

//Add Jobs
builder.Services.AddTransient<CpuMetricsJob>();
builder.Services.AddSingleton(new JobSchedule(typeof(CpuMetricsJob), "0/9 * * ? * * *"));

builder.Services.AddTransient<HddMetricsJob>();
builder.Services.AddSingleton(new JobSchedule(typeof(HddMetricsJob), "0/9 * * ? * * *"));

builder.Services.AddTransient<NetworkMetricsJob>();
builder.Services.AddSingleton(new JobSchedule(typeof(NetworkMetricsJob), "0/9 * * ? * * *"));

builder.Services.AddTransient<RamMetricsJob>();
builder.Services.AddSingleton(new JobSchedule(typeof(RamMetricsJob), "0/9 * * ? * * *"));


builder.Services.AddScoped<IAgentRepository, AgentsRepository>();

/*Add Metrics Services*/


/*Add Repositories*/
builder.Services.AddTransient<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddTransient<IHddMetricsRepository, HddMetricsRepository>();
builder.Services.AddTransient<INetworkMetricsRepository, NetworkMetricsRepository>();
builder.Services.AddTransient<IRamMetricsRepository, RamMetricsRepository>();

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
