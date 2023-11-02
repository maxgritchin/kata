namespace Airport.MeasureService.WebApi.Entities;

/// <summary>
/// Point on the surface
/// </summary>
public struct Point
{
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public double X { get; }
    public double Y { get; }
}