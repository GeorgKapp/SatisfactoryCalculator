namespace SatisfactoryCalculator.Controls.Converters;

public class DiameterAndThicknessToStrokeDashArrayConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length < 2 || !double.TryParse(values[0].ToString(), out var result) || !double.TryParse(values[1].ToString(), out var result2))
		{
			return new DoubleCollection(new double[1]);
		}

		var num = Math.PI * (result - result2);
		var num2 = num * 0.75;
		var num3 = num - num2;

		return new DoubleCollection(new[]
		{
			num2 / result2,
			num3 / result2
		});
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
