using Item = SatisfactoryCalculator.DocsServices.Models.DataModels.Item;

namespace SatisfactoryCalculator.DocsServices.Services;

public partial class DocsParserService
{
    private Item FiscitCoupon = new Item
    {
        ClassName = "ResourceSinkCoupon_C",
        DisplayName = "FICSIT Coupon",
        Description = "A special FICSIT bonus program Coupon, obtained through the AWESOME Sink. Can be redeemed in the AWESOME Shop for bonus milestones and rewards.",
        SinkPoints = null,
        SmallIconPath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Resource\\Parts\\ResourceSinkCoupon\\UI\\IconDesc_Ficsit_Coupon_256.png"
    };

    private Item CoffeeCup = new Item
    {
        ClassName = "Cup_C",
        DisplayName = "FICSIT™ Coffee Cup",
        Description = "The Cup is a solely cosmetic piece of hand equipment. Similar to Beryl Nuts, it is small and does not cause too much obstruction when the player decides to take a screenshot.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\Cup\\UI\\IconDesc_CoffeeCup_256.png"
    };

    private Item GoldenCoffeeCup = new Item
    {
        ClassName = "CupGold_C",
        DisplayName = "Employee of the Planet' Cup",
        Description = "A shiny little cup, allowing extra hard working Pioneers to enjoy a cup of well deserved coffee while they wait for what the future will bring.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\GoldenCup\\UI\\IconDesc_CupGold_256.png"
    };

    private Item BoomBox = new Item
    {
        ClassName = "BoomBox_C",
        DisplayName = "Boom Box",
        Description = "Boost your efficiency now, with the completely unnecessary FICSIT Boombox!\r\n\r\n*Tapes are sold separately.",
        SmallIconPath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_64.png",
        BigIconPath = "\\Game\\FactoryGame\\Equipment\\BoomBox\\UI\\IconDesc_Boombox_256.png"
    };

    private Equipment CoffeeCupEquipment = new Equipment
    {
        ClassName = "Cup_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private Equipment GoldenCoffeeCupEquipment = new Equipment
    {
        ClassName = "CupGold_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private Equipment BoomBoxEquipment = new Equipment
    {
        ClassName = "BoomBox_C",
        EquipmentSlot = EquipmentSlot.Arms
    };

    private Emote[] Emotes = new Emote[]
    {
        new Emote
        {
            ClassName = "Clap_C",
            DisplayName = "Clap",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_Clap_256.png"
        },
        new Emote
        {
            ClassName = "BuildGunSpin_C",
            DisplayName = "Twirl It",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\Emote_BuildGunSpin_256.png"
        },
        new Emote
        {
            ClassName = "FacePalm_C",
            DisplayName = "Facepalm",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFacepalm_256.png"
        },
        new Emote
        {
            ClassName = "Rock_C",
            DisplayName = "Rock",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteRock_256.png"
        },
        new Emote
        {
            ClassName = "Paper_C",
            DisplayName = "Paper",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePaper_256.png"
        },
        new Emote
        {
            ClassName = "Scissors_C",
            DisplayName = "Scissors",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors2_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteScissors_256.png"
        },
        new Emote
        {
            ClassName = "Point_C",
            DisplayName = "Point",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmotePoint_256.png"
        },
        new Emote
        {
            ClassName = "Wave_C",
            DisplayName = "Wave",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteWave_256.png"
        },
        new Emote
        {
            ClassName = "Heart_C",
            DisplayName = "Heart",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteHeart_256.png"
        },
        new Emote
        {
            ClassName = "Fingerguns_C",
            DisplayName = "Finger Guns",
            SmallIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Interface\\UI\\Assets\\EmoteIcons\\IconDesc_EmoteFingerGuns_256.png"
        }
    };

    private Statue[] Statues = new Statue[]
    {
        new Statue
        {
            ClassName = "Hog_Statue_C",
            DisplayName = "Silver Hog",
            Description = "The statue of the Silver Pouncing Hog. Perfect as a hood ornament.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Hog_256.png"
        },
        new Statue
        {
            ClassName = "SpaceGiraffeStatue_C",
            DisplayName = "Confusing Creature",
            Description = "A beautiful shiny statue of a weird creature... For really though, what is that thing?",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_SpaceGiraffe_256.png"
        },
        new Statue
        {
            ClassName = "DoggoStatue_C",
            DisplayName = "Lizard Doggo Statue",
            Description = "A statue of the Lizard Doggo.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_LizardDoggo_256.png"
        },
        new Statue
        {
            ClassName = "GoldenNut_Statue_C",
            DisplayName = "Golden Nut",
            Description = "The statue of the golden nut.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_Nut_256.png"
        },
        new Statue
        {
            ClassName = "CharacterClap_Statue_C",
            DisplayName = "Pretty Good Pioneering",
            Description = "The statue of the Clapping Character.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharSilver_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\IconDesc_AwardStatue_Char_256.png"
        },
        new Statue
        {
            ClassName = "CharacterRunStatue_C",
            DisplayName = "Adequate Pioneering",
            Description = "The statue of the Running Character.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharBronze_256.png"
        },
        new Statue
        {
            ClassName = "CharacterSpin_Statue_C",
            DisplayName = "Satisfactory Pioneering",
            Description = "The statue of the Character Spinning the Build Gun.",
            SmallIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_64.png",
            BigIconPath = "\\Game\\FactoryGame\\Buildable\\Building\\Decor\\Statues\\UI\\TXUI_Award_CharGold_256.png"
        }
    };

}
