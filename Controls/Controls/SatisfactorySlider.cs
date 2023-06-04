namespace SatisfactoryCalculator.Controls.Controls;
public class SatisfactorySlider : Slider
{
    static SatisfactorySlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SatisfactorySlider), 
            new FrameworkPropertyMetadata(
                typeof(SatisfactorySlider)
                ));
    }
}
