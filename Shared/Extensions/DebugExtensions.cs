namespace SatisfactoryCalculator.Shared.Extensions;

public static class DebugExtensions
{
    public static T Profile<T>(Func<T> func, string name)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = func();
        stopwatch.Stop();
        Debug.WriteLine($"Ellapsed Time for {name}: {stopwatch.Elapsed}");
        return result;
    }

    public static async Task<T> ProfileAsync<T>(Task<T> task, string name)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = await task;
        stopwatch.Stop();
        Debug.WriteLine($"Ellapsed Time for {name}: {stopwatch.Elapsed}");
        return result;
    }
}
