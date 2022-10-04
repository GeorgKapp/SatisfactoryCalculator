namespace SatisfactoryCalculator.Services.DataAccess;

public class JsonService
{
	private JsonSerializerOptions _options = new JsonSerializerOptions
	{
		WriteIndented = true
	};

	public T? ReadJson<T>(string jsonFileName)
	{
		if (!File.Exists(jsonFileName))
			return default(T);

		string json = File.ReadAllText(jsonFileName);
		return JsonSerializer.Deserialize<T>(json, _options);
	}

	public void WriteJson<T>(T serializedObject, string fileName)
	{
		string contents = JsonSerializer.Serialize(serializedObject, _options);
		File.WriteAllText(fileName, contents);
	}

	public async Task<T?> ReadJsonAsync<T>(string jsonFileName)
	{
		if (!File.Exists(jsonFileName))
			return default(T);

		return JsonSerializer.Deserialize<T>(await File.ReadAllTextAsync(jsonFileName), _options);
	}

	public async Task WriteJsonAsync<T>(T serializedObject, string fileName)
	{
		string jsonString = JsonSerializer.Serialize(serializedObject, _options);
		await File.WriteAllTextAsync(fileName, jsonString);
	}
}
