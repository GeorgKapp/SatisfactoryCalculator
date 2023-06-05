using Fuel = SatisfactoryCalculator.Source.Models.Fuel;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class FuelControl
{
    internal Fuel Fuel
    {
        get => (Fuel)GetValue(FuelProperty);
        set => SetValue(FuelProperty, value);
    }
    internal static readonly DependencyProperty FuelProperty = 
        DependencyProperty.Register(
            nameof(Fuel), 
            typeof(Fuel), 
            typeof(FuelControl)
            );

    internal bool ShowFuelGenerator
    {
        get => (bool)GetValue(ShowFuelGeneratorProperty);
        // ReSharper disable once HeapView.BoxingAllocation
        set => SetValue(ShowFuelGeneratorProperty, value);
    }
    internal static readonly DependencyProperty ShowFuelGeneratorProperty = 
        DependencyProperty.Register(
            nameof(ShowFuelGenerator), 
            typeof(bool), 
            typeof(FuelControl), 
            // ReSharper disable once HeapView.BoxingAllocation
            new(true));
    
    public FuelControl()
    {
        InitializeComponent();
    }
}
