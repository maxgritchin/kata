using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;

public class FailedJsonParsingException: JsonParserException
{
    public FailedJsonParsingException()
    {
    }

    protected FailedJsonParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FailedJsonParsingException(string? message) : base(message)
    {
    }

    public FailedJsonParsingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}