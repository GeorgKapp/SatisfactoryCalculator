namespace SatisfactoryCalculator.Source.Features.Weapon;

public partial class AmmunitionControl
{
    public AmmunitionControl()
    {
        InitializeComponent();
    }
    
    internal Ammunition.Ammunition Ammunition
    {
        get => (Ammunition.Ammunition)GetValue(AmmunitionProperty);
        set => SetValue(AmmunitionProperty, value);
    }
    
    internal static readonly DependencyProperty AmmunitionProperty = 
        DependencyProperty.Register(
            nameof(Ammunition), 
            typeof(Ammunition.Ammunition), 
            typeof(AmmunitionControl)
        );
}