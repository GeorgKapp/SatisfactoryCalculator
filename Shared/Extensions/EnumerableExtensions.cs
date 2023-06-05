namespace SatisfactoryCalculator.Shared.Extensions;

public static class EnumerableExtensions
{
	// ReSharper disable once HeapView.ClosureAllocation
	public static bool Contains<TSource>(this IEnumerable<TSource> source, params TSource[] values)
	{
		return values
			.Any(value => Enumerable.Contains(source, value));
	}

	public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, params TSource[] second)
	{
		return Enumerable.Except(first, second);
	}
}
