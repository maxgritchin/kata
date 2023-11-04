using System.Runtime.Serialization;

namespace Airport.Measure.Domain.Exceptions;

/// <summary>
/// Invalid IATA code 
/// </summary>
public class InvalidIataCode: ArgumentException
{
    public InvalidIataCode()
    {
    }

    protected InvalidIataCode(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidIataCode(string? message) : base(message)
    {
    }

    public InvalidIataCode(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public InvalidIataCode(string? message, string? paramName) : base(message, paramName)
    {
    }

    public InvalidIataCode(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}