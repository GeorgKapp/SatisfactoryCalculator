using SatisfactoryCalculator.DocsServices.Utility.Parser;
using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;

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
            DisplayName = class2.mDisplayName,
            Description = class2.mDescription,
            SmallIconPath = IconPathParseUtility.ConvertIconPathToUePath(smallIconPath)!,
            BigIconPath = IconPathParseUtility.ConvertIconPathToUePath(bigIconPath)!,
            PowerConsumption = NumberParseUtility.MapToNullableDouble(class2.mPowerConsumption),
            PowerConsumptionExponent = NumberParseUtility.MapToNullableDouble(class2.mPowerConsumptionExponent),
            ManuFacturingSpeed = NumberParseUtility.MapToNullableDouble(class2.mManufacturingSpeed),
            PowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerConsumption(NumberParseUtility.MapToNullableDouble(class2.mEstimatedMininumPowerConsumption), NumberParseUtility.MapToNullableDouble(class2.mEstimatedMaximumPowerConsumption))
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
            DisplayName = class2.mDisplayName,
            Description = class2.mDescription,
            Form = StringToEnumParseUtility.ParseFormStringToEnum(class2.mForm),
            StackSize = StringToEnumParseUtility.ParseStackSizeStringToEnum(class2.mStackSize),
            EnergyValue = NumberParseUtility.MapToDouble(class2.mEnergyValue),
            IsRadioActive = string.IsNullOrEmpty(class2.mIsRadioActive)
                ? null
                : Convert.ToBoolean(class2.mIsRadioActive),
            RadioActiveDecay = NumberParseUtility.MapToDouble(class2.mRadioactiveDecay),
            SmallIconPath = IconPathParseUtility.ConvertIconPathToUePath(class2.mSmallIcon)!,
            BigIconPath = IconPathParseUtility.ConvertIconPathToUePath(class2.mPersistentBigIcon)!,
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
            EquipmentSlot = StringToEnumParseUtility.ParseEquipmentSlotStringToEnum(class2.mEquipmentSlot)
        };
        
        return equipment;
    }

    private IEnumerable<Vehicle> ParseVehicles(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseVehicle)
            .ToArray();
    }

    private Vehicle ParseVehicle(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            FuelConsumption = string.IsNullOrEmpty(class2.mFuelConsumption)
                ? null
                : NumberParseUtility.MapToDouble(class2.mFuelConsumption),
            InventorySize = string.IsNullOrEmpty(class2.mInventorySize) ? null : Convert.ToInt32(class2.mInventorySize)
        };
    }

    private IEnumerable<Weapon> ParseWeapons(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseWeapon)
            .ToArray();
    }

    private Weapon ParseWeapon(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            AutoReloadDelay = NumberParseUtility.MapToNullableDouble(class2.mAutoReloadDelay),
            ReloadTime = NumberParseUtility.MapToNullableDouble(class2.mReloadTime),
            DamageMultiplier = NumberParseUtility.MapToNullableDouble(class2.mWeaponDamageMultiplier),
            EquipmentSlot = StringToEnumParseUtility.ParseEquipmentSlotStringToEnum(class2.mEquipmentSlot),
            UsesAmmunition = ReferenceParseUtility.GetReferences(class2.mAllowedAmmoClasses).Except("CartridgePlasma_C").ToArray()
        };
    }

    private IEnumerable<Ammunition> ParseAmmunitions(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseAmmunition)
            .ToArray();
    }

    private Ammunition ParseAmmunition(Classes class2)
    {
        return new()
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            FireRate = NumberParseUtility.MapToDouble(class2.mFireRate),
            MaxAmmoEffectiveRange = NumberParseUtility.MapToDouble(class2.mMaxAmmoEffectiveRange),
            ReloadTimeMultiplier = NumberParseUtility.MapToNullableDouble(class2.mReloadTimeMultiplier),
            WeaponDamageMultiplier = NumberParseUtility.MapToDouble(class2.mWeaponDamageMultiplier)
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

    private IEnumerable<Recipe> ParseRecipes(IEnumerable<Classes> classes2)
    {
        return classes2
            .Where(p => !ExcludeRecipe(p.ClassName))
            .Select(ParseRecipe)
            .Where(p => p is not null).Cast<Recipe>()
            .ToArray();
    }

    private Recipe? ParseRecipe(Classes class2)
    {
        var array = ReferenceParseUtility.GetReferences(class2.mProducedIn);
        if (array.Contains("Converter_C"))
            return null;

        var constructedByBuildGun = array.Contains("BuildGun_C") || array.Contains("FGBuildGun");
        var constructedInWorkshop = array.Contains("WorkshopComponent_C");
        // ReSharper disable once HeapView.ObjectAllocation
        var constructedInWorkbench = array.Contains("WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C");
        
        array = array
            // ReSharper disable once HeapView.ObjectAllocation
            .Except("BuildGun_C", "FGBuildGun", "WorkshopComponent_C", "WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C")
            .ToArray();
        
        var recipe = new Recipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            DisplayName = class2.mDisplayName,
            VariablePowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerVariableConsumption(NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionConstant), NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionFactor)),
            ManufacturingMenuPriority = NumberParseUtility.MapToNullableDouble(class2.mManufacturingMenuPriority),
            ManufactoringDuration = NumberParseUtility.MapToDouble(class2.mManufactoringDuration),
            ManualManufacturingMultiplier = NumberParseUtility.MapToDouble(class2.mManualManufacturingMultiplier),
            Ingredients = ReferenceParseUtility.GetReferencesWithAmount(class2.mIngredients),
            Products = ReferenceParseUtility.GetReferencesWithAmount(class2.mProduct),
            Buildings = array.ToArray(),
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
        var array = ReferenceParseUtility.GetReferences(class2.mProducedIn);

        var constructedByBuildGun = array.Contains("BuildGun_C") || array.Contains("FGBuildGun");
        var constructedInWorkshop = array.Contains("WorkshopComponent_C");
        // ReSharper disable once HeapView.ObjectAllocation
        var constructedInWorkbench = array.Contains("WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C");

        // ReSharper disable once HeapView.ObjectAllocation
        array = array.Except("BuildGun_C", "FGBuildGun", "WorkshopComponent_C", "WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C").ToArray();

        var customizationRecipe = new CustomizationRecipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            DisplayName = (class2.mDisplayName == "N/A" ? null : class2.mDisplayName)!,
            VariablePowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerVariableConsumption(NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionConstant), NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionFactor)),
            ManufacturingMenuPriority = NumberParseUtility.MapToNullableDouble(class2.mManufacturingMenuPriority),
            ManufactoringDuration = NumberParseUtility.MapToDouble(class2.mManufactoringDuration),
            ManualManufacturingMultiplier = NumberParseUtility.MapToDouble(class2.mManualManufacturingMultiplier),
            Ingredients = ReferenceParseUtility.GetReferencesWithAmount(class2.mIngredients),
            Products = ReferenceParseUtility.GetReferencesWithAmount(class2.mProduct),
            Buildings = array.ToArray(),
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
            ExtractCycleTime = NumberParseUtility.MapToNullableDouble(class2.mExtractCycleTime),
            AllowedResourceForms = class2.ClassName == "Build_GeneratorGeoThermal_C"
                ? new [] { Form.Gas }
                : MultiFormParseUtility.MapToForms(class2.mAllowedResourceForms)
        };
    }

    private IEnumerable<Schematic> ParseSchematics(IEnumerable<Classes> classes2)
    {
        return classes2
            .Select(ParseSchematic)
            .ToArray();
    }

    private Schematic ParseSchematic(Classes class2)
    {
        var schematic = new Schematic
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)!,
            DisplayName = class2.mDisplayName,
            MenuPriority = NumberParseUtility.MapToNullableDouble(class2.mMenuPriority),
            TimeToComplete = NumberParseUtility.MapToNullableDouble(class2.mTimeToComplete),
            TechTier = Convert.ToInt32(class2.mTechTier),
            Type = StringToEnumParseUtility.ParseSchematicTypeStringToEnum(class2.mType),
            SmallIconPath = string.IsNullOrEmpty(class2.mSmallSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSmallSchematicIcon),
            BigIconPath = string.IsNullOrEmpty(class2.mSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSchematicIcon),
            RelevantEvents = string.IsNullOrEmpty(class2.mRelevantEvents) ? Array.Empty<string>() : ReferenceParseUtility.GetReferences(class2.mRelevantEvents),
            Cost = string.IsNullOrEmpty(class2.mCost) ? Array.Empty<Reference>() : ReferenceParseUtility.GetReferencesWithAmount(class2.mCost),
            HiddenUntilDependenciesMet = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess),
            DependenciesBlocksSchematicAccess = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess),
            UnlocksRecipes = Array.Empty<string>(),
            UnlocksScannerResources = Array.Empty<string>(),
            UnlocksScannerResourcePairs = Array.Empty<string>(),
            Emotes = Array.Empty<string>(),
            ItemsToGive = Array.Empty<Reference>(),
            SchematicDependencies = Array.Empty<SchematicDependency>()
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
                    schematic.UnlocksRecipes = ReferenceParseUtility.GetReferences(munlock.mRecipes);
                    break;
                
                case "BP_UnlockScannableObject_C":
                    schematic.UnlocksScannerObject = ScannableObjectParseUtility.MapToScannableObject(munlock.mScannableObjects);
                    break;
                
                case "BP_UnlockEmote_C":
                    schematic.Emotes = ReferenceParseUtility.GetReferences(munlock.mEmotes);
                    break;
                
                case "BP_UnlockScannableResource_C":
                    schematic.UnlocksScannerResources = string.IsNullOrEmpty(munlock.mResourcesToAddToScanner) ? Array.Empty<string>() : UnrealEngineClassParser.ParseInputs(munlock.mResourcesToAddToScanner).Select(p => p.ClassName).ToArray();
                    schematic.UnlocksScannerResourcePairs = string.IsNullOrEmpty(munlock.mResourcePairsToAddToScanner) ? Array.Empty<string>() : UnrealEngineClassParser.ParseInputs(munlock.mResourcePairsToAddToScanner).Select(p => p.ClassName).ToArray();
                    break;
                
                case "BP_UnlockGiveItem_C":
                    schematic.ItemsToGive = ReferenceParseUtility.GetReferencesWithAmount(munlock.mItemsToGive);
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
                    schematicDependency.Schematics = ReferenceParseUtility.GetReferences(mschematicdependency.mSchematics);
                    schematicDependency.RequireAllSchematicsToBePurchased = Convert.ToBoolean(mschematicdependency.mRequireAllSchematicsToBePurchased);
                    schematicDependency.SchematicDependencyType = SchematicDependencyType.PurchasedDependency;
                    break;
                
                case "BP_GamePhaseReachedDependency_C":
                    schematicDependency.SchematicDependencyType = SchematicDependencyType.GamePhaseReached;
                    break;
            }

            schematicDependencies.Add(schematicDependency);
        }

        schematic.SchematicDependencies = schematicDependencies.ToArray();
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
            PowerProduction = NumberParseUtility.MapToDouble(class2.mPowerProduction),
            SupplementToPowerRatio = NumberParseUtility.MapToNullableDouble(class2.mSupplementalToPowerRatio),
            SupplementalLoadAmount = NumberParseUtility.MapToNullableDouble(class2.mSupplementalLoadAmount)
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
            HealthGain = NumberParseUtility.MapToNullableDouble(class2.mHealthGain)
        };
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private Fuel[] ParseFuels(MFuel[]? mFuel, Item[] biomassItems)
    {
        if(mFuel is null)
            return Array.Empty<Fuel>();
        
        return mFuel
            .SelectMany(p => ParseFuel(p, biomassItems))
            .ToArray();
    }

    // ReSharper disable once HeapView.ClosureAllocation
    private IEnumerable<Fuel> ParseFuel(MFuel mFuel, IEnumerable<Item> biomassItems)
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

    private Fuel ParseFuel(MFuel mFuel)
    {
        return new()
        {
            FuelClass = ClassNameParseUtility.CleanClassName(mFuel.mFuelClass)!,
            SupplementalResourceClass = ClassNameParseUtility.CleanClassName(mFuel.mSupplementalResourceClass),
            ByProduct = string.IsNullOrEmpty(mFuel.mByproduct)
                ? null
                : ClassNameParseUtility.CleanClassName(mFuel.mByproduct),
            ByProductAmount = string.IsNullOrEmpty(mFuel.mByproductAmount)
                ? null
                : Convert.ToInt32(mFuel.mByproductAmount)
        };
    }
}