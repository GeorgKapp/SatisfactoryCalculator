namespace SatisfactoryCalculator.Shared.Services;

public class JsonService
{
	private readonly JsonSerializerOptions _options = new()
	{
		WriteIndented = true
	};
	
	public async Task<T?> ReadJsonAsync<T>(string jsonFileName)
	{
		if (!File.Exists(jsonFileName))
			return default;
		
		var json = await File.ReadAllTextAsync(jsonFileName);
		return JsonSerializer.Deserialize<T>(json, _options);
	}
	
	public async Task<T?> ReadUtf8JsonAsync<T>(string jsonFileName)
	{
		if (!File.Exists(jsonFileName))
			return default;

		await using var fileStream = new FileStream(jsonFileName, FileMode.Open, FileAccess.Read);
		return await JsonSerializer.DeserializeAsync<T>(fileStream, _options);
	}
}
