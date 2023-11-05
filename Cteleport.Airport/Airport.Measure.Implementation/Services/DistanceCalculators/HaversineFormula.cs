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
    public Distance Calculate(LocationPoint from, LocationPoint to)
    {
        // convert latitude and longitude from degrees to radians
        var radLat1 = Math.PI * from.Latitude / 180.0;
        var radLon1 = Math.PI * from.Longitude / 180.0;
        var radLat2 = Math.PI * to.Latitude / 180.0;
        var radLon2 = Math.PI * to.Longitude / 180.0;

        // tricky formula
        var deltaLat = radLat2 - radLat1;
        var deltaLon = radLon2 - radLon1;
        
        var a =  Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                 Math.Cos(radLat1) * Math.Cos(radLat2) *
                 Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
        
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var miles = EARTH_RADIUS_IN_MILES * c;

        //
        return new Distance(miles);
    }
}