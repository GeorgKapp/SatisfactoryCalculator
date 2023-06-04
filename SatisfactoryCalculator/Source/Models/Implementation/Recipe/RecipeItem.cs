namespace SatisfactoryCalculator.Source.Models;

public class RecipeItem
{
    public RecipeItem(Item item, double amount, double sourceAmount, double? amountPerMinute)
    {
        Item = item;
        Amount = amount;
        SourceAmount = sourceAmount;
        AmountPerMinute = amountPerMinute;
    }

    public RecipeItem(Item item, double sourceAmount)
    {
        Item = item;
        SourceAmount = sourceAmount;
    }

    public Item Item { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double CurrentAmount { get; set; }
    public double SourceAmount { get; set; }
    public double Amount { get; set; }
    public string AmountText => $"{Amount} x";
}
