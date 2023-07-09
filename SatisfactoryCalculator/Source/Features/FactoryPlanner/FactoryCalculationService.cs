namespace SatisfactoryCalculator.Source.Features.FactoryPlanner;

internal class FactoryCalculationService
{
    public FactoryCalculationService(ApplicationState applicationState)
    {
        _applicationState = applicationState;
    }

    public Result CalculateFactoryConfiguration(FactoryConfiguration factoryConfiguration)
    {
        var validationResult = ValidateFactoryConfiguration(factoryConfiguration);
        if (!validationResult.IsSuccess)
            return validationResult;

        foreach (var desiredOutput in factoryConfiguration.DesiredOutputs)
        {
            
        }
        
        

        factoryConfiguration.CalculatedPowerConsumption =
            factoryConfiguration.FactoryBuildingConfigurations.Sum(p => p.EnergyConsumption);
        
        factoryConfiguration.CalculatedInVersion = _satisfactoryVersion;
        
        return Result.Success();
    }

    private Result ValidateFactoryConfiguration(FactoryConfiguration factoryConfiguration)
    {
        if(string.IsNullOrEmpty(factoryConfiguration.Name))
            return Result.Failure("Factory Configuration needs a name");
        
        if (factoryConfiguration.DesiredOutputs.Count == 0)
            return Result.Failure("Configuration needs outputs to calculate");
        
        if(factoryConfiguration.DesiredOutputs.Any(p => p.Entity is null))
            return Result.Failure("Every Output needs to have an item selected");
        
        if(factoryConfiguration.DesiredOutputs.Any(p => p.Amount is null or 0M && p.BuildingAmount is null or 0M))
            return Result.Failure("Every Output needs at least a single amount filled");
        
        return Result.Success();
    }
    
    private const string _satisfactoryVersion = "0.7.1.1";
    
    private readonly ApplicationState _applicationState;
}
