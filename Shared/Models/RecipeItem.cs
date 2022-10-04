namespace SatisfactoryCalculator.Shared.Models;

public class RecipeItem
{
	public string ItemName { get; set; }
	public double? AmountPerMinute { get; set; }
	public double StackSize { get; set; }

	public RecipeItem() { }
	public RecipeItem(string itemName, double? amountPerMinute, double stackSize)
	{
		ItemName = itemName;
		AmountPerMinute = amountPerMinute;
		StackSize = stackSize;
	}
}
