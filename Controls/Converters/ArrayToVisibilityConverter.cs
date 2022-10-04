namespace SatisfactoryCalculator.Controls.Converters;


[ValueConversion(typeof(Array), typeof(Visibility))]
public class ArrayToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is null || (value as Array)?.Length == 0
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
