using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Airport.MeasureService.WebApi.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/distance")]
public class DistanceCalculatorController: ControllerBase
{
    /// <summary>
    /// Calculate the distance between two airports using their IATA codes.
    /// </summary>
    /// <param name="fromIataCode">The IATA code of the departure airport.</param>
    /// <param name="toIataCode">The IATA code of the destination airport.</param>
    /// <returns>The distance in miles between the two airports.</returns>
    [HttpGet("calculate"), MapToApiVersion(1.0)]
    public string CalculateDistanceBetweenAirports([FromQuery] string fromIataCode, [FromQuery] string toIataCode)
    {
        
        throw new NotImplementedException();
    }
}