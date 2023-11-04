using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;

public class UnexpectedJsonParserException: JsonParserException
{
    public UnexpectedJsonParserException()
    {
    }

    protected UnexpectedJsonParserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnexpectedJsonParserException(string? message) : base(message)
    {
    }

    public UnexpectedJsonParserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}