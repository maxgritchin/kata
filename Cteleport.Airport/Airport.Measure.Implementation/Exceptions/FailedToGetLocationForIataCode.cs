using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Exceptions;

public class FailedToGetLocationForIataCodeException: Exception
{
    public FailedToGetLocationForIataCodeException()
    {
    }

    protected FailedToGetLocationForIataCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FailedToGetLocationForIataCodeException(string? message) : base(message)
    {
    }

    public FailedToGetLocationForIataCodeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}