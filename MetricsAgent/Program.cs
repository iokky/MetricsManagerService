using Microsoft.OpenApi.Any;
using Microsoft.EntityFrameworkCore;
using MetricsAgent.DAL;
using MetricsAgent.Repositories.CpuRepository;
using MetricsAgent.Logger;
using NLog;
using MetricsAgent.Repositories.DotNetRepository;
using MetricsAgent.Repositories.HddRepository;
using MetricsAgent.Repositories.NetworkRepository;
using MetricsAgent.Repositories.RamRepository;
using Quartz;
using Quartz.Spi;
using MetricsAgent.Jobs;
using Quartz.Impl;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            options.MapType<TimeSpan>(() => new Microsoft.OpenApi.Models.OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            })
        );


        /*Add Db Context*/
        builder.Services.AddDbContext<AgentDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("AgentDB"));  
            // Lifetime options
        }, ServiceLifetime.Singleton);



        /*Add automapper*/
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        /*Add metrics services*/

            // Last AddScoped
        builder.Services.AddTransient<ICpuMetricsRepository, CpuMetricsRepository>();
            // Last AddScoped
        builder.Services.AddTransient<IDotNetMetricsRepository, DotNetRepository>();
            // Last AddScoped
        builder.Services.AddTransient<IHddRepository, HddRepository>();
        // Last AddScoped
        builder.Services.AddTransient<INetworkRepository, NetworkRepository>();
            // Last AddScoped
        builder.Services.AddTransient<IRamRepository, RamRepository>();

        /*Add Quartz*/
        builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
        builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

        builder.Services.AddHostedService<QuartzHostedService>();

            //Add Jobs

                //Cpu
        builder.Services.AddTransient<CpuMetricsJob>();
        builder.Services.AddSingleton(new JobSchedule(typeof(CpuMetricsJob), "0/5 * * ? * * *"));
                //DotNet
        //builder.Services.AddTransient<DotNetMetricsJob>();
        //builder.Services.AddSingleton(new JobSchedule(typeof(DotNetMetricsJob), "0/5 * * ? * * *"));
                //Ram
        builder.Services.AddTransient<RamMetricsJob>();
        builder.Services.AddSingleton(new JobSchedule(typeof(RamMetricsJob), "0/5 * * ? * * *"));
                //Hdd
        builder.Services.AddTransient<HddMetricsJob>();
        builder.Services.AddSingleton(new JobSchedule(typeof(HddMetricsJob), "0/5 * * ? * * *"));
                //Network
        builder.Services.AddTransient<NetworkMetricsJob>();
        builder.Services.AddSingleton(new JobSchedule(typeof(NetworkMetricsJob), "0/5 * * ? * * *"));

        /*Add logger*/
        builder.Services.AddScoped<IAgentLogger, AgentLogger>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}