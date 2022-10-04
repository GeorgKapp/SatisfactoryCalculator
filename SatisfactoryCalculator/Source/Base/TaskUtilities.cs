namespace SatisfactoryCalculator.Source.Base;

internal static class TaskUtilities
{
    public static async void FireAndForgetSafeAsync(this Task task)
    {
        await task;
    }
}
