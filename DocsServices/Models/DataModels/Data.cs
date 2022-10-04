namespace SatisfactoryCalculator.DocsServices.Models.DataModels;

public class Data
{
	public List<Recipe> Recipes { get; set; } = new List<Recipe>();
	public List<CustomizationRecipe> CustomizationRecipes { get; set; } = new List<CustomizationRecipe>();
	public List<Item> Items { get; set; } = new List<Item>();
	public List<Weapon> Weapons { get; set; } = new List<Weapon>();
	public List<Ammunition> Ammunition { get; set; } = new List<Ammunition>();
	public List<Building> Buildings { get; set; } = new List<Building>();
	public List<Generator> Generators { get; set; } = new List<Generator>();
	public List<Miner> Miners { get; set; } = new List<Miner>();
	public List<Schematic> Schematics { get; set; } = new List<Schematic>();
	public List<Resource> Resources { get; set; } = new List<Resource>();
	public List<Equipment> Equipments { get; set; } = new List<Equipment>();
	public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
	public List<Consumable> Consumables { get; set; } = new List<Consumable>();
	public List<Emote> Emotes { get; set; } = new List<Emote>();
	public List<Statue> Statues { get; set; } = new List<Statue>();

}
