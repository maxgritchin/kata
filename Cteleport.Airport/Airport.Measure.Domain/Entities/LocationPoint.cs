namespace Airport.Measure.Domain.Entities;

/// <summary>
/// Point of an airport's location
/// </summary>
public struct LocationPoint
{
    public double Longitude { get; }
    public double Latitude { get; }
    
    public LocationPoint(double lon, double lat)
    {
        Longitude = lon;
        Latitude = lat;
    }
}