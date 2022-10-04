namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryTextBox : TextBox
{
	static SatisfactoryTextBox()
	{
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SatisfactoryTextBox), new FrameworkPropertyMetadata(typeof(SatisfactoryTextBox)));
	}
}
