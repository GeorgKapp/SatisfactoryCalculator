namespace SatisfactoryCalculator.Shared.Models;

public class Building
{
	public string Name { get; set; }
	public string ClassName { get; set; }
	public string Description { get; set; }
	public double? PowerConsumption { get; set; }
	public PowerConsumptionRange? PowerConsumptionRange { get; set; }
	public byte[] PictureData { get; set; }

	public Building() { }
	public Building(string name, string description, double? powerConsumption, PowerConsumptionRange? powerConsumptionRange, byte[] pictureData)
	{
		Name = name;
		Description = description;
		PowerConsumption = powerConsumption;
		PowerConsumptionRange = powerConsumptionRange;
		PictureData = pictureData;
	}
}
