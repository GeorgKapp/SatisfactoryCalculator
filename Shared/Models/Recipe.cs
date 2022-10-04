namespace SatisfactoryCalculator.Shared.Models;

public class Recipe
{
	public string Name { get; set; }
	public bool IsAlternateRecipe { get; set; }
	public List<RecipeItem> Ingredients { get; set; }
	public List<RecipeItem> Products { get; set; }
	public List<Building> Buildings { get; set; }
	public PowerConsumptionRange? PowerConsumptionRange { get; set; }

	public Recipe() { }
	public Recipe(string name, bool isAlternateRecipe, List<RecipeItem> ingredients, List<RecipeItem> products, List<Building> buildings, PowerConsumptionRange? powerConsumptionRange)
	{
		Name = name;
		IsAlternateRecipe = isAlternateRecipe;
		Ingredients = ingredients;
		Products = products;
		Buildings = buildings;
		PowerConsumptionRange = powerConsumptionRange;
	}
}
