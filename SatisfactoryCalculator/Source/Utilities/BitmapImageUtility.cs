namespace SatisfactoryCalculator.Source.Utilities;

internal static class BitmapImageUtility
{
    public static BitmapImage ConvertPathToBitMapImage(string filePath)
    {
        BitmapImage bitmapImage = new BitmapImage();

        if (string.IsNullOrEmpty(filePath))
            return bitmapImage;

        using FileStream streamSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        bitmapImage.BeginInit();
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        bitmapImage.StreamSource = streamSource;
        bitmapImage.EndInit();
        bitmapImage.Freeze();
        return bitmapImage;
    }
}
