namespace SatisfactoryCalculator.Source.Models;

public class BuildingModel
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double? PowerConsumption { get; set; }
    public PowerConsumptionRange? PowerConsumptionRange { get; set; }

    private string _imagePath;
    public string ImagePath
    {
        get => _imagePath;
        set
        {
            _imagePath = value;
            BitmapImage = Application.Current.Dispatcher.Invoke(() => BitmapImageUtility.ConvertPathToBitMapImage(value));
        }
    }

    public BitmapImage BitmapImage { get; set; }
}
