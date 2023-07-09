// ReSharper disable once CheckNamespace
namespace SatisfactoryCalculator.Source.Features.Shared.UserControls;

public partial class EquipmentSlotControl
{
    public EquipmentSlotControl()
    {
        InitializeComponent();
    }
    
    public EquipmentSlot? EquipmentSlot
    {
        get => (EquipmentSlot?)GetValue(EquipmentSlotProperty);
        // ReSharper disable once HeapView.BoxingAllocation
        set => SetValue(EquipmentSlotProperty, value);
    }

    public static readonly DependencyProperty EquipmentSlotProperty =
        DependencyProperty.Register(
            nameof(EquipmentSlot),
            typeof(EquipmentSlot?),
            typeof(EquipmentSlotControl),
            new(EquipmentSlotChangedCallback));

    private static void EquipmentSlotChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((EquipmentSlotControl)d).EquipmentSlotChanged();
    }

    private void EquipmentSlotChanged()
    {
        headSlot.Visibility = Visibility.Collapsed;
        legSlot.Visibility = Visibility.Collapsed;
        backSlot.Visibility = Visibility.Collapsed;
        bodySlot.Visibility = Visibility.Collapsed;
        handSlot.Visibility = Visibility.Collapsed;
        
        switch (EquipmentSlot)
        {
            case null: break;
            case SatisfactoryCalculator.Shared.Enums.EquipmentSlot.Arms: handSlot.Visibility = Visibility.Visible; break;
            case SatisfactoryCalculator.Shared.Enums.EquipmentSlot.Back: backSlot.Visibility = Visibility.Visible; break;
            case SatisfactoryCalculator.Shared.Enums.EquipmentSlot.Body: bodySlot.Visibility = Visibility.Visible; break;
            case SatisfactoryCalculator.Shared.Enums.EquipmentSlot.Head: headSlot.Visibility = Visibility.Visible; break;
            case SatisfactoryCalculator.Shared.Enums.EquipmentSlot.Legs: legSlot.Visibility = Visibility.Visible; break;
            default: throw new ArgumentOutOfRangeException();
        }
    }
}