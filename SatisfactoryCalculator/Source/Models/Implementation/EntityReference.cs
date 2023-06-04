namespace SatisfactoryCalculator.Source.Models.Refernces;

internal class EntityReference
{
    public EntityReference(string entityClassName, Recipe[] recipeIngredient, Recipe[] recipeBuildingIngredient, Recipe[] recipeProduct, Recipe[] recipeBuilding, Fuel[] fuelIngredient, Fuel[] fuelByProduct, Fuel[] fuelGenerator)
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

    public Recipe[] RecipeIngredient { get; set; } = Array.Empty<Recipe>();
    public Recipe[] RecipeProduct { get; set; } = Array.Empty<Recipe>();
    public Recipe[] RecipeBuildingIngredient { get; set; } = Array.Empty<Recipe>();
    public Recipe[] RecipeBuilding { get; set; } = Array.Empty<Recipe>();
    public Recipe[] RecipeFuels { get; set; } = Array.Empty<Recipe>(); 
    public Fuel[] FuelIngredient { get; set; } = Array.Empty<Fuel>();
    public Fuel[] FuelByProduct { get; set; } = Array.Empty<Fuel>();
    public Fuel[] FuelGenerator { get; set; } = Array.Empty<Fuel>();

    public Schema[] SchemaIngredient { get; set; } = Array.Empty<Schema>();
    public Schema[] SchemaUnlock { get; set; } = Array.Empty<Schema>();
    public Schema[] SchemaBuilding { get; set; } = Array.Empty<Schema>();
}