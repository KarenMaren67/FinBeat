using Contracts.Interfaces;
using DB;
using DB.DbContexts;
using DB.DbContexts.Linq2DbContexts;
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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VoskanyanFinBeatAPI", Version = "v1" });
        });

        //Custom services
        var postgreConnectionString = configuration.GetConnectionString("PostgreConnectionString");

        services.AddSingleton(new ConnectionStringsProvider(postgreConnectionString));

        services.AddLinqToDBContext<SomethingDataContext>((provider, options) =>
            options.UsePostgreSQL(postgreConnectionString)
                   .UseDefaultLogging(provider));

        services.AddScoped<ISomethingService, SomethingService>();

        services.AddTransient<ISomethingDbContext, SomethingDbContext>();
    }
}
