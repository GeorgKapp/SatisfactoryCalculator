namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryConfiguration
{
    public int? ID { get; set; }
    
    public string? Name { get; set; }
    public double DesiredOverclock { get; set; } = 100;
    public bool SplitOverclockEvenly { get; set; } = false;
    public bool MergeConfigurationOutput { get; set; } = true;
    public string? CalculatedInVersion { get; set; }
    
    public decimal CalculatedPowerConsumption { get; set; }
    
    public ObservableCollection<FactoryBuildingConfiguration> FactoryBuildingConfigurations { get; set; } = new ObservableCollection<FactoryBuildingConfiguration>(); 
    public ObservableCollection<OutputRequirements> OutputRequirements { get; set; } = new ObservableCollection<OutputRequirements>();
}
