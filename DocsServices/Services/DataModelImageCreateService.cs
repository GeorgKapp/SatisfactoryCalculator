namespace SatisfactoryCalculator.DocsServices.Services;

public class DataModelImageCreateService
{
    public static async Task<DataContainer> CreateImagesAsync(DataContainer data, string ueModelExportPath, string imageFilePath, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
    {
        progress?.ReportOrThrow("Creating Images", token);
        if (!Directory.Exists(imageFilePath))
            Directory.CreateDirectory(imageFilePath);

        progress?.ReportOrThrow("Creating Item Images", token);
        await CreateImagesAsync(data.Items, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Building Images", token);
        await CreateImagesAsync(data.Buildings, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Schematic Images", token);
        await CreateImagesAsync(data.Schematics, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Emote Images", token);
        await CreateImagesAsync(data.Emotes, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Statue Images", token);
        await CreateImagesAsync(data.Statues, ueModelExportPath, imageFilePath);
        
        progress?.ReportOrThrow("Creating Creature Images", token);
        await CreateImagesAsync(data.Creatures, ueModelExportPath, imageFilePath);

        return data;
    }

    private static async Task CreateImagesAsync(IEnumerable<IImage> iconObjects, string ueModelExportPath, string imageFilePath)
    {
        foreach (var iconObject in iconObjects)
        {
            iconObject.SmallImagePath = await CreateImageAsync(iconObject.SmallImagePath, ueModelExportPath, imageFilePath);
            iconObject.BigImagePath = await CreateImageAsync(iconObject.BigImagePath, ueModelExportPath, imageFilePath);
        }
    }

    private static async Task<string> CreateImageAsync(string? sourceRelativePath, string ueModelExportPath, string imageFilePath)
    {
        if (string.IsNullOrEmpty(sourceRelativePath))
            return string.Empty;

        var fileName = Path.GetFileName(sourceRelativePath);
        var sourceFile = ueModelExportPath.Append(sourceRelativePath);
        var targetPath = Path.Combine(imageFilePath, fileName);
        
        if (!File.Exists(sourceFile))
        {
            // ReSharper disable once HeapView.ObjectAllocation
            Debug.Print($"File missing: {sourceFile}");
            return string.Empty;
        }

        await CopyFileAsync(sourceFile, targetPath);
        return targetPath;
    }

    private static async Task CopyFileAsync(string sourceFile, string destinationFile, CancellationToken? token = null)
    {
        await using var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        await using var destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None);

        token ??= CancellationToken.None;
        await sourceStream.CopyToAsync(destinationStream, token.Value);
    }
}
