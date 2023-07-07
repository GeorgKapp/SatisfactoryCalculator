namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private static async Task CreateImagesAsync(TempModelContext tempModelContext, string ueModelExportPath, string imageFilePath, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
    {
        progress?.ReportOrThrow("Creating Images", token);
        if (!Directory.Exists(imageFilePath))
            Directory.CreateDirectory(imageFilePath);

        progress?.ReportOrThrow("Creating Item Images", token);
        await CreateImagesAsync(tempModelContext.Items, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Building Images", token);
        await CreateImagesAsync(tempModelContext.Buildings, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Schematic Images", token);
        await CreateImagesAsync(tempModelContext.Schematics, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Emote Images", token);
        await CreateImagesAsync(tempModelContext.Emotes, ueModelExportPath, imageFilePath);

        progress?.ReportOrThrow("Creating Statue Images", token);
        await CreateImagesAsync(tempModelContext.Statues, ueModelExportPath, imageFilePath);
        
        progress?.ReportOrThrow("Creating Creature Images", token);
        await CreateImagesAsync(tempModelContext.Creatures, ueModelExportPath, imageFilePath);

        await tempModelContext.SaveChangesAsync();
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
        return fileName;
    }

    private static async Task CopyFileAsync(string sourceFile, string destinationFile, CancellationToken? token = null)
    {
        await using var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        await using var destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None);

        token ??= CancellationToken.None;
        await sourceStream.CopyToAsync(destinationStream, token.Value);
    }
}
