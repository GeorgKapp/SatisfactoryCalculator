namespace SatisfactoryCalculator.Source.Models;

internal class FactoryConfiguration
{
    public int? ID { get; set; }
    
    public string Name { get; set; }
    public double? DesiredOverclock { get; set; }
    public bool SplitOverclockEvenly { get; set; }
    public string CalculatedInVersion { get; set; }
    
    public FactoryBuildingConfiguration[] FactoryBuildingConfigurations { get; set; }
    public FactoryConfigurationOutput[] DesiredOutputs { get; set; }
}
