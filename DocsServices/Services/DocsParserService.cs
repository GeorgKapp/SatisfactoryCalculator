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
			var rootObjects = (await _jsonService.ReadJsonAsync<RootObject[]>(docsFilePath))!;

			var allTypes = GetAllTypes(rootObjects);
			
			var rootObjectHandledDictionary = rootObjects
				.ToDictionary(p => p.NativeClass, c => false);

			var classesDictionary = rootObjects
				.SelectMany(p => p.Classes)
				.ToDictionary(p => p.ClassName!, p => p);

			Item[] biomassItems = null!;
			
			progress?.ReportOrThrow("Add items and buildings", token);
			foreach (var class1 in rootObjects)
				switch (class1.NativeClass)
				{
					case "Class'/Script/FactoryGame.FGItemDescriptorBiomass'":
						biomassItems = ParseItems(class1.Classes);
						rootObjectHandledDictionary[class1.NativeClass] = true;
						tempModelContext.Items.AddRange(biomassItems);
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGItemDescriptor'":
					case "Class'/Script/FactoryGame.FGItemDescriptorNuclearFuel'":
					case "Class'/Script/FactoryGame.FGEquipmentDescriptor'":
					case "Class'/Script/FactoryGame.FGConsumableDescriptor'":
					case "Class'/Script/FactoryGame.FGResourceDescriptor'":
					case "Class'/Script/FactoryGame.FGAmmoTypeInstantHit'":
					case "Class'/Script/FactoryGame.FGAmmoTypeProjectile'":
					case "Class'/Script/FactoryGame.FGAmmoTypeSpreadshot'":
					case "Class'/Script/FactoryGame.FGVehicleDescriptor'":
						tempModelContext.Items.AddRange(ParseItems(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGBuildableResourceExtractor'":
					case "Class'/Script/FactoryGame.FGBuildableWaterPump'":
					case "Class'/Script/FactoryGame.FGBuildableGeneratorFuel'":
					case "Class'/Script/FactoryGame.FGBuildableGeneratorNuclear'":
					case "Class'/Script/FactoryGame.FGBuildableGeneratorGeoThermal'":
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
						tempModelContext.Buildings.AddRange(ParseBuildings(class1.Classes, classesDictionary));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}

			var itemClassNames = tempModelContext.Items.Select(p => p.ClassName).ToArray();
			var buildingClassNames = tempModelContext.Buildings.Select(p => p.ClassName).ToArray();

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
			foreach (var class1 in rootObjects)
				switch (class1.NativeClass)
				{
					case "Class'/Script/FactoryGame.FGWeapon'":
					case "Class'/Script/FactoryGame.FGEquipmentStunSpear'":
					case "Class'/Script/FactoryGame.FGChargedWeapon'":
						var parsedWeaponResults = ParseWeapons(class1.Classes, tempModelContext.Ammunitions).ToArray();
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
			foreach (var class1 in rootObjects)
				switch (class1.NativeClass)
				{
					case "Class'/Script/FactoryGame.FGAmmoTypeInstantHit'":
					case "Class'/Script/FactoryGame.FGAmmoTypeProjectile'":
					case "Class'/Script/FactoryGame.FGAmmoTypeSpreadshot'":
						tempModelContext.Ammunitions.AddRange(ParseAmmunitions(class1.Classes, ammunitionWeaponReferences));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
				}
			
			foreach(var class1 in rootObjects)
				if (class1.NativeClass == "Class'/Script/FactoryGame.FGResourceDescriptor'")
				{
					tempModelContext.Resources.AddRange(ParseResources(class1.Classes));
					rootObjectHandledDictionary[class1.NativeClass] = true;
					await tempModelContext.SaveChangesAsync();
				}

			progress?.ReportOrThrow("Add other entities", token);
			foreach (var class1 in rootObjects)
			{
				token?.ThrowIfCancellationRequested();
				switch (class1.NativeClass)
				{
					case "Class'/Script/FactoryGame.FGConsumableDescriptor'":
						tempModelContext.Consumables.AddRange(ParseConsumables(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
					
					case "Class'/Script/FactoryGame.FGVehicleDescriptor'":
						tempModelContext.Vehicles.AddRange(ParseVehicles(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
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
						tempModelContext.Equipments.AddRange(ParseEquipments(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGBuildableResourceExtractor'":
					case "Class'/Script/FactoryGame.FGBuildableWaterPump'":
						tempModelContext.Miners.AddRange(ParseMiners(class1.Classes, tempModelContext));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGBuildableGeneratorFuel'":
					case "Class'/Script/FactoryGame.FGBuildableGeneratorNuclear'":
					case "Class'/Script/FactoryGame.FGBuildableGeneratorGeoThermal'":
						tempModelContext.Generators.AddRange(ParseGenerators(class1.Classes, biomassItems!));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGRecipe'":
						tempModelContext.Recipes.AddRange(ParseRecipes(class1.Classes, tempModelContext));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;

					case "Class'/Script/FactoryGame.FGCustomizationRecipe'":
						tempModelContext.CustomizationRecipes.AddRange(ParseCustomizationRecipes(class1.Classes));
						rootObjectHandledDictionary[class1.NativeClass] = true;
						await tempModelContext.SaveChangesAsync();
						break;
					
					case "Class'/Script/FactoryGame.FGPortableMinerDispenser'":
					case "Class'/Script/FactoryGame.FGPoleDescriptor'":
					case "Class'/Script/FactoryGame.FGGolfCartDispenser'":
					case "Class'/Script/FactoryGame.FGBuildingDescriptor'":
					case "Class'/Script/FactoryGame.FGConsumableEquipment'":
						//Ignore Classes
						rootObjectHandledDictionary[class1.NativeClass] = true;
						break;
				}
			}
			
			foreach (var class1 in rootObjects)
				if (class1.NativeClass == "Class'/Script/FactoryGame.FGSchematic'")
				{
					tempModelContext.Schematics.AddRange(ParseSchematics(class1.Classes, tempModelContext));
					rootObjectHandledDictionary[class1.NativeClass] = true;
					await tempModelContext.SaveChangesAsync();
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
