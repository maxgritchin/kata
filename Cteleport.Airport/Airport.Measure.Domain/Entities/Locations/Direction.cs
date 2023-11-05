using System.Runtime.Serialization;

namespace Airport.Measure.Domain.Entities.Locations;

/// <summary>
/// Direction to calculate distance
/// </summary>
public enum Direction
{
    [EnumMember(Value = "East")]
    East = 0,

    [EnumMember(Value = "West")]
    West = 1
}

