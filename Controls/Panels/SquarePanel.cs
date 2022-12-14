namespace SatisfactoryCalculator.Controls.Panels;

public class SquarePanel : Panel
{
	protected override Size MeasureOverride(Size availableSize)
	{
		Size result = default(Size);
		foreach (UIElement child in base.Children)
		{
			child.Measure(availableSize);
			result.Width = Math.Max(child.DesiredSize.Width, result.Width);
			result.Height = Math.Max(child.DesiredSize.Height, result.Height);
		}
		result.Width = (double.IsPositiveInfinity(availableSize.Width) ? result.Width : availableSize.Width);
		result.Height = (double.IsPositiveInfinity(availableSize.Height) ? result.Height : availableSize.Height);
		return result;
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		Size size = default(Size);
		double num = Math.Min(finalSize.Height, finalSize.Width);
		size = new Size(num, num);
		foreach (UIElement child in base.Children)
		{
			child.Arrange(new Rect(size));
		}
		return size;
	}
}
