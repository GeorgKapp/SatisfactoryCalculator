namespace SatisfactoryCalculator.Shared.Models;

public class PathOptions
{
    public string DataFolder { get; set; }
    public string DataFile => @$"{DataFolder}\Data.db";
    public string TempDataFile => @$"{DataFolder}\TempData.db";
    public string ImageFolder{ get; set; }
}
