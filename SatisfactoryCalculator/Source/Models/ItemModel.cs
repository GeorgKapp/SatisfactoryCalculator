namespace SatisfactoryCalculator.Source.Models;

public class ItemModel
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double EnergyValue { get; set; }

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
