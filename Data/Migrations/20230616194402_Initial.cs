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
                name: "Buildings",
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
                    table.PrimaryKey("PK_Buildings", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "CustomizationRecipes",
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
                    table.PrimaryKey("PK_CustomizationRecipes", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Emotes",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    BigImagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotes", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Items",
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
                    table.PrimaryKey("PK_Items", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
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
                    table.PrimaryKey("PK_Recipes", x => x.ClassName);
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
                name: "Schematics",
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
                    table.PrimaryKey("PK_Schematics", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Statues",
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
                    table.PrimaryKey("PK_Statues", x => x.ClassName);
                });

            migrationBuilder.CreateTable(
                name: "Generators",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    PowerProduction = table.Column<decimal>(type: "TEXT", nullable: false),
                    SupplementToPowerRatio = table.Column<decimal>(type: "TEXT", nullable: true),
                    SupplementalLoadAmount = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generators", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Generators_Buildings_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Buildings",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Miners",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    ItemsPerCycle = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtractCycleTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    AllowedResourceForm = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miners", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Miners_Buildings_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Buildings",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    HealthGain = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Consumables_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
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
                        name: "FK_CreatureLoot_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
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
                        name: "FK_CustomizationRecipeIngredient_CustomizationRecipes_CustomizationRecipeClassName",
                        column: x => x.CustomizationRecipeClassName,
                        principalTable: "CustomizationRecipes",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomizationRecipeIngredient_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    EquipmentSlot = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Equipments_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Resources_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    FuelConsumption = table.Column<decimal>(type: "TEXT", nullable: true),
                    InventorySize = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Vehicles_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
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
                    table.PrimaryKey("PK_Weapons", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Weapons_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
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
                        name: "FK_RecipeBuilding_Buildings_BuildingsClassName",
                        column: x => x.BuildingsClassName,
                        principalTable: "Buildings",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeBuilding_Recipes_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipes",
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
                        name: "FK_RecipeIngredient_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipes_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipes",
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
                        name: "FK_RecipeProduct_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeProduct_Recipes_RecipeClassName",
                        column: x => x.RecipeClassName,
                        principalTable: "Recipes",
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
                        name: "FK_ScannableObject_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScannableObject_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicCost_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicCost_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicEmoteUnlock_Emotes_UnlocksEmotesClassName",
                        column: x => x.UnlocksEmotesClassName,
                        principalTable: "Emotes",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicEmoteUnlock_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicItemGive_Items_GivesItemsClassName",
                        column: x => x.GivesItemsClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicItemGive_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicRecipeUnlock_Recipes_UnlocksRecipesClassName",
                        column: x => x.UnlocksRecipesClassName,
                        principalTable: "Recipes",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicRecipeUnlock_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicSchematicDependency_Schematics_SchematicsClassName",
                        column: x => x.SchematicsClassName,
                        principalTable: "Schematics",
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
                        name: "FK_FuelItem_Generators_GeneratorClassName",
                        column: x => x.GeneratorClassName,
                        principalTable: "Generators",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelItem_Items_ByProductClassName",
                        column: x => x.ByProductClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_FuelItem_Items_FuelClassName",
                        column: x => x.FuelClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelItem_Items_SupplementClassName",
                        column: x => x.SupplementClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName");
                });

            migrationBuilder.CreateTable(
                name: "Creatures",
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
                    table.PrimaryKey("PK_Creatures", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Creatures_CreatureLoot_LootID",
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
                        name: "FK_SchematicScannerResourcePairUnlock_Resources_UnlocksScannerResourcePairsClassName",
                        column: x => x.UnlocksScannerResourcePairsClassName,
                        principalTable: "Resources",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourcePairUnlock_Schematics_Schematic1ClassName",
                        column: x => x.Schematic1ClassName,
                        principalTable: "Schematics",
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
                        name: "FK_SchematicScannerResourceUnlock_Resources_UnlocksScannerResourcesClassName",
                        column: x => x.UnlocksScannerResourcesClassName,
                        principalTable: "Resources",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchematicScannerResourceUnlock_Schematics_SchematicClassName",
                        column: x => x.SchematicClassName,
                        principalTable: "Schematics",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ammunitions",
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
                    table.PrimaryKey("PK_Ammunitions", x => x.ClassName);
                    table.ForeignKey(
                        name: "FK_Ammunitions_Items_ClassName",
                        column: x => x.ClassName,
                        principalTable: "Items",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ammunitions_Weapons_WeaponClassName",
                        column: x => x.WeaponClassName,
                        principalTable: "Weapons",
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
                        name: "FK_ScanningActor_Buildings_BuildingClassName",
                        column: x => x.BuildingClassName,
                        principalTable: "Buildings",
                        principalColumn: "ClassName");
                    table.ForeignKey(
                        name: "FK_ScanningActor_Items_ItemClassName",
                        column: x => x.ItemClassName,
                        principalTable: "Items",
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
                        name: "FK_CreatureVariant_Creatures_CreatureClassName",
                        column: x => x.CreatureClassName,
                        principalTable: "Creatures",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreatureVariant_Creatures_VariantsClassName",
                        column: x => x.VariantsClassName,
                        principalTable: "Creatures",
                        principalColumn: "ClassName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ammunitions_WeaponClassName",
                table: "Ammunitions",
                column: "WeaponClassName");

            migrationBuilder.CreateIndex(
                name: "IX_CreatureLoot_ItemClassName",
                table: "CreatureLoot",
                column: "ItemClassName");

            migrationBuilder.CreateIndex(
                name: "IX_Creatures_LootID",
                table: "Creatures",
                column: "LootID");

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
                name: "Ammunitions");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "CreatureVariant");

            migrationBuilder.DropTable(
                name: "CustomizationRecipeIngredient");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "FuelItem");

            migrationBuilder.DropTable(
                name: "Miners");

            migrationBuilder.DropTable(
                name: "Plants");

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
                name: "Statues");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Creatures");

            migrationBuilder.DropTable(
                name: "CustomizationRecipes");

            migrationBuilder.DropTable(
                name: "Generators");

            migrationBuilder.DropTable(
                name: "ScannableObject");

            migrationBuilder.DropTable(
                name: "Emotes");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "SchematicDependency");

            migrationBuilder.DropTable(
                name: "CreatureLoot");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Schematics");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
