namespace SatisfactoryCalculator.Shared.Models;

public class Item
{
	public string Name { get; set; }
	public string ClassName { get; set; }
	public string Description { get; set; }
	public byte[] PictureData { get; set; }
	public int? ResourceSinkPints { get; set; }

	public Item() { }
	public Item(string name, string description, byte[] pictureData)
	{
		Name = name;
		Description = description;
		PictureData = pictureData;
	}
}
