namespace Data.Models.Implementation;

public class ScannableObject : IIDPrimaryKey
{
    public int ID { get; set; }
    public Item Item { get; set; }
    
    public ICollection<ScanningActor> ScanningActors = new List<ScanningActor>();
}
