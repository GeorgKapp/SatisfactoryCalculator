using System.Net.Mime;

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
	public DocsParserService(
		JsonService jsonService, 
		IDbContextFactory<TempModelContext> tempModelContextFactory,
		IDbContextFactory<ModelContext> modelContextFactory,
		IOptions<PathOptions> pathOptions)
	{
		_jsonService = jsonService ?? throw new ArgumentNullException(nameof(jsonService));
		_tempModelContextFactory = tempModelContextFactory ?? throw new ArgumentNullException(nameof(tempModelContextFactory));
		_modelContextFactory = modelContextFactory ?? throw new ArgumentNullException(nameof(modelContextFactory));
		_pathOptions = pathOptions ?? throw new ArgumentNullException(nameof(pathOptions));
	}

	public async Task<Result> ParseDocsJsonAsync(
		string docsFilePath, 
		string ueModelExportDirectoryPath,
		IExtendedProgress<string>? progress = null, 
		CancellationToken? token = null)
	{
		await using var tempModelContext = await _tempModelContextFactory.CreateDbContextAsync();
		
		try
		{
			await tempModelContext.Database.EnsureCreatedAsync();

			progress?.ReportOrThrow("Read docs.json file", token);
			var classes1 = (await _jsonService.ReadJsonAsync<Class1[]>(docsFilePath))!;

#if DEBUG
			//TODO: do enum parsing and check for missing matches programmatically via this method, so its faster than manual checking enums
			var nativeClasses = string.Join(Environment.NewLine, classes1.Select(p => p.NativeClass).ToArray());
			var allClassPrefixes = string.Join(Environment.NewLine, GetAllClassPrefixes(classes1));
			var allForms = string.Join(Environment.NewLine, GetAllForms(classes1));
			var allStackSizes = string.Join(Environment.NewLine, GetAllStackSizes(classes1));
			var allEquipmentSlots = string.Join(Environment.NewLine, GetAllEquipmentSlots(classes1));
			var allTypes = string.Join(Environment.NewLine, GetAllTypes(classes1));
			var allSchematicIcons = string.Join(Environment.NewLine, GetAllSchematicIcons(classes1));
			var allSmallSchematicIcons = string.Join(Environment.NewLine, GetAllSmallSchematicIcons(classes1));
			var allSmallSchematicDependencyClasses = string.Join(Environment.NewLine, GetAllSmallSchematicDependencyClasses(classes1));		
#endif
			
			var rootObjectHandledDictionary = classes1
                .ToDictionary(p => p.NativeClass, c => false);

			var classesDictionary = classes1
                .SelectMany(p => p.Classes)
				.ToDictionary(p => p.ClassName!, p => p);

			Item[] biomassItems = null!;
			
			progress?.ReportOrThrow("Add items", token);
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGItemDescriptorBiomass'":
						biomassItems = ParseItems(class1.Classes);
						rootObjectHandledDictionary[class1.NativeClass] = true;
						tempModelContext.Items.AddRange(biomassItems);
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGItemDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGItemDescriptorNuclearFuel'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGEquipmentDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGConsumableDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGResourceDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeInstantHit'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeProjectile'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeSpreadshot'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGVehicleDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGItemDescriptorPowerBoosterFuel'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGPowerShardDescriptor'":
						tempModelContext.Items.AddRange(ParseItems(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}
			
			progress?.ReportOrThrow("Add buildings", token);
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildingDescriptor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGPoleDescriptor'":
						tempModelContext.Buildings.AddRange(ParseBuildings(class1.Classes, classesDictionary));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}

			progress?.ReportOrThrow("Edit Building Information", token);
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWall'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWallLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGConveyorPoleStackable'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGPipeHyperStart'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePole'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePoleLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildable'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableAttachmentMerger'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableAttachmentSplitter'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableBeam'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableBeamLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableBlueprintDesigner'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableCircuitSwitch'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableConveyorBelt'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableConveyorLift'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableCornerWall'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableDockingStation'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableDoor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableDroneStation'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFactory'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFactoryBuilding'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFactorySimpleProducer'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFloodlight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFoundation'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFoundationLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFrackingActivator'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableFrackingExtractor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableJumppad'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableLadder'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableLightSource'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableLightsControlPanel'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableMAM'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableManufacturer'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableManufacturerVariablePower'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePassthrough'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePillar'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePillarLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipeHyper'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipeReservoir'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipeline'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipelineJunction'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipelinePump'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePipelineSupport'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePowerPole'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePowerStorage'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRadarTower'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRailroadSignal'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRailroadStation'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRailroadTrack'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRamp'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableResourceSink'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableResourceSinkShop'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableSnowDispenser'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableSpaceElevator'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableSplitterSmart'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableStair'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableStorage'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableTradingPost'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableTrainPlatformCargo'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableTrainPlatformEmpty'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWalkway'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWalkwayLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWidgetSign'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWire'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePoleBase'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableRampLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGCentralStorageContainer'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePassthroughPipeHyper'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorFuel'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorGeoThermal'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorNuclear'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableResourceExtractor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWaterPump'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePortalSatellite'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePriorityPowerSwitch'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePortal'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableCornerWallLightweight'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePowerBooster'":
						tempModelContext.Buildings.UpdateRange(EditBuildings(tempModelContext, class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
			}
			
			progress?.ReportOrThrow("Add missing items", token);
			tempModelContext.Items.Add(_coffeeCup);
			tempModelContext.Items.Add(_goldenCoffeeCup);
			tempModelContext.Items.Add(_boomBox);
			tempModelContext.Items.Add(_fiscitCoupon);
			tempModelContext.Items.Add(_harddrive);
			await tempModelContext.SaveChangesAsync();

			progress?.ReportOrThrow("Add missing equipment", token);
			tempModelContext.Equipments.Add(_coffeeCupEquipment);
			tempModelContext.Equipments.Add(_goldenCoffeeCupEquipment);
			tempModelContext.Equipments.Add(_boomBoxEquipment);
			await tempModelContext.SaveChangesAsync();
			
			progress?.ReportOrThrow("Add missing emotes", token);
			tempModelContext.Emotes.AddRange(_emotes);
			await tempModelContext.SaveChangesAsync();

			progress?.ReportOrThrow("Add missing statues", token);
			tempModelContext.Statues.AddRange(_statues);
			await tempModelContext.SaveChangesAsync();
			
			progress?.ReportOrThrow("Add missing creatures", token);
			tempModelContext.Creatures.AddRange(_creatures);
			await tempModelContext.SaveChangesAsync();
			
			foreach (var creature in tempModelContext.Creatures)
			{
				var creatureVariants = _createVariantGroups
					.FirstOrDefault(p => p.Contains(creature.ClassName));

				if (creatureVariants is null)
					continue;
				
				creature.Variants = tempModelContext.Creatures
					.Where(p => creatureVariants.Contains(p.ClassName) && p.ClassName != creature.ClassName)
					.ToArray();
				
				await tempModelContext.SaveChangesAsync();
			}

			progress?.ReportOrThrow("Add missing plants", token);
			tempModelContext.Plants.AddRange(_plants);
			await tempModelContext.SaveChangesAsync();
			
			progress?.ReportOrThrow("Add missing vehicles", token);
			tempModelContext.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCart_C"]));
			tempModelContext.Vehicles.Add(ParseVehicle(classesDictionary["Desc_GolfCartGold_C"]));
			await tempModelContext.SaveChangesAsync();
			
			progress?.ReportOrThrow("Add weapons", token);
			var ammunitionWeaponReferences = new Dictionary<string, string>();
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGWeapon'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGEquipmentStunSpear'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGChargedWeapon'":
						var parsedWeaponResults = ParseWeapons(class1.Classes).ToArray();
						tempModelContext.Weapons.AddRange(parsedWeaponResults.Select(p => p.Item1));
						
						foreach (var parsedWeaponResult in parsedWeaponResults)
						{
							foreach (var ammunitionReference in parsedWeaponResult.Item2)
								ammunitionWeaponReferences.Add(ammunitionReference, parsedWeaponResult.Item1.ClassName);
						}
						
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}

			progress?.ReportOrThrow("Add ammunitions", token);
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeInstantHit'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeProjectile'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGAmmoTypeSpreadshot'":
						tempModelContext.Ammunitions.AddRange(ParseAmmunitions(class1.Classes, ammunitionWeaponReferences));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}
			
			foreach(var class1 in classes1)
				if (class1.NativeClass == "/Script/CoreUObject.Class'/Script/FactoryGame.FGResourceDescriptor'")
				{
					tempModelContext.Resources.AddRange(ParseResources(class1.Classes));
					rootObjectHandledDictionary[class1.NativeClass] = true;
					await tempModelContext.SaveChangesAsync();
				}

			progress?.ReportOrThrow("Add other entities", token);
			foreach (var class1 in classes1)
			{
				token?.ThrowIfCancellationRequested();
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGConsumableDescriptor'":
						tempModelContext.Consumables.AddRange(ParseConsumables(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
					
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGVehicleDescriptor'":
						tempModelContext.Vehicles.AddRange(ParseVehicles(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGSuitBase'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGParachute'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGObjectScanner'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGJumpingStilts'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGJetPack'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGHoverPack'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGGasMask'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGEquipmentZipline'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGChainsaw'":
						tempModelContext.Equipments.AddRange(ParseEquipments(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableResourceExtractor'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableWaterPump'":
						tempModelContext.Miners.AddRange(ParseMiners(class1.Classes, tempModelContext));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorFuel'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorNuclear'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildableGeneratorGeoThermal'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGBuildablePowerBooster'":
						tempModelContext.Generators.AddRange(ParseGenerators(class1.Classes, biomassItems!));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGRecipe'":
						tempModelContext.Recipes.AddRange(ParseRecipes(class1.Classes, tempModelContext));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGCustomizationRecipe'":
						tempModelContext.CustomizationRecipes.AddRange(ParseCustomizationRecipes(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}
			}
			
			foreach (var class1 in classes1)
				if (class1.NativeClass == "/Script/CoreUObject.Class'/Script/FactoryGame.FGSchematic'")
				{
					tempModelContext.Schematics.AddRange(ParseSchematics(class1.Classes, tempModelContext));
					rootObjectHandledDictionary[class1.NativeClass] = true;
					await tempModelContext.SaveChangesAsync();
				}

			//Ignore Classes
			foreach (var class1 in classes1)
				switch (class1.NativeClass)
				{
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGPortableMinerDispenser'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGGolfCartDispenser'":
					case "/Script/CoreUObject.Class'/Script/FactoryGame.FGConsumableEquipment'":
						rootObjectHandledDictionary[class1.NativeClass] = true;
						break;
				}

			progress?.ReportOrThrow("Check if all references very traversed");
			var missingReferencesResult = ValidateIfAllEntitiesWereTraversed(rootObjectHandledDictionary);
			if (!missingReferencesResult.IsSuccess)
				return Result.Failure(missingReferencesResult.Error!);

			progress?.ReportOrThrow("Edit equipment description", token);
			EditEquipmentDescription(tempModelContext.Items);
			await tempModelContext.SaveChangesAsync();
			
			progress?.ReportOrThrow("Remove Generator Fuels with no energy value", token);
			RemoveGeneratorFuelsWithNoEnergy(tempModelContext.Generators);
			await tempModelContext.SaveChangesAsync();

			progress?.ReportOrThrow("Check for duplicates", token);
			var duplicateCheckResult = SeperatelyValidateDataForDuplicates(tempModelContext);
			if (!duplicateCheckResult.IsSuccess)
				return Result.Failure(duplicateCheckResult.Error!);

			progress?.ReportOrThrow("Check data references", token);
			var dataReferencesCheckResult = ValidateDataReferences(tempModelContext);
			if (!dataReferencesCheckResult.IsSuccess)
				return Result.Failure(dataReferencesCheckResult.Error!);

			progress?.ReportOrThrow("Check if all items exist for recipe info", token);
			var validateItemExistenceInRecipesCheckResult = ValidateItemExistanceInRecipes(tempModelContext);
			if (!validateItemExistenceInRecipesCheckResult.IsSuccess)
				return Result.Failure(validateItemExistenceInRecipesCheckResult.Error!);

			progress?.ReportOrThrow("Check if all items and all schematic references exist for schematic info", token);
			var validateItemExistenceInSchematicsCheckResult = ValidateItemExistanceInSchematics(tempModelContext);
			if (!validateItemExistenceInSchematicsCheckResult.IsSuccess)
				return Result.Failure(validateItemExistenceInSchematicsCheckResult.Error!);
			
			//TODO: insert configuration copying beforehand
			progress?.ReportOrThrow("Parse images", token);
			await CreateImagesAsync(tempModelContext, ueModelExportDirectoryPath, _pathOptions.Value.ImageFolder, progress, token);
			
			await CopyMigrationHistoryAsync(tempModelContext);

			File.Copy(
				_pathOptions.Value.TempDataFile, 
				_pathOptions.Value.DataFile, 
				true);
			
			progress?.ReportSuccess("Data succesfully parsed");
			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(exception.ToString());
		}
		finally
		{
			await tempModelContext.Database.EnsureDeletedAsync();
		}
	}

	private async Task CopyMigrationHistoryAsync(TempModelContext tempModelContext)
	{
		var modelContext = await _modelContextFactory.CreateDbContextAsync();
		var data = modelContext.SelectQuery("SELECT MigrationId, ProductVersion FROM __EFMigrationsHistory;");
		
		await tempModelContext.Database.ExecuteSqlAsync(
			$"CREATE TABLE __EFMigrationsHistory (MigrationId TEXT NOT NULL CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY, ProductVersion TEXT NOT NULL);");

		foreach (DataRow dataRow in data.Rows)
		{
			await tempModelContext.Database.ExecuteSqlAsync(
				$"INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) SELECT {dataRow[0]}, {dataRow[1]}");
		}
	}

	private readonly JsonService _jsonService;
    private readonly IDbContextFactory<TempModelContext> _tempModelContextFactory;
    private readonly IDbContextFactory<ModelContext> _modelContextFactory;
    private readonly IOptions<PathOptions> _pathOptions;
}
