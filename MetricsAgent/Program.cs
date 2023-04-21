using Microsoft.OpenApi.Any;
using Microsoft.EntityFrameworkCore;
using MetricsAgent.DAL;
using MetricsAgent.Repositories.CpuRepository;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using MetricsAgent;
using MetricsAgent.Logger;
using NLog;
using MetricsAgent.Repositories.DotNetRepository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

        // Add services to the container.

        builder.Services.AddControllers();
/*            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
            });*/


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            options.MapType<TimeSpan>(() => new Microsoft.OpenApi.Models.OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            })
        );


        builder.Services.AddDbContext<AgentDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("AgentDB"));
        });


        //add metrics services

        builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
        builder.Services.AddScoped<IDotNetMetricsRepository, DotNetRepository>();


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