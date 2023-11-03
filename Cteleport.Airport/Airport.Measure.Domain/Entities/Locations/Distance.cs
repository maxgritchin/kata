namespace Airport.Measure.Domain.Entities.Locations;

public struct Distance
{
    private readonly double _kilometers;
    
    public Distance(double kilometers)
    {
        _kilometers = kilometers;
    }
}