namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IResource : IItem
{
    public IMiner[] Miners { get; set; }
}
