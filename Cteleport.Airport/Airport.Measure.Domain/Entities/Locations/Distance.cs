namespace Airport.Measure.Domain.Entities.Locations;

public struct Distance
{
    public int Miles { get; }
    
    public Distance(double miles)
    {
        Miles = (int)miles;
    }
}