using Configuration = SatisfactoryCalculator.Source.Models.Configuration;

namespace SatisfactoryCalculator.Source.State;

internal class ApplicationState : ObservableObject
{
    private Configuration _configuration = new();
    public Configuration Configuration
    {
        get => _configuration;
        // ReSharper disable once UnusedMember.Global
        set => SetProperty(ref _configuration, value);
    }
    
    public void SetConfig(DataModelMappingResult mappingResult)
	{
        Configuration.Items = new(mappingResult.Items);
        Configuration.Equipments = new(mappingResult.Equipments);
        Configuration.Consumables = new(mappingResult.Consumables);
        Configuration.Weapons = new(mappingResult.Weapons);
        Configuration.Ammunitions = new(mappingResult.Ammunitions);
        Configuration.Resources = new(mappingResult.Resources);
        Configuration.Buildings = new(mappingResult.Buildings);
        Configuration.Generators = new(mappingResult.Generators);
        Configuration.Miners = new(mappingResult.Miners);
        Configuration.Recipes = new(mappingResult.Recipes);
        Configuration.ReferenceDictionary = mappingResult.ReferenceDictionary;
        Configuration.LastSyncDate = mappingResult.LastSyncDate;
    }
}
