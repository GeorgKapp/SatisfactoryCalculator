namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryComboBox : ComboBox
{
    static SatisfactoryComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SatisfactoryComboBox), 
            new FrameworkPropertyMetadata(
                typeof(SatisfactoryComboBox)
            ));
    }
    
    
}
