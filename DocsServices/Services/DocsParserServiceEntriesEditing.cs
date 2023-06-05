namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    public void RemoveGeneratorFuelsWithNoEnergy(List<Item> items, List<Generator> generators)
    {
        foreach (var generator in generators)
        {
            generator.Fuels = generator.Fuels
                .Where(p =>  items
                    .Where(i => i.ClassName == p.FuelClass)
                    .Single().EnergyValue > 0)
                .ToArray();
        }
    }
    
    public void EditEquipmentDescription(List<Item> items)
    {
        foreach (var item in items)
        {
            if (item.ClassName == "Chainsaw_C")
            {
                item.Description = item.Description
                    .Replace("Slot: Hands", "")
                    .Trim();
            }
        }
    }
}