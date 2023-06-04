// ReSharper disable HeapView.BoxingAllocation
namespace SatisfactoryCalculator.Controls.Panels;

public class ExtendedStackPanel : Panel
{
	public Orientation Orientation
	{
		get => (Orientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}
	
	public static readonly DependencyProperty OrientationProperty = 
		DependencyProperty.Register(
			nameof(Orientation), 
			typeof(Orientation), 
			typeof(ExtendedStackPanel), 
			new FrameworkPropertyMetadata(
				Orientation.Vertical, 
				FrameworkPropertyMetadataOptions.AffectsMeasure
				));

    public double Spacing
	{
		get => (double)GetValue(SpacingProperty);
		set => SetValue(SpacingProperty, value);
	}
    
	public static readonly DependencyProperty SpacingProperty = 
		DependencyProperty.Register(
			nameof(Spacing), 
			typeof(double), 
			typeof(ExtendedStackPanel), 
			new FrameworkPropertyMetadata(
				0.0, 
				FrameworkPropertyMetadataOptions.AffectsMeasure
				));


    protected override Size MeasureOverride(Size constraint)
	{
		Size result = new();
		
		if (Orientation == Orientation.Horizontal)
            constraint.Width = double.PositiveInfinity;
		else
            constraint.Height = double.PositiveInfinity;

		for (var i = 0; i< InternalChildren.Count; i++)
		{
			var uIElement = InternalChildren[i];
			
			if (uIElement is null) 
				continue;
			
			uIElement.Measure(constraint);
			var desiredSize = uIElement.DesiredSize;
			var num = i != 0 ? Spacing : 0.0;
			
			if (Orientation == Orientation.Horizontal)
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
		return result;
	}

	protected override Size ArrangeOverride(Size arrangeSize)
	{
		var finalRect = new Rect(arrangeSize);
		var num = 0.0;
        for (var i = 0; i < InternalChildren.Count; i++)
        {
			var uIElement = base.InternalChildren[i];
			
			if (uIElement is null) 
				continue;

			var num2 = i == 0
				? 0.0 
				: Spacing;
			
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
		return arrangeSize;
	}
}
