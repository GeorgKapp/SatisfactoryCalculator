namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class DataModelImageCreateService
{
    public async Task<Data> CreateImagesAsync(Data data, string ueModelExportPath, IExtendedProgress<string> progress = null, CancellationToken? token = null)
    {
        ProgressUtility.ReportOrThrow("Creating Images", progress, token);
        Constants.ImageFilePath.CreateDirectoryIfNotExists();

        ProgressUtility.ReportOrThrow("Creating Item Images", progress, token);
        await CreateImagesAsync(data.Items, ueModelExportPath);

        ProgressUtility.ReportOrThrow("Creating Building Images", progress, token);
        await CreateImagesAsync(data.Buildings, ueModelExportPath);

        ProgressUtility.ReportOrThrow("Creating Schematic Images", progress, token);
        await CreateImagesAsync(data.Schematics, ueModelExportPath);

        ProgressUtility.ReportOrThrow("Creating Emote Images", progress, token);
        await CreateImagesAsync(data.Emotes, ueModelExportPath);

        ProgressUtility.ReportOrThrow("Creating Statue Images", progress, token);
        await CreateImagesAsync(data.Statues, ueModelExportPath);

        return data;
    }

    private async Task CreateImagesAsync(IEnumerable<IIcon> iconObjects, string ueModelExportPath)
    {
        foreach (var iconObject in iconObjects)
        {
            iconObject.SmallIconPath = await CreateImageAsync(iconObject.SmallIconPath, ueModelExportPath);
            iconObject.BigIconPath = await CreateImageAsync(iconObject.BigIconPath, ueModelExportPath);
        }
    }

    private async Task<string> CreateImageAsync(string sourceRelativePath, string ueModelExportPath)
    {
        if (string.IsNullOrEmpty(sourceRelativePath))
            return null;

        string fileName = Path.GetFileName(sourceRelativePath);
        string sourceFile = ueModelExportPath.Append(sourceRelativePath);
        string targetPath = Path.Combine(Constants.ImageFilePath, fileName);

        await CopyFileAsync(sourceFile, targetPath);
        return targetPath;
    }

    private async Task CopyFileAsync(string sourceFile, string destinationFile, CancellationToken? token = null)
    {
        using FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        using FileStream destinationStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None);

        token ??= CancellationToken.None;
        await sourceStream.CopyToAsync(destinationStream, token.Value);
    }
}
