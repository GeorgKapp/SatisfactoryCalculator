namespace SatisfactoryCalculator.Source.Models;

internal class GeneratorModel
{
    public string ClassName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Form? Form { get; set; }
    public double PowerProduction { get; set; }
    public double PowerProductionExponent { get; set; }
    public double? SupplementalToPowerRatio { get; set; }
    public double? SupplementalLoadAmount { get; set; }

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
