namespace SatisfactoryCalculator.Source.State;

internal class ApplicationState : ObservableObject
{
    private ConfigurationModel _configuration = new();
    public ConfigurationModel Configuration
    {
        get => _configuration;
        set => SetProperty(ref _configuration, value);
    }

	public Data Data { get; set; }
	public Dictionary<string, BuildingModel> BuildingDictionary { get; set; } = new();
	public Dictionary<string, ItemModel> ItemDictionary { get; set; } = new();
	public Dictionary<string, List<RecipeModel>> RecipeDictionary { get; set; } = new();
	public Dictionary<string, ItemRecipeModel> ItemRecipesDictionary { get; set; } = new();
    public Dictionary<string, ItemRecipeModel> BuildingRecipesDictionary { get; set; } = new();

    public ApplicationState(JsonService jsonService, DataModelMappingService dataModelMappingService)
	{
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_dataModelMappingService = dataModelMappingService ?? throw new ArgumentNullException(nameof(dataModelMappingService));
		InitializeConfiguration();
		BuildDictionariesAndMappingWhichIsReallyNotGoodBecauseItShouldBeDoneInTheMappingServiceAsAMappingResultWhichContainsDictionaries();
	}

	private void InitializeConfiguration()
	{
		var data = _jsonService.ReadJson<Data>(Constants.InformationFileName);
		var configurationModel = _dataModelMappingService.MapToConfigurationModel(data);
		Configuration = configurationModel ??= new();
	}

	public void BuildDictionariesAndMappingWhichIsReallyNotGoodBecauseItShouldBeDoneInTheMappingServiceAsAMappingResultWhichContainsDictionaries()
	{
		BuildingDictionary = new();
		ItemDictionary = new();
		RecipeDictionary = new();
		ItemRecipesDictionary = new();
		BuildingRecipesDictionary = new();

		foreach (var building in Configuration.Buildings)
			BuildingDictionary.Add(building.ClassName, building);

		foreach (var item in Configuration.Items)
			ItemDictionary.Add(item.ClassName, item);

		foreach (var recipe in Configuration.Recipes)
		{
			if (recipe.ConstructedInWorkbench)
				recipe.Buildings = recipe.Buildings.Add(ToRecipeBuildingModel(BuildingDictionary["WorkBench_C"]));

			if (recipe.ConstructedInWorkshop)
				recipe.Buildings = recipe.Buildings.Add(ToRecipeBuildingModel(BuildingDictionary["Workshop_C"]));

			foreach (var ingredient in recipe.Ingredients)
				ingredient.Item = ItemDictionary[ingredient.ItemName];

			foreach (var product in recipe.Products)
				product.Item = ItemDictionary.ContainsKey(product.ItemName)
					? ItemDictionary[product.ItemName]
					: MapToItemModel(BuildingDictionary[product.ItemName]);

			foreach (var building in recipe.Buildings)
				building.Building = BuildingDictionary[building.BuildingName];

			if (RecipeDictionary.ContainsKey(recipe.RecipeName))
			{
				RecipeDictionary[recipe.ClassName].Add(recipe);
				continue;
			}

			RecipeDictionary.Add(recipe.ClassName, new() { recipe });
		}

		foreach (var item in Configuration.Items)
		{
			List<RecipeModel> products = new();
			List<RecipeModel> ingredients = new();
			List<RecipeModel> buildingIngredient = new();

			foreach (var recipe in Configuration.Recipes)
			{
				if (recipe.Ingredients.Where(p => p.Item.ClassName == item.ClassName).Any())
				{
					if (recipe.Products.Where(p => BuildingDictionary.ContainsKey(p.Item.ClassName)).Any())
					{
						foreach (var ingredient in recipe.Ingredients)
							ingredient.AmountPerMinute = null;

						foreach (var product in recipe.Products)
							product.AmountPerMinute = null;

						buildingIngredient.Add(recipe);
					}
					else
						ingredients.Add(recipe);
                }

				if (recipe.Products.Where(p => p.Item.ClassName == item.ClassName).Any())
					products.Add(recipe);
			}

			ItemRecipesDictionary.Add(item.ClassName, new ItemRecipeModel
			{
				ItemName = item.ClassName,
				ContainedAsIngredient = OrderRecipeModel(ingredients),
				ContainedAsProduct = OrderRecipeModel(products),
				ContainedAsBuildingIngredient = OrderRecipeModel(buildingIngredient)
			});


		}

		foreach(var building in Configuration.Buildings)
		{
            List<RecipeModel> buildingProductRecipes = new();
            foreach (var recipe in Configuration.Recipes)
			{
				if (recipe.Products.Where(p => p.ItemName == building.ClassName).Any())
					buildingProductRecipes.Add(recipe);
            }

            BuildingRecipesDictionary.Add(building.ClassName, new ItemRecipeModel
            {
                ItemName = building.ClassName,
                ContainedAsProduct = OrderRecipeModel(buildingProductRecipes),
				ContainedAsIngredient = Array.Empty<RecipeModel>(),
				ContainedAsBuildingIngredient = Array.Empty<RecipeModel>()
            });
        }

	}

	private RecipeModel[] OrderRecipeModel(List<RecipeModel> input) => input
                .OrderBy(p => p.IsAlternateRecipe)
                .ThenBy(p => p.RecipeName)
                .ToArray();

	private RecipeBuildingModel ToRecipeBuildingModel(BuildingModel buildingModel)
	{
		return new RecipeBuildingModel
		{
			Building = buildingModel,
			BuildingName = buildingModel.ClassName,
			PowerConsumptionRange = null
		};
	}

    private ItemModel MapToItemModel(BuildingModel buildingModel)
    {
        return new ItemModel
        {
            Name = buildingModel.Name,
            ClassName = buildingModel.ClassName,
			Description = buildingModel.Description,
			ImagePath = buildingModel.ImagePath
        };
    }

    public void SaveConfiguration()
	{
		_jsonService.WriteJson(Data, Constants.InformationFileName);
	}

    private readonly JsonService _jsonService;
    private readonly DataModelMappingService _dataModelMappingService;
}
