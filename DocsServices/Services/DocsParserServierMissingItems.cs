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

}
