using System.Text.RegularExpressions;
using Airport.Measure.Domain.Services;

namespace Airport.Measure.Implementation.Services.Validators;

/// <inheritdoc />
public class IataCodeValidator: IIataCodeValidator
{
    const string PATTERN = @"^[A-Z]{3}$";

    /// <inheritdoc />
    public bool IsValidIataCode(string code)
        => Regex.IsMatch(code, PATTERN);
}