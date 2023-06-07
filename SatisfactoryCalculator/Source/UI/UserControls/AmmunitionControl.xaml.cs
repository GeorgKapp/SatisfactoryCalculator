using Ammunition = SatisfactoryCalculator.Source.Models.Ammunition;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class AmmunitionControl
{
    public AmmunitionControl()
    {
        InitializeComponent();
    }
    
    internal Ammunition Ammunition
    {
        get => (Ammunition)GetValue(AmmunitionProperty);
        set => SetValue(AmmunitionProperty, value);
    }
    internal static readonly DependencyProperty AmmunitionProperty = 
        DependencyProperty.Register(
            nameof(Ammunition), 
            typeof(Ammunition), 
            typeof(AmmunitionControl)
        );
}