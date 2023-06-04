namespace SatisfactoryCalculator.Source.Models;

internal class FuelContentModel : ObservableObject
{
    private ItemModel _item;
    public ItemModel Item
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