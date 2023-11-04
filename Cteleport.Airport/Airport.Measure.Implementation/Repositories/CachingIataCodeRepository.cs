using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Exceptions;
using Airport.Measure.Domain.Repositories;
using Airport.Measure.Implementation.Repositories.Cache;

namespace Airport.Measure.Implementation.Repositories;

/// <summary>
/// Caching IATA codes repository
/// </summary>
public class CachingIataCodeRepository: IAirportCodesRepository
{
    #region .ctor

    public CachingIataCodeRepository(
        IAirportCodesRepository codesRepository,
        IRepositoryCache cache)
    {
        _codesRepository = codesRepository ?? throw new ArgumentNullException(nameof(codesRepository));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    #endregion
 
    #region Private
    
    private readonly IAirportCodesRepository _codesRepository;
    private readonly IRepositoryCache _cache;
    
    #endregion
    
    #region IAirportCodesRepository implementation

    /// <inheritdoc />
    public async Task<LocationPoint?> GetLocationAsync(IataCode code)
    {
        // validate 
        if (code == null)
            throw new InvalidIataCode("IATA code is not specified");
        
        // trying to get from the cache
        var location = await _cache.GetAsync(code);
        if (location != null)
            return location.Value;
        
        // get from base repository 
        location = await _codesRepository.GetLocationAsync(code);
        
        // put to the cache 
        if (location != null)
        {
            await _cache.PutAsync(code, location.Value);
        }
        
        // 
        return location;
    }
    
    #endregion
}