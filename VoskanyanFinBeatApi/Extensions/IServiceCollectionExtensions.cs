using Contracts.Interfaces;
using DB.Configuration;
using DB.DbContexts;
using Library.Services;
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
        services.AddOptions<PgSqlDbConfiguration>()
            .Bind(configuration.GetSection(PgSqlDbConfiguration.SectionName))
            .ValidateDataAnnotations();

        services.AddTransient<ISomethingService, SomethingService>();
        services.AddTransient<ISomethingDbContext, SomethingDbContext>();
    }
}
