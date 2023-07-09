namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryConfiguration
{
    public int? ID { get; set; }
    
    public string? Name { get; set; }
    public double DesiredOverclock { get; set; } = 100;
    public bool SplitOverclockEvenly { get; set; } = false;
    public string? CalculatedInVersion { get; set; }
    
    public decimal CalculatedPowerConsumption { get; set; }
    
    public ObservableCollection<FactoryBuildingConfiguration> FactoryBuildingConfigurations { get; set; } = new ObservableCollection<FactoryBuildingConfiguration>(); 
    public ObservableCollection<FactoryConfigurationOutput> DesiredOutputs { get; set; } = new ObservableCollection<FactoryConfigurationOutput>();
}
