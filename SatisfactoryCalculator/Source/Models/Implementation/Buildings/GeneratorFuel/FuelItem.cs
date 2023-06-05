// ReSharper disable CheckNamespace
#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class FuelItem : ObservableObject
{
    public FuelItem(IItem item)
    {
        Item = item;
    }

    public FuelItem(IItem item, double? amountPerMinute)
    {
        Item = item;
        AmountPerMinute = amountPerMinute;
    }
    
    private IItem _item;
    public IItem Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    private double? _amountPerMinute;

    public double? AmountPerMinute
    {
        get => _amountPerMinute;
        set
        {
            SetProperty(ref _amountPerMinute, value);
            Notify(nameof(AmountPerMinuteText));
        }
    }

    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
}