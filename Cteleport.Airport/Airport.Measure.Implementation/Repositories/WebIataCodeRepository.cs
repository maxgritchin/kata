using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Repositories;
using Airport.Measure.Implementation.Repositories.Web;

namespace Airport.Measure.Implementation.Repositories;

public class WebIataCodeRepository: IAirportCodesRepository
{
    #region .ctor

    public WebIataCodeRepository(IHttpGet http, IJsonParser json)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http), "HTTP getter must be provided.");
        _json = json ?? throw new ArgumentNullException(nameof(json), "JSON parser must be provided.");
    }
    
    #endregion

    #region Private 
    
    private readonly IHttpGet _http;
    private readonly IJsonParser _json;
    
    #endregion
    
    #region IAirportCodesRepository
    
    public async Task<LocationPoint?> GetLocationAsync(IataCode code)
    {
        // validate
        if (code == null)
            throw new ArgumentNullException(nameof(code));
        
        // make a request 
        var response = await _http.GetAsync(code.Value);

        // parse
        return _json.GetLocationFromJson(response);
    }
    
    #endregion
}