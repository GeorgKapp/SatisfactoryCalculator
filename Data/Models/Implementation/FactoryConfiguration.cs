namespace Data.Models.Implementation;

public class FactoryConfiguration : IIDPrimaryKey
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double DesiredOverclock { get; set; }
    public bool SplitOverclockEvenly { get; set; }
    public string CalculatedInVersion { get; set; }
    
    public virtual ICollection<FactoryBuildingConfiguration> FactoryBuildingConfigurations { get; set; }
    public virtual ICollection<FactoryConfigurationOutput> DesiredOutputs { get; set; }
    
}
