using Contracts.Interfaces;
using DB;
using DB.DbContexts.Linq2DbContexts;
using Library;
using Library.Services;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.OpenApi.Models;

namespace VoskanyanFinBeatApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        //Defailt services
        services.AddControllers();
        services.AddCors();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VoskanyanFinBeatAPI", Version = "v1" });
        });

        //Custom services
        var postgreConnectionString = configuration.GetConnectionString("PostgreConnectionString");
        services.AddSingleton(new ConnectionStringsProvider(postgreConnectionString));

        var kafkaBootstrapServers = configuration.GetConnectionString("KafkaBootstrapServers");
        services.AddSingleton(new KafkaConnectionStringsProvider(kafkaBootstrapServers));
        services.AddScoped<IKafkaProducerService, KafkaProducerService>();

        services.AddLinqToDBContext<SomethingDataContext>((provider, options) =>
            options.UsePostgreSQL(postgreConnectionString)
                   .UseDefaultLogging(provider));

        services.AddScoped<ISomethingService, SomethingService>();

        //Реализация с даппером
        //services.AddTransient<ISomethingDbContext, SomethingDbContext>();


    }
}
