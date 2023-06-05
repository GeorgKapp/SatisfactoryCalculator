namespace SatisfactoryCalculator.Controls.Converters;

public class BooleanConverter<T> : IValueConverter
{
	public T True { get; set; }

	public T False { get; set; }

	protected BooleanConverter(T trueValue, T falseValue)
	{
		True = trueValue;
		False = falseValue;
	}

	public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		// ReSharper disable once HeapView.PossibleBoxingAllocation
		return (value is true ? True : False)!;
	}

	public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		// ReSharper disable once HeapView.BoxingAllocation
		return value is T value1 && EqualityComparer<T>.Default.Equals(value1, True);
	}
}
