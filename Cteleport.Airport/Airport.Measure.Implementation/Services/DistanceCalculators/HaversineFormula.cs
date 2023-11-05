using Airport.Measure.Domain.Entities.Locations;
using Airport.Measure.Domain.Services;

namespace Airport.Measure.Implementation.Services.DistanceCalculators;

/// <summary>
/// Haversine formula to calculate distance between two points on a sphere
/// <remarks>https://en.wikipedia.org/wiki/Haversine_formula</remarks>
/// </summary>
public class HaversineFormula: IDistanceCalculator
{
    private const double EARTH_RADIUS_IN_MILES = 3963.19;

    /// <inheritdoc />
    public Distance Calculate(LocationPoint from, LocationPoint to, Direction direction)
    {
        // latitude and longitude from degrees to radians
        var radLat1 = Math.PI * from.Latitude / 180.0;
        var radLon1 = Math.PI * from.Longitude / 180.0;
        var radLat2 = Math.PI * to.Latitude / 180.0;
        var radLon2 = Math.PI * to.Longitude / 180.0;

        // longitudinal difference
        var deltaLon = radLon2 - radLon1;

        // central angle between the two points
        var centralAngle = Math.Acos(Math.Sin(radLat1) * Math.Sin(radLat2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Cos(deltaLon));

        // distance in miles based on the direction
        var distance = direction switch
        {
            Direction.West => EARTH_RADIUS_IN_MILES * centralAngle,
            Direction.East => EARTH_RADIUS_IN_MILES * (2 * Math.PI - centralAngle),
            _ => throw new ArgumentException($"Direction '{direction}' is not supported")
        };

        // Return the distances in both directions
        return new Distance(distance);
    }
}