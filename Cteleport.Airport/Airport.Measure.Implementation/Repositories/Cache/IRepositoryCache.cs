using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;

namespace Airport.Measure.Implementation.Repositories.Cache;

/// <summary>
/// Repository cache 
/// </summary>
public interface IRepositoryCache
{
    /// <summary>
    /// Get IATA location from cache
    /// </summary>
    /// <param name="code">IATA code to check</param>
    /// <returns>Location is exists in cache; otherwise, false.</returns>
    public ValueTask<LocationPoint?> GetAsync(IataCode code);

    /// <summary>
    /// Put a value to cache
    /// </summary>
    /// <param name="code">IATA code</param>
    /// <param name="location">Location value</param>
    /// <returns></returns>
    public Task PutAsync(IataCode code, LocationPoint location);
}