using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Services;

namespace Airport.Measure.Implementation.Services.DistanceCalculators;

/// <summary>
/// Haversine formula to calculate distance between two points on a sphere
/// <remarks>https://en.wikipedia.org/wiki/Haversine_formula</remarks>
/// </summary>
public class HaversineFormula: IDistanceCalculator
{
    /// <inheritdoc />
    public Distance Calculate(LocationPoint form, LocationPoint to)
    {
        throw new NotImplementedException();
    }
}