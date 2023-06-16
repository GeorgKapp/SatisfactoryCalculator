// ReSharper disable HeapView.ObjectAllocation

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private readonly Item _fiscitCoupon = new()
    {
        ClassName = "ResourceSinkCoupon",
        Name = "FICSIT Coupon",
        Description = "A special FICSIT bonus program Coupon, obtained through the AWESOME Sink. Can be redeemed in the AWESOME Shop for bonus milestones and rewards.",
        SinkPoints = null,
        SmallImagePath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_256.png"
    };

    private readonly Item _coffeeCup = new()
    {
        ClassName = "Cup",
        Name = "FICSIT™ Coffee Cup",
        Description = "The Cup is a solely cosmetic piece of hand equipment. Similar to Beryl Nuts, it is small and does not cause too much obstruction when the player decides to take a screenshot.",
        SmallImagePath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_256.png"
    };

    private readonly Item _goldenCoffeeCup = new()
    {
        ClassName = "CupGold",
        Name = "Employee of the Planet' Cup",
        Description = "A shiny little cup, allowing extra hard working Pioneers to enjoy a cup of well deserved coffee while they wait for what the future will bring.",
        SmallImagePath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_256.png"
    };

    private readonly Item _boomBox = new()
    {
        ClassName = "BoomBox",
        Name = "Boom Box",
        Description = "Boost your efficiency now, with the completely unnecessary FICSIT Boombox!\r\n\r\n*Tapes are sold separately.",
        SmallImagePath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_256.png"
    };
    
    private readonly Item _harddrive = new()
    {
        ClassName = "HardDrive",
        Name = "Hard Drive",
        Description = "A hard drive with FICSIT data. Analyze it in the MAM to salvage its contents.",
        SmallImagePath = "\\Game\\FactoryGame\\Resource\\Environment\\CrashSites\\UI\\HardDrive_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Resource\\Environment\\CrashSites\\UI\\HardDrive_256.png",
        StackSize = StackSize.Medium
    };

    private readonly Equipment _coffeeCupEquipment = new()
    {
        ClassName = "Cup",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Equipment _goldenCoffeeCupEquipment = new()
    {
        ClassName = "CupGold",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Equipment _boomBoxEquipment = new()
    {
        ClassName = "BoomBox",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private readonly Emote[] _emotes = {
        new()
        {
            ClassName = "Clap",
            Name = "Clap",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_256.png"
        },
        new()
        {
            ClassName = "BuildGunSpin",
            Name = "Twirl It",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_256.png"
        },
        new()
        {
            ClassName = "FacePalm",
            Name = "Facepalm",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_256.png"
        },
        new()
        {
            ClassName = "Rock",
            Name = "Rock",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock2_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock_256.png"
        },
        new()
        {
            ClassName = "Paper",
            Name = "Paper",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper2_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper_256.png"
        },
        new()
        {
            ClassName = "Scissors",
            Name = "Scissors",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors2_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors_256.png"
        },
        new()
        {
            ClassName = "Point",
            Name = "Point",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_256.png"
        },
        new()
        {
            ClassName = "Wave",
            Name = "Wave",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_256.png"
        },
        new()
        {
            ClassName = "Heart",
            Name = "Heart",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_256.png"
        },
        new()
        {
            ClassName = "Fingerguns",
            Name = "Finger Guns",
            SmallImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_256.png"
        }
    };

    private readonly Statue[] _statues = {
        new()
        {
            ClassName = "Hog_Statue",
            Name = "Silver Hog",
            Description = "The statue of the Silver Pouncing Hog. Perfect as a hood ornament.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_256.png"
        },
        new()
        {
            ClassName = "SpaceGiraffeStatue",
            Name = "Confusing Creature",
            Description = "A beautiful shiny statue of a weird creature... For really though, what is that thing?",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_256.png"
        },
        new()
        {
            ClassName = "DoggoStatue",
            Name = "Lizard Doggo Statue",
            Description = "A statue of the Lizard Doggo.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_256.png"
        },
        new()
        {
            ClassName = "GoldenNut_Statue",
            Name = "Golden Nut",
            Description = "The statue of the golden nut.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_256.png"
        },
        new()
        {
            ClassName = "CharacterClap_Statue",
            Name = "Pretty Good Pioneering",
            Description = "The statue of the Clapping Character.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharSilver_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\IconDesc_AwardStatue_Char_256.png"
        },
        new()
        {
            ClassName = "CharacterRunStatue",
            Name = "Adequate Pioneering",
            Description = "The statue of the Running Character.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_256.png"
        },
        new()
        {
            ClassName = "CharacterSpin_Statue",
            Name = "Satisfactory Pioneering",
            Description = "The statue of the Character Spinning the Build Gun.",
            SmallImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_64.png",
            BigImagePath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_256.png"
        }
    };

    private static readonly Creature _hog = new()
    {
        ClassName = "Hog",
        Name = "Fluffy-tailed Hog",
        Description = "The Fluffy-tailed Hog (a.k.a 'Hog'), is a hostile melee creature that the Pioneer can encounter while exploring. It attacks by charging like a bull, dealing damage with a knockback effect. It is among the most common creatures found in the game world.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hog_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hog_256.png",
        HitPoints = 20,
        Damage = new [] { "Charge Damage: 10" },
        Loot = new() { Item = new() { ClassName = "HogParts" }, Amount = 1 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _alphaHog  }
    };
    
    private static readonly Creature _alphaHog = new()
    {
        ClassName = "HogAlpha",
        Name = "Alpha Hog",
        Description =
            "The Alpha Hog is the larger version of Fluffy-tailed Hog that is also considerably more dangerous, dealing up to 3x the damage. Other than its larger body size, it can be distinguished by the bright edges on the back of its carapace, and its tusks (which regular hogs lack). It also emits a shockwave-like particle effect when it prepares to attack.\r\n\r\nThe Alpha Hog usually patrols around Crash Sites, Power Slugs or artifacts.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_HogAlpha_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_HogAlpha_256.png",
        HitPoints = 80,
        Damage = new[] { "Charge Damage: 20", "Bite Damage: 30" },
        Loot = new() { Item = new() { ClassName = "HogParts" }, Amount = 3 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _hog }
    };
    
    private static readonly Creature _desertSplitter = new()
    {
        ClassName = "SpitterDesert",
        Name = "Desert Spitter",
        Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesert_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesert_256.png",
        HitPoints = 30,
        Damage = new [] { "Melee fireball Damage: 1-11" , "Ranged fireball Damage: 1-6"},
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 2 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _aquaSplitter, _forestSplitter, _redForestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _aquaSplitter = new()
    {
        ClassName = "SpitterAqua",
        Name = "Aqua Spitter",
        Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAqua_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAqua_256.png",
        HitPoints = 30,
        Damage = new [] { "Melee fireball Damage: 1-13" , "Ranged fireball Damage: 1-8"},
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 2 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _forestSplitter, _redForestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _forestSplitter = new()
    {
        ClassName = "SpitterForest",
        Name = "Forest Spitter",
        Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForest_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForest_256.png",
        HitPoints = 30,
        Damage = new []{ "Melee fireball Damage: 1-12" , "Ranged fireball Damage: 1-6"},
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 2 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _redForestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _redForestSplitter = new()
    {
        ClassName = "SpitterRForest",
        Name = "Red Forest Spitter",
        Description = "The Plasma Spitter, or Spitter, is a class of ranged hostile creature that deals damage with its fireball attack. It can be seen in groups, guarding the mid-tier resource patches such as Coal.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForest_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForest_256.png",
        HitPoints = 30,
        Damage = new [] { "Melee fireball Damage: 1-13" , "Ranged fireball Damage: 1-8"},
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 2 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _forestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _desertAlphaSplitter = new()
    {
        ClassName = "SpitterDesertAlpha",
        Name = "Desert Alpha Splitter",
        Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesertAlpha_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterDesertAlpha_256.png",
        HitPoints = 60,
        Damage = new [] { "Melee Fireball Damage: 1-12", "A large fireball (<20) which splits into 5 smaller fireball (<10) mid-air", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 4 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _forestSplitter, _redForestSplitter, _aquaAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _aquaAlphaSplitter = new()
    {
        ClassName = "SpitterAquaAlpha",
        Name = "Aqua Alpha Splitter",
        Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAquaAlpha_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterAquaAlpha_256.png",
        HitPoints = 60,
        Damage = new [] { "Melee Fireball Damage: 1-12", "4 consecutive shots at <18 damage each", "A charged, sniped shot at <36 damage" },
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 4 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _forestSplitter, _redForestSplitter, _desertAlphaSplitter, _forestAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _forestAlphaSplitter = new()
    {
        ClassName = "SpitterForestAlpha",
        Name = "Forest Alpha Splitter",
        Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForestAlpha_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterForestAlpha_256.png",
        HitPoints = 60,
        Damage = new [] { "Melee Fireball Damage: 1-16", "4 consecutive shots at <18 damage each", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 4 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _forestSplitter, _redForestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _redForestAlphaSplitter }
    };
    
    private static readonly Creature _redForestAlphaSplitter = new()
    {
        ClassName = "SpitterRForestAlpha",
        Name = "Red Forest Alpha Splitter",
        Description = "Alpha Spitter refers to a class of larger and more powerful variant of the Spitter.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForestAlpha_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpitterRForestAlpha_256.png",
        HitPoints = 60,
        Damage = new [] { "Melee Fireball Damage: 1-12", "4 consecutive shots at <18 damage each", "A large fireball which deals 50 explosive damage at large radius, dealing massive knockback" },
        Loot = new() { Item = new() { ClassName = "SpitterParts" }, Amount = 4 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _desertSplitter, _aquaSplitter, _forestSplitter, _redForestSplitter, _desertAlphaSplitter, _aquaAlphaSplitter, _forestAlphaSplitter }
    };
    
    private static readonly Creature _flyingCrabHatcher = new()
    {
        ClassName = "Hatcher",
        Name = "Flying Crab Hatcher",
        Description = "Flying Crab Hatchers are nest-like objects often found near rivers, cliff edges, resource nodes, Power Slugs and Crash Sites. Approaching a Hatcher will cause it to continuously spawn Flying Crabs at 5-second intervals until it is destroyed by the player.[1] When destroyed, it drops Hatcher Remains.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hatcher_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Hatcher_256.png",
        HitPoints = 25,
        Damage = new [] { "Spawns 3 Flying Crabs dealing 10 Damage each" },
        Loot = new() { Item = new() { ClassName = "HatcherParts" }, Amount = 1 },
        Behaviour = CreatureBehaviour.Hostile
    };
    
    private static readonly Creature _babyStingerArachnophia = new()
    {
        ClassName = "ArachnophobiaStingerChild",
        Name = "Baby Stinger (Arachnophia)",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerChild_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerChild_256.png",
        HitPoints = 10,
        Damage = new [] { "Slash Damage: 5" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 1 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _alphaStingerArachnophia, _eliteGasStingerArachnophia, _babyStinger, _alphaStinger, _eliteGasStinger }
    };
    
    private static readonly Creature _alphaStingerArachnophia = new()
    {
        ClassName = "ArachnophobiaStinger",
        Name = "Alpha Stinger (Arachnophia)",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStinger_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStinger_256.png",
        HitPoints = 50,
        Damage = new [] { "Slash Damage: 30", "Leap Damage: 50" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 3 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _babyStingerArachnophia, _eliteGasStingerArachnophia, _babyStinger, _alphaStinger, _eliteGasStinger }
    };
    
    private static readonly Creature _eliteGasStingerArachnophia = new()
    {
        ClassName = "ArachnophobiaStingerElite",
        Name = "Elite Gas Stinger (Arachnophia)",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerElite_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_ArachnophobiaStingerElite_256.png",
        HitPoints = 100,
        Damage = new [] { "Slash Damage: 30", "Leap Damage: 50", "Gas Damage: 5/sec" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 5 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _babyStingerArachnophia, _alphaStingerArachnophia, _babyStinger, _alphaStinger, _eliteGasStinger }
    };
    
    private static readonly Creature _babyStinger = new()
    {
        ClassName = "StingerChild",
        Name = "Baby Stinger",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerChild_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerChild_256.png",
        HitPoints = 10,
        Damage = new [] { "Slash Damage: 5" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 1 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _babyStingerArachnophia, _alphaStingerArachnophia, _eliteGasStingerArachnophia, _alphaStinger, _eliteGasStinger }
    };
    
    private static readonly Creature _alphaStinger = new()
    {
        ClassName = "Stinger",
        Name = "Alpha Stinger",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Stinger_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_Stinger_256.png",
        HitPoints = 50,
        Damage = new [] { "Slash Damage: 30", "Leap Damage: 50" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 3 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _babyStingerArachnophia, _alphaStingerArachnophia, _eliteGasStingerArachnophia, _babyStinger, _eliteGasStinger }
    };
    
    private static readonly Creature _eliteGasStinger = new()
    {
        ClassName = "StingerElite",
        Name = "Elite Gas Stinger",
        Description = "Stingers are spider-like creatures found mostly in caves and jungles. Their body consists of four pointy legs attached to their torso, and another 4 similar pointy appendages attached to their head which they use for combat. They move very swiftly and come in three variants.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerElite_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_StingerElite_256.png",
        HitPoints = 100,
        Damage = new [] { "Slash Damage: 30", "Leap Damage: 50", "Gas Damage: 5/sec" },
        Loot = new() { Item = new() { ClassName = "StingerParts" }, Amount = 5 },
        Behaviour = CreatureBehaviour.Hostile,
        Variants = new List<Creature>() { _babyStingerArachnophia, _alphaStingerArachnophia, _eliteGasStingerArachnophia, _babyStinger, _alphaStinger }
    };
    
    private static readonly Creature _lizardDoggo = new()
    {
        ClassName = "LizardDoggo",
        Name = "Lizard Doggo",
        Description = "The Lizard Doggo is a short reptilian dog-like creature, which can be tamed by the pioneer.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_LizardDoggo_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_LizardDoggo_256.png",
        HitPoints = 15,
        Behaviour = CreatureBehaviour.Peaceful
    };
    
    private static readonly Creature _nonFlyingBird = new()
    {
        ClassName = "NonFlyingBird",
        Name = "Non Flying Bird",
        Description = "The Non Flying Bird is a small bird-like creature that sticks to the undergrowth. It has a colorful plumage with green upper parts and blue underparts. It has a small blue crest or tufted feathers on the crown. The bare tail is black. It uses its four-parted bill and three tongues to mimic the petals and pistils of a flower to lure in unsuspecting insects that it feeds on. Very surprising, the bird may flee by taking flight if attacked.",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_NonFlyingBird_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_NonFlyingBird_256.png",
        HitPoints = 10,
        Behaviour = CreatureBehaviour.Peaceful
    };
    
    private static readonly Creature _spaceTick = new()
    {
        ClassName = "SpaceTick",
        Name = "Space Giraffe-Tick-Penguin-Whale Thing aka \"Bean\"",
        Description = "The Space Giraffe-Tick-Penguin-Whale Thing (most commonly referred to as a \"Bean\" by the community and also sometimes referred to as \"Land Whale\", \"Chonky Boy\", \"Mr. Bean\", \"Steve\" or \"Frank\") is a passive land creature found throughout the world. A statue of it can be bought in the AWESOME Shop where it is referred to as a \"Confusing Creature\".",
        SmallImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpaceTick_64.png",
        BigImagePath = "\\Game\\FactoryGame\\Character\\Creature\\CreatureDescriptors\\UI\\IconDesc_SpaceTick_256.png",
        HitPoints = 200,
        Behaviour = CreatureBehaviour.Peaceful
    };

    private readonly Creature[] _creatures =
    {
        _alphaHog,
        _hog,
        _desertSplitter,
        _aquaSplitter,
        _forestSplitter,
        _redForestSplitter,
        _desertAlphaSplitter,
        _aquaAlphaSplitter,
        _forestAlphaSplitter,
        _redForestAlphaSplitter,
        _flyingCrabHatcher,
        _babyStingerArachnophia,
        _alphaStingerArachnophia,
        _eliteGasStingerArachnophia,
        _babyStinger,
        _alphaStinger,
        _eliteGasStinger,
        _lizardDoggo,
        _nonFlyingBird,
        _spaceTick
    };

    private readonly Plant[] _plants = 
    {
        new() { ClassName = "BerryBush", Name = "Berry Bush" },
        new() { ClassName = "NutBush" , Name = "Nut Bush" }
    };
}
