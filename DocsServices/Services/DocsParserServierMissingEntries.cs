using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;
// ReSharper disable HeapView.ObjectAllocation

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private readonly Item _fiscitCoupon = new()
    {
        ClassName = "ResourceSinkCoupon_C",
        DisplayName = "FICSIT Coupon",
        Description = "A special FICSIT bonus program Coupon, obtained through the AWESOME Sink. Can be redeemed in the AWESOME Shop for bonus milestones and rewards.",
        SinkPoints = null,
        SmallIconPath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_256.png"
    };

    private readonly Item _coffeeCup = new()
    {
        ClassName = "Cup_C",
        DisplayName = "FICSIT™ Coffee Cup",
        Description = "The Cup is a solely cosmetic piece of hand equipment. Similar to Beryl Nuts, it is small and does not cause too much obstruction when the player decides to take a screenshot.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_256.png"
    };

    private readonly Item _goldenCoffeeCup = new()
    {
        ClassName = "CupGold_C",
        DisplayName = "Employee of the Planet' Cup",
        Description = "A shiny little cup, allowing extra hard working Pioneers to enjoy a cup of well deserved coffee while they wait for what the future will bring.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_256.png"
    };

    private readonly Item _boomBox = new()
    {
        ClassName = "BoomBox_C",
        DisplayName = "Boom Box",
        Description = "Boost your efficiency now, with the completely unnecessary FICSIT Boombox!\r\n\r\n*Tapes are sold separately.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_256.png"
    };
    
    private readonly Item _harddrive = new()
    {
        ClassName = "HardDrive_C",
        DisplayName = "Hard Drive",
        Description = "A hard drive with FICSIT data. Analyze it in the MAM to salvage its contents.",
        SmallIconPath = "\\Game\\FactoryGame\\Resource\\Environment\\CrashSites\\UI\\HardDrive_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Resource\\Environment\\CrashSites\\UI\\HardDrive_256.png",
        StackSize = StackSize.Medium
    };

    private readonly Equipment _coffeeCupEquipment = new()
    {
        ClassName = "Cup_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Equipment _goldenCoffeeCupEquipment = new()
    {
        ClassName = "CupGold_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Equipment _boomBoxEquipment = new()
    {
        ClassName = "BoomBox_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Emote[] _emotes = {
        new()
        {
            ClassName = "Clap_C",
            DisplayName = "Clap",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_256.png"
        },
        new()
        {
            ClassName = "BuildGunSpin_C",
            DisplayName = "Twirl It",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_256.png"
        },
        new()
        {
            ClassName = "FacePalm_C",
            DisplayName = "Facepalm",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_256.png"
        },
        new()
        {
            ClassName = "Rock_C",
            DisplayName = "Rock",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock_256.png"
        },
        new()
        {
            ClassName = "Paper_C",
            DisplayName = "Paper",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper_256.png"
        },
        new()
        {
            ClassName = "Scissors_C",
            DisplayName = "Scissors",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors_256.png"
        },
        new()
        {
            ClassName = "Point_C",
            DisplayName = "Point",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_256.png"
        },
        new()
        {
            ClassName = "Wave_C",
            DisplayName = "Wave",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_256.png"
        },
        new()
        {
            ClassName = "Heart_C",
            DisplayName = "Heart",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_256.png"
        },
        new()
        {
            ClassName = "Fingerguns_C",
            DisplayName = "Finger Guns",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_256.png"
        }
    };

    private readonly Statue[] _statues = {
        new()
        {
            ClassName = "Hog_Statue_C",
            DisplayName = "Silver Hog",
            Description = "The statue of the Silver Pouncing Hog. Perfect as a hood ornament.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_256.png"
        },
        new()
        {
            ClassName = "SpaceGiraffeStatue_C",
            DisplayName = "Confusing Creature",
            Description = "A beautiful shiny statue of a weird creature... For really though, what is that thing?",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_256.png"
        },
        new()
        {
            ClassName = "DoggoStatue_C",
            DisplayName = "Lizard Doggo Statue",
            Description = "A statue of the Lizard Doggo.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_256.png"
        },
        new()
        {
            ClassName = "GoldenNut_Statue_C",
            DisplayName = "Golden Nut",
            Description = "The statue of the golden nut.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_256.png"
        },
        new()
        {
            ClassName = "CharacterClap_Statue_C",
            DisplayName = "Pretty Good Pioneering",
            Description = "The statue of the Clapping Character.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharSilver_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\IconDesc_AwardStatue_Char_256.png"
        },
        new()
        {
            ClassName = "CharacterRunStatue_C",
            DisplayName = "Adequate Pioneering",
            Description = "The statue of the Running Character.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_256.png"
        },
        new()
        {
            ClassName = "CharacterSpin_Statue_C",
            DisplayName = "Satisfactory Pioneering",
            Description = "The statue of the Character Spinning the Build Gun.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_256.png"
        }
    };

    private readonly Creature[] _creatures =
    {
        new()
        {
            ClassName = "HogAlpha_C",
            DisplayName = "Alpha Hog",
            Description = "The Alpha Hog is the larger version of Fluffy-tailed Hog that is also considerably more dangerous, dealing up to 3x the damage. Other than its larger body size, it can be distinguished by the bright edges on the back of its carapace, and its tusks (which regular hogs lack). It also emits a shockwave-like particle effect when it prepares to attack.\r\n\r\nThe Alpha Hog usually patrols around Crash Sites, Power Slugs or artifacts.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_HogAlpha_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_HogAlpha_256.png",
            HitPoints = 80,
            Damage = new [] { "Charge Damage: 20", "Bite Damage: 30" },
            Loot = new [] { new CreatureLoot{ ClassName = "HogParts_C", Amount = 3}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Hog"
        },
        new()
        {
            ClassName = "Hog_C",
            DisplayName = "Fluffy-tailed Hog",
            Description = "The Fluffy-tailed Hog (a.k.a 'Hog'), is a hostile melee creature that the Pioneer can encounter while exploring. It attacks by charging like a bull, dealing damage with a knockback effect. It is among the most common creatures found in the game world.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hog_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hog_256.png",
            HitPoints = 20,
            Damage = new [] { "Charge Damage: 10" },
            Loot = new [] { new CreatureLoot{ ClassName = "HogParts_C", Amount = 1}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Hog"
        },
        new()
        {
            ClassName = "SpitterDesert_C",
            DisplayName = "Desert Spitter",
            Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesert_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesert_256.png",
            HitPoints = 30,
            Damage = new [] { "Melee fireball Damage: 1-11" , "Ranged fireball Damage: 1-6"},
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 2}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterAqua_C",
            DisplayName = "Aqua Spitter",
            Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAqua_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAqua_256.png",
            HitPoints = 30,
            Damage = new [] { "Melee fireball Damage: 1-13" , "Ranged fireball Damage: 1-8"},
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 2}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterForest_C",
            DisplayName = "Forest Spitter",
            Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForest_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForest_256.png",
            HitPoints = 30,
            Damage = new []{ "Melee fireball Damage: 1-12" , "Ranged fireball Damage: 1-6"},
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 2}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterRForest_C",
            DisplayName = "Red Forest Spitter",
            Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForest_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForest_256.png",
            HitPoints = 30,
            Damage = new [] { "Melee fireball Damage: 1-13" , "Ranged fireball Damage: 1-8"},
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 2}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterDesertAlpha_C",
            DisplayName = "Desert Alpha Splitter",
            Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesertAlpha_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesertAlpha_256.png",
            HitPoints = 60,
            Damage = new [] { "Melee Fireball Damage: 1-12", "A large fireball (<20) which splits into 5 smaller fireball (<10) mid-air", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 4}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterAquaAlpha_C",
            DisplayName = "Aqua Alpha Splitter",
            Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAquaAlpha_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAquaAlpha_256.png",
            HitPoints = 60,
            Damage = new [] { "Melee Fireball Damage: 1-12", "4 consecutive shots at <18 damage each", "A charged, sniped shot at <36 damage" },
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 4}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterForestAlpha_C",
            DisplayName = "Forest Alpha Splitter",
            Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForestAlpha_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForestAlpha_256.png",
            HitPoints = 60,
            Damage = new [] { "Melee Fireball Damage: 1-16", "4 consecutive shots at <18 damage each", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 4}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "SpitterRForestAlpha_C",
            DisplayName = "Red Forest Alpha Splitter",
            Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForestAlpha_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForestAlpha_256.png",
            HitPoints = 60,
            Damage = new [] { "Melee Fireball Damage: 1-12", "4 consecutive shots at <18 damage each", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
            Loot = new [] { new CreatureLoot{ ClassName = "SpitterParts_C", Amount = 4}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Splitter"
        },
        new()
        {
            ClassName = "Hatcher_C",
            DisplayName = "Flying Crab Hatcher",
            Description = "Flying Crab Hatchers are nest-like objects often found near rivers, cliff edges, resource nodes, Power Slugs and Crash Sites. Approaching a Hatcher will cause it to continuously spawn Flying Crabs at 5-second intervals until it is destroyed by the player.[1] When destroyed, it drops Hatcher Remains.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hatcher_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hatcher_256.png",
            HitPoints = 25,
            Damage = new [] { "Spawns 3 Flying Crabs dealing 10 Damage each" },
            Loot = new [] { new CreatureLoot{ ClassName = "HatcherParts_C", Amount = 1}},
            Behaviour = CreatureBehaviour.Hostile
        },
        new()
        {
            ClassName = "ArachnophobiaStingerChild_C",
            DisplayName = "Baby Stinger (Arachnophia)",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerChild_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerChild_256.png",
            HitPoints = 10,
            Damage = new [] { "Slash Damage: 5" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 1}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "ArachnophobiaStinger_C",
            DisplayName = "Alpha Stinger (Arachnophia)",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStinger_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStinger_256.png",
            HitPoints = 50,
            Damage = new [] { "Slash Damage: 30", "Leap Damage: 50" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 3}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "ArachnophobiaStingerElite_C",
            DisplayName = "Elite Gas Stinger (Arachnophia)",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerElite_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerElite_256.png",
            HitPoints = 100,
            Damage = new [] { "Slash Damage: 30", "Leap Damage: 50", "Gas Damage: 5/sec" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 5}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "StingerChild_C",
            DisplayName = "Baby Stinger",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerChild_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerChild_256.png",
            HitPoints = 10,
            Damage = new [] { "Slash Damage: 5" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 1}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "Stinger_C",
            DisplayName = "Alpha Stinger",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Stinger_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Stinger_256.png",
            HitPoints = 50,
            Damage = new [] { "Slash Damage: 30", "Leap Damage: 50" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 3}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "StingerElite_C",
            DisplayName = "Elite Gas Stinger",
            Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerElite_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerElite_256.png",
            HitPoints = 100,
            Damage = new [] { "Slash Damage: 30", "Leap Damage: 50", "Gas Damage: 5/sec" },
            Loot = new [] { new CreatureLoot{ ClassName = "StingerParts_C", Amount = 5}},
            Behaviour = CreatureBehaviour.Hostile,
            VariantGroup = "Stinger"
        },
        new()
        {
            ClassName = "LizardDoggo_C",
            DisplayName = "Lizard Doggo",
            Description = "The Lizard Doggo is a short reptilian dog-like creature, which can be tamed by the pioneer.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_LizardDoggo_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_LizardDoggo_256.png",
            HitPoints = 15,
            Behaviour = CreatureBehaviour.Peaceful
        },
        new()
        {
            ClassName = "NonFlyingBird_C",
            DisplayName = "Non Flying Bird",
            Description = "The Non Flying Bird is a small bird-like creature that sticks to the undergrowth. It has a colorful plumage with green upper parts and blue underparts. It has a small blue crest or tufted feathers on the crown. The bare tail is black. It uses its four-parted bill and three tongues to mimic the petals and pistils of a flower to lure in unsuspecting insects that it feeds on. Very surprising, the bird may flee by taking flight if attacked.",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_NonFlyingBird_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_NonFlyingBird_256.png",
            HitPoints = 10,
            Behaviour = CreatureBehaviour.Peaceful
        },
        new()
        {
            ClassName = "SpaceTick_C",
            DisplayName = "Space Giraffe-Tick-Penguin-Whale Thing aka \"Bean\"",
            Description = "The Space Giraffe-Tick-Penguin-Whale Thing (most commonly referred to as a \"Bean\" by the community and also sometimes referred to as \"Land Whale\", \"Chonky Boy\", \"Mr. Bean\", \"Steve\" or \"Frank\") is a passive land creature found throughout the world. A statue of it can be bought in the AWESOME Shop where it is referred to as a \"Confusing Creature\".",
            SmallIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpaceTick_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpaceTick_256.png",
            HitPoints = 200,
            Behaviour = CreatureBehaviour.Peaceful
        }
    };

    private readonly Plant[] _plants = 
    {
        new() { ClassName = "BerryBush_C", DisplayName = "Berry Bush" },
        new() { ClassName = "NutBush_C" , DisplayName = "Nut Bush" }
    };
}
