using System.Text.Json;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Airport.Measure.Implementation.Repositories.Web.Json;

/// <inheritdoc />
public class CteleportIataJsonParser: IJsonParser
{
    #region .ctor

    public CteleportIataJsonParser() : this(NullLogger<CteleportIataJsonParser>.Instance) {}
    
    public CteleportIataJsonParser(ILogger<CteleportIataJsonParser> logger)
    {
        _logger = logger;
    }

    #endregion
    
    #region Private 
    
    #region Sections

    private const string AIRPORT_TYPE = "airport";

    #endregion
    
    #region DTO 
    
    private class LocationDto
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    } 
    
    private class LocationDataDto
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public LocationDto Location { get; set; }
    }
    
    #endregion
    
    private readonly ILogger<CteleportIataJsonParser> _logger;
    
    #endregion
    
    #region IJsonParser implementation

    /// <inheritdoc />
    public LocationPoint GetLocationFromJson(string json)
    {
        _logger.LogTrace("Parse JSON: {Json}", json);
            
        // validate
        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidJsonContentException("Invalid JSON content for IATA code");
        
        // parse
        var obj = JsonSerializer.Deserialize<LocationDataDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (obj == null)
            throw new FailedJsonParsingException("Failed to parse JSON for IATA code. Result is null.");
        
        // check if airport found 
        if (obj.Type == AIRPORT_TYPE)
            return new LocationPoint(obj.Location.Lon, obj.Location.Lat);

        // no location
        if (obj.Type == null)
            throw new NoLocationFoundException(obj.Detail ?? "");

        //
        throw new UnexpectedJsonParserException("Unexpected exception when parsing JSON");
    }
    
    #endregion
}