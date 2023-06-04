namespace SatisfactoryCalculator.Source.Models.Refernces;

internal class EntityReference
{
    public EntityReference(string entityClassName, RecipeModel[] recipeIngredient, RecipeModel[] recipeBuildingIngredient, RecipeModel[] recipeProduct, RecipeModel[] recipeBuilding, FuelModel[] fuelIngredient, FuelModel[] fuelByProduct, FuelModel[] fuelGenerator)
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

    public RecipeModel[] RecipeIngredient { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] RecipeProduct { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] RecipeBuildingIngredient { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] RecipeBuilding { get; set; } = Array.Empty<RecipeModel>();
    public RecipeModel[] RecipeFuels { get; set; } = Array.Empty<RecipeModel>();
    public FuelModel[] FuelIngredient { get; set; } = Array.Empty<FuelModel>();
    public FuelModel[] FuelByProduct { get; set; } = Array.Empty<FuelModel>();
    public FuelModel[] FuelGenerator { get; set; } = Array.Empty<FuelModel>();

    public SchemaModel[] SchemaIngredient { get; set; } = Array.Empty<SchemaModel>();
    public SchemaModel[] SchemaUnlock { get; set; } = Array.Empty<SchemaModel>();
    public SchemaModel[] SchemaBuilding { get; set; } = Array.Empty<SchemaModel>();
}