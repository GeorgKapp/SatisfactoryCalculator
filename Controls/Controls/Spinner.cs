// ReSharper disable HeapView.BoxingAllocation
namespace SatisfactoryCalculator.Controls.Controls;

public class Spinner : Control
{
    static Spinner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(Spinner), 
            new FrameworkPropertyMetadata(
                typeof(Spinner)
                ));
    }
    
    public bool IsSpinning
    {
        get => (bool)GetValue(IsSpinningProperty);
        set => SetValue(IsSpinningProperty, value);
    }
    
    public static readonly DependencyProperty IsSpinningProperty =
        DependencyProperty.Register(
            nameof(IsSpinning), 
            typeof(bool), 
            typeof(Spinner), 
            new(false)
            );

    public double Diameter
    {
        get => (double)GetValue(DiameterProperty);
        set => SetValue(DiameterProperty, value);
    }
    
    public static readonly DependencyProperty DiameterProperty = 
        DependencyProperty.Register(
            nameof(Diameter), 
            typeof(double), 
            typeof(Spinner), 
            new(100.0)
            );

    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }
    
    public static readonly DependencyProperty ThicknessProperty = 
        DependencyProperty.Register(
            nameof(Thickness), 
            typeof(double), 
            typeof(Spinner), 
            new(1.0)
            );

    public Brush Color
    {
        get => (Brush)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    
    public static readonly DependencyProperty ColorProperty = 
        DependencyProperty.Register(
            nameof(Color), 
            typeof(Brush), 
            typeof(Spinner), 
            new(Brushes.Black)
            );

    public PenLineCap Cap
    {
        get => (PenLineCap)GetValue(CapProperty);
        set => SetValue(CapProperty, value);
    }
    
    public static readonly DependencyProperty CapProperty = 
        DependencyProperty.Register(
            nameof(Cap), 
            typeof(PenLineCap), 
            typeof(Spinner), 
            new(PenLineCap.Flat)
            );
}
