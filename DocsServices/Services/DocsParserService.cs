namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
	public DocsParserService(JsonService jsonService)
	{
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
	}

	public async Task<Result<Data>> ParseDocsJsonAsync(string docsFilePath, IExtendedProgress<string>? progress = null, CancellationToken? token = null)
	{
		progress?.ReportOrThrow("Read docs.json file", token);
		var rootObjects = (await _jsonService.ReadJsonAsync<RootObject[]>(docsFilePath))!;

		var classesDictionary = rootObjects
			.SelectMany(p => p.Classes)
#pragma warning disable CS8714
			.ToDictionary(p => p.ClassName!, p => p);
#pragma warning restore CS8714

		progress?.ReportOrThrow("Parse data", token);
		var data = new Data();

        var biomassItems = Array.Empty<Item>();
        foreach (var class1 in rootObjects)
        {
	        if (class1.NativeClass != "Class'/Script/FactoryGame.FGItemDescriptorBiomass'") 
		        continue;
	        
	        biomassItems = ParseItems(class1.Classes);
	        break;
        }
		data.Items.AddRange(biomassItems);

        foreach (var class1 in rootObjects)
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
                case "Class'/Script/FactoryGame.FGBuildableBeamLightweight'":
                case "Class'/Script/FactoryGame.FGBuildableBlueprintDesigner'":
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
                case "Class'/Script/FactoryGame.FGBuildableFoundationLightweight'":
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
                case "Class'/Script/FactoryGame.FGBuildablePillarLightweight'":
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
                case "Class'/Script/FactoryGame.FGBuildableWalkwayLightweight'":
                case "Class'/Script/FactoryGame.FGBuildableWall'":
                case "Class'/Script/FactoryGame.FGBuildableWallLightweight'":
				case "Class'/Script/FactoryGame.FGBuildableWidgetSign'":
				case "Class'/Script/FactoryGame.FGBuildableWire'":
				case "Class'/Script/FactoryGame.FGConveyorPoleStackable'":
				case "Class'/Script/FactoryGame.FGPipeHyperStart'":
				case "Class'/Script/FactoryGame.FGBuildablePole'":
                case "Class'/Script/FactoryGame.FGBuildablePoleLightweight'":
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
					// ReSharper disable once HeapView.ObjectAllocation
					return Result<Data>.Failure($"{class1.NativeClass} not parsed");
			}
		}
        
        progress?.ReportOrThrow("Add missing items", token);
        data.Items.Add(_coffeeCup);
		data.Items.Add(_goldenCoffeeCup);
		data.Items.Add(_boomBox);
		data.Items.Add(_fiscitCoupon);

		progress?.ReportOrThrow("Add missing equipment", token);
		data.Equipments.Add(_coffeeCupEquipment);
		data.Equipments.Add(_goldenCoffeeCupEquipment);
		data.Equipments.Add(_boomBoxEquipment);
		
		progress?.ReportOrThrow("Edit equipment description", token);
        EditEquipmentDescription(data.Items);

		progress?.ReportOrThrow("Add missing vehicles", token);
		data.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCart_C"]));
		data.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCartGold_C"]));

		progress?.ReportOrThrow("Add missing emotes", token);
		data.Emotes.AddRange(_emotes);

		progress?.ReportOrThrow("Add missing statues", token);
		data.Statues.AddRange(_statues);
		
		progress?.ReportOrThrow("Add ammunition and weapon references for each other", token);
		AddWeaponToAmmunitionReferences(data.Items, data.Weapons, data.Ammunition);

		progress?.ReportOrThrow("Remove Generator Fuels with no energy value", token);
        RemoveGeneratorFuelsWithNoEnergy(data.Items, data.Generators);

		progress?.ReportOrThrow("Check for duplicates", token);
		var duplicateCheckResult = SeperatelyValidateDataForDuplicates(data);
        if (!duplicateCheckResult.IsSuccess)
			return Result<Data>.Failure(duplicateCheckResult.Error!);

        progress?.ReportOrThrow("Check data references", token);
        var dataReferencesCheckResult = ValidateDataReferences(data);
        if (!dataReferencesCheckResult.IsSuccess)
            return Result<Data>.Failure(dataReferencesCheckResult.Error!);

        progress?.ReportOrThrow("Check if all items exist for recipe info", token);
        var validateItemExistenceInRecipesCheckResult = ValidateItemExistanceInRecipes(data);
        if (!validateItemExistenceInRecipesCheckResult.IsSuccess)
            return Result<Data>.Failure(validateItemExistenceInRecipesCheckResult.Error!);

        progress?.ReportOrThrow("Check if all items and all schematic references exist for schematic info", token);
        var validateItemExistenceInSchematicsCheckResult = ValidateItemExistanceInSchematics(data);
        if (!validateItemExistenceInSchematicsCheckResult.IsSuccess)
            return Result<Data>.Failure(validateItemExistenceInSchematicsCheckResult.Error!);

		progress?.ReportSuccess("Data succesfully parsed");
		return Result<Data>.Success(data);
	}

    private readonly JsonService _jsonService;
}
