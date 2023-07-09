namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class MinerResourceControl
{
    internal MinerResource MinerResource
    {
        get => (MinerResource)GetValue(MinerResourceProperty);
        set => SetValue(MinerResourceProperty, value);
    }
    
    internal static readonly DependencyProperty MinerResourceProperty = 
        DependencyProperty.Register(
            nameof(MinerResource), 
            typeof(MinerResource), 
            typeof(MinerResourceControl)
        );
    
    public MinerResourceControl()
    {
        InitializeComponent();
    }
}
