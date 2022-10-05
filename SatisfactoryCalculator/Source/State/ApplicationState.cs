using SatisfactoryCalculator.Source.ApplicationServices.MappingService;

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

    public ApplicationState(JsonService jsonService, DataModelMappingService dataModelMappingService)
	{
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_dataModelMappingService = dataModelMappingService ?? throw new ArgumentNullException(nameof(dataModelMappingService));
        InitializeConfig();
    }

    private void InitializeConfig()
    {
        var mappingResult = ReadConfig();
        SetConfig(mappingResult);
    }

	private DataModelMappingResult ReadConfig()
	{
		var data = _jsonService.ReadJson<Data>(Constants.InformationFileName);
		return _dataModelMappingService.MapToConfigurationModel(data);
    }

	public void SetConfig(DataModelMappingResult mappingResult)
	{
        Configuration ??= new();
        Configuration.Items = new(mappingResult.Items);
        Configuration.Buildings = new(mappingResult.Buildings);
        Configuration.Recipes = new(mappingResult.Recipes);
        Configuration.ItemDictionary = mappingResult.ItemDictionary;
        Configuration.BuildingDictionary = mappingResult.BuildingDictionary;
        Configuration.ItemRecipesDictionary = mappingResult.ItemRecipesDictionary;
        Configuration.BuildingRecipesDictionary = mappingResult.BuildingRecipesDictionary;
        Configuration.LastSyncDate = mappingResult.LastSyncDate;
    }

    public void SaveConfiguration()
	{
		_jsonService.WriteJson(Data, Constants.InformationFileName);
	}

    private readonly JsonService _jsonService;
    private readonly DataModelMappingService _dataModelMappingService;
}
