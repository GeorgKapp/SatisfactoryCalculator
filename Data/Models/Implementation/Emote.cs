namespace Data.Models.Implementation;

public class Emote : IClassNamePrimaryKey, INameEntity, IImage
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string? SmallImagePath { get; set; }
    public string? BigImagePath { get; set; }
}
