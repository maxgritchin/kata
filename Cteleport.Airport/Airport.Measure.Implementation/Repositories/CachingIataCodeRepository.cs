using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Exceptions;
using Airport.Measure.Domain.Repositories;
using Airport.Measure.Implementation.Repositories.Cache;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Airport.Measure.Implementation.Repositories;

/// <summary>
/// Caching IATA codes repository
/// </summary>
public class CachingIataCodeRepository: IAirportCodesRepository
{
    #region .ctor

    public CachingIataCodeRepository(
        IAirportCodesRepository codesRepository,
        IRepositoryCache cache): this(codesRepository, cache, NullLogger<CachingIataCodeRepository>.Instance) {}
        
    public CachingIataCodeRepository(
        IAirportCodesRepository codesRepository,
        IRepositoryCache cache,
        ILogger<CachingIataCodeRepository> logger)
    {
        _codesRepository = codesRepository ?? throw new ArgumentNullException(nameof(codesRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger;
    }

    #endregion
 
    #region Private
    
    private readonly IAirportCodesRepository _codesRepository;
    private readonly IRepositoryCache _cache;
    private readonly ILogger<CachingIataCodeRepository> _logger;

    #endregion
    
    #region IAirportCodesRepository implementation

    /// <inheritdoc />
    public async Task<LocationPoint?> GetLocationAsync(IataCode code)
    {
        _logger.LogDebug("Get Location for '{IataCode}' from Caching Repository", code.Value);
        
        // validate 
        if (code == null)
            throw new InvalidIataCode("IATA code is not specified");
        
        // trying to get from the cache
        var location = await _cache.GetAsync(code);
        if (location != null)
        {
            _logger.LogDebug(
                "Cache contains Loactions for '{IataCode}'. Data: '{Latitude}', '{Longitude}'", 
                code.Value, 
                location.Value.Latitude, 
                location.Value.Longitude);
            
            return location.Value;
        }
        
        // get from base repository 
        location = await _codesRepository.GetLocationAsync(code);
        
        // put to the cache 
        if (location != null)
        {
            _logger.LogDebug(
                "Update record in Cache for '{IataCode}'. Data: '{Latitude}', '{Longitude}'", 
                code.Value, 
                location.Value.Latitude, 
                location.Value.Longitude);
            
            await _cache.PutAsync(code, location.Value);
        }
        
        // 
        return location;
    }
    
    #endregion
}