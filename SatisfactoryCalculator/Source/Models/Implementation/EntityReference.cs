namespace SatisfactoryCalculator.Source.Models.Refernces;

internal class EntityReference
{
    public EntityReference(string entityClassName, IRecipe[] recipeIngredient, IRecipe[] recipeBuildingIngredient, IRecipe[] recipeProduct, IRecipe[] recipeBuilding, Fuel[] fuelIngredient, Fuel[] fuelByProduct, Fuel[] fuelGenerator)
    {
        EntityClassName = entityClassName;
        RecipeIngredient = recipeIngredient;
        RecipeBuildingIngredient = recipeBuildingIngredient;
        RecipeProduct = recipeProduct;
        RecipeBuilding = recipeBuilding;
        FuelIngredient = fuelIngredient;
        FuelByProduct = fuelByProduct;
        FuelGenerator = fuelGenerator;
    }

    public string EntityClassName { get; set; }

    public IRecipe[] RecipeIngredient { get; set; } = Array.Empty<IRecipe>();
    public IRecipe[] RecipeProduct { get; set; } = Array.Empty<IRecipe>();
    public IRecipe[] RecipeBuildingIngredient { get; set; } = Array.Empty<IRecipe>();
    public IRecipe[] RecipeBuilding { get; set; } = Array.Empty<IRecipe>();
    public IRecipe[] RecipeFuels { get; set; } = Array.Empty<IRecipe>(); 
    public Fuel[] FuelIngredient { get; set; } = Array.Empty<Fuel>();
    public Fuel[] FuelByProduct { get; set; } = Array.Empty<Fuel>();
    public Fuel[] FuelGenerator { get; set; } = Array.Empty<Fuel>();

    public ISchema[] SchemaIngredient { get; set; } = Array.Empty<ISchema>();
    public ISchema[] SchemaUnlock { get; set; } = Array.Empty<ISchema>();
    public ISchema[] SchemaBuilding { get; set; } = Array.Empty<ISchema>();
}