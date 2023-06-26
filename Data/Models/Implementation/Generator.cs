namespace Data.Models.Implementation;

public class Generator : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Building Building { get; set; }
    public decimal PowerProduction { get; set; }
    public decimal? SupplementToPowerRatio { get; set; }
    public decimal? SupplementalLoadAmount { get; set; }
    public virtual ICollection<FuelItem> Fuels { get; set; } = new List<FuelItem>();
}
