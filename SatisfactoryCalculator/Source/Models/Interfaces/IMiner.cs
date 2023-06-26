namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IMiner : IBuilding
{
    public IResource[] Resources { get; set; }
}
