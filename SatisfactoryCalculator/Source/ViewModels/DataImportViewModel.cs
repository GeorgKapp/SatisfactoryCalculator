namespace SatisfactoryCalculator.Source.ViewModels;

internal class DataImportViewModel : ObservableObject
{
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private string _report;
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

    private IAsyncCommand _loadDataCommand;
    public IAsyncCommand LoadDataCommand => _loadDataCommand ??= new AsyncCommand(LoadData);
    
	private ICommand _cancelLoadDataCommand;
    public ICommand CancelLoadDataCommand => _cancelLoadDataCommand ??= new SimpleCommand(CancelLoadData);

    public DataImportViewModel(ApplicationState applicationState, ClipBoardService clipBoardService, DataModelMappingService mappingService, DocsParserService docsParserService, DataModelImageCreateService dataModelImageCreateService)
    {
        _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        _clipBoardService = clipBoardService ?? throw new ArgumentNullException(nameof(clipBoardService));
        _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        _docsParserService = docsParserService ?? throw new ArgumentNullException(nameof(docsParserService));
        _dataModelImageCreateService = dataModelImageCreateService ?? throw new ArgumentNullException(nameof(dataModelImageCreateService));
    }

    private async Task LoadData()
	{
        try
        {
            await Task.Run(async () =>
            {
                IsLoading = true;
                var progress = new ExtendedProgress<string>(ReportMessage);

                var result = await _docsParserService.ParseDocsJsonAsync(DocsJsonFilePath, progress, _cancellationTokenSource.Token);
                if (!result.IsSuccess)
                {
                    ReportMessage("Error occured: Error copied to clipboard");
                    await Task.Delay(1000);
                    _clipBoardService.CopyToClipboard(result.Error);
                    return;
                }

                var data = await _dataModelImageCreateService.CreateImagesAsync(result.Content, UeModelExportDirectoryPath, progress, _cancellationTokenSource.Token);
                var mappingResult = _mappingService.MapToConfigurationModel(data, progress, _cancellationTokenSource.Token);
                _applicationState.SetConfig(data, mappingResult);

                Settings.Default.Save();
            });
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception exception)
        {
            ReportMessage("Exception occured: Exception copied to clipboard");
            await Task.Delay(1000);
            _clipBoardService.CopyToClipboard(exception.ToString());
        }
        finally
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            IsLoading = false;
        }
	}

	private void CancelLoadData()
	{
		_cancellationTokenSource.Cancel();
	}

	private void ReportMessage(ReportState<string> report)
	{
		Report = report.Value;
	}

    private void ReportMessage(string report)
    {
        Report = report;
    }

    private ApplicationState _applicationState;
    private CancellationTokenSource _cancellationTokenSource = new();

    private readonly ClipBoardService _clipBoardService;
    private readonly DataModelMappingService _mappingService;
    private readonly DataModelImageCreateService _dataModelImageCreateService;
    private readonly DocsParserService _docsParserService;
}
