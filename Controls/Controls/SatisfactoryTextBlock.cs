namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryTextBlock : TextBlock
{
	static SatisfactoryTextBlock()
	{
		DefaultStyleKeyProperty.OverrideMetadata(
			typeof(SatisfactoryTextBlock), 
			new FrameworkPropertyMetadata(
				typeof(SatisfactoryTextBlock)
				));
	}
}
