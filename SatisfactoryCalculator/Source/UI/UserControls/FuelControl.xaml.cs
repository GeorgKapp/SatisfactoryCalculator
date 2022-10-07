namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class FuelControl : UserControl
{
    internal FuelModel Fuel
    {
        get => (FuelModel)GetValue(FuelProperty);
        set => SetValue(FuelProperty, value);
    }
    internal static readonly DependencyProperty FuelProperty = DependencyProperty.Register(nameof(Fuel), typeof(FuelModel), typeof(FuelControl), new PropertyMetadata(UpdateVisibilities));

    internal bool ShowFuelGenerator
    {
        get => (bool)GetValue(ShowFuelGeneratorProperty);
        set => SetValue(ShowFuelGeneratorProperty, value);
    }
    internal static readonly DependencyProperty ShowFuelGeneratorProperty = DependencyProperty.Register(nameof(ShowFuelGenerator), typeof(bool), typeof(FuelControl));

    private static void UpdateVisibilities(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (d as FuelControl);
        //control.textBoxEqual.Visibility = (e.NewValue as RecipeModel)?.Products?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        //control.textBoxArrow.Visibility = (e.NewValue as RecipeModel)?.Buildings?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public FuelControl()
    {
        InitializeComponent();
    }
}
