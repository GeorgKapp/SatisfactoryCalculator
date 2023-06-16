namespace Data.Models.Implementation;

public class Building : IClassNamePrimaryKey, INameEntity, IDescriptionEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? SmallImagePath { get; set; }
    public string? BigImagePath { get; set; }
    
    public decimal? ManufactoringSpeed { get; set; }
    public decimal? PowerConsumption { get; set; }
    public decimal? PowerConsumptionExponent { get; set; }
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }
    
    public Miner? Miner  { get; set; }
    public Generator? Generator  { get; set; }
}
