// ReSharper disable CheckNamespace

using SatisfactoryCalculator.Source.Features.Shared.Models;

namespace SatisfactoryCalculator.Source.Features.Recipe;

internal class RecipePart
{
    public RecipePart(IEntity part, decimal amount, decimal sourceAmount, decimal? amountPerMinute)
    {
        Part = part;
        Amount = amount;
        SourceAmount = sourceAmount;
        AmountPerMinute = amountPerMinute;
    }

    public RecipePart(IEntity part, decimal sourceAmount)
    {
        Part = part;
        SourceAmount = sourceAmount;
    }

    public IEntity Part { get; }
    public decimal? AmountPerMinute { get; set; }
    
    // ReSharper disable once HeapView.ObjectAllocation
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public decimal SourceAmount { get; }
    public decimal Amount { get; set; }
    
    // ReSharper disable once HeapView.ObjectAllocation
    public string AmountText => $"{Amount} x";
}
