using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Exceptions;

/// <summary>
/// Invalid URL parameter
/// </summary>
public class InvalidUrlParameter: Exception
{
    public InvalidUrlParameter()
    {
    }

    protected InvalidUrlParameter(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidUrlParameter(string? message) : base(message)
    {
    }

    public InvalidUrlParameter(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}