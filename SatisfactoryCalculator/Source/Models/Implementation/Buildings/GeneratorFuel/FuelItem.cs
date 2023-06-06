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
    
    private readonly IItem _item;
    public IItem Item
    {
        get => _item;
        private init => SetProperty(ref _item, value);
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

    // ReSharper disable once HeapView.ObjectAllocation
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
}