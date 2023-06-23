using Microsoft.EntityFrameworkCore;
using SatisfactoryCalculator.DocsServices.Utility.Parser;

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private bool ExcludeBuilding(string? className)
    {
        return new []
            {
                "Desc_Wall_Window_8x4_03_Steel_C",
                "Build_PillarTop_C",
                "Desc_QuarterPipeMiddle_Ficsit_4x1_C",
                "Desc_QuarterPipeMiddle_Ficsit_4x2_C",
                "Desc_QuarterPipeMiddle_Ficsit_4x4_C"
            }
            .Contains(className);
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private IEnumerable<Building> ParseBuildings(IEnumerable<Classes> classes2, IReadOnlyDictionary<string, Classes> classesDictionary)
    {
        return classes2
            .Where(p => !ExcludeBuilding(p.ClassName))
            .Select(p => ParseBuilding(p, classesDictionary))
            .ToArray();
    }

    private Building ParseBuilding(Classes class2, IReadOnlyDictionary<string, Classes> classesDictionary)
    {
        var smallIconPath = class2.mSmallIcon;
        var bigIconPath = class2.mPersistentBigIcon;
        var correctedClassName = ClassNameParseUtility.CorrectClassNameForImageLookup(class2.ClassName)!;
        
        if (correctedClassName == class2.ClassName)
            correctedClassName = ClassNameParseUtility.ConvertClassNameToDescClassName(correctedClassName);
        
        if (classesDictionary.ContainsKey(correctedClassName))
        {
            if (string.IsNullOrEmpty(smallIconPath))
                smallIconPath = IconPathParseUtility.ReplaceSmallIconPathFromClass(classesDictionary[correctedClassName]);
            
            if (string.IsNullOrEmpty(bigIconPath))
                bigIconPath = IconPathParseUtility.ReplaceBigIconPathFromClass(classesDictionary[correctedClassName]);
        }
        else
        {
            smallIconPath = IconPathParseUtility.ReplaceSmallIconPathFromClass(class2);
            bigIconPath = IconPathParseUtility.ReplaceBigIconPathFromClass(class2);
        }

        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Name = class2.mDisplayName,
            Description = class2.mDescription,
            SmallImagePath = IconPathParseUtility.ConvertIconPathToUePath(smallIconPath)!,
            BigImagePath = IconPathParseUtility.ConvertIconPathToUePath(bigIconPath)!,
            PowerConsumption = class2.mPowerConsumption.MapToNullableDecimal(),
            PowerConsumptionExponent = class2.mPowerConsumptionExponent.MapToNullableDecimal(),
            ManufactoringSpeed = class2.mManufacturingSpeed.MapToNullableDecimal(),
            PowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerConsumption(class2.mEstimatedMininumPowerConsumption.MapToNullableDecimal(), class2.mEstimatedMaximumPowerConsumption.MapToNullableDecimal())
        };
    }

    private Item[] ParseItems(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseItem)
            .ToArray();
    }

    private Item ParseItem(Classes class2)
    {
        var item = new Item
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Name = class2.mDisplayName,
            Description = class2.mDescription,
            Form = class2.mForm.ParseToForm(),
            StackSize = class2.mStackSize.ParseToStackSize(),
            EnergyValue = class2.mEnergyValue.MapToDecimal(),
            IsRadioActive = !string.IsNullOrEmpty(class2.mIsRadioActive) && Convert.ToBoolean(class2.mIsRadioActive),
            RadioActiveDecay = class2.mRadioactiveDecay.MapToDecimal(),
            SmallImagePath = IconPathParseUtility.ConvertIconPathToUePath(class2.mSmallIcon)!,
            BigImagePath = IconPathParseUtility.ConvertIconPathToUePath(class2.mPersistentBigIcon)!,
            SinkPoints = string.IsNullOrEmpty(class2.mResourceSinkPoints)
                ? null
                : Convert.ToInt32(class2.mResourceSinkPoints)
        };

        return item;
    }

    private IEnumerable<Resource> ParseResources(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseResource)
            .ToArray();
    }

    private Resource ParseResource(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!
        };
    }

    private IEnumerable<Equipment> ParseEquipments(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseEquipment)
            .ToArray();
    }

    private Equipment ParseEquipment(Classes class2)
    {
        var equipment = new Equipment
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            EquipmentSlot = class2.mEquipmentSlot.ParseToEquipmentSlot()
        };
        
        return equipment;
    }

    private IEnumerable<Vehicle> ParseVehicles(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseVehicle)
            .ToArray();
    }

    private Vehicle ParseVehicle(Classes class2) =>
        new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            FuelConsumption = string.IsNullOrEmpty(class2.mFuelConsumption)
                ? null
                : class2.mFuelConsumption.MapToDecimal(),
            InventorySize = string.IsNullOrEmpty(class2.mInventorySize) ? null : Convert.ToInt32(class2.mInventorySize)
        };

    private IEnumerable<(Weapon, string[])> ParseWeapons(IEnumerable<Classes> classes2, DbSet<Ammunition> ammunitions)
    {
        return classes2
            .Select(p => ParseWeapon(p, ammunitions))
            .ToArray();
    }

    private (Weapon, string[]) ParseWeapon(Classes class2, DbSet<Ammunition> ammunitions)
    {
        var weapon = new Weapon()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            AutoReloadDelay = class2.mAutoReloadDelay.MapToNullableDecimal(),
            ReloadTime = class2.mReloadTime.MapToNullableDecimal(),
            DamageMultiplier = class2.mWeaponDamageMultiplier.MapToNullableDecimal(),
            EquipmentSlot = class2.mEquipmentSlot.ParseToEquipmentSlot()
        };

        return (
            weapon, 
            ReferenceParseUtility.GetReferences(class2.mAllowedAmmoClasses).ToArray()
            );
    }

    private IEnumerable<Ammunition> ParseAmmunitions(IEnumerable<Classes> classes2, Dictionary<string, string> ammunitionWeaponReferences)
    {
        return classes2
            .Select(p => ParseAmmunition(p , ammunitionWeaponReferences))
            .ToArray();
    }

    private Ammunition ParseAmmunition(Classes class2, Dictionary<string, string> ammunitionWeaponReferences)
    {
        var cleanedClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!;
        
        return new()
        {
            ClassName = cleanedClassName,
            FireRate = class2.mFireRate.MapToDecimal(),
            MaxAmmoEffectiveRange = class2.mMaxAmmoEffectiveRange.MapToDecimal(),
            ReloadTimeMultiplier = class2.mReloadTimeMultiplier.MapToDecimal(),
            WeaponDamageMultiplier = class2.mWeaponDamageMultiplier.MapToDecimal(),
            WeaponClassName = ammunitionWeaponReferences[cleanedClassName]
        };
    }

    private bool ExcludeRecipe(string? className)
    {
        return new[]
            {
                "Recipe_Wall_Window_8x4_03_Steel_C",
                "Recipe_CandyCaneBasher_C",
                "Recipe_PillarTop_C"
            }
            .Contains(className);
    }

    private IEnumerable<Recipe> ParseRecipes(IEnumerable<Classes> classes2, TempModelContext tempModelContext)
    {
        return classes2
            .Where(p => !ExcludeRecipe(p.ClassName))
            .Select(p => ParseRecipe(p, tempModelContext))
            .Where(p => p is not null).Cast<Recipe>()
            .ToArray();
    }

    private Recipe? ParseRecipe(Classes class2, TempModelContext tempModelContext)
    {
        var producedInBuildings = ReferenceParseUtility.GetReferences(class2.mProducedIn);
        if (producedInBuildings.Contains("Converter_C"))
            return null;

        var constructedByBuildGun = producedInBuildings.Contains("BuildGun") || producedInBuildings.Contains("FGBuildGun");
        var constructedInWorkshop = producedInBuildings.Contains("WorkshopComponent");
        // ReSharper disable once HeapView.ObjectAllocation
        var constructedInWorkbench = producedInBuildings.Contains("WorkBenchComponent", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench");
        
        producedInBuildings = producedInBuildings
            .Except("BuildGun", "FGBuildGun", "WorkshopComponent", "WorkBenchComponent", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench")
            .ToArray();

        var buildings = tempModelContext.Buildings
            .Where(p => producedInBuildings.Contains(p.ClassName))
            .ToArray();
        
        var ingredientParseResults = UnrealEngineClassParser.ParseInputs(class2.mIngredients);
        var ingredients = ingredientParseResults.Select(p => new RecipeIngredient
        {
            ItemClassName = p.ClassName,
            Amount = p.Amount!.Value
        }).ToArray();
        
        var productParseResults = UnrealEngineClassParser.ParseInputs(class2.mProduct);
        var products = productParseResults.Select(p =>
        {
            string? itemClassName = null;
            string? buildingClassName = null;
            
            if (tempModelContext.Items.Any(item => item.ClassName == p.ClassName))
                itemClassName = p.ClassName;
            else if (tempModelContext.Buildings.Any(building => building.ClassName == p.ClassName))
                buildingClassName = p.ClassName;
            else
                throw new($"Recipe Product: {p.ClassName} is not contained as either building or item for Recipe: {class2.ClassName}");
            
            return new RecipeProduct()
            {
                ItemClassName = itemClassName,
                BuildingClassName = buildingClassName,
                Amount = p.Amount!.Value
            };
        }).ToArray();
        
        var recipe = new Recipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Name = class2.mDisplayName,
            VariablePowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerVariableConsumption(class2.mVariablePowerConsumptionConstant.MapToDecimal(), class2.mVariablePowerConsumptionFactor.MapToDecimal()),
            ManufacturingMenuPriority = class2.mManufacturingMenuPriority.MapToNullableDecimal(),
            ManufactoringDuration = class2.mManufactoringDuration.MapToDecimal(),
            ManualManufacturingMultiplier = class2.mManualManufacturingMultiplier.MapToDecimal(),
            Ingredients = ingredients,
            Products = products,
            Buildings = buildings,
            ConstructedByBuildGun = constructedByBuildGun,
            ConstructedInWorkshop = constructedInWorkshop,
            ConstructedInWorkbench = constructedInWorkbench,
            IsAlternate = class2.mDisplayName.StartsWith("Alternate")
        };
        
        return recipe;
    }

    private IEnumerable<CustomizationRecipe> ParseCustomizationRecipes(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseCustomizationRecipe)
            .ToArray();
    }

    private CustomizationRecipe ParseCustomizationRecipe(Classes class2)
    {
        var producedInBuildings = ReferenceParseUtility.GetReferences(class2.mProducedIn);

        var constructedByBuildGun = producedInBuildings.Contains("BuildGun") || producedInBuildings.Contains("FGBuildGun");
        var constructedInWorkshop = producedInBuildings.Contains("WorkshopComponent");
        var constructedInWorkbench = producedInBuildings.Contains("WorkBenchComponent", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench");
        
        var ingredientParseResults = UnrealEngineClassParser.ParseInputs(class2.mIngredients);
        var ingredients = ingredientParseResults.Select(p => new CustomizationRecipeIngredient
        {
            ItemClassName = p.ClassName,
            Amount = p.Amount!.Value
        }).ToArray();

        var customizationRecipe = new CustomizationRecipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Name = class2.mDisplayName,
            ManufacturingMenuPriority = class2.mManufacturingMenuPriority.MapToNullableDecimal(),
            ManufactoringDuration = class2.mManufactoringDuration.MapToDecimal(),
            ManualManufacturingMultiplier = class2.mManualManufacturingMultiplier.MapToDecimal(),
            Ingredients = ingredients,
            ConstructedByBuildGun = constructedByBuildGun,
            ConstructedInWorkshop = constructedInWorkshop,
            ConstructedInWorkbench = constructedInWorkbench,
            IsSwatchRecipe = class2.ClassName!.Contains("_Swatch"),
            IsPatternRemover = class2.ClassName.Contains("_Pattern_Remover")
        };
        
        return customizationRecipe;
    }

    private IEnumerable<Miner> ParseMiners(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseMiner)
            .ToArray();
    }

    private Miner ParseMiner(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            ItemsPerCycle = Convert.ToInt32(class2.mItemsPerCycle),
            ExtractCycleTime = class2.mExtractCycleTime.MapToNullableDecimal(),
            AllowedResourceForm = class2.ClassName == "Build_GeneratorGeoThermal_C"
                ? Form.Gas
                : MultiFormParseUtility.MapToForms(class2.mAllowedResourceForms).First()
        };
    }

    private IEnumerable<Schematic> ParseSchematics(IEnumerable<Classes> classes2, TempModelContext tempModelContext)
    {
        return classes2
            .Where(p => !ExcludeSchematics(p.ClassName))
            .Select(x => ParseSchematic(x, tempModelContext))
            .ToArray();
    }

    private bool ExcludeSchematics(string className)
    {
        return className is "Schematic_StartingRecipes_C" or "Schematic_SaveCompatibility_C";
    }

    private Schematic ParseSchematic(Classes class2, TempModelContext tempModelContext)
    {
        var costs = UnrealEngineClassParser.ParseInputs(class2.mCost)
            .Select(p => new SchematicCost { ItemClassName = p.ClassName, Amount = p.Amount ?? 1})
            .ToArray();
        
        var schematic = new Schematic
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Name = class2.mDisplayName,
            Description = class2.mDescription,
            MenuPriority = class2.mMenuPriority.MapToNullableDecimal(),
            TimeToComplete = class2.mTimeToComplete.MapToNullableDecimal(),
            TechTier = Convert.ToInt32(class2.mTechTier),
            SchematicType = class2.mType.ParseToSchematicType(),
            SmallImagePath = string.IsNullOrEmpty(class2.mSmallSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSmallSchematicIcon),
            BigImagePath = string.IsNullOrEmpty(class2.mSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSchematicIcon),
            RelevantEvent = string.IsNullOrEmpty(class2.mRelevantEvents) ? null : MultiRelevantEventsParseUtility.MapToRelevantEvents(class2.mRelevantEvents).Single(),
            Costs = costs,
            HiddenUntilDependenciesMet = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess),
            DependenciesBlocksSchematicAccess = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess)
        };

        foreach (var munlock in class2.mUnlocks)
        {
            switch (munlock.Class)
            {
                case "BP_UnlockArmEquipmentSlot_C":
                    schematic.UnlocksArmEquipmentSlot = Convert.ToInt32(munlock.mNumArmEquipmentSlotsToUnlock);
                    break;
                
                case "BP_UnlockInventorySlot_C":
                    schematic.UnlocksInventoryEquipmentSlot = Convert.ToInt32(munlock.mNumInventorySlotsToUnlock);
                    break;
                
                case "BP_UnlockMap_C":
                    schematic.UnlocksMap = true;
                    break;
                
                case "BP_UnlockRecipe_C":
                    var recipeReferences =  ReferenceParseUtility.GetReferences(munlock.mRecipes);
                    schematic.UnlocksRecipes = tempModelContext.Recipes
                        .Where(p => recipeReferences.Contains(p.ClassName))
                        .ToArray();
                    break;
                
                case "BP_UnlockScannableObject_C":
                    var itemClassNames = tempModelContext.Items.Select(p => p.ClassName).ToArray();
                    var buildingClassNames = tempModelContext.Buildings.Select(p => p.ClassName).ToArray();
                    schematic.UnlocksScannableObjects = ScannableObjectParseUtility.MapToScannableObjects(munlock.mScannableObjects, itemClassNames, buildingClassNames);
                    break;
                
                case "BP_UnlockEmote_C":
                    var emoteReferences =  ReferenceParseUtility.GetReferences(munlock.mEmotes);
                    schematic.UnlocksEmotes = tempModelContext.Emotes
                        .Where(p => emoteReferences.Contains(p.ClassName))
                        .ToArray();
                    break;
                
                case "BP_UnlockScannableResource_C":
                    if (!string.IsNullOrEmpty(munlock.mResourcesToAddToScanner))
                    {
                        var scannerResourcesReferences = UnrealEngineClassParser
                            .ParseInputs(munlock.mResourcesToAddToScanner)
                            .Select(p => p.ClassName)
                            .ToArray();
                        
                        schematic.UnlocksScannerResources = tempModelContext.Resources
                            .Where(p => scannerResourcesReferences.Contains(p.ClassName))
                            .ToArray();
                    }
                    
                    if (!string.IsNullOrEmpty(munlock.mResourcePairsToAddToScanner))
                    {
                        var scannerResourcePairsReferences = UnrealEngineClassParser
                            .ParseInputs(munlock.mResourcePairsToAddToScanner)
                            .Select(p => p.ClassName)
                            .ToArray();
                        
                        schematic.UnlocksScannerResourcePairs = tempModelContext.Resources
                            .Where(p => scannerResourcePairsReferences.Contains(p.ClassName))
                            .ToArray();
                    }
                    break;
                
                case "BP_UnlockGiveItem_C":
                    var itemReferences =  ReferenceParseUtility.GetReferences(munlock.mItemsToGive);
                    schematic.GivesItems = tempModelContext.Items
                        .Where(p => itemReferences.Contains(p.ClassName))
                        .ToArray();
                    break;
            }
        }

        List<SchematicDependency> schematicDependencies = new();
        foreach (var mschematicdependency in class2.mSchematicDependencies)
        {
            SchematicDependency schematicDependency = new();
            
            switch (mschematicdependency.Class)
            {
                case "BP_SchematicPurchasedDependency_C":
                    //TODO: Fix schematic schematic references
                    //schematicDependency.Schematics = ReferenceParseUtility.GetReferences(mschematicdependency.mSchematics);
                    schematicDependency.RequireAllSchematicsToBePurchased = Convert.ToBoolean(mschematicdependency.mRequireAllSchematicsToBePurchased);
                    schematicDependency.SchematicDependencyType = SchematicDependencyType.PurchasedDependency;
                    break;
                
                case "BP_GamePhaseReachedDependency_C":
                    schematicDependency.SchematicDependencyType = SchematicDependencyType.GamePhaseReached;
                    break;
            }

            schematicDependencies.Add(schematicDependency);
        }

        //schematic.Dependencies = schematicDependencies;
        return schematic;
    }

    // ReSharper disable once HeapView.ClosureAllocation
    // ReSharper disable once ReturnTypeCanBeEnumerable.Local
    private Generator[] ParseGenerators(IEnumerable<Classes> classes2, Item[] biomassItems)
    {
        return classes2
            .Select(p => ParseGenerator(p, biomassItems))
            .ToArray();
    }

    private Generator ParseGenerator(Classes class2, Item[] biomassItems)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            Fuels = ParseFuels(class2.mFuel, biomassItems),
            PowerProduction = class2.mPowerProduction.MapToDecimal(),
            SupplementToPowerRatio = class2.mSupplementalToPowerRatio.MapToNullableDecimal(),
            SupplementalLoadAmount = class2.mSupplementalLoadAmount.MapToNullableDecimal()
        };
    }

    private IEnumerable<Consumable> ParseConsumables(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseConsumable)
            .ToArray();
    }

    private Consumable ParseConsumable(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            HealthGain = class2.mHealthGain.MapToNullableDecimal()
        };
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private FuelItem[] ParseFuels(MFuel[]? mFuel, Item[] biomassItems)
    {
        if(mFuel is null)
            return Array.Empty<FuelItem>();
        
        return mFuel
            .SelectMany(p => ParseFuel(p, biomassItems))
            .ToArray();
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private IEnumerable<FuelItem> ParseFuel(MFuel mFuel, IEnumerable<Item> biomassItems)
    {
        if(mFuel.mFuelClass == "FGItemDescriptorBiomass")
        {
            return biomassItems.Select(p => ParseFuel(new()
            {
                mFuelClass = p.ClassName,
                mSupplementalResourceClass = mFuel.mSupplementalResourceClass,
                mByproduct = mFuel.mByproduct,
                mByproductAmount = mFuel.mByproductAmount
            })).ToArray();
        }

        return new[] { ParseFuel(mFuel) };
    }

    private FuelItem ParseFuel(MFuel mFuel)
    {
        return new()
        {
            FuelClassName = ClassNameParseUtility.CleanClassName(mFuel.mFuelClass)!,
            SupplementClassName = string.IsNullOrEmpty(mFuel.mSupplementalResourceClass)
                ? null
                : ClassNameParseUtility.CleanClassName(mFuel.mSupplementalResourceClass),
            ByProductClassName = string.IsNullOrEmpty(mFuel.mByproduct)
                ? null
                : ClassNameParseUtility.CleanClassName(mFuel.mByproduct),
            ByProductAmount = string.IsNullOrEmpty(mFuel.mByproductAmount)
               ? null
               : Convert.ToInt32(mFuel.mByproductAmount)
        };
    }
}