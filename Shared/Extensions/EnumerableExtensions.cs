namespace SatisfactoryCalculator.Shared.Extensions;

public static class EnumerableExtensions
{
	public static bool Contains<TSource>(this IEnumerable<TSource> source, params TSource[] values)
	{
		foreach (TSource value in values)
		{
			if (Enumerable.Contains(source, value))
			{
				return true;
			}
		}
		return false;
	}

	public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, params TSource[] second)
	{
		return Enumerable.Except(first, second);
	}

    public static object First(this IEnumerable collection)
    {
        object retVal = null;
        IEnumerator en = collection.GetEnumerator();
        if (en.MoveNext()) retVal = en.Current;

        return retVal;
    }

	public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => !source.Any(predicate);
}
