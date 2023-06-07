using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace SatisfactoryCalculator.DocsServices.Services;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public partial class DocsParserService
{
    // ReSharper disable once HeapView.ClosureAllocation
    private void RemoveGeneratorFuelsWithNoEnergy(IEnumerable<Item> items, List<Generator> generators)
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

    private void EditEquipmentDescription(IEnumerable<Item> items)
    {
        foreach (var item in items.Where(item => item.ClassName == "Chainsaw_C"))
        {
            item.Description = item.Description
                .Replace("Slot: Hands", "")
                .Trim();
        }
    }
    
    private void AddWeaponToAmmunitionReferences(IEnumerable<Item> items, IEnumerable<Weapon> weapons, IEnumerable<Ammunition> ammunitions)
    {
        // ReSharper disable once HeapView.ClosureAllocation
        foreach (var ammunition in ammunitions)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            ammunition.UsedInWeapon = weapons
                .Where(p => p.UsesAmmunition.Contains(ammunition.ClassName))
                .Select(p => p.ClassName)
                .Single();
        }
    }
}