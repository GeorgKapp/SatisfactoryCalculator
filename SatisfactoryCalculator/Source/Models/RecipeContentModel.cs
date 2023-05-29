namespace SatisfactoryCalculator.Source.Models;

public class RecipeContentModel
{
    public ItemModel Item { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double CurrentAmount { get; set; }
    public double SourceAmount { get; set; }
    public double Amount { get; set; }
    public string AmountText => $"{Amount} x";
}
