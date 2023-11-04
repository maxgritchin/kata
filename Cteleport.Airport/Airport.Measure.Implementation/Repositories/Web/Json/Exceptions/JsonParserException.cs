using System.Runtime.Serialization;

namespace Airport.Measure.Implementation.Repositories.Web.Json.Exceptions;

public class JsonParserException: Exception
{
    public JsonParserException()
    {
    }

    protected JsonParserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public JsonParserException(string? message) : base(message)
    {
    }

    public JsonParserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}