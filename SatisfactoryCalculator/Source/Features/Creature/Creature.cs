namespace SatisfactoryCalculator.Source.Features.Creature;

internal class Creature : ICreature
{
    public string ClassName { get; init; }
    public string Name { get; init; }
    public BitmapImage? Image { get; init; }
    public string Description { get; init; }
    public ICreature[]? Variants { get; set; }
    public int HitPoints { get; init; }
    public string[]? Damage { get; init; }
    public CreatureBehaviour Behaviour { get; init; }
}
