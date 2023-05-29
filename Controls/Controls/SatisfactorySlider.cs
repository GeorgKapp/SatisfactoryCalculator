namespace SatisfactoryCalculator.Controls.Controls;
public class SatisfactorySlider : Slider
{
    static SatisfactorySlider()
    {
        FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SatisfactorySlider), new FrameworkPropertyMetadata(typeof(SatisfactorySlider)));
    }
}
