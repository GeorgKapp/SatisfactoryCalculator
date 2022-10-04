namespace SatisfactoryCalculator.Source.Models;

public class RecipeContentModel
{
    public ItemModel Item { get; set; }
    public string ItemName { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
    public double StackSize { get; set; }
    public string StackSizeText => $"{StackSize} x";
}
