namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface ICreature : IEntity
{
    string Description { get; }
    ICreature[]? Variants { get; set; }
    int HitPoints { get; }
    string[]? Damage { get; }
    Loot[] Loot { get; set; }
    CreatureBehaviour Behaviour { get; }
}