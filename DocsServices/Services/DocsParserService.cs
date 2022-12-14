namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
	public DocsParserService(JsonService jsonService)
	{
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
	}

	public async Task<Result<Data>> ParseDocsJsonAsync(string docsFilePath, IExtendedProgress<string> progress, CancellationToken? token = null)
	{
		ProgressUtility.ReportOrThrow("Read docs.json file", progress, token);
		var classes1 = await _jsonService.ReadJsonAsync<Class1[]>(docsFilePath);

		var classesDictionary = classes1.SelectMany(p => p.Classes).ToDictionary(p => p.ClassName, p => p);

		ProgressUtility.ReportOrThrow("Parse data", progress, token);
		var data = new Data();

        Item[] biomassItems = Array.Empty<Item>();
        foreach (Class1 class1 in classes1)
		{
			if (class1.NativeClass == "Class'/Script/FactoryGame.FGItemDescriptorBiomass'")
			{
                biomassItems = ParseItems(class1.Classes);
				break;
			}
		}
		data.Items.AddRange(biomassItems);

        foreach (Class1 class1 in classes1)
		{
			token?.ThrowIfCancellationRequested();
			switch (class1.NativeClass)
			{
				case "Class'/Script/FactoryGame.FGItemDescriptor'":
				case "Class'/Script/FactoryGame.FGItemDescriptorNuclearFuel'":
				case "Class'/Script/FactoryGame.FGEquipmentDescriptor'":
					data.Items.AddRange(ParseItems(class1.Classes)); 
					break;

				case "Class'/Script/FactoryGame.FGConsumableDescriptor'":
					data.Items.AddRange(ParseItems(class1.Classes));
					data.Consumables.AddRange(ParseConsumables(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGResourceDescriptor'":
					data.Items.AddRange(ParseItems(class1.Classes));
					data.Resources.AddRange(ParseResources(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGAmmoTypeInstantHit'":
				case "Class'/Script/FactoryGame.FGAmmoTypeProjectile'":
				case "Class'/Script/FactoryGame.FGAmmoTypeSpreadshot'":
					data.Items.AddRange(ParseItems(class1.Classes));
					data.Ammunition.AddRange(ParseAmmunitions(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGWeapon'":
				case "Class'/Script/FactoryGame.FGEquipmentStunSpear'":
				case "Class'/Script/FactoryGame.FGChargedWeapon'":
					data.Weapons.AddRange(ParseWeapons(class1.Classes)); 
					break;

				case "Class'/Script/FactoryGame.FGVehicleDescriptor'":
					data.Items.AddRange(ParseItems(class1.Classes));
					data.Vehicles.AddRange(ParseVehicles(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGSuitBase'":
				case "Class'/Script/FactoryGame.FGParachute'":
				case "Class'/Script/FactoryGame.FGObjectScanner'":
				case "Class'/Script/FactoryGame.FGJumpingStilts'":
				case "Class'/Script/FactoryGame.FGJetPack'":
				case "Class'/Script/FactoryGame.FGHoverPack'":
				case "Class'/Script/FactoryGame.FGGasMask'":
				case "Class'/Script/FactoryGame.FGEquipmentZipline'":
				case "Class'/Script/FactoryGame.FGChainsaw'":
					data.Equipments.AddRange(ParseEquipments(class1.Classes)); 
					break;

				case "Class'/Script/FactoryGame.FGBuildableResourceExtractor'":
				case "Class'/Script/FactoryGame.FGBuildableWaterPump'":
					data.Buildings.AddRange(ParseBuildings(class1.Classes, classesDictionary));
					data.Miners.AddRange(ParseMiners(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGBuildableGeneratorFuel'":
				case "Class'/Script/FactoryGame.FGBuildableGeneratorNuclear'":
                case "Class'/Script/FactoryGame.FGBuildableGeneratorGeoThermal'":
                    data.Buildings.AddRange(ParseBuildings(class1.Classes, classesDictionary));
					data.Generators.AddRange(ParseGenerators(class1.Classes, biomassItems));
					break;

                case "Class'/Script/FactoryGame.FGBuildable'":
				case "Class'/Script/FactoryGame.FGBuildableAttachmentMerger'":
				case "Class'/Script/FactoryGame.FGBuildableAttachmentSplitter'":
				case "Class'/Script/FactoryGame.FGBuildableBeam'":
				case "Class'/Script/FactoryGame.FGBuildableCircuitSwitch'":
				case "Class'/Script/FactoryGame.FGBuildableConveyorBelt'":
				case "Class'/Script/FactoryGame.FGBuildableConveyorLift'":
				case "Class'/Script/FactoryGame.FGBuildableCornerWall'":
				case "Class'/Script/FactoryGame.FGBuildableDockingStation'":
				case "Class'/Script/FactoryGame.FGBuildableDoor'":
				case "Class'/Script/FactoryGame.FGBuildableDroneStation'":
				case "Class'/Script/FactoryGame.FGBuildableFactory'":
				case "Class'/Script/FactoryGame.FGBuildableFactoryBuilding'":
				case "Class'/Script/FactoryGame.FGBuildableFactorySimpleProducer'":
				case "Class'/Script/FactoryGame.FGBuildableFloodlight'":
				case "Class'/Script/FactoryGame.FGBuildableFoundation'":
				case "Class'/Script/FactoryGame.FGBuildableFrackingActivator'":
				case "Class'/Script/FactoryGame.FGBuildableFrackingExtractor'":
				case "Class'/Script/FactoryGame.FGBuildableJumppad'":
				case "Class'/Script/FactoryGame.FGBuildableLadder'":
				case "Class'/Script/FactoryGame.FGBuildableLightSource'":
				case "Class'/Script/FactoryGame.FGBuildableLightsControlPanel'":
				case "Class'/Script/FactoryGame.FGBuildableMAM'":
				case "Class'/Script/FactoryGame.FGBuildableManufacturer'":
                case "Class'/Script/FactoryGame.FGBuildableManufacturerVariablePower'":
                case "Class'/Script/FactoryGame.FGBuildablePassthrough'":
				case "Class'/Script/FactoryGame.FGBuildablePillar'":
				case "Class'/Script/FactoryGame.FGBuildablePipeHyper'":
				case "Class'/Script/FactoryGame.FGBuildablePipeReservoir'":
				case "Class'/Script/FactoryGame.FGBuildablePipeline'":
				case "Class'/Script/FactoryGame.FGBuildablePipelineJunction'":
				case "Class'/Script/FactoryGame.FGBuildablePipelinePump'":
				case "Class'/Script/FactoryGame.FGBuildablePipelineSupport'":
				case "Class'/Script/FactoryGame.FGBuildablePowerPole'":
				case "Class'/Script/FactoryGame.FGBuildablePowerStorage'":
				case "Class'/Script/FactoryGame.FGBuildableRadarTower'":
				case "Class'/Script/FactoryGame.FGBuildableRailroadSignal'":
				case "Class'/Script/FactoryGame.FGBuildableRailroadStation'":
				case "Class'/Script/FactoryGame.FGBuildableRailroadTrack'":
				case "Class'/Script/FactoryGame.FGBuildableRamp'":
				case "Class'/Script/FactoryGame.FGBuildableResourceSink'":
				case "Class'/Script/FactoryGame.FGBuildableResourceSinkShop'":
				case "Class'/Script/FactoryGame.FGBuildableSnowDispenser'":
				case "Class'/Script/FactoryGame.FGBuildableSpaceElevator'":
				case "Class'/Script/FactoryGame.FGBuildableSplitterSmart'":
				case "Class'/Script/FactoryGame.FGBuildableStair'":
				case "Class'/Script/FactoryGame.FGBuildableStorage'":
				case "Class'/Script/FactoryGame.FGBuildableTradingPost'":
				case "Class'/Script/FactoryGame.FGBuildableTrainPlatformCargo'":
				case "Class'/Script/FactoryGame.FGBuildableTrainPlatformEmpty'":
				case "Class'/Script/FactoryGame.FGBuildableWalkway'":
				case "Class'/Script/FactoryGame.FGBuildableWall'":
				case "Class'/Script/FactoryGame.FGBuildableWidgetSign'":
				case "Class'/Script/FactoryGame.FGBuildableWire'":
				case "Class'/Script/FactoryGame.FGConveyorPoleStackable'":
				case "Class'/Script/FactoryGame.FGPipeHyperStart'":
				case "Class'/Script/FactoryGame.FGBuildablePole'":
					data.Buildings.AddRange(ParseBuildings(class1.Classes, classesDictionary));
					break;

				case "Class'/Script/FactoryGame.FGSchematic'":
					data.Schematics.AddRange(ParseSchematics(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGRecipe'":
					data.Recipes.AddRange(ParseRecipes(class1.Classes)); 
					break;

				case "Class'/Script/FactoryGame.FGCustomizationRecipe'":
					data.CustomizationRecipes.AddRange(ParseCustomizationRecipes(class1.Classes));
					break;

				case "Class'/Script/FactoryGame.FGPortableMinerDispenser'":
				case "Class'/Script/FactoryGame.FGPoleDescriptor'":
				case "Class'/Script/FactoryGame.FGGolfCartDispenser'":
				case "Class'/Script/FactoryGame.FGBuildingDescriptor'":
				case "Class'/Script/FactoryGame.FGConsumableEquipment'":
				case "Class'/Script/FactoryGame.FGItemDescriptorBiomass'":
                    //Ignore classes
                    break;

				default:
					return Result<Data>.Failure($"{class1.NativeClass} not parsed");
			}
		}

		ProgressUtility.ReportOrThrow("Add missing items", progress, token);
		data.Items.Add(CoffeeCup);
		data.Items.Add(GoldenCoffeeCup);
		data.Items.Add(BoomBox);
		data.Items.Add(FiscitCoupon);

		ProgressUtility.ReportOrThrow("Add missing equipment", progress, token);
		data.Equipments.Add(CoffeeCupEquipment);
		data.Equipments.Add(GoldenCoffeeCupEquipment);
		data.Equipments.Add(BoomBoxEquipment);

		ProgressUtility.ReportOrThrow("Add missing vehicles", progress, token);
		data.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCart_C"]));
		data.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCartGold_C"]));

		ProgressUtility.ReportOrThrow("Add missing emotes", progress, token);
		data.Emotes.AddRange(Emotes);

		ProgressUtility.ReportOrThrow("Add missing statues", progress, token);
		data.Statues.AddRange(Statues);

		ProgressUtility.ReportOrThrow("Check for duplicates", progress, token);
		var duplicateCheckResult = SeperatelyValidateDataForDuplicates(data);
        if (!duplicateCheckResult.IsSuccess)
			return Result<Data>.Failure(duplicateCheckResult.Error);

		ProgressUtility.ReportOrThrow("Check data references", progress, token);
        var dataReferencesCheckResult = ValidateDataReferences(data);
        if (!dataReferencesCheckResult.IsSuccess)
            return Result<Data>.Failure(dataReferencesCheckResult.Error);

		ProgressUtility.ReportOrThrow("Check if all items exist for recipe info", progress, token);
        var validateItemExistenceInRecipesCheckResult = ValidateItemExistanceInRecipes(data);
        if (!validateItemExistenceInRecipesCheckResult.IsSuccess)
            return Result<Data>.Failure(validateItemExistenceInRecipesCheckResult.Error);

		ProgressUtility.ReportOrThrow("Check if all items and all schematic references exist for schematic info", progress, token);
        var validateItemExistenceInSchematicsCheckResult = ValidateItemExistanceInSchematics(data);
        if (!validateItemExistenceInSchematicsCheckResult.IsSuccess)
            return Result<Data>.Failure(validateItemExistenceInSchematicsCheckResult.Error);

		progress.ReportSuccess("Data succesfully parsed");
		return Result<Data>.Success(data);
	}

    private readonly JsonService _jsonService;
}
