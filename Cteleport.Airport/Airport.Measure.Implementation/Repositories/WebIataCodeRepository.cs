using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Repositories;
using Airport.Measure.Implementation.Exceptions;
using Airport.Measure.Implementation.Repositories.Web;
using Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Airport.Measure.Implementation.Repositories;

public class WebIataCodeRepository: IAirportCodesRepository
{
    #region .ctor

    public WebIataCodeRepository(
        IHttpGet http,
        IJsonParser json) : this(http, json, NullLogger<WebIataCodeRepository>.Instance) {}
    
    public WebIataCodeRepository(
        IHttpGet http, 
        IJsonParser json,
        ILogger<WebIataCodeRepository> logger) 
    {
        _http = http ?? throw new ArgumentNullException(nameof(http), "HTTP getter must be provided.");
        _json = json ?? throw new ArgumentNullException(nameof(json), "JSON parser must be provided.");
        _logger = logger;
    }
    
    #endregion

    #region Private 
    
    private readonly IHttpGet _http;
    private readonly IJsonParser _json;
    private readonly ILogger<WebIataCodeRepository> _logger;
    
    #endregion
    
    #region IAirportCodesRepository
    
    public async Task<LocationPoint?> GetLocationAsync(IataCode code)
    {
        _logger.LogDebug("Get locations from Web repository for '{IataCode}'", code?.Value);
            
        // validate
        if (code == null)
            throw new ArgumentNullException(nameof(code));
        
        // make a request 
        var response = await _http.GetAsync(code.Value);
        if (string.IsNullOrWhiteSpace(response))
        {
            _logger.LogWarning("Response from Web for '{IataCode}' is empty", code.Value);
            return null;
        }

        // parse
        try
        {
            _logger.LogDebug("Response for '{IataCode}': {Response}", code.Value, response);
            return _json.GetLocationFromJson(response);
        }
        catch (JsonParserException ex)
        {
            _logger.LogError("Error for JSON parsing: {Exception}", ex.ToString());
            throw new FailedToGetLocationForIataCodeException($"{code.Value}: {ex.Message}", ex);
        }
    }
    
    #endregion
}