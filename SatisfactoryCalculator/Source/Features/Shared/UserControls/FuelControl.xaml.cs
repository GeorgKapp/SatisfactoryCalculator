namespace SatisfactoryCalculator.Source.Features.Shared.UserControls;

public partial class FuelControl
{
    internal GeneratorFuel GeneratorFuel
    {
        get => (GeneratorFuel)GetValue(GeneratorFuelProperty);
        set => SetValue(GeneratorFuelProperty, value);
    }
    internal static readonly DependencyProperty GeneratorFuelProperty = 
        DependencyProperty.Register(
            nameof(GeneratorFuel), 
            typeof(GeneratorFuel), 
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
