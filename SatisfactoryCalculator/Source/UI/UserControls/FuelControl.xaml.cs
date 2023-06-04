﻿using Fuel = SatisfactoryCalculator.Source.Models.Fuel;

namespace SatisfactoryCalculator.Source.UI.UserControls;

public partial class FuelControl : UserControl
{
    internal Fuel Fuel
    {
        get => (Fuel)GetValue(FuelProperty);
        set => SetValue(FuelProperty, value);
    }
    internal static readonly DependencyProperty FuelProperty = DependencyProperty.Register(nameof(Fuel), typeof(Fuel), typeof(FuelControl), new PropertyMetadata(UpdateVisibilities));

    internal bool ShowFuelGenerator
    {
        get => (bool)GetValue(ShowFuelGeneratorProperty);
        set => SetValue(ShowFuelGeneratorProperty, value);
    }
    internal static readonly DependencyProperty ShowFuelGeneratorProperty = DependencyProperty.Register(nameof(ShowFuelGenerator), typeof(bool), typeof(FuelControl), new PropertyMetadata(true));

    private static void UpdateVisibilities(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (d as FuelControl);
        //control.textBoxEqual.Visibility = (e.NewValue as Recipe)?.Products?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        //control.textBoxArrow.Visibility = (e.NewValue as Recipe)?.Buildings?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    public FuelControl()
    {
        InitializeComponent();
    }
}
