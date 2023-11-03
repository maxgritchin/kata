using Airport.Measure.Domain.Entities;

namespace Airport.Measure.Domain.Services;

/// <summary>
/// Calculator of the distance between two points
/// </summary>
public interface IDistanceCalculator
{
    /// <summary>
    /// Calculate distance between two locations
    /// </summary>
    /// <param name="form">First location point</param>
    /// <param name="to">Second location point</param>
    /// <returns></returns>
    Distance Calculate(LocationPoint form, LocationPoint to);
}