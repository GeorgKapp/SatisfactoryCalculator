namespace SatisfactoryCalculator.Source.Models;

public class RecipeContentModel
{
    public RecipeContentModel(ItemModel item, double amount, double sourceAmount, double? amountPerMinute)
    {
        Item = item;
        Amount = amount;
        SourceAmount = sourceAmount;
        AmountPerMinute = amountPerMinute;
    }

    public RecipeContentModel(ItemModel item, double sourceAmount)
    {
        Item = item;
        SourceAmount = sourceAmount;
    }

    public ItemModel Item { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double CurrentAmount { get; set; }
    public double SourceAmount { get; set; }
    public double Amount { get; set; }
    public string AmountText => $"{Amount} x";
}
