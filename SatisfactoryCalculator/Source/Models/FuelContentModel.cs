namespace SatisfactoryCalculator.Source.Models;

internal class FuelContentModel
{
    public ItemModel Item { get; set; }
    public string ItemName { get; set; }
    public double? AmountPerMinute { get; set; }
    public string AmountPerMinuteText => $"{AmountPerMinute} p/m";
}