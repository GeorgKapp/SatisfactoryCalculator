namespace SatisfactoryCalculator.Source.State;

internal class ApplicationState : ObservableObject
{
    private ConfigurationModel _configuration = new();
    public ConfigurationModel Configuration
    {
        get => _configuration;
        set => SetProperty(ref _configuration, value);
    }

    public Data? Data { get; set; }

	public void SetConfig(Data data, DataModelMappingResult mappingResult)
	{
        Data = data;
        Configuration.Items = new(mappingResult.Items);
        Configuration.Buildings = new(mappingResult.Buildings);
        Configuration.Generators = new(mappingResult.Generators);
        Configuration.Recipes = new(mappingResult.Recipes);
        Configuration.ReferenceDictionary = mappingResult.ReferenceDictionary;

        Configuration.LastSyncDate = mappingResult.LastSyncDate;
    }
}
