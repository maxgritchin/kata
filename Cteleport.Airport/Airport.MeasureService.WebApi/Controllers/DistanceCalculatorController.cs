using Airport.Measure.Domain.Entities.Codes;
using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Repositories;
using Airport.Measure.Domain.Services;
using Airport.Measure.Implementation.Exceptions;
using Airport.Measure.Implementation.Services.Validators;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Airport.MeasureService.WebApi.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/distance")]
public class DistanceCalculatorController: ControllerBase
{
    #region Private 
    
    private readonly IIataCodeValidator _iataCodeValidator;
    private readonly IAirportCodesRepository _repository;
    private readonly IDistanceCalculator _calculator;

    #endregion
    
    #region .ctor

    public DistanceCalculatorController(
        IIataCodeValidator iataCodeValidator,
        IAirportCodesRepository repository,
        IDistanceCalculator calculator)
    {
        _iataCodeValidator = iataCodeValidator;
        _repository = repository;
        _calculator = calculator;
    }
    
    #endregion
    
    #region Private

    private BadRequestObjectResult? ValidateIataCode(string code, string name) =>
        !_iataCodeValidator.IsValidIataCode(code) 
            ? BadRequest($"Invalid '{name}' parameter. It should be a valid 3-letter IATA code.") 
            : null;

    private async Task<ActionResult<double>> CalculateDistanceInMilesAsync(IataCode from, IataCode to)
    {
        try
        {
            var f = await _repository.GetLocationAsync(from);
            var t = await _repository.GetLocationAsync(to);

            // calculate distance
            var distance = _calculator.Calculate(f.Value, t.Value);

            // return result
            return Ok(new { DistanceInMiles = distance.Miles });
        }
        catch (FailedToGetLocationForIataCodeException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    #endregion
    
    /// <summary>
    /// Calculate the distance between two airports using their IATA codes.
    /// </summary>
    /// <param name="fromIataCode">The IATA code of the departure airport.</param>
    /// <param name="toIataCode">The IATA code of the destination airport.</param>
    /// <returns>The distance in miles between the two airports.</returns>
    [HttpGet("calculate"), MapToApiVersion(1.0)]
    public async Task<ActionResult<double>> CalculateDistanceBetweenAirports([FromQuery] string fromIataCode, [FromQuery] string toIataCode)
    {
        // codes
        var airportFrom = new IataCode(fromIataCode);
        var airportTo = new IataCode(toIataCode);
        
        // validate codes 
        var validationResult =
            ValidateIataCode(airportFrom.Value, nameof(fromIataCode)) ??
            ValidateIataCode(airportTo.Value, nameof(toIataCode));
        
        if (validationResult != null)
            return validationResult;
        
        // calculate
        return await CalculateDistanceInMilesAsync(airportFrom, airportTo);
    }
}