namespace SatisfactoryCalculator.Controls.Converters;

public class DiameterAndThicknessToStrokeDashArrayConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values.Length < 2 || !double.TryParse(values[0].ToString(), out var result) || !double.TryParse(values[1].ToString(), out var result2))
		{
			return new DoubleCollection(new double[1]);
		}

		double num = Math.PI * (result - result2);
		double num2 = num * 0.75;
		double num3 = num - num2;

		return new DoubleCollection(new double[2]
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
