namespace Airport.Measure.Domain.Entities.Locations;

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
    
    #region Equity
    
    private sealed class longitudeLatitudeEqualityComparer : IEqualityComparer<LocationPoint>
    {
        public bool Equals(LocationPoint x, LocationPoint y)
        {
            return x.Longitude.Equals(y.Longitude) && x.Latitude.Equals(y.Latitude);
        }

        public int GetHashCode(LocationPoint obj)
        {
            return HashCode.Combine(obj.Longitude, obj.Latitude);
        }
    }

    public static IEqualityComparer<LocationPoint> LongitudeLatitudeComparer { get; } = new longitudeLatitudeEqualityComparer();
    
    #endregion
}