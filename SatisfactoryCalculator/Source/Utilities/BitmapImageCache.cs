namespace SatisfactoryCalculator.Source.Utilities;

public static class BitmapImageCache
{
    public static BitmapImage? Fetch(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return null;
        
        using var streamSource = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        bitmapImage.StreamSource = streamSource;
        bitmapImage.EndInit();
        bitmapImage.Freeze();
        
        return bitmapImage;
    }
}