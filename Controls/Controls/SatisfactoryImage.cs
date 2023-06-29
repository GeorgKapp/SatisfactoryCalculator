namespace SatisfactoryCalculator.Controls.Controls;

public class SatisfactoryImage : Image
{
    static SatisfactoryImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(SatisfactoryImage), 
            new FrameworkPropertyMetadata(
                typeof(SatisfactoryImage)
            ));
    }
    
    public string ToolTipText
    {
        get => (string)GetValue(ToolTipTextProperty);
        set => SetValue(ToolTipTextProperty, value);
    }
    
    private static readonly DependencyProperty ToolTipTextProperty = 
        DependencyProperty.Register(
            nameof(ToolTipText), 
            typeof(string), 
            typeof(SatisfactoryImage));
}