using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;

namespace Airport.Measure.Domain.Repositories;

/// <summary>
/// Airport codes repository
/// </summary>
public interface IAirportCodesRepository
{
     /// <summary>
     /// Get Location by the provided IATA code
     /// </summary>
     /// <param name="code">IATA code</param>
     /// <returns></returns>
     Task<LocationPoint?> GetLocationAsync(IataCode code);
}