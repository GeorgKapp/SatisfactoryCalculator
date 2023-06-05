namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    // ReSharper disable once HeapView.ClosureAllocation
    private static void RemoveGeneratorFuelsWithNoEnergy(IEnumerable<Item> items, List<Generator> generators)
    {
        foreach (var generator in generators)
        {
            generator.Fuels = generator.Fuels
                // ReSharper disable once HeapView.ClosureAllocation
                .Where(p =>  items
                    .Single(i => i.ClassName == p.FuelClass).EnergyValue > 0)
                .ToArray();
        }
    }

    private static void EditEquipmentDescription(IEnumerable<Item> items)
    {
        foreach (var item in items.Where(item => item.ClassName == "Chainsaw_C"))
        {
            item.Description = item.Description
                .Replace("Slot: Hands", "")
                .Trim();
        }
    }
}