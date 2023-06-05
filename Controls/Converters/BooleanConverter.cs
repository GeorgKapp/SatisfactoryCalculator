namespace SatisfactoryCalculator.Controls.Converters;

public class BooleanConverter<T> : IValueConverter
{
	public T True { get; set; }

	public T False { get; set; }

	public BooleanConverter(T trueValue, T falseValue)
	{
		True = trueValue;
		False = falseValue;
	}

	public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((value is bool b && b) ? True : False)!;
	}

	public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
	}
}
