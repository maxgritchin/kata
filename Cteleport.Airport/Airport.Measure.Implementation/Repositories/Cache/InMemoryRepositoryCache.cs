using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;

namespace Airport.Measure.Implementation.Repositories.Cache;

/// <summary>
/// In-Memory IATA repository cache
/// </summary>
public class InMemoryRepositoryCache: IRepositoryCache
{
    #region Private
    
    private readonly IDictionary<string, LocationPoint> _dict = new Dictionary<string, LocationPoint>();

    #endregion
    
    #region IRepositoryCache implementation

    /// <inheritdoc />
    public ValueTask<LocationPoint?> GetAsync(IataCode code)
    {
        // check if exists
        if (code == null ||
            !_dict.ContainsKey(code.Value))
            return ValueTask.FromResult<LocationPoint?>(null);
        
        // get 
        return ValueTask.FromResult<LocationPoint?>(_dict[code.Value]);
    }

    /// <inheritdoc />
    public Task PutAsync(IataCode code, LocationPoint location)
    {
        if (code != null)
        {
            _dict[code.Value] = location;
        }
        
        return Task.CompletedTask;
    }
    #endregion
}