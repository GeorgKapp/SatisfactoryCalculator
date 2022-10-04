namespace SatisfactoryCalculator.Shared.Extensions;

public static class ArrayExtensions
{
    public static T[] Add<T>(this T[] array, T value)
    {
        return array.Append(value).ToArray();
    }
}
