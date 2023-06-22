using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace SatisfactoryCalculator.DocsServices.Services;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public partial class DocsParserService
{
    // ReSharper disable once HeapView.ClosureAllocation
    private void RemoveGeneratorFuelsWithNoEnergy(IQueryable<Generator> generators)
    {
        foreach (var generator in generators)
        {
            generator.Fuels = generator.Fuels
                // ReSharper disable once HeapView.ClosureAllocation
                .Where(p =>  p.Fuel.EnergyValue > 0)
                .ToList();
        }
    }

    private void EditEquipmentDescription(IQueryable<Item> items)
    {
        foreach (var item in items.Where(item => item.ClassName == "Chainsaw_C"))
        {
            item.Description = item.Description
                .Replace("Slot: Hands", "")
                .Trim();
        }
    }
}