using Settings = SatisfactoryCalculator.Properties.Settings;

namespace SatisfactoryCalculator.Source.Features.DataImport;

internal class DataImportViewModel : ObservableObject
{
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private string _report = string.Empty;
    public string Report
    {
        get => _report;
        set => SetProperty(ref _report, value);
    }

    public string UeModelExportDirectoryPath
    {
        get => Settings.Default.UeModelExportDirectory;
        set
        {
            Settings.Default.UeModelExportDirectory = value;
            Notify(nameof(UeModelExportDirectoryPath));
        }
    }

    public string DocsJsonFilePath
    {
        get => Settings.Default.DocsFilePath;
        set
        {
            Settings.Default.DocsFilePath = value;
            Notify(nameof(DocsJsonFilePath));
        }
    }

    private IAsyncCommand? _loadDataCommand;
    public IAsyncCommand LoadDataCommand => _loadDataCommand ??= new AsyncCommand(LoadData);
    
	private ICommand? _cancelLoadDataCommand;
    public ICommand CancelLoadDataCommand => _cancelLoadDataCommand ??= new SimpleCommand(CancelLoadData);

    public DataImportViewModel(
        ApplicationState applicationState,
        DataModelMappingService mappingService, 
        DocsParserService docsParserService)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        _docsParserService = docsParserService ?? throw new ArgumentNullException(nameof(docsParserService));
    }

    private async Task LoadData()
	{
        try
        {
            await Task.Run(async () =>
            {
                IsLoading = true;
                
                var progress = new ExtendedProgress<string>(ReportMessage);
                
                var result = await DebugExtensions.ProfileAsync(
                    _docsParserService.ParseDocsJsonAsync(DocsJsonFilePath, UeModelExportDirectoryPath, progress, _cancellationTokenSource.Token), 
                    "ParseDocsJson");
                
                if (!result.IsSuccess)
                {
                    ReportMessage("Error occured: Error copied to clipboard");
                    await Task.Delay(1000);
                    ClipBoardService.CopyToClipboard(result.Error!);
                    return;
                }
                
                var mappingResult = await _mappingService.MapConfigurationModelsAsync(progress, _cancellationTokenSource.Token);
                _applicationState.SetConfig(mappingResult);

                Settings.Default.Save();
            });
        }
        catch (OperationCanceledException) { }
        catch (Exception exception)
        {
            ReportMessage("Exception occured: Exception copied to clipboard");
            await Task.Delay(1000);
            ClipBoardService.CopyToClipboard(exception.ToString());
        }
        finally
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new();
            IsLoading = false;
        }
	}

	private void CancelLoadData()
	{
		_cancellationTokenSource.Cancel();
	}

	private void ReportMessage(ReportState<string> report)
	{
		Report = report.Value!;
	}

    private void ReportMessage(string report)
    {
        Report = report;
    }

    private readonly ApplicationState _applicationState;
    private CancellationTokenSource _cancellationTokenSource = new();
    private readonly DataModelMappingService _mappingService;
    private readonly DocsParserService _docsParserService;
}
