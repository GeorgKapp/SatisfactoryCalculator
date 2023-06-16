namespace Data.Models.Implementation;

public class Statue : IClassNamePrimaryKey, INameEntity, IDescriptionEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SmallImagePath { get; set; }
    public string BigImagePath { get; set; }
}
