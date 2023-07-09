namespace SatisfactoryCalculator.Source.Features.Creature;

internal class CreatureLoot
{
    public ICreature Creature { get; set; }
    public IItem Item { get; set; }
    public int Amount { get; set; }
}
