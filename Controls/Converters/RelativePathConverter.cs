namespace SatisfactoryCalculator.Controls.Converters;


[ValueConversion(typeof(string), typeof(string))]
public class RelativePathConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value is string 
			? AppDomain.CurrentDomain.BaseDirectory + (value as string) 
			: value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
