using Airport.Measure.Domain.Exceptions;

namespace Airport.Measure.Domain.Entities.Codes;

/// <summary>
/// IATA code
/// </summary>
public record IataCode
{
    const string PATTERN = @"^[A-Z]{3}$";
    
    public string Value { get; }
    
    public IataCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidIataCode("IATA code cannot be empty");

        Value = Normalize(value);
    }

    private string Normalize(string code)
        => code.ToUpper().Trim();
}