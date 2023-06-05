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

    public IEntity Part { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double CurrentAmount { get; set; }
    public double SourceAmount { get; set; }
    public double Amount { get; set; }
    public string AmountText => $"{Amount} x";
}
