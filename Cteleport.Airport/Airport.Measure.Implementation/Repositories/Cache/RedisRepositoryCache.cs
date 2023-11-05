using System.Text.Json;
using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Airport.Measure.Implementation.Repositories.Cache;

/// <summary>
/// Redis IATA repository cache
/// </summary>
public class RedisRepositoryCache: IRepositoryCache
{
    #region .ctor

    public RedisRepositoryCache(IDistributedCache cache): this(cache, NullLogger<RedisRepositoryCache>.Instance) {}
    
    public RedisRepositoryCache(IDistributedCache cache, ILogger<RedisRepositoryCache> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger;
    }
    
    #endregion
    
    #region Private
    
    private readonly IDictionary<string, LocationPoint> _dict = new Dictionary<string, LocationPoint>();
    private readonly IDistributedCache _cache;
    private readonly ILogger<RedisRepositoryCache> _logger;
    
    #endregion
    
    #region IRepositoryCache implementation

    /// <inheritdoc />
    public async ValueTask<LocationPoint?> GetAsync(IataCode code)
    {
        _logger.LogTrace("Get '{IataCode}' from Redis Cache", code?.Value);
            
        // validate
        if (code == null)
            return null;

        // get
        try
        {
            var key = code.Value;
            var jsonContentOfLocation= await _cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(jsonContentOfLocation))
            {
                _logger.LogInformation("Cache does not contain info for '{IataCode}'", key);
                return null;
            }
            _logger.LogTrace("JSON from Radis cache: {Json}", jsonContentOfLocation);
                
            // parse 
            var location = JsonSerializer.Deserialize<LocationPoint>(jsonContentOfLocation);
            _logger.LogTrace(
                "Redis Cache contains data for '{IataCode}': {Longitude} and {Latitude}", 
                key,
                location.Longitude,
                location.Latitude);
        
            return location;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while reading data from Redis cache. Error: {Ex}", ex.ToString());
            return null;
        }
    }

    /// <inheritdoc />
    public async Task PutAsync(IataCode code, LocationPoint location)
    {
        _logger.LogTrace("Put '{IataCode}' to Redis Cache", code?.Value);

        if (code == null)
        {
            _logger.LogWarning("Cannot write empty code as key to Redis");
            return;
        }

        try
        {
            var key = code.Value;
            var json = JsonSerializer.Serialize(location);
            var options = new DistributedCacheEntryOptions
            {
                // here need to figure out time expiration
            };

            await _cache.SetStringAsync(key, json, options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while storing data in Redis cache. Error: {Ex}", ex.ToString());
        }
    }

    #endregion
}