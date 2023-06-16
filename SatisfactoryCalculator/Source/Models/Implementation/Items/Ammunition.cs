#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Ammunition : IAmmunition
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public decimal? EnergyValue { get; init; }
    public decimal MaxAmmoEffectiveRange { get; init;  }
    public decimal? ReloadTimeMultiplier { get; init;  }
    public decimal FireRate { get; init;  }
    public decimal WeaponDamageMultiplier { get; init;  }
    public IWeapon UsedInWeapon { get; set; }
}