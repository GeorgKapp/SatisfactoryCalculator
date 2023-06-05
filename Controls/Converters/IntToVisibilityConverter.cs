namespace SatisfactoryCalculator.Controls.Converters;


[ValueConversion(typeof(int), typeof(Visibility))]
public class IntToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // ReSharper disable once HeapView.BoxingAllocation
        return (int)value == 0
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
