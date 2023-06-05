namespace SatisfactoryCalculator.Controls.Converters;

[ValueConversion(typeof(string), typeof(Visibility))]
public class StringToVisibilityConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
	{
		// ReSharper disable once HeapView.BoxingAllocation
		return string.IsNullOrEmpty(value?.ToString()) 
			? Visibility.Collapsed 
			: Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
