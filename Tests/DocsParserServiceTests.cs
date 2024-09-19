using System.Diagnostics;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SatisfactoryCalculator.DocsServices.Services;
using SatisfactoryCalculator.Shared.Models;
using SatisfactoryCalculator.Shared.Services;

namespace SatisfactoryCalculator.Tests;

public class DocsParserServiceTests
{
    private readonly DocsParserService _docsParserService;
    
    public DocsParserServiceTests()
    {
        var pathOptions = CreatePathOptions();
        
        var services = new ServiceCollection();
        services.Configure<PathOptions>(options =>
        {
            options.DataFolder = pathOptions.DataFolder;
            options.ImageFolder = pathOptions.ImageFolder;
        });
        
        services.AddDbContextFactory<ModelContext>(options =>
            options
                .UseSqlite($@"Data Source={pathOptions.DataFile};Pooling=false")
        );

        services.AddDbContextFactory<TempModelContext>(options =>
            options
                .UseSqlite($@"Data Source={pathOptions.TempDataFile};Pooling=false")
                .LogTo(p => Debug.WriteLine(p), LogLevel.Error)
                .EnableSensitiveDataLogging());

        services.AddTransient<JsonService>();
        services.AddTransient<DocsParserService>();
        
        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = false
        });
        
        Directory.CreateDirectory(pathOptions.DataFolder);
        Directory.CreateDirectory(pathOptions.ImageFolder);
        
        using var modelContext = serviceProvider.GetRequiredService<ModelContext>();
        modelContext.Database.EnsureDeleted();
        modelContext.Database.Migrate();
        
        using var tempModelContext = serviceProvider.GetRequiredService<TempModelContext>();
        tempModelContext.Database.EnsureDeleted();
        
        _docsParserService = serviceProvider.GetRequiredService<DocsParserService>();
    }

    private PathOptions CreatePathOptions()
    {
        var dataFolder = Environment.CurrentDirectory + "\\Data";
        var imageFolder = dataFolder + "\\Images";
        
        return new PathOptions
        {
            DataFolder = dataFolder,
            ImageFolder = imageFolder
        };
    }
    
    [Fact(DisplayName = "Try parse docs")]
    public async Task TryParseDocs()
    {
        var docsFilePath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Satisfactory\\CommunityResources\\Docs\\en-GB.json";
        var ueModelExportDirectoryPath = "C:\\Program Files\\FModel\\Output\\Exports";
        
        var result = await _docsParserService.ParseDocsJsonAsync(docsFilePath, ueModelExportDirectoryPath);
       
        if (!result.IsSuccess)
            throw new Exception(result.Error);
        
        Assert.True(result.IsSuccess);
    }
}