namespace Data.Models.Implementation;

public class Plant : IClassNamePrimaryKey, INameEntity
{
    public string ClassName { get; set; }
    public string Name { get; set; }
}
