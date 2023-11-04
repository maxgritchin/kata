using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;

public class InvalidJsonContentException: JsonParserException
{
    public InvalidJsonContentException()
    {
    }

    protected InvalidJsonContentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidJsonContentException(string? message) : base(message)
    {
    }

    public InvalidJsonContentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}