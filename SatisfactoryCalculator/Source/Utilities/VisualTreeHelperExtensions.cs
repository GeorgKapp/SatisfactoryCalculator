namespace SatisfactoryCalculator.Source.UI.Extensions;

internal static class VisualTreeHelperExtensions
{
    public static T? FindVisualChild<T>(this DependencyObject parent) where T : DependencyObject
    {
        var childCount = VisualTreeHelper.GetChildrenCount(parent);
        for (var i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
                return typedChild;

            var foundChild = FindVisualChild<T>(child);
            if (foundChild is not null)
                return foundChild;
        }

        return null;
    }

    public static T FindVisualParent<T>(this DependencyObject child) where T : DependencyObject
    {
        var parent = VisualTreeHelper.GetParent(child) ?? throw new Exception("Parent not found");

        return parent is T parentMatch 
            ? parentMatch 
            : FindVisualParent<T>(parent);
    }
}