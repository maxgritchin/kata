using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;

public class NoLocationFoundException: JsonParserException
{
    public NoLocationFoundException()
    {
    }

    protected NoLocationFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NoLocationFoundException(string? message) : base(message)
    {
    }

    public NoLocationFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}