namespace SatisfactoryCalculator.Controls.Panels;

public class ExtendedStackPanel : Panel
{
	public Orientation Orientation
	{
		get => (Orientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}
	public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(ExtendedStackPanel), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double Spacing
	{
		get => (double)GetValue(SpacingProperty);
		set => SetValue(SpacingProperty, value);
	}
	public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(ExtendedStackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));


    protected override Size MeasureOverride(Size constraint)
	{
		Size result = new();

		bool flag = Orientation == Orientation.Horizontal;
		if (flag)
            constraint.Width = double.PositiveInfinity;
		else
            constraint.Height = double.PositiveInfinity;

		for (int i = 0; i< InternalChildren.Count; i++)
		{
			UIElement uIElement = InternalChildren[i];
			if (uIElement is not null)
			{
				uIElement.Measure(constraint);
				Size desiredSize = uIElement.DesiredSize;
				double num = i != 0 ? Spacing : 0.0;
				if (flag)
				{
					result.Height = Math.Max(result.Height, desiredSize.Height);
					result.Width += desiredSize.Width;
					result.Width += num;
				}
				else
				{
					result.Width = Math.Max(result.Width, desiredSize.Width);
					result.Height += desiredSize.Height;
					result.Height += num;
				}
			}
		}
		return result;
	}

	protected override Size ArrangeOverride(Size arrangeSize)
	{
		Rect finalRect = new Rect(arrangeSize);
		double num = 0.0;
        for (int i = 0; i < InternalChildren.Count; i++)
        {
			UIElement uIElement = base.InternalChildren[i];
			if (uIElement != null)
			{
				double num2 = ((i == 0) ? 0.0 : Spacing);
				if (Orientation == Orientation.Horizontal)
				{
					finalRect.X += num + num2;
					num = finalRect.Width = uIElement.DesiredSize.Width;
					finalRect.Height = Math.Max(arrangeSize.Height, uIElement.DesiredSize.Height);
				}
				else
				{
					finalRect.Y += num + num2;
					num = finalRect.Height = uIElement.DesiredSize.Height;
					finalRect.Width = Math.Max(arrangeSize.Width, uIElement.DesiredSize.Width);
				}
				uIElement.Arrange(finalRect);
			}
		}
		return arrangeSize;
	}
}
