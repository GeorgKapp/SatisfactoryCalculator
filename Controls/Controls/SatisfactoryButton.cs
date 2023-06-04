// ReSharper disable HeapView.BoxingAllocation
namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryButton : Button
{
	static SatisfactoryButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(
			typeof(SatisfactoryButton), 
			new FrameworkPropertyMetadata(
				typeof(SatisfactoryButton)
				));
	}
	
	public CornerRadius CornerRadius
	{
		get => (CornerRadius)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}
	
	public static readonly DependencyProperty CornerRadiusProperty = 
		DependencyProperty.Register(
			nameof(CornerRadius), 
			typeof(CornerRadius), 
			typeof(SatisfactoryButton),
			new PropertyMetadata(
				new CornerRadius(0)
				));

    public SolidColorBrush HoverColor
    {
        get => (SolidColorBrush)GetValue(HoverColorProperty);
        set => SetValue(HoverColorProperty, value);
    }
    
    public static readonly DependencyProperty HoverColorProperty = 
	    DependencyProperty.Register(
		    nameof(HoverColor), 
		    typeof(SolidColorBrush), 
		    typeof(SatisfactoryButton), 
		    new PropertyMetadata(null)
		    );
}
