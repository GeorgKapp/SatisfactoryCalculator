using Building = SatisfactoryCalculator.DocsServices.Models.DataModels.Building;
using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
using Recipe = SatisfactoryCalculator.DocsServices.Models.DataModels.Recipe;

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private bool ExcludeBuilding(string className)
    {
        return new string[5] { "Desc_Wall_Window_8x4_03_Steel_C", "Build_PillarTop_C", "Desc_QuarterPipeMiddle_Ficsit_4x1_C", "Desc_QuarterPipeMiddle_Ficsit_4x2_C", "Desc_QuarterPipeMiddle_Ficsit_4x4_C" }.Contains(className);
    }

    private Building[] ParseBuildings(Class2[] classes2, Dictionary<string, Class2> classesDictionary)
    {
        return classes2
            .Where(p => !ExcludeBuilding(p.ClassName))
            .Select(p => ParseBuilding(p, classesDictionary))
            .ToArray();
    }

    private Building ParseBuilding(Class2 class2, Dictionary<string, Class2> classesDictionary)
    {
        string smallIconPath = class2.mSmallIcon;
        string bigIconPath = class2.mPersistentBigIcon;

        string correctedClassName = ClassNameParseUtility.CorrectClassNameForImageLookup(class2.ClassName);
        
        if (correctedClassName == class2.ClassName)
        {
            correctedClassName = ClassNameParseUtility.ConvertClassNameToDescClassName(correctedClassName);
        }
        if (classesDictionary.ContainsKey(correctedClassName))
        {
            if (string.IsNullOrEmpty(smallIconPath))
            {
                smallIconPath = IconPathParseUtility.ReplaceSmallIconPathFromClass(classesDictionary[correctedClassName]);
            }
            if (string.IsNullOrEmpty(bigIconPath))
            {
                bigIconPath = IconPathParseUtility.ReplaceBigIconPathFromClass(classesDictionary[correctedClassName]);
            }
        }
        else
        {
            smallIconPath = IconPathParseUtility.ReplaceSmallIconPathFromClass(class2);
            bigIconPath = IconPathParseUtility.ReplaceBigIconPathFromClass(class2);
        }

        return new Building
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            DisplayName = class2.mDisplayName,
            Description = class2.mDescription,
            SmallIconPath = IconPathParseUtility.ConvertIconPathToUEPath(smallIconPath),
            BigIconPath = IconPathParseUtility.ConvertIconPathToUEPath(bigIconPath),
            PowerConsumption = NumberParseUtility.MapToNullableDouble(class2.mPowerConsumption),
            PowerConsumptionExponent = NumberParseUtility.MapToNullableDouble(class2.mPowerConsumptionExponent),
            ManuFacturingSpeed = NumberParseUtility.MapToNullableDouble(class2.mManufacturingSpeed),
            PowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerConsumption(NumberParseUtility.MapToNullableDouble(class2.mEstimatedMininumPowerConsumption), NumberParseUtility.MapToNullableDouble(class2.mEstimatedMaximumPowerConsumption)),
            Form = StringToEnumParseUtility.ParseFormStringToNNullableEnum(class2.mForm)
        };
    }

    private Item[] ParseItems(Class2[] classes2)
    {
        return classes2.Select(p => ParseItem(p)).ToArray();
    }

    private Item ParseItem(Class2 class2)
    {
        return new Item
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            DisplayName = class2.mDisplayName,
            Description = class2.mDescription,
            Form = StringToEnumParseUtility.ParseFormStringToEnum(class2.mForm),
            StackSize = StringToEnumParseUtility.ParseStackSizeStringToEnum(class2.mStackSize),
            EnergyValue = NumberParseUtility.MapToDouble(class2.mEnergyValue),
            IsRadioActive = string.IsNullOrEmpty(class2.mIsRadioActive) ? null : Convert.ToBoolean(class2.mIsRadioActive),
            RadioActiveDecay = NumberParseUtility.MapToDouble(class2.mRadioactiveDecay),
            SmallIconPath = IconPathParseUtility.ConvertIconPathToUEPath(class2.mSmallIcon),
            BigIconPath = IconPathParseUtility.ConvertIconPathToUEPath(class2.mPersistentBigIcon),
            SinkPoints = string.IsNullOrEmpty(class2.mResourceSinkPoints) ? null : Convert.ToInt32(class2.mResourceSinkPoints)
        };
    }

    private Resource[] ParseResources(Class2[] classes2)
    {
        return classes2.Select(p => ParseResource(p)).ToArray();
    }

    private Resource ParseResource(Class2 class2)
    {
        return new Resource
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName)
        };
    }

    private Equipment[] ParseEquipments(Class2[] classes2)
    {
        return classes2.Select(p => ParseEquipment(p)).ToArray();
    }

    private Equipment ParseEquipment(Class2 class2)
    {
        return new Equipment
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            EquipmentSlot = StringToEnumParseUtility.ParseEquipmentSlotStringToEnum(class2.mEquipmentSlot)
        };
    }

    private Vehicle[] ParseVehicles(Class2[] classes2)
    {
        return classes2.Select(p => ParseVehicle(p)).ToArray();
    }

    private Vehicle ParseVehicle(Class2 class2)
    {
        return new Vehicle
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            FuelConsumption = string.IsNullOrEmpty(class2.mFuelConsumption) ? null : NumberParseUtility.MapToDouble(class2.mFuelConsumption),
            InventorySize = string.IsNullOrEmpty(class2.mInventorySize) ? null : Convert.ToInt32(class2.mInventorySize)
        };
    }

    private Weapon[] ParseWeapons(Class2[] classes2)
    {
        return classes2.Select(p => ParseWeapon(p)).ToArray();
    }

    private Weapon ParseWeapon(Class2 class2)
    {
        return new Weapon
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            AutoReloadDelay = NumberParseUtility.MapToNullableDouble(class2.mAutoReloadDelay),
            ReloadTime = NumberParseUtility.MapToNullableDouble(class2.mReloadTime),
            DamageMultiplier = NumberParseUtility.MapToNullableDouble(class2.mWeaponDamageMultiplier),
            EquipmentSlot = StringToEnumParseUtility.ParseEquipmentSlotStringToEnum(class2.mEquipmentSlot)
        };
    }

    private Ammunition[] ParseAmmunitions(Class2[] classes2)
    {
        return classes2.Select(p => ParseAmmunition(p)).ToArray();
    }

    private Ammunition ParseAmmunition(Class2 class2)
    {
        return new Ammunition
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            FireRate = NumberParseUtility.MapToDouble(class2.mFireRate),
            MaxAmmoEffectiveRange = NumberParseUtility.MapToDouble(class2.mMaxAmmoEffectiveRange),
            ReloadTimeMultiplier = NumberParseUtility.MapToNullableDouble(class2.mReloadTimeModifier),
            WeaponDamageMultiplier = NumberParseUtility.MapToDouble(class2.mWeaponDamageMultiplier)
        };
    }

    private bool ExcludeRecipe(string className)
    {
        return new[] { "Recipe_Wall_Window_8x4_03_Steel_C", "Recipe_CandyCaneBasher_C", "Recipe_PillarTop_C" }.Contains(className);
    }

    private Recipe[] ParseRecipes(Class2[] classes2)
    {
        return classes2
            .Where(p => !ExcludeRecipe(p.ClassName))
            .Select(p => ParseRecipe(p))
            .Where(p => p is not null).Cast<Recipe>()
            .ToArray();
    }

    private Recipe? ParseRecipe(Class2 class2)
    {
        var array = ReferenceParseUtility.MapToReferenceArray(class2.mProducedIn);
        if (array.Contains("Converter_C"))
            return null;

        var constructedByBuildGun = array.Contains("BuildGun_C") || array.Contains("FGBuildGun");
        var constructedInWorkshop = array.Contains("WorkshopComponent_C");
        var constructedInWorkbench = array.Contains("WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C");
        
        array = array.Except("BuildGun_C", "FGBuildGun", "WorkshopComponent_C", "WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C").ToArray();
        
        var recipe = new Recipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            DisplayName = class2.mDisplayName,
            VariablePowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerVariableConsumption(NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionConstant), NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionFactor)),
            ManufacturingMenuPriority = NumberParseUtility.MapToNullableDouble(class2.mManufacturingMenuPriority),
            ManufactoringDuration = NumberParseUtility.MapToDouble(class2.mManufactoringDuration),
            ManualManufacturingMultiplier = NumberParseUtility.MapToDouble(class2.mManualManufacturingMultiplier),
            Ingredients = ReferenceParseUtility.MapToReferenceWithAmountArray(class2.mIngredients),
            Products = ReferenceParseUtility.MapToReferenceWithAmountArray(class2.mProduct),
            Buildings = array.ToArray(),
            ConstructedByBuildGun = constructedByBuildGun,
            ConstructedInWorkshop = constructedInWorkshop,
            ConstructedInWorkbench = constructedInWorkbench,
            IsAlternate = class2.mDisplayName.StartsWith("Alternate")
        };

        double factor = 60.0 / recipe.ManufactoringDuration;
        foreach (Reference ingredient in recipe.Ingredients)
        {
            ingredient.Amount = ingredient.Amount;
            ingredient.AmountPerMinute = ingredient.Amount * factor;
        }

        foreach (Reference product in recipe.Products)
        {
            product.Amount = product.Amount;
            product.AmountPerMinute = product.Amount * factor;
        }

        return recipe;
    }

    private CustomizationRecipe[] ParseCustomizationRecipes(Class2[] classes2, Dictionary<string, Class2> classesDictionary)
    {
        return classes2
            .Select(p => ParseCustomizationRecipe(p, classesDictionary))
            .ToArray();
    }

    private CustomizationRecipe ParseCustomizationRecipe(Class2 class2, Dictionary<string, Class2> classesDictionary)
    {
        var array = ReferenceParseUtility.MapToReferenceArray(class2.mProducedIn);
        
        var constructedByBuildGun = array.Contains("BuildGun_C");
        var constructedInWorkshop = array.Contains("WorkshopComponent_C");
        var constructedInWorkbench = array.Contains("WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C");
        
        array = array.Except("BuildGun_C", "WorkshopComponent_C", "WorkBenchComponent_C", "FGBuildableAutomatedWorkBench", "AutomatedWorkBench_C").ToArray();
       
        CustomizationRecipe customizationRecipe = new CustomizationRecipe
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            DisplayName = ((class2.mDisplayName == "N/A") ? null : class2.mDisplayName),
            VariablePowerConsumptionRange = PowerConsumptionRangeConverterUtility.ConverToPowerVariableConsumption(NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionConstant), NumberParseUtility.MapToDouble(class2.mVariablePowerConsumptionFactor)),
            ManufacturingMenuPriority = NumberParseUtility.MapToNullableDouble(class2.mManufacturingMenuPriority),
            ManufactoringDuration = NumberParseUtility.MapToDouble(class2.mManufactoringDuration),
            ManualManufacturingMultiplier = NumberParseUtility.MapToDouble(class2.mManualManufacturingMultiplier),
            Ingredients = ReferenceParseUtility.MapToReferenceWithAmountArray(class2.mIngredients),
            Products = ReferenceParseUtility.MapToReferenceWithAmountArray(class2.mProduct),
            Buildings = array.ToArray(),
            ConstructedByBuildGun = constructedByBuildGun,
            ConstructedInWorkshop = constructedInWorkshop,
            ConstructedInWorkbench = constructedInWorkbench,
            IsSwatchRecipe = class2.ClassName.Contains("_Swatch"),
            IsPatternRemover = class2.ClassName.Contains("_Pattern_Remover")
        };

        double factor = 60.0 / customizationRecipe.ManufactoringDuration;

        foreach (Reference ingredient in customizationRecipe.Ingredients)
        {
            var form = StringToEnumParseUtility.ParseFormStringToEnum(classesDictionary[ingredient.ClassName].mForm);
            ingredient.Amount = form != Form.Solid ? ingredient.Amount / 1000.0 : ingredient.Amount;
            ingredient.AmountPerMinute = ingredient.Amount * factor;
        }

        foreach (Reference product in customizationRecipe.Products)
        {
            var form = StringToEnumParseUtility.ParseFormStringToEnum(classesDictionary[product.ClassName].mForm);
            product.Amount = form != Form.Solid ? product.Amount / 1000.0 : product.Amount;
            product.AmountPerMinute = product.Amount * factor;
        }

        return customizationRecipe;
    }

    private Miner[] ParseMiners(Class2[] classes2)
    {
        return classes2.Select(p => ParseMiner(p)).ToArray();
    }

    private Miner ParseMiner(Class2 class2)
    {
        return new Miner
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            ItemsPerCycle = Convert.ToInt32(class2.mItemsPerCycle),
            ExtractCycleTime = NumberParseUtility.MapToNullableDouble(class2.mExtractCycleTime),
            AllowedResourceForms = class2.ClassName == "Build_GeneratorGeoThermal_C" ? new Form[1] { Form.Gas } : MultiFormParseUtility.MapToForms(class2.mAllowedResourceForms)
        };
    }

    private Schematic[] ParseSchematics(Class2[] classes2)
    {
        return classes2.Select(p => ParseSchematic(p)).ToArray();
    }

    private Schematic ParseSchematic(Class2 class2)
    {
        Schematic schematic = new Schematic
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            DisplayName = class2.mDisplayName,
            MenuPriority = NumberParseUtility.MapToNullableDouble(class2.mMenuPriority),
            TimeToComplete = NumberParseUtility.MapToNullableDouble(class2.mTimeToComplete),
            TechTier = Convert.ToInt32(class2.mTechTier),
            Type = StringToEnumParseUtility.ParseSchematicTypeStringToEnum(class2.mType),
            SmallIconPath = string.IsNullOrEmpty(class2.mSmallSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSmallSchematicIcon),
            BigIconPath = string.IsNullOrEmpty(class2.mSchematicIcon) ? null : IconPathParseUtility.ReadIconPathFromSchematicIcon(class2.mSchematicIcon),
            RelevantEvents = string.IsNullOrEmpty(class2.mRelevantEvents) ? Array.Empty<string>() : ReferenceParseUtility.MapToReferenceArray(class2.mRelevantEvents),
            Cost = string.IsNullOrEmpty(class2.mRelevantEvents) ? Array.Empty<Reference>() : ReferenceParseUtility.MapToReferenceWithAmountArray(class2.mCost),
            HiddenUntilDependenciesMet = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess),
            DependenciesBlocksSchematicAccess = Convert.ToBoolean(class2.mDependenciesBlocksSchematicAccess),
            UnlocksRecipes = Array.Empty<string>(),
            UnlocksScannerResources = Array.Empty<string>(),
            UnlocksScannerResourcePairs = Array.Empty<string>(),
            Emotes = Array.Empty<string>(),
            ItemsToGive = Array.Empty<Reference>(),
            SchematicDependencies = Array.Empty<SchematicDependency>()
        };

        Munlock[] mUnlocks = class2.mUnlocks;
        foreach (Munlock munlock in mUnlocks)
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
                    schematic.UnlocksRecipes = ReferenceParseUtility.MapToReferenceArray(munlock.mRecipes);
                    break;
                case "BP_UnlockScannableObject_C":
                    schematic.UnlocksScannerObject = ScannableObjectParseUtility.MapToScannableObject(munlock.mScannableObjects);
                    break;
                case "BP_UnlockEmote_C":
                    schematic.Emotes = ReferenceParseUtility.MapToReferenceArray(munlock.mEmotes);
                    break;
                case "BP_UnlockScannableResource_C":
                    schematic.UnlocksScannerResources = string.IsNullOrEmpty(munlock.mResourcesToAddToScanner) ? Array.Empty<string>() : ReferenceParseUtility.MapToReferenceArray(munlock.mResourcesToAddToScanner);
                    schematic.UnlocksScannerResourcePairs = string.IsNullOrEmpty(munlock.mResourcePairsToAddToScanner) ? Array.Empty<string>() : ReferenceParseUtility.MapToReferenceArrayIgnoreIdentifiers(munlock.mResourcePairsToAddToScanner);
                    break;
                case "BP_UnlockGiveItem_C":
                    schematic.ItemsToGive = ReferenceParseUtility.MapToReferenceWithAmountArray(munlock.mItemsToGive);
                    break;
            }
        }

        List<SchematicDependency> schematicDependencies = new();
        foreach (var mschematicdependency in class2.mSchematicDependencies)
        {
            SchematicDependency schematicDependency = new();
            if (mschematicdependency.Class == "BP_SchematicPurchasedDependency_C")
            {
                schematicDependency.Schematics = ReferenceParseUtility.MapToReferenceArray(mschematicdependency.mSchematics);
                schematicDependency.RequireAllSchematicsToBePurchased = Convert.ToBoolean(mschematicdependency.mRequireAllSchematicsToBePurchased);
                schematicDependency.SchematicDependencyType = SchematicDependencyType.PurchasedDependency;
            }
            else
            {
                if (mschematicdependency.Class == "BP_GamePhaseReachedDependency_C")
                    schematicDependency.SchematicDependencyType = SchematicDependencyType.GamePhaseReached;
            }

            schematicDependencies.Add(schematicDependency);
        }

        schematic.SchematicDependencies = schematicDependencies.ToArray();
        return schematic;
    }

    private Generator[] ParseGenerators(Class2[] classes2)
    {
        return classes2.Select((Class2 p) => ParseGenerator(p)).ToArray();
    }

    private Generator ParseGenerator(Class2 class2)
    {
        return new Generator
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            Fuel = class2.mFuel is null ? Array.Empty<Fuel>() : ParseFuel(class2.mFuel),
            PowerProduction = NumberParseUtility.MapToNullableDouble(class2.mPowerProduction),
            PowerProductionExponent = NumberParseUtility.MapToNullableDouble(class2.mPowerProductionExponent),
            SupplementToPowerRatio = NumberParseUtility.MapToNullableDouble(class2.mSupplementalToPowerRatio),
            SupplementalLoadAmount = NumberParseUtility.MapToNullableDouble(class2.mSupplementalLoadAmount)
        };
    }

    private Consumable[] ParseConsumables(Class2[] classes2)
    {
        return classes2.Select(p => ParseConsumable(p)).ToArray();
    }

    private Consumable ParseConsumable(Class2 class2)
    {
        return new Consumable
        {
            ClassName = ClassNameParseUtility.CleanClassName(class2.ClassName),
            HealthGain = NumberParseUtility.MapToNullableDouble(class2.mHealthGain)
        };
    }

    private Fuel[] ParseFuel(Mfuel[] mFuel)
    {
        return mFuel.Select(p => new Fuel
        {
            FuelClass = ClassNameParseUtility.CleanClassName(p.mFuelClass),
            SupplementalResourceClass = ClassNameParseUtility.CleanClassName(p.mSupplementalResourceClass),
            ByProduct = string.IsNullOrEmpty(p.mByproduct) ? null : ClassNameParseUtility.CleanClassName(p.mByproduct),
            ByProductAmount = string.IsNullOrEmpty(p.mByproductAmount) ? null : Convert.ToInt32(p.mByproductAmount)
        }).ToArray();
    }
}
