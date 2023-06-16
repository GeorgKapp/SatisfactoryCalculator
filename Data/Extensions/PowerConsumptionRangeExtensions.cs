namespace Data.Extensions;

internal static class PowerConsumptionRangeExtensions
{
    public static string? ConvertToDataString(this PowerConsumptionRange? input)
    {
        return input is not null 
            ? $"{input.Start}-{input.End}" 
            : null;
    }

    public static PowerConsumptionRange? ConvertToPowerConsumptionRange(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return null;

        var splittedInput = input.Split("-");

        return new(
            Convert.ToDecimal(splittedInput[0]),
            Convert.ToDecimal(splittedInput[2]));
    }
}
