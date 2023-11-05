using Airport.Measure.Domain.Repositories;
using Airport.Measure.Domain.Services;
using Airport.Measure.Implementation.Repositories;
using Airport.Measure.Implementation.Repositories.Cache;
using Airport.Measure.Implementation.Repositories.Web;
using Airport.Measure.Implementation.Repositories.Web.Http;
using Airport.Measure.Implementation.Repositories.Web.Json;
using Airport.Measure.Implementation.Services.DistanceCalculators;
using Airport.Measure.Implementation.Services.Validators;

namespace Airport.MeasureService.WebApi.Extensions;

/// <summary>
/// Service Collection
/// </summary>
public static class ApplicationServiceCollectionExrensions
{
    /// <summary>
    /// Add web repository to read IATA code details
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/></param>
    /// <param name="url">URL to connect</param>
    /// <exception cref="ArgumentException">Throw when URL is empty</exception>
    public static IServiceCollection AddWebIataCodeRepository(this IServiceCollection services, string url)
    { 
       services.AddSingleton<IJsonParser, CteleportIataJsonParser>();
       services.AddSingleton<IHttpGet>(_ =>
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Web Repository base URL is not provided. Please, update configuration.");

            return new HttpGetService(url);
        });

       services.AddSingleton<IAirportCodesRepository, WebIataCodeRepository>();
       return services;
    }

    /// <summary>
    /// Add InMemory cache for the IATA code repository
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/></param>
    /// <exception cref="ArgumentException">Throw when cannot find the base <see cref="IAirportCodesRepository"/></exception>
    public static IServiceCollection AddInMemoryCacheForIataCodeRepository(this IServiceCollection services)
    {
        services.AddSingleton<IRepositoryCache, InMemoryRepositoryCache>();
        services.Decorate<IAirportCodesRepository, CachingIataCodeRepository>();

        return services;
    }

    /// <summary>
    /// Add IATA code operations
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/></param>
    public static IServiceCollection AddIataCodeOperationsServices(this IServiceCollection services)
    {
        services.AddSingleton<IIataCodeValidator, IataCodeValidator>();
        services.AddSingleton<IDistanceCalculator, HaversineFormula>();

        return services;
    }
}