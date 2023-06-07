#pragma warning disable CS8618
namespace SatisfactoryCalculator.Source.Models;

internal class Ammunition : IAmmunition
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public Form? Form { get; init; }
    public double EnergyValue { get; init; }
    public double MaxAmmoEffectiveRange { get; init;  }
    public double? ReloadTimeMultiplier { get; init;  }
    public double FireRate { get; init;  }
    public double WeaponDamageMultiplier { get; init;  }
    public IWeapon UsedInWeapon { get; set; }
}