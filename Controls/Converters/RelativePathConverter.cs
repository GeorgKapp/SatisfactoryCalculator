namespace SatisfactoryCalculator.Controls.Converters;


[ValueConversion(typeof(string), typeof(string))]
public class RelativePathConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value is string s
			// ReSharper disable once HeapView.ObjectAllocation
			? $"{AppDomain.CurrentDomain.BaseDirectory}{s}"
			: value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
