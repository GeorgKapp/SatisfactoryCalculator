namespace SatisfactoryCalculator.Shared.Extensions;

public static class EnumerableExtensions
{
	public static bool Contains<TSource>(this IEnumerable<TSource> source, params TSource[] values)
	{
		foreach (var value in values)
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
}
