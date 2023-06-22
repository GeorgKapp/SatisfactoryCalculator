using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    ManufactoringSpeed = table.Column<decimal>(type: "TEXT", nullable: true),
                    PowerConsumption = table.Column<decimal>(type: "TEXT", nullable: true),
                    PowerConsumptionExponent = table.Column<decimal>(type: "TEXT", nullable: true),
                    PowerConsumptionRange = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "CustomizationRecipe",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ManualManufacturingMultiplier = table.Column<decimal>(type: "TEXT", nullable: false),
                    ManufactoringDuration = table.Column<decimal>(type: "TEXT", nullable: false),
                    ManufacturingMenuPriority = table.Column<decimal>(type: "TEXT", nullable: true),
                    ConstructedByBuildGun = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConstructedInWorkshop = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConstructedInWorkbench = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSwatchRecipe = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPatternRemover = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizationRecipe", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Emote",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emote", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    Form = table.Column<string>(type: "TEXT", nullable: true),
                    StackSize = table.Column<string>(type: "TEXT", nullable: false),
                    EnergyValue = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsRadioActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    RadioActiveDecay = table.Column<decimal>(type: "TEXT", nullable: true),
                    SinkPoints = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    ManualManufacturingMultiplier = table.Column<decimal>(type: "TEXT", nullable: false),
                    ManufactoringDuration = table.Column<decimal>(type: "TEXT", nullable: false),
                    ManufacturingMenuPriority = table.Column<decimal>(type: "TEXT", nullable: true),
                    ConstructedByBuildGun = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConstructedInWorkshop = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConstructedInWorkbench = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAlternate = table.Column<bool>(type: "INTEGER", nullable: false),
                    VariablePowerConsumptionRange = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Schematic",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SchematicType = table.Column<string>(type: "TEXT", nullable: false),
                    RelevantEvent = table.Column<string>(type: "TEXT", nullable: true),
                    MenuPriority = table.Column<decimal>(type: "TEXT", nullable: true),
                    TechTier = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeToComplete = table.Column<decimal>(type: "TEXT", nullable: true),
                    UnlocksArmEquipmentSlot = table.Column<int>(type: "INTEGER", nullable: true),
                    UnlocksInventoryEquipmentSlot = table.Column<int>(type: "INTEGER", nullable: true),
                    HiddenUntilDependenciesMet = table.Column<bool>(type: "INTEGER", nullable: false),
                    DependenciesBlocksSchematicAccess = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnlocksMap = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schematic", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "SchematicDependency",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequireAllSchematicsToBePurchased = table.Column<bool>(type: "INTEGER", nullable: false),
                    SchematicDependencyType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicDependency", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Statue",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statue", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Generator",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    PowerProduction = table.Column<decimal>(type: "TEXT", nullable: false),
                    SupplementToPowerRatio = table.Column<decimal>(type: "TEXT", nullable: true),
                    SupplementalLoadAmount = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generator", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Generator_Building_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Miner",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    ItemsPerCycle = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtractCycleTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    AllowedResourceForm = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miner", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Miner_Building_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumable",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    HealthGain = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumable", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Consumable_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreatureLoot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureLoot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CreatureLoot_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomizationRecipeIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomizationRecipeClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizationRecipeIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomizationRecipeIngredient_CustomizationRecipe_CustomizationRecipeClassName",
                        column: x => x.CustomizationRecipeClassName,
                        principalTable: "CustomizationRecipe",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomizationRecipeIngredient_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    EquipmentSlot = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Equipment_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Resource_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    FuelConsumption = table.Column<decimal>(type: "TEXT", nullable: true),
                    InventorySize = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Vehicle_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    DamageMultiplier = table.Column<decimal>(type: "TEXT", nullable: true),
                    ReloadTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    AutoReloadDelay = table.Column<decimal>(type: "TEXT", nullable: true),
                    EquipmentSlot = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Weapon_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeBuilding",
                columns: table => new
                {
                    BuildingsClassName = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeBuilding", x => new { x.BuildingsClassName, x.RecipeClassName });
                    table.ForeignKey(
                        name: "FK_RecipeBuilding_Building_BuildingsClassName",
                        column: x => x.BuildingsClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeBuilding_Recipe_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipe",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    RecipeClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipe_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipe",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeProduct",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    RecipeClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeProduct", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeProduct_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeProduct_Recipe_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipe",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScannableObject",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannableObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScannableObject_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScannableObject_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicCost",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicCost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchematicCost_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicCost_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicEmoteUnlock",
                columns: table => new
                {
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false),
                    UnlocksEmotesClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicEmoteUnlock", x => new { x.SchematicClassName, x.UnlocksEmotesClassName });
                    table.ForeignKey(
                        name: "FK_SchematicEmoteUnlock_Emote_UnlocksEmotesClassName",
                        column: x => x.UnlocksEmotesClassName,
                        principalTable: "Emote",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicEmoteUnlock_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicItemGive",
                columns: table => new
                {
                    GivesItemsClassName = table.Column<string>(type: "TEXT", nullable: false),
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicItemGive", x => new { x.GivesItemsClassName, x.SchematicClassName });
                    table.ForeignKey(
                        name: "FK_SchematicItemGive_Item_GivesItemsClassName",
                        column: x => x.GivesItemsClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicItemGive_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicRecipeUnlock",
                columns: table => new
                {
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false),
                    UnlocksRecipesClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicRecipeUnlock", x => new { x.SchematicClassName, x.UnlocksRecipesClassName });
                    table.ForeignKey(
                        name: "FK_SchematicRecipeUnlock_Recipe_UnlocksRecipesClassName",
                        column: x => x.UnlocksRecipesClassName,
                        principalTable: "Recipe",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicRecipeUnlock_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicSchematicDependency",
                columns: table => new
                {
                    DependenciesID = table.Column<int>(type: "INTEGER", nullable: false),
                    SchematicsClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicSchematicDependency", x => new { x.DependenciesID, x.SchematicsClassName });
                    table.ForeignKey(
                        name: "FK_SchematicSchematicDependency_SchematicDependency_DependenciesID",
                        column: x => x.DependenciesID,
                        principalTable: "SchematicDependency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicSchematicDependency_Schematic_SchematicsClassName",
                        column: x => x.SchematicsClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuelClassName = table.Column<string>(type: "TEXT", nullable: false),
                    SupplementClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ByProductClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ByProductAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                    GeneratorClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FuelItem_Generator_GeneratorClassName",
                        column: x => x.GeneratorClassName,
                        principalTable: "Generator",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelItem_Item_ByProductClassName",
                        column: x => x.ByProductClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_FuelItem_Item_FuelClassName",
                        column: x => x.FuelClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelItem_Item_SupplementClassName",
                        column: x => x.SupplementClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName");
                });

            migrationBuilder.CreateTable(
                name: "Creature",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    HitPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Damage = table.Column<string>(type: "TEXT", nullable: false),
                    Behaviour = table.Column<string>(type: "TEXT", nullable: false),
                    LootID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creature", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Creature_CreatureLoot_LootID",
                        column: x => x.LootID,
                        principalTable: "CreatureLoot",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SchematicScannerResourcePairUnlock",
                columns: table => new
                {
                    Schematic1ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    UnlocksScannerResourcePairsClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicScannerResourcePairUnlock", x => new { x.Schematic1ClassName, x.UnlocksScannerResourcePairsClassName });
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourcePairUnlock_Resource_UnlocksScannerResourcePairsClassName",
                        column: x => x.UnlocksScannerResourcePairsClassName,
                        principalTable: "Resource",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourcePairUnlock_Schematic_Schematic1ClassName",
                        column: x => x.Schematic1ClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchematicScannerResourceUnlock",
                columns: table => new
                {
                    SchematicClassName = table.Column<string>(type: "TEXT", nullable: false),
                    UnlocksScannerResourcesClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchematicScannerResourceUnlock", x => new { x.SchematicClassName, x.UnlocksScannerResourcesClassName });
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourceUnlock_Resource_UnlocksScannerResourcesClassName",
                        column: x => x.UnlocksScannerResourcesClassName,
                        principalTable: "Resource",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourceUnlock_Schematic_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematic",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ammunition",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    MaxAmmoEffectiveRange = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReloadTimeMultiplier = table.Column<decimal>(type: "TEXT", nullable: false),
                    FireRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeaponDamageMultiplier = table.Column<decimal>(type: "TEXT", nullable: false),
                    WeaponClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammunition", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Ammunition_Item_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ammunition_Weapon_WeaponClassName",
                        column: x => x.WeaponClassName,
                        principalTable: "Weapon",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScanningActor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemClassName = table.Column<string>(type: "TEXT", nullable: true),
                    BuildingClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ScannableObjectID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanningActor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScanningActor_Building_BuildingClassName",
                        column: x => x.BuildingClassName,
                        principalTable: "Building",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_ScanningActor_Item_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Item",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_ScanningActor_ScannableObject_ScannableObjectID",
                        column: x => x.ScannableObjectID,
                        principalTable: "ScannableObject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreatureVariant",
                columns: table => new
                {
                    CreatureClassName = table.Column<string>(type: "TEXT", nullable: false),
                    VariantsClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatureVariant", x => new { x.CreatureClassName, x.VariantsClassName });
                    table.ForeignKey(
                        name: "FK_CreatureVariant_Creature_CreatureClassName",
                        column: x => x.CreatureClassName,
                        principalTable: "Creature",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreatureVariant_Creature_VariantsClassName",
                        column: x => x.VariantsClassName,
                        principalTable: "Creature",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ammunition_WeaponClassName",
                table: "Ammunition",
                column: "WeaponClassName");

            migrationBuilder.CreateIndex(
                name: "IX_Creature_LootID",
                table: "Creature",
                column: "LootID");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureLoot_ItemClassName",
                table: "CreatureLoot",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureVariant_VariantsClassName",
                table: "CreatureVariant",
                column: "VariantsClassName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationRecipeIngredient_CustomizationRecipeClassName",
                table: "CustomizationRecipeIngredient",
                column: "CustomizationRecipeClassName");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationRecipeIngredient_ItemClassName",
                table: "CustomizationRecipeIngredient",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FuelItem_ByProductClassName",
                table: "FuelItem",
                column: "ByProductClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FuelItem_FuelClassName",
                table: "FuelItem",
                column: "FuelClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FuelItem_GeneratorClassName",
                table: "FuelItem",
                column: "GeneratorClassName");

            migrationBuilder.CreateIndex(
                name: "IX_FuelItem_SupplementClassName",
                table: "FuelItem",
                column: "SupplementClassName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeBuilding_RecipeClassName",
                table: "RecipeBuilding",
                column: "RecipeClassName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_ItemClassName",
                table: "RecipeIngredient",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeClassName",
                table: "RecipeIngredient",
                column: "RecipeClassName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_ItemClassName",
                table: "RecipeProduct",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_RecipeClassName",
                table: "RecipeProduct",
                column: "RecipeClassName");

            migrationBuilder.CreateIndex(
                name: "IX_ScannableObject_ItemClassName",
                table: "ScannableObject",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_ScannableObject_SchematicClassName",
                table: "ScannableObject",
                column: "SchematicClassName");

            migrationBuilder.CreateIndex(
                name: "IX_ScanningActor_BuildingClassName",
                table: "ScanningActor",
                column: "BuildingClassName");

            migrationBuilder.CreateIndex(
                name: "IX_ScanningActor_ItemClassName",
                table: "ScanningActor",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_ScanningActor_ScannableObjectID",
                table: "ScanningActor",
                column: "ScannableObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicCost_ItemClassName",
                table: "SchematicCost",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicCost_SchematicClassName",
                table: "SchematicCost",
                column: "SchematicClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicEmoteUnlock_UnlocksEmotesClassName",
                table: "SchematicEmoteUnlock",
                column: "UnlocksEmotesClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicItemGive_SchematicClassName",
                table: "SchematicItemGive",
                column: "SchematicClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicRecipeUnlock_UnlocksRecipesClassName",
                table: "SchematicRecipeUnlock",
                column: "UnlocksRecipesClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicScannerResourcePairUnlock_UnlocksScannerResourcePairsClassName",
                table: "SchematicScannerResourcePairUnlock",
                column: "UnlocksScannerResourcePairsClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicScannerResourceUnlock_UnlocksScannerResourcesClassName",
                table: "SchematicScannerResourceUnlock",
                column: "UnlocksScannerResourcesClassName");

            migrationBuilder.CreateIndex(
                name: "IX_SchematicSchematicDependency_SchematicsClassName",
                table: "SchematicSchematicDependency",
                column: "SchematicsClassName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ammunition");

            migrationBuilder.DropTable(
                name: "Consumable");

            migrationBuilder.DropTable(
                name: "CreatureVariant");

            migrationBuilder.DropTable(
                name: "CustomizationRecipeIngredient");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "FuelItem");

            migrationBuilder.DropTable(
                name: "Miner");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "RecipeBuilding");

            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "RecipeProduct");

            migrationBuilder.DropTable(
                name: "ScanningActor");

            migrationBuilder.DropTable(
                name: "SchematicCost");

            migrationBuilder.DropTable(
                name: "SchematicEmoteUnlock");

            migrationBuilder.DropTable(
                name: "SchematicItemGive");

            migrationBuilder.DropTable(
                name: "SchematicRecipeUnlock");

            migrationBuilder.DropTable(
                name: "SchematicScannerResourcePairUnlock");

            migrationBuilder.DropTable(
                name: "SchematicScannerResourceUnlock");

            migrationBuilder.DropTable(
                name: "SchematicSchematicDependency");

            migrationBuilder.DropTable(
                name: "Statue");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "Creature");

            migrationBuilder.DropTable(
                name: "CustomizationRecipe");

            migrationBuilder.DropTable(
                name: "Generator");

            migrationBuilder.DropTable(
                name: "ScannableObject");

            migrationBuilder.DropTable(
                name: "Emote");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "SchematicDependency");

            migrationBuilder.DropTable(
                name: "CreatureLoot");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Schematic");

            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
