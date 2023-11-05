using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Airport.Measure.Implementation.Repositories.Cache;

/// <summary>
/// In-Memory IATA repository cache
/// </summary>
public class InMemoryRepositoryCache: IRepositoryCache
{
    #region .ctor

    public InMemoryRepositoryCache(): this(NullLogger<InMemoryRepositoryCache>.Instance) {}
    
    public InMemoryRepositoryCache(ILogger<InMemoryRepositoryCache> logger)
    {
        _logger = logger;
    }
    
    #endregion
    
    #region Private
    
    private readonly IDictionary<string, LocationPoint> _dict = new Dictionary<string, LocationPoint>();
    private readonly ILogger<InMemoryRepositoryCache> _logger;
    
    #endregion
    
    #region IRepositoryCache implementation

    /// <inheritdoc />
    public ValueTask<LocationPoint?> GetAsync(IataCode code)
    {
        _logger.LogTrace("Get '{IataCode}' from Cache", code?.Value);
            
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
        _logger.LogTrace("Put '{IataCode}' into Cache", code?.Value);
        
        if (code != null)
        {
            _dict[code.Value] = location;
        }
        
        return Task.CompletedTask;
    }
    #endregion
}