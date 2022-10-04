namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryTextBlock : TextBlock
{
	static SatisfactoryTextBlock()
	{
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SatisfactoryTextBlock), new FrameworkPropertyMetadata(typeof(SatisfactoryTextBlock)));
	}
}
