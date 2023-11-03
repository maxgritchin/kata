namespace Airport.Measure.Domain.Services;
public interface IIataCodeValidator
{
    /// <summary>
    /// Validates whether a given string is a IATA airport code.
    /// </summary>
    /// <param name="code">The string to validate.</param>
    /// <returns>True if the string is a valid IATA airport code; otherwise, false.</returns>
    bool IsValidIataCode(string code); 
}