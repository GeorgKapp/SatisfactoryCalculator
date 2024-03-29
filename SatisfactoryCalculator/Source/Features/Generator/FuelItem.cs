﻿namespace SatisfactoryCalculator.Source.Features.Generator;

internal class FuelItem : ObservableObject
{
    public FuelItem(IItem item)
    {
        Item = item;
    }

    public FuelItem(IItem item, decimal? amountPerMinute)
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

    private decimal? _amountPerMinute;

    public decimal? AmountPerMinute
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