using SatisfactoryCalculator.Source.Features.Shared.Models;

namespace SatisfactoryCalculator.Source.Models.Interfaces;

internal interface IStatue : IEntity
{
    string Description { get; }
}
