// ReSharper disable CheckNamespace
namespace SatisfactoryCalculator.Source.Models;

internal class RecipePart
{
    public RecipePart(IEntity part, double amount, double sourceAmount, double? amountPerMinute)
    {
        Part = part;
        Amount = amount;
        SourceAmount = sourceAmount;
        AmountPerMinute = amountPerMinute;
    }

    public RecipePart(IEntity part, double sourceAmount)
    {
        Part = part;
        SourceAmount = sourceAmount;
    }

    public IEntity Part { get; }
    public double? AmountPerMinute { get; set; }
    
    // ReSharper disable once HeapView.ObjectAllocation
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double SourceAmount { get; }
    public double Amount { get; set; }
    
    // ReSharper disable once HeapView.ObjectAllocation
    public string AmountText => $"{Amount} x";
}
