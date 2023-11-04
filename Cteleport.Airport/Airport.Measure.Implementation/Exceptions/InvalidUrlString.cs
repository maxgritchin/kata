using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Exceptions;

/// <summary>
/// Invalid URL parameter
/// </summary>
public class InvalidUrlParameterException: Exception
{
    public InvalidUrlParameterException()
    {
    }

    protected InvalidUrlParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidUrlParameterException(string? message) : base(message)
    {
    }

    public InvalidUrlParameterException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}