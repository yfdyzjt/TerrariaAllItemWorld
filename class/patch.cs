// ###################################################################################
// Terraria.Chat > ChatCommandProcessor > ProcessIncomingMessage (OR CreateOutgoingMessage)
using System;
using System.Collections.Generic;
using Terraria.Chat.Commands;
using Terraria.Localization;
// ###################################################################################
using Terraria.GameContent;
using Terraria.ID;
using System.Linq;
using System.IO;
// ###################################################################################
namespace Terraria.Chat
{
    public partial class ChatCommandProcessor : IChatProcessor
    {
        public void ProcessIncomingMessage(ChatMessage message, int clientId)
        {
            // ###################################################################################
            if (message.Text == "/start")
            {
                string weapons = "Weapons 武器", tools = "Tools 工具", armor = "Armor 盔甲", accessories = "Accessories 饰品", placeables = "Placeables 放置物", consumables = "Consumables 消耗品", bossAndFish = "BossAndFish 首领和钓鱼", others = "Others 其他";
                string melee = "Melee 近战", magic = "Magic 魔法", ranged = "Ranged 远程", summon = "Summon 召唤", whips = "Whips 鞭子", throwing = "Throwing 投掷", sentry = "Sentry 炮塔", yoyo = "Yoyo 悠悠球";
                string pickaxes = "Pickaxes 镐子", axes = "Axes 斧头", hammers = "Hammers 锤子", drill = "Drill 钻头", chainsaw = "Chainsaw 链锯", tother = "Other 其他";
                string head = "Head 头部", body = "Body 身体", legs = "Legs 腿部", vanity = "Vanity 时装";
                string aaccessories = "Accessories 饰品", wings = "Wings 翅膀", hooks = "Hooks 钩爪", pets = "Pets 宠物", lightPets = "LightPets 照明宠物", mounts = "Mounts 坐骑", carts = "Carts 矿车";
                string tiles = "Tiles 物块", walls = "Walls 墙壁", furniture = "Other Furniture 其他家具", pother = "Other 其他", wiring = "Wiring 电路", torches = "Torches 火把", campfires = "Campfires 篝火", workbenches = "Workbenches 工作台";
                string ammo = "Ammo 弹药", potions = "Potions 药水", food = "Food 食物", pickup = "Pickup 拾取物", cother = "Other 其他", environment = "Environment 环境";
                string bossSummons = "BossSummons 首领召唤物", bossBag = "BossBag 宝藏袋", bait = "Bait 鱼饵", fish = "ffish 鱼", questFish = "QuestFish 任务鱼", relic = "Relic 圣物", fishingCrate = "FishingCrate 宝匣", poles = "Poles 鱼竿";
                string materials = "Materials 材料", kites = "Kites 风筝", paint = "Paint 油漆", paintTool = "PaintTool 油漆工具", dyes = "Dyes 染料", oothers = "Others 其它", gameplay = "GamePlay 游戏流程", golf = "Golf 高尔夫";
                string chests = "Chests 箱子", trappedChests = "Trapped Chests 陷阱箱", metalBars = "MetalBars 矿物锭", tombstones = "Tombstones 墓碑 ", pot = "Pot 盆", dressers = "Dressers 梳妆台", boulders = "Boulders 巨石", chairs = "Chairs 椅子", beds = "Beds 床", critterCages = "Critter Cages 小动物笼", paintings = "Paintings 画", platforms = "Platforms 平台", doors = "Doors 门", tables = "Tables 桌子", toilets = "Toilets 马桶", candles = "Candles 蜡烛", chandeliers = "Chandeliers 吊灯", lanterns = "Lanterns 灯笼", pianos = "Pianos 钢琴", sofas = "Sofas 沙发", bathtubs = "Bathtubs 浴缸", banners = "Banners 旗", lamps = "Lamps 灯", candelabras = "Candelabras 烛台", bookcases = "Bookcases 书架", clocks = "Clocks 时钟", statues = "Statues 雕像", musicBoxes = "Music Boxes 八音盒", sinks = "Sinks 水槽", fountains = "Fountains 喷泉", pylons = "Pylons 晶塔", gems = "Gems 宝石";

                var orderList = new List<string>() { bossAndFish, accessories, armor, weapons, tools, placeables, consumables, others };

                var categories = new DefaultDictionary<string, DefaultDictionary<string, List<Item>>>();

                var isSpear = ItemID.Sets.Factory.CreateBoolSet(ItemID.Spear, ItemID.Trident, ItemID.Swordfish, ItemID.ThunderSpear, ItemID.TheRottedFork, ItemID.DarkLance, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.ObsidianSwordfish, ItemID.Gungnir, ItemID.MushroomSpear, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.MonkStaffT3, ItemID.ChlorophytePartisan, ItemID.NorthPole);
                var isWhip = ItemID.Sets.Factory.CreateBoolSet(4672, 4678, 4679, 4680, 4911, 4912, 4913, 4914, 5074, 5473, 5474, 5475, 5476, 5477, 5478, 5479, 5480, 5688);
                var _itemIdsThatAreAccepted = new HashSet<int> { 213, 5295, 509, 850, 851, 3612, 3625, 3611, 510, 849, 3620, 1071, 1543, 1072, 1544, 1100, 1545, 50, 3199, 3124, 5358, 5359, 5360, 5361, 5437, 1326, 5335, 3384, 4263, 4819, 4262, 946, 4707, 205, 206, 207, 1128, 3031, 4820, 5302, 5364, 4460, 4608, 4872, 3032, 5303, 5304, 1991, 4821, 3183, 779, 5134, 1299, 4711, 4049, 114, 5667 };
                var sortingPriorityBossSpawns = ItemID.Sets.Factory.CreateIntSet(-1, 43, 1, 560, 2, 70, 3, 1331, 3, 361, 4, 5120, 5, 1133, 5, 4988, 6, 5334, 7, 544, 8, 556, 9, 557, 10, ItemID.PirateMap, 11, 2673, 12, 602, 13, 1844, 14, 1958, 15, 1293, 16, 2767, 17, 4271, 18, 3601, 19, 1291, 20, 109, 21, 29, 22, 50, 23, 3199, 24, 3124, 25, 5437, 26, 5358, 27, 5359, 28, 5360, 29, 5361, 30, 4263, 31, 4819, 32);
                var sortingPriorityBossSpawnsExclusions = new List<int> { ItemID.LifeCrystal, ItemID.ManaCrystal, ItemID.CellPhone, ItemID.IceMirror, ItemID.MagicMirror, ItemID.LifeFruit, ItemID.TreasureMap, ItemID.Shellphone, ItemID.ShellphoneDummy, ItemID.ShellphoneHell, ItemID.ShellphoneOcean, ItemID.ShellphoneSpawn, ItemID.MagicConch, ItemID.DemonConch };
                var consumablesOtherAdded = ItemID.Sets.Factory.CreateBoolSet(71, 72, 73, 74, 3822, 3817, 327, 329, 1141, 1169, 1533, 1534, 1535, 1536, 1537, 4714);
                var consumablesEnvironmentAdded = ItemID.Sets.Factory.CreateBoolSet(6135, 6136, 5532, 5533, 5467, 5468, 62, 59, 195, 194, 369, 2171, 5241, 307, 308, 309, 310, 311, 312, 2357, 4041, 4042, 4043, 4044, 4045, 4046, 4047, 4048, 4241, 1828, 27, 4851, 4852, 4853, 4854, 4855, 4856, 4857, 4400, 779, 5134, 780, 781, 782, 783, 784, 5392, 5393, 5394, 422, 423, 3477, 2886, 2887, 66, 67, 209, 3031, 3032, 3182, 4447, 4824, 4827, 3186, 207, 3184, 4448, 4820, 4872, 4825, 1128, 3185, 4449, 4826, 5302, 5303, 5364, 5304, 2015, 2016, 3194, 2019, 1338, 4838, 4839, 4840, 4841, 4842, 4843, 4844, 2017, 5312, 5313, 2123, 2122, 3191, 5350, 4070, 4069, 4068, 1992, 2121, 2007, 261, 2740, 4374, 3192, 4361, 4847, 2004, 5212, 5300, 4363, 4849, 2003, 4395, 2205, 4373, 4375, 2157, 2156, 4359, 4480, 3193, 2006, 2018, 3563, 4831, 4832, 4833, 4834, 4835, 4836, 4837, 5132, 5311, 4464, 4465, 4418, 2002, 2001, 1994, 1995, 1996, 1998, 1999, 1997, 2000, 4845, 4961, 4339, 4338, 4334, 4335, 4337, 4336, 2889, 2890, 2891, 4340, 2892, 4274, 2893, 4362, 2894, 4482, 3564, 4419, 2895, 4961, 2673);
                var workbenchesList = ItemID.Sets.Workbenches.ToList();
                var isWiring = TileID.Sets.Factory.CreateBoolSet(419, 420, TileID.PressurePlates, TileID.MinecartTrack, TileID.LogicSensor, TileID.WeightedPressurePlate, TileID.ProjectilePressurePad, TileID.GolfHole, TileID.GemLocks, TileID.Switches, TileID.GeyserTrap, TileID.Timers, TileID.FakeContainers, TileID.Containers2, TileID.Lever, TileID.Detonator, TileID.Timers, TileID.ConveyorBeltLeft, TileID.ConveyorBeltRight, TileID.AmethystGemsparkOff, TileID.Chimney, TileID.SillyBalloonMachine, TileID.Detonator, TileID.Sundial, TileID.Moondial, TileID.AnnouncementBox, TileID.Fireplace, TileID.Cannon, TileID.SnowballLauncher, TileID.Campfire, TileID.ActiveStoneBlock, TileID.InactiveStoneBlock, TileID.TrapdoorOpen, TileID.TrapdoorClosed, TileID.TallGateOpen, TileID.TallGateClosed, TileID.OpenDoor, TileID.ClosedDoor, TileID.Firework, TileID.Toilets, TileID.Chairs, TileID.FireworksBox, TileID.FireworkFountain, TileID.Teleporter, TileID.Torches, TileID.WireBulb, TileID.HolidayLights, TileID.BubbleMachine, TileID.FogMachine, TileID.HangingLanterns, TileID.Lamps, TileID.DiscoBall, TileID.ChineseLanterns, TileID.Candelabras, TileID.PlatinumCandelabra, TileID.PlasmaLamp, TileID.VolcanoSmall, TileID.VolcanoLarge, TileID.Chandeliers, TileID.MinecartTrack, TileID.Candles, TileID.PlatinumCandle, TileID.WaterCandle, TileID.PeaceCandle, TileID.ShadowCandle, TileID.Lampposts, TileID.Traps, TileID.GeyserTrap, TileID.MusicBoxes, TileID.Jackolanterns, TileID.WaterFountain, TileID.LunarMonolith, TileID.BloodMoonMonolith, TileID.VoidMonolith, TileID.EchoMonolith, TileID.ShimmerMonolith, TileID.PartyMonolith, TileID.Explosives, TileID.LandMine, TileID.InletPump, TileID.OutletPump, TileID.BoulderStatue, TileID.MushroomStatue, TileID.CatBast, TileID.Statues, TileID.Grate, TileID.GrateClosed, TileID.PixelBox);
                var isWiring2 = ItemID.Sets.Factory.CreateBoolSet(851, 850, 509, 3625, 3611, 3619, 2799, 3616, 849, 510, 3624, 3620, 512, 511, 3609, 3610, 3202);
                var itemNameToId = new Dictionary<string, short>{{"IronPickaxe",1},{"DirtBlock",2},{"StoneBlock",3},{"IronBroadsword",4},{"Mushroom",5},{"IronShortsword",6},{"IronHammer",7},{"Torch",8},{"Wood",9},{"IronAxe",10},{"IronOre",11},{"CopperOre",12},{"GoldOre",13},{"SilverOre",14},{"CopperWatch",15},{"SilverWatch",16},{"GoldWatch",17},{"DepthMeter",18},{"GoldBar",19},{"CopperBar",20},{"SilverBar",21},{"IronBar",22},{"Gel",23},{"WoodenSword",24},{"WoodenDoor",25},{"StoneWall",26},{"Acorn",27},{"LesserHealingPotion",28},{"LifeCrystal",29},{"DirtWall",30},{"Bottle",31},{"WoodenTable",32},{"Furnace",33},{"WoodenChair",34},{"IronAnvil",35},{"WorkBench",36},{"Goggles",37},{"Lens",38},{"WoodenBow",39},{"WoodenArrow",40},{"FlamingArrow",41},{"Shuriken",42},{"SuspiciousLookingEye",43},{"DemonBow",44},{"WarAxeoftheNight",45},{"Light'sBane",46},{"UnholyArrow",47},{"Chest",48},{"BandofRegeneration",49},{"MagicMirror",50},{"Jester'sArrow",51},{"AngelStatue",52},{"CloudinaBottle",53},{"HermesBoots",54},{"EnchantedBoomerang",55},{"DemoniteOre",56},{"DemoniteBar",57},{"Heart",58},{"CorruptSeeds",59},{"VileMushroom",60},{"EbonstoneBlock",61},{"GrassSeeds",62},{"Sunflower",63},{"Vilethorn",64},{"Starfury",65},{"PurificationPowder",66},{"VilePowder",67},{"RottenChunk",68},{"WormTooth",69},{"WormFood",70},{"CopperCoin",71},{"SilverCoin",72},{"GoldCoin",73},{"PlatinumCoin",74},{"FallenStar",75},{"CopperGreaves",76},{"IronGreaves",77},{"SilverGreaves",78},{"GoldGreaves",79},{"CopperChainmail",80},{"IronChainmail",81},{"SilverChainmail",82},{"GoldChainmail",83},{"GrapplingHook",84},{"Chain",85},{"ShadowScale",86},{"PiggyBank",87},{"MiningHelmet",88},{"CopperHelmet",89},{"IronHelmet",90},{"SilverHelmet",91},{"GoldHelmet",92},{"WoodWall",93},{"WoodPlatform",94},{"FlintlockPistol",95},{"Musket",96},{"MusketBall",97},{"Minishark",98},{"IronBow",99},{"ShadowGreaves",100},{"ShadowScalemail",101},{"ShadowHelmet",102},{"NightmarePickaxe",103},{"TheBreaker",104},{"Candle",105},{"CopperChandelier",106},{"SilverChandelier",107},{"GoldChandelier",108},{"ManaCrystal",109},{"LesserManaPotion",110},{"BandofStarpower",111},{"FlowerofFire",112},{"MagicMissile",113},{"DirtRod",114},{"ShadowOrb",115},{"Meteorite",116},{"MeteoriteBar",117},{"Hook",118},{"Flamarang",119},{"MoltenFury",120},{"FieryGreatsword",121},{"MoltenPickaxe",122},{"MeteorHelmet",123},{"MeteorSuit",124},{"MeteorLeggings",125},{"BottledWater",126},{"SpaceGun",127},{"RocketBoots",128},{"GrayBrick",129},{"GrayBrickWall",130},{"RedBrick",131},{"RedBrickWall",132},{"ClayBlock",133},{"BlueBrick",134},{"BlueBrickWall",135},{"ChainLantern",136},{"GreenBrick",137},{"GreenBrickWall",138},{"PinkBrick",139},{"PinkBrickWall",140},{"GoldBrick",141},{"GoldBrickWall",142},{"SilverBrick",143},{"SilverBrickWall",144},{"CopperBrick",145},{"CopperBrickWall",146},{"Spike",147},{"WaterCandle",148},{"Book",149},{"Cobweb",150},{"NecroHelmet",151},{"NecroBreastplate",152},{"NecroGreaves",153},{"Bone",154},{"Muramasa",155},{"CobaltShield",156},{"AquaScepter",157},{"LuckyHorseshoe",158},{"ShinyRedBalloon",159},{"Harpoon",160},{"SpikyBall",161},{"BallO'Hurt",162},{"BlueMoon",163},{"Handgun",164},{"WaterBolt",165},{"Bomb",166},{"Dynamite",167},{"Grenade",168},{"SandBlock",169},{"Glass",170},{"Sign",171},{"AshBlock",172},{"Obsidian",173},{"Hellstone",174},{"HellstoneBar",175},{"MudBlock",176},{"Sapphire",177},{"Ruby",178},{"Emerald",179},{"Topaz",180},{"Amethyst",181},{"Diamond",182},{"GlowingMushroom",183},{"Star",184},{"IvyWhip",185},{"BreathingReed",186},{"Flipper",187},{"HealingPotion",188},{"ManaPotion",189},{"BladeofGrass",190},{"ThornChakram",191},{"ObsidianBrick",192},{"ObsidianSkull",193},{"MushroomGrassSeeds",194},{"JungleGrassSeeds",195},{"WoodenHammer",196},{"StarCannon",197},{"BluePhaseblade",198},{"RedPhaseblade",199},{"GreenPhaseblade",200},{"PurplePhaseblade",201},{"WhitePhaseblade",202},{"YellowPhaseblade",203},{"MeteorHamaxe",204},{"EmptyBucket",205},{"WaterBucket",206},{"LavaBucket",207},{"JungleRose",208},{"Stinger",209},{"Vine",210},{"FeralClaws",211},{"AnkletoftheWind",212},{"StaffofRegrowth",213},{"HellstoneBrick",214},{"WhoopieCushion",215},{"Shackle",216},{"MoltenHamaxe",217},{"Flamelash",218},{"PhoenixBlaster",219},{"Sunfury",220},{"Hellforge",221},{"ClayPot",222},{"Nature'sGift",223},{"Bed",224},{"Silk",225},{"LesserRestorationPotion",226},{"RestorationPotion",227},{"JungleHat",228},{"JungleShirt",229},{"JunglePants",230},{"MoltenHelmet",231},{"MoltenBreastplate",232},{"MoltenGreaves",233},{"MeteorShot",234},{"StickyBomb",235},{"BlackLens",236},{"Sunglasses",237},{"WizardHat",238},{"TopHat",239},{"TuxedoShirt",240},{"TuxedoPants",241},{"SummerHat",242},{"BunnyHood",243},{"Plumber'sHat",244},{"Plumber'sShirt",245},{"Plumber'sPants",246},{"Hero'sHat",247},{"Hero'sShirt",248},{"Hero'sPants",249},{"FishBowl",250},{"Archaeologist'sHat",251},{"Archaeologist'sJacket",252},{"Archaeologist'sPants",253},{"BlackThread",254},{"GreenThread",255},{"NinjaHood",256},{"NinjaShirt",257},{"NinjaPants",258},{"Leather",259},{"RedHat",260},{"Goldfish",261},{"Robe",262},{"RobotHat",263},{"GoldCrown",264},{"HellfireArrow",265},{"Sandgun",266},{"GuideVoodooDoll",267},{"DivingHelmet",268},{"FamiliarShirt",269},{"FamiliarPants",270},{"FamiliarWig",271},{"DemonScythe",272},{"Night'sEdge",273},{"DarkLance",274},{"Coral",275},{"Cactus",276},{"Trident",277},{"SilverBullet",278},{"ThrowingKnife",279},{"Spear",280},{"Blowpipe",281},{"Glowstick",282},{"Seed",283},{"WoodenBoomerang",284},{"Aglet",285},{"StickyGlowstick",286},{"PoisonedKnife",287},{"ObsidianSkinPotion",288},{"RegenerationPotion",289},{"SwiftnessPotion",290},{"GillsPotion",291},{"IronskinPotion",292},{"ManaRegenerationPotion",293},{"MagicPowerPotion",294},{"FeatherfallPotion",295},{"SpelunkerPotion",296},{"InvisibilityPotion",297},{"ShinePotion",298},{"NightOwlPotion",299},{"BattlePotion",300},{"ThornsPotion",301},{"WaterWalkingPotion",302},{"ArcheryPotion",303},{"HunterPotion",304},{"GravitationPotion",305},{"GoldChest",306},{"DaybloomSeeds",307},{"MoonglowSeeds",308},{"BlinkrootSeeds",309},{"DeathweedSeeds",310},{"WaterleafSeeds",311},{"FireblossomSeeds",312},{"Daybloom",313},{"Moonglow",314},{"Blinkroot",315},{"Deathweed",316},{"Waterleaf",317},{"Fireblossom",318},{"SharkFin",319},{"Feather",320},{"Tombstone",321},{"MimeMask",322},{"AntlionMandible",323},{"IllegalGunParts",324},{"TheDoctor'sShirt",325},{"TheDoctor'sPants",326},{"GoldenKey",327},{"ShadowChest",328},{"ShadowKey",329},{"ObsidianBrickWall",330},{"JungleSpores",331},{"Loom",332},{"Piano",333},{"Dresser",334},{"Bench",335},{"Bathtub",336},{"RedBanner",337},{"GreenBanner",338},{"BlueBanner",339},{"YellowBanner",340},{"LampPost",341},{"TikiTorch",342},{"Barrel",343},{"ChineseLantern",344},{"CookingPot",345},{"Safe",346},{"SkullLantern",347},{"TrashCan",348},{"Candelabra",349},{"PinkVase",350},{"Mug",351},{"Keg",352},{"Ale",353},{"Bookcase",354},{"Throne",355},{"Bowl",356},{"BowlofSoup",357},{"Toilet",358},{"GrandfatherClock",359},{"ArmorStatue",360},{"GoblinBattleStandard",361},{"TatteredCloth",362},{"Sawmill",363},{"CobaltOre",364},{"MythrilOre",365},{"AdamantiteOre",366},{"Pwnhammer",367},{"Excalibur",368},{"HallowedSeeds",369},{"EbonsandBlock",370},{"CobaltHat",371},{"CobaltHelmet",372},{"CobaltMask",373},{"CobaltBreastplate",374},{"CobaltLeggings",375},{"MythrilHood",376},{"MythrilHelmet",377},{"MythrilHat",378},{"MythrilChainmail",379},{"MythrilGreaves",380},{"CobaltBar",381},{"MythrilBar",382},{"CobaltChainsaw",383},{"MythrilChainsaw",384},{"CobaltDrill",385},{"MythrilDrill",386},{"AdamantiteChainsaw",387},{"AdamantiteDrill",388},{"DaoofPow",389},{"MythrilHalberd",390},{"AdamantiteBar",391},{"GlassWall",392},{"Compass",393},{"DivingGear",394},{"GPS",395},{"ObsidianHorseshoe",396},{"ObsidianShield",397},{"Tinkerer'sWorkshop",398},{"CloudinaBalloon",399},{"AdamantiteHeadgear",400},{"AdamantiteHelmet",401},{"AdamantiteMask",402},{"AdamantiteBreastplate",403},{"AdamantiteLeggings",404},{"SpectreBoots",405},{"AdamantiteGlaive",406},{"Toolbelt",407},{"PearlsandBlock",408},{"PearlstoneBlock",409},{"MiningShirt",410},{"MiningPants",411},{"PearlstoneBrick",412},{"IridescentBrick",413},{"MudstoneBrick",414},{"CobaltBrick",415},{"MythrilBrick",416},{"PearlstoneBrickWall",417},{"IridescentBrickWall",418},{"MudstoneBrickWall",419},{"CobaltBrickWall",420},{"MythrilBrickWall",421},{"HolyWater",422},{"UnholyWater",423},{"SiltBlock",424},{"FairyBell",425},{"BreakerBlade",426},{"BlueTorch",427},{"RedTorch",428},{"GreenTorch",429},{"PurpleTorch",430},{"WhiteTorch",431},{"YellowTorch",432},{"DemonTorch",433},{"ClockworkAssaultRifle",434},{"CobaltRepeater",435},{"MythrilRepeater",436},{"DualHook",437},{"StarStatue",438},{"SwordStatue",439},{"SlimeStatue",440},{"GoblinStatue",441},{"ShieldStatue",442},{"BatStatue",443},{"FishStatue",444},{"BunnyStatue",445},{"SkeletonStatue",446},{"ReaperStatue",447},{"WomanStatue",448},{"ImpStatue",449},{"GargoyleStatue",450},{"GloomStatue",451},{"HornetStatue",452},{"BombStatue",453},{"CrabStatue",454},{"HammerStatue",455},{"PotionStatue",456},{"SpearStatue",457},{"CrossStatue",458},{"JellyfishStatue",459},{"BowStatue",460},{"BoomerangStatue",461},{"BootStatue",462},{"ChestStatue",463},{"BirdStatue",464},{"AxeStatue",465},{"CorruptStatue",466},{"TreeStatue",467},{"AnvilStatue",468},{"PickaxeStatue",469},{"MushroomStatue",470},{"EyeballStatue",471},{"PillarStatue",472},{"HeartStatue",473},{"PotStatue",474},{"SunflowerStatue",475},{"KingStatue",476},{"QueenStatue",477},{"PiranhaStatue",478},{"PlankedWall",479},{"WoodenBeam",480},{"AdamantiteRepeater",481},{"AdamantiteSword",482},{"CobaltSword",483},{"MythrilSword",484},{"MoonCharm",485},{"Ruler",486},{"CrystalBall",487},{"DiscoBall",488},{"SorcererEmblem",489},{"WarriorEmblem",490},{"RangerEmblem",491},{"DemonWings",492},{"AngelWings",493},{"MagicalHarp",494},{"RainbowRod",495},{"IceRod",496},{"Neptune'sShell",497},{"Mannequin",498},{"GreaterHealingPotion",499},{"GreaterManaPotion",500},{"PixieDust",501},{"CrystalShard",502},{"ClownHat",503},{"ClownShirt",504},{"ClownPants",505},{"Flamethrower",506},{"Bell",507},{"Harp",508},{"RedWrench",509},{"WireCutter",510},{"ActiveStoneBlock",511},{"InactiveStoneBlock",512},{"Lever",513},{"LaserRifle",514},{"CrystalBullet",515},{"HolyArrow",516},{"MagicDagger",517},{"CrystalStorm",518},{"CursedFlames",519},{"SoulofLight",520},{"SoulofNight",521},{"CursedFlame",522},{"CursedTorch",523},{"AdamantiteForge",524},{"MythrilAnvil",525},{"UnicornHorn",526},{"DarkShard",527},{"LightShard",528},{"RedPressurePlate",529},{"Wire",530},{"SpellTome",531},{"StarCloak",532},{"Megashark",533},{"Shotgun",534},{"Philosopher'sStone",535},{"TitanGlove",536},{"CobaltNaginata",537},{"Switch",538},{"DartTrap",539},{"Boulder",540},{"GreenPressurePlate",541},{"GrayPressurePlate",542},{"BrownPressurePlate",543},{"MechanicalEye",544},{"CursedArrow",545},{"CursedBullet",546},{"SoulofFright",547},{"SoulofMight",548},{"SoulofSight",549},{"Gungnir",550},{"HallowedPlateMail",551},{"HallowedGreaves",552},{"HallowedHelmet",553},{"CrossNecklace",554},{"ManaFlower",555},{"MechanicalWorm",556},{"MechanicalSkull",557},{"HallowedHeadgear",558},{"HallowedMask",559},{"SlimeCrown",560},{"LightDisc",561},{"MusicBox(OverworldDay)",562},{"MusicBox(Eerie)",563},{"MusicBox(Night)",564},{"MusicBox(Title)",565},{"MusicBox(Underground)",566},{"MusicBox(Boss1)",567},{"MusicBox(Jungle)",568},{"MusicBox(Corruption)",569},{"MusicBox(UndergroundCorruption)",570},{"MusicBox(TheHallow)",571},{"MusicBox(Boss2)",572},{"MusicBox(UndergroundHallow)",573},{"MusicBox(Boss3)",574},{"SoulofFlight",575},{"MusicBox",576},{"DemoniteBrick",577},{"HallowedRepeater",578},{"Drax",579},{"Explosives",580},{"InletPump",581},{"OutletPump",582},{"1SecondTimer",583},{"3SecondTimer",584},{"5SecondTimer",585},{"CandyCaneBlock",586},{"CandyCaneWall",587},{"SantaHat",588},{"SantaShirt",589},{"SantaPants",590},{"GreenCandyCaneBlock",591},{"GreenCandyCaneWall",592},{"SnowBlock",593},{"SnowBrick",594},{"SnowBrickWall",595},{"BlueLight",596},{"RedLight",597},{"GreenLight",598},{"BluePresent",599},{"GreenPresent",600},{"YellowPresent",601},{"SnowGlobe",602},{"Carrot",603},{"YellowPhasesaber",3769},{"WhitePhasesaber",3768},{"PurplePhasesaber",3767},{"GreenPhasesaber",3766},{"RedPhasesaber",3765},{"BluePhasesaber",3764},{"PlatinumBow",3480},{"PlatinumHammer",3481},{"PlatinumAxe",3482},{"PlatinumShortsword",3483},{"PlatinumBroadsword",3484},{"PlatinumPickaxe",3485},{"TungstenBow",3486},{"TungstenHammer",3487},{"TungstenAxe",3488},{"TungstenShortsword",3489},{"TungstenBroadsword",3490},{"TungstenPickaxe",3491},{"LeadBow",3492},{"LeadHammer",3493},{"LeadAxe",3494},{"LeadShortsword",3495},{"LeadBroadsword",3496},{"LeadPickaxe",3497},{"TinBow",3498},{"TinHammer",3499},{"TinAxe",3500},{"TinShortsword",3501},{"TinBroadsword",3502},{"TinPickaxe",3503},{"CopperBow",3504},{"CopperHammer",3505},{"CopperAxe",3506},{"CopperShortsword",3507},{"CopperBroadsword",3508},{"CopperPickaxe",3509},{"SilverBow",3510},{"SilverHammer",3511},{"SilverAxe",3512},{"SilverShortsword",3513},{"SilverBroadsword",3514},{"SilverPickaxe",3515},{"GoldBow",3516},{"GoldHammer",3517},{"GoldAxe",3518},{"GoldShortsword",3519},{"GoldBroadsword",3520},{"GoldPickaxe",3521}};
                var itemIdToName = new Dictionary<int, string>();
                foreach(var kv in itemNameToId) { itemIdToName[kv.Value] = kv.Key; }

                // 初始化
                for (int id = 1; id < ItemID.Count; id++)
                {
                    if (!ItemID.Sets.Deprecated[id])
                    {
                        Item item = new Item();
                        item.SetDefaults(id);
                        bool other = true;

                        string itemName = itemIdToName.TryGetValue(item.type, out var name) ? name : "";

                        bool isConsumable = (item.type == 267 || item.type == 1307) || (item.consumable && !(item.createTile != -1 || item.createWall != -1 || item.tileWand != -1));
                        bool isPotions = (item.UseSound == SoundID.Item3 && !ItemID.Sets.IsFood[item.type]) || item.type == ItemID.Mushroom || item.type == ItemID.LovePotion || item.type == ItemID.GenderChangePotion || item.type == ItemID.WormholePotion || item.type == ItemID.RecallPotion || item.type == ItemID.StinkPotion || item.type == ItemID.TeleportationPotion || item.type == ItemID.PotionOfReturn || item.type == ItemID.HerbBag;
                        bool isBossSummons = (sortingPriorityBossSpawns[item.type] != -1 && !sortingPriorityBossSpawnsExclusions.Contains(item.type)) || item.type == ItemID.PirateMap || item.type == ItemID.SnowGlobe || item.type == ItemID.DD2ElderCrystal || item.type == ItemID.GuideVoodooDoll || item.type == ItemID.ClothierVoodooDoll || item.type == ItemID.NightKey || item.type == ItemID.LightKey || item.type == ItemID.EmpressButterfly;
                        bool isTool = item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.fishingPole > 0 || item.tileWand != -1 || _itemIdsThatAreAccepted.Contains(item.type) || item.type == ItemID.GravediggerShovel || item.type == ItemID.SpectreGoggles || item.type == ItemID.ClosedVoidBag || item.type == ItemID.VoidLens || item.type == ItemID.MoneyTrough || item.type == ItemID.ChesterPetItem || item.type == ItemID.DontHurtCrittersBook || item.type == ItemID.DontHurtNatureBook || item.type == ItemID.DontHurtComboBook || item.type == ItemID.UncumberingStone || item.type == ItemID.EncumberingStone || item.type == ItemID.DirtRod;
                        bool isPaintScraper = item.type == ItemID.PaintSprayer || item.type == ItemID.PaintScraper || item.type == ItemID.Paintbrush || item.type == ItemID.PaintRoller || item.type == ItemID.SpectrePaintbrush || item.type == ItemID.SpectrePaintScraper || item.type == ItemID.SpectrePaintRoller;
                        bool isGolf = itemName.Contains("Golf") || ItemID.Sets.SortingPriorityToolsGolf[item.type] != -1 || item.type == 3989 || item.type == 4040 || (item.type >= 4319 && item.type <= 4320) || (item.type >= 4083 && item.type <= 4089) || item.type == 4095 || (item.type >= 4242 && item.type <= 4255) || item.type == 4264 || (item.type >= 4587 && item.type <= 4601);

                        // 武器
                        if (item.melee && !isTool)
                        { categories[weapons][melee].Add(item); other = false; } // 近战
                        if (item.magic)
                        { categories[weapons][magic].Add(item); other = false; } // 魔法
                        if (item.ranged && item.ammo == 0)
                        { categories[weapons][ranged].Add(item); other = false; } // 远程
                        if (item.summon && !isWhip[item.type] && !item.sentry)
                        { categories[weapons][summon].Add(item); other = false; } // 召唤
                        if (isWhip[item.type] && !item.sentry)
                        { categories[weapons][whips].Add(item); other = false; } // 鞭子
                        if (item.noMelee && item.ranged && item.consumable)
                        { categories[weapons][throwing].Add(item); other = false; } // 投掷 
                        if (item.summon && item.sentry)
                        { categories[weapons][sentry].Add(item); other = false; } // 炮塔
                        if (ItemID.Sets.Yoyo[item.type])
                        { categories[weapons][yoyo].Add(item); other = false; } // 悠悠球

                        // 工具
                        bool isOtherTool = true;
                        if (item.pick > 0 && !ItemID.Sets.IsDrill[item.type])
                        { categories[tools][pickaxes].Add(item); other = false; isOtherTool = false; } // 镐子
                        if (item.axe > 0 && !ItemID.Sets.IsChainsaw[item.type])
                        { categories[tools][axes].Add(item); other = false; isOtherTool = false; } // 斧头
                        if (item.hammer > 0)
                        { categories[tools][hammers].Add(item); other = false; isOtherTool = false; } // 锤子
                        if (ItemID.Sets.IsDrill[item.type])
                        { categories[tools][drill].Add(item); other = false; isOtherTool = false; } // 钻头
                        if (ItemID.Sets.IsChainsaw[item.type])
                        { categories[tools][chainsaw].Add(item); other = false; isOtherTool = false; } // 链锯
                        if (item.fishingPole > 0)
                        { categories[tools][poles].Add(item); other = false; isOtherTool = false; } // 鱼竿
                        if (isTool && isOtherTool)
                        { categories[tools][tother].Add(item); other = false; } // 其他

                        if (Main.projHook[item.shoot])
                        { categories[tools][hooks].Add(item); other = false; } // 钩爪
                        if (Main.lightPet[item.buffType])
                        { categories[tools][lightPets].Add(item); other = false; } // 照明宠物
                        if (item.mountType != -1 && !MountID.Sets.Cart[item.mountType])
                        { categories[tools][mounts].Add(item); other = false; } // 坐骑
                        if (item.mountType != -1 && MountID.Sets.Cart[item.mountType])
                        { categories[tools][carts].Add(item); other = false; } // 矿车
                        if (isPaintScraper)
                        { categories[tools][paintTool].Add(item); other = false; } // 油漆工具

                        // 盔甲
                        if (item.headSlot != -1 && !item.vanity)
                        { categories[armor][head].Add(item); other = false; } // 头部
                        if (item.bodySlot != -1 && !item.vanity)
                        { categories[armor][body].Add(item); other = false; } // 身体
                        if (item.legSlot != -1 && !item.vanity)
                        { categories[armor][legs].Add(item); other = false; } // 腿部
                        if (item.vanity)
                        { categories[armor][vanity].Add(item); other = false; } // 时装

                        // 饰品
                        if (item.accessory && item.wingSlot <= 0)
                        { categories[accessories][aaccessories].Add(item); other = false; } // 饰品
                        if (item.accessory && item.wingSlot > 0)
                        { categories[accessories][wings].Add(item); other = false; } // 翅膀
                        if (Main.projHook[item.shoot])
                        { categories[accessories][hooks].Add(item); other = false; } // 钩爪
                        if (Main.vanityPet[item.buffType])
                        { categories[accessories][pets].Add(item); other = false; } // 宠物
                        if (Main.lightPet[item.buffType])
                        { categories[accessories][lightPets].Add(item); other = false; } // 照明宠物
                        if (item.mountType != -1 && !MountID.Sets.Cart[item.mountType])
                        { categories[accessories][mounts].Add(item); other = false; } // 坐骑
                        if (item.mountType != -1 && MountID.Sets.Cart[item.mountType])
                        { categories[accessories][carts].Add(item); other = false; } // 矿车

                        // 放置物
                        bool isOtherFurniture = true;
                        if (item.createTile != -1 && !Main.tileFrameImportant[item.createTile] && !(item.tileWand != -1 || ItemID.Sets.AlsoABuildingItem[item.type] || item.type == 5295))
                        { categories[placeables][tiles].Add(item); other = false; isOtherFurniture = false; } // 物块
                        if (item.createWall != -1)
                        { categories[placeables][walls].Add(item); other = false; isOtherFurniture = false; } // 墙壁
                        if (item.createTile != -1 && item.rare == -13)
                        { categories[placeables][relic].Add(item); other = false; isOtherFurniture = false; } // 圣物
                        if (ItemID.Sets.Torches[item.type] || itemName.Contains("Torch"))
                        { categories[placeables][torches].Add(item); other = false; isOtherFurniture = false; } // 火把
                        if (ItemID.Sets.Campfires[item.type] || itemName.Contains("Campfire"))
                        { categories[placeables][campfires].Add(item); other = false; isOtherFurniture = false; } // 篝火
                        if (workbenchesList.Contains((short)item.type) || itemName.Contains("Workbench"))
                        { categories[placeables][workbenches].Add(item); other = false; isOtherFurniture = false; } // 工作台
                        if ((item.createTile != -1 && (TileID.Sets.BasicChest[item.createTile] || TileID.Sets.IsAContainer[item.createTile])) || (itemName.Contains("Chest") && !itemName.Contains("Fake_")))
                        { categories[placeables][chests].Add(item); other = false; isOtherFurniture = false; } // 箱子
                        if ((item.createTile != -1 && TileID.Sets.BasicChestFake[item.createTile]) || itemName.Contains("Fake_"))
                        { categories[placeables][trappedChests].Add(item); other = false; isOtherFurniture = false; } // 陷阱箱
                        if ((item.createTile != -1 && TileID.Sets.BasicDresser[item.createTile]) || itemName.Contains("Dresser"))
                        { categories[placeables][dressers].Add(item); other = false; isOtherFurniture = false; } // 梳妆台
                        if (item.createTile != -1 && TileID.Sets.Boulders[item.createTile])
                        { categories[placeables][boulders].Add(item); other = false; isOtherFurniture = false; } // 巨石
                        if (item.createTile != -1 && TileID.Sets.CanBeSatOnForPlayers[item.createTile] || itemName.Contains("Chair"))
                        { categories[placeables][chairs].Add(item); other = false; isOtherFurniture = false; } // 椅子
                        if ((item.createTile != -1 && TileID.Sets.CanBeSleptIn[item.createTile]) || itemName.Contains("Bed"))
                        { categories[placeables][beds].Add(item); other = false; isOtherFurniture = false; } // 床
                        if ((item.createTile != -1 && TileID.Sets.CritterCageLidStyle[item.createTile] != -1) || itemName.Contains("Jar") || item.createTile == 619 || item.createTile == 620 || item.createTile == 580 || item.createTile == 581 || item.createTile == 582 || item.createTile == 568 || item.createTile == 569 || item.createTile == 570 || (item.createTile >= 521 && item.createTile <= 527) || (item.createTile >= 358 && item.createTile <= 364) || (item.createTile >= 316 && item.createTile <= 318) || (item.createTile >= 288 && item.createTile <= 299) || (item.createTile >= 285 && item.createTile <= 286) || (item.createTile >= 275 && item.createTile <= 282) || (item.createTile >= 309 && item.createTile <= 310) || item.createTile == 339 || (item.createTile >= 391 && item.createTile <= 394) || (item.createTile >= 413 && item.createTile <= 414) || item.createTile == 532 || item.createTile == 533 || item.createTile == 538 || (item.createTile >= 542 && item.createTile <= 544) || (item.createTile >= 550 && item.createTile <= 559 && item.createTile != 552 && item.createTile != 557) || (item.createTile >= 598 && item.createTile <= 612))
                        { categories[placeables][critterCages].Add(item); other = false; isOtherFurniture = false; } // 小动物笼
                        if (item.createTile != -1 && TileID.Sets.Paintings[item.createTile])
                        { categories[placeables][paintings].Add(item); other = false; isOtherFurniture = false; } // 画
                        if ((item.createTile != -1 && TileID.Sets.Platforms[item.createTile]) || itemName.Contains("Platform"))
                        { categories[placeables][platforms].Add(item); other = false; isOtherFurniture = false; } // 平台
                        if ((item.createTile == 10 || item.createTile == 11 || item.createTile == 386 || item.createTile == 387 || item.createTile == 388) || itemName.Contains("Door"))
                        { categories[placeables][doors].Add(item); other = false; isOtherFurniture = false; } // 门
                        if (item.createTile == 14 || item.createTile == 469 || itemName.Contains("Table"))
                        { categories[placeables][tables].Add(item); other = false; isOtherFurniture = false; } // 桌子
                        if (item.createTile != -1 && TileID.Sets.CanBeSatOnForPlayers[item.createTile] || itemName.Contains("Toilet"))
                        { categories[placeables][toilets].Add(item); other = false; isOtherFurniture = false; } // 马桶
                        if (item.createTile == 33 || item.createTile == 174 || itemName.Contains("Candle"))
                        { categories[placeables][candles].Add(item); other = false; isOtherFurniture = false; } // 蜡烛
                        if (item.createTile == 34 || itemName.Contains("Chandelier"))
                        { categories[placeables][chandeliers].Add(item); other = false; isOtherFurniture = false; } // 吊灯
                        if (item.createTile == 35 || item.createTile == 42 || item.createTile == 95 || item.createTile == 98 || item.createTile == 126 || itemName.Contains("Lantern"))
                        { categories[placeables][lanterns].Add(item); other = false; isOtherFurniture = false; } // 灯笼
                        if (item.createTile == 87 || itemName.Contains("Piano"))
                        { categories[placeables][pianos].Add(item); other = false; isOtherFurniture = false; } // 钢琴
                        if (item.createTile == 89 || itemName.Contains("Sofa"))
                        { categories[placeables][sofas].Add(item); other = false; isOtherFurniture = false; } // 沙发
                        if (item.createTile == 90 || itemName.Contains("Bathtub"))
                        { categories[placeables][bathtubs].Add(item); other = false; isOtherFurniture = false; } // 浴缸
                        if (item.createTile == 91)
                        { categories[placeables][banners].Add(item); other = false; isOtherFurniture = false; } // 旗
                        if (item.createTile == 92 || item.createTile == 93 || itemName.Contains("Lamp"))
                        { categories[placeables][lamps].Add(item); other = false; isOtherFurniture = false; } // 灯
                        if (item.createTile == 100 || item.createTile == 173 || itemName.Contains("Candelabra"))
                        { categories[placeables][candelabras].Add(item); other = false; isOtherFurniture = false; } // 烛台
                        if (item.createTile == 101 || itemName.Contains("Bookcase"))
                        { categories[placeables][bookcases].Add(item); other = false; isOtherFurniture = false; } // 书架
                        if (item.createTile == 104 || itemName.Contains("Clock"))
                        { categories[placeables][clocks].Add(item); other = false; isOtherFurniture = false; } // 时钟
                        if ((item.createTile == 105 || item.createTile == 337 || item.createTile == 349) && !(item.type >= 1408 && item.type <= 1410) || itemName.Contains("Statue"))
                        { categories[placeables][statues].Add(item); other = false; isOtherFurniture = false; } // 雕像
                        if (item.createTile == 139 || item.type == 5638 || item.type == 5639 || item.type == 6144 || (item.type >= 5578 && item.type <= 5582) || itemName.Contains("MusicBox"))
                        { categories[placeables][musicBoxes].Add(item); other = false; isOtherFurniture = false; } // 八音盒
                        if (item.createTile == 172 || itemName.Contains("Sink"))
                        { categories[placeables][sinks].Add(item); other = false; isOtherFurniture = false; } // 水槽
                        if (item.createTile == 207 || itemName.Contains("Fountain"))
                        { categories[placeables][fountains].Add(item); other = false; isOtherFurniture = false; } // 喷泉
                        if (item.createTile == 597 || itemName.Contains("TeleportationPylon"))
                        { categories[placeables][pylons].Add(item); other = false; isOtherFurniture = false; } // 晶塔
                        if (item.createTile == 85 || itemName.Contains("Gravestone"))
                        { categories[placeables][tombstones].Add(item); other = false; isOtherFurniture = false; } // 墓碑
                        if (itemName.Contains("Potted") || item.createTile == 380 || item.createTile == 78 || item.createTile == 547 || item.createTile == 548 || item.createTile == 591 || item.createTile == 592 || item.createTile == 613 || item.createTile == 614 || item.createTile == 615 || item.createTile == 623)
                        { categories[placeables][pot].Add(item); other = false; isOtherFurniture = false; } // 花盆
                        if (item.createTile == 239)
                        { categories[placeables][metalBars].Add(item); other = false; isOtherFurniture = false; } // 矿物锭
                        if (ItemID.Sets.IsFishingCrate[item.type])
                        { categories[placeables][fishingCrate].Add(item); other = false; isOtherFurniture = false; } // 宝匣
                        if ((item.createTile != -1 && TileID.Sets.Gems[item.createTile]) || item.createTile == 178)
                        { categories[placeables][gems].Add(item); other = false; isOtherFurniture = false; } // 宝石
                        if (item.createTile != -1 && isGolf)
                        { categories[placeables][golf].Add(item); other = false; isOtherFurniture = false; } // 高尔夫
                        if (item.createTile != -1 && ItemID.Sets.IsAKite[item.type])
                        { categories[placeables][kites].Add(item); other = false; isOtherFurniture = false; } // 风筝
                        if (item.createTile != -1 && (consumablesEnvironmentAdded[item.type] || ItemID.Sets.SortingPriorityTerraforming[item.type] != -1 || ItemID.Sets.Moss[item.type] || ItemID.Sets.GrassSeeds[item.type]))
                        { categories[placeables][environment].Add(item); other = false; isOtherFurniture = false; } // 环境
                        if (item.tileWand != -1 || ItemID.Sets.AlsoABuildingItem[item.type] || item.type == 5295)
                        { categories[placeables][pother].Add(item); other = false; isOtherFurniture = false; } // 其他
                        if ((ItemID.Sets.SortingPriorityWiring[item.type] != -1 || ItemID.Sets.IsWireableStatue[item.type] || (item.createTile != -1 && isWiring[item.createTile]) || isWiring2[item.type]) && isOtherFurniture)
                        { categories[placeables][wiring].Add(item); other = false; isOtherFurniture = false; } // 电路
                        if (item.createTile != -1 && Main.tileFrameImportant[item.createTile] && isOtherFurniture)
                        { categories[placeables][furniture].Add(item); other = false; } // 家具

                        // 消耗品
                        bool isOtherConsumables = true;
                        if (item.ammo != 0 || item.type == ItemID.Seed)
                        { categories[consumables][ammo].Add(item); other = false; isOtherConsumables = false; } // 弹药
                        if (isPotions)
                        { categories[consumables][potions].Add(item); other = false; isOtherConsumables = false; } // 药水
                        if (ItemID.Sets.IsFood[item.type])
                        { categories[consumables][food].Add(item); other = false; isOtherConsumables = false; } // 食物
                        if (item.bait > 0)
                        { categories[consumables][bait].Add(item); other = false; isOtherConsumables = false; } // 鱼饵
                        if (ItemID.Sets.IsBasicFish[item.type])
                        { categories[consumables][fish].Add(item); other = false; isOtherConsumables = false; } // 鱼
                        if (item.questItem)
                        { categories[consumables][questFish].Add(item); other = false; isOtherConsumables = false; } // 任务鱼
                        if (ItemID.Sets.IsFishingCrate[item.type])
                        { categories[consumables][fishingCrate].Add(item); other = false; isOtherConsumables = false; } // 宝匣
                        if (ItemID.Sets.IsAPickup[item.type])
                        { /*categories[consumables][pickup].Add(item);*/ other = false; isOtherConsumables = false; } // 拾取物
                        if (item.paint > 0 || item.type == ItemID.GlowPaint || item.type == ItemID.EchoCoating)
                        { categories[consumables][paint].Add(item); other = false; isOtherConsumables = false; } // 油漆
                        if (isBossSummons)
                        { categories[consumables][bossSummons].Add(item); other = false; isOtherConsumables = false; } // Boss召唤物
                        if (ItemID.Sets.BossBag[item.type] && item.expert)
                        { categories[consumables][bossBag].Add(item); other = false; isOtherConsumables = false; } // 宝藏袋
                        if (consumablesEnvironmentAdded[item.type] || ItemID.Sets.SortingPriorityTerraforming[item.type] != -1 || ItemID.Sets.Moss[item.type] || ItemID.Sets.GrassSeeds[item.type])
                        { categories[consumables][environment].Add(item); other = false; isOtherConsumables = false; } // 环境
                        if ((isConsumable && isOtherConsumables) || consumablesOtherAdded[item.type])
                        { categories[consumables][cother].Add(item); other = false; } // 其他

                        // 首领
                        if (isBossSummons)
                        { categories[bossAndFish][bossSummons].Add(item); other = false; } // Boss召唤物
                        if (ItemID.Sets.BossBag[item.type] && item.expert)
                        { categories[bossAndFish][bossBag].Add(item); other = false; } // 宝藏袋
                        if (item.createTile != -1 && item.rare == -13)
                        { categories[bossAndFish][relic].Add(item); other = false; } // 圣物
                        if (item.fishingPole > 0)
                        { categories[bossAndFish][poles].Add(item); other = false; } // 鱼竿
                        if (item.bait > 0)
                        { categories[bossAndFish][bait].Add(item); other = false; } // 鱼饵
                        if (item.questItem)
                        { categories[bossAndFish][questFish].Add(item); other = false; } // 任务鱼
                        if (ItemID.Sets.IsFishingCrate[item.type])
                        { categories[bossAndFish][fishingCrate].Add(item); other = false; } // 宝匣

                        // 其它
                        if (item.material)
                        { categories[others][materials].Add(item); /* other = false; */ } // 材料
                        if (ItemID.Sets.IsAKite[item.type])
                        { categories[others][kites].Add(item); other = false; } // 风筝
                        if (item.dye != 0)
                        { categories[others][dyes].Add(item); other = false; } // 染料
                        if (ItemID.Sets.SortingPriorityMiscImportants[item.type] != -1)
                        { categories[others][gameplay].Add(item); other = false; } // 娱乐
                        if (isGolf)
                        { categories[others][golf].Add(item); other = false; } // 高尔夫
                        if (other)
                        { categories[others][oothers].Add(item); } // 其他
                    }
                }

                // 排序
                foreach (var category in categories)
                {
                    foreach (var subCategory in category.Value)
                    {
                        // 大类排序
                        if (category.Key == weapons)
                        {
                            subCategory.Value.Sort((x, y) => x.damage.CompareTo(y.damage));
                        }
                        else if (category.Key == armor)
                        {
                            subCategory.Value.Sort((x, y) => x.defense.CompareTo(y.defense));
                        }
                        else if (category.Key == tiles)
                        {
                            subCategory.Value.Sort((x, y) =>
                            {
                                if (x.createTile != -1 && y.createTile != -1)
                                    return x.createTile.CompareTo(y.createTile);
                                else if (x.createWall != -1 && y.createWall != -1)
                                    return x.createWall.CompareTo(y.createWall);
                                else
                                    return x.type.CompareTo(y.type);
                            });
                        }
                        // 子类排序
                        if (subCategory.Key == pickaxes)
                        {
                            subCategory.Value.Sort((x, y) => x.pick.CompareTo(y.pick));
                        }
                        else if (subCategory.Key == axes)
                        {
                            subCategory.Value.Sort((x, y) => x.axe.CompareTo(y.axe));
                        }
                        else if (subCategory.Key == hammers)
                        {
                            subCategory.Value.Sort((x, y) => x.hammer.CompareTo(y.hammer));
                        }
                        else if (subCategory.Key == drill)
                        {
                            subCategory.Value.Sort((x, y) => x.pick.CompareTo(y.pick));
                        }
                        else if (subCategory.Key == chainsaw)
                        {
                            subCategory.Value.Sort((x, y) => x.axe.CompareTo(y.axe));
                        }
                        else if (subCategory.Key == wings)
                        {
                            subCategory.Value.Sort((x, y) => x.wingSlot.CompareTo(y.wingSlot));
                        }
                        else if (subCategory.Key == ammo)
                        {
                            subCategory.Value.Sort((x, y) => x.ammo.CompareTo(y.ammo));
                        }
                        else if (subCategory.Key == food)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.IsFood[x.type].CompareTo(ItemID.Sets.IsFood[y.type]));
                        }
                        else if (subCategory.Key == bait)
                        {
                            subCategory.Value.Sort((x, y) => x.bait.CompareTo(y.bait));
                        }
                        else if (subCategory.Key == poles)
                        {
                            subCategory.Value.Sort((x, y) => x.fishingPole.CompareTo(y.fishingPole));
                        }
                        else if (subCategory.Key == bossSummons)
                        {
                            subCategory.Value.Sort((x, y) => sortingPriorityBossSpawns[x.type].CompareTo(sortingPriorityBossSpawns[y.type]));
                        }
                        else if (subCategory.Key == bossBag)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.BossBag[x.type].CompareTo(ItemID.Sets.BossBag[y.type]));
                        }
                        else if (subCategory.Key == paint)
                        {
                            subCategory.Value.Sort((x, y) => x.paint.CompareTo(y.paint));
                        }
                        else if (subCategory.Key == dyes)
                        {
                            subCategory.Value.Sort((x, y) => x.dye.CompareTo(y.dye));
                        }
                        else if (subCategory.Key == golf)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.SortingPriorityToolsGolf[x.type].CompareTo(ItemID.Sets.SortingPriorityToolsGolf[y.type]));
                        }
                        else if (subCategory.Key == kites)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.SortingPriorityToolsKites[x.type].CompareTo(ItemID.Sets.SortingPriorityToolsKites[y.type]));
                        }
                        else if (subCategory.Key == potions)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.SortingPriorityPotionsBuffs[x.type].CompareTo(ItemID.Sets.SortingPriorityPotionsBuffs[y.type]));
                        }
                        else if (subCategory.Key == wiring)
                        {
                            subCategory.Value.Sort((x, y) => ItemID.Sets.SortingPriorityWiring[x.type].CompareTo(ItemID.Sets.SortingPriorityWiring[y.type]));
                        }
                    }
                }

                // 写入
                var writer = new StreamWriter("categories.lua");
                writer.WriteLine("Categories = {");
                foreach (var categoryName in orderList)
                {
                    var category = categories[categoryName];

                    writer.WriteLine("\t{");
                    writer.WriteLine("\t\tName = \"" + categoryName + "\",");
                    writer.WriteLine("\t\tSubs = {");

                    var subCategories = category.ToList().OrderByDescending(s => s.Value.Count);
                    foreach (var subCategory in subCategories)
                    {
                        writer.WriteLine("\t\t\t{");
                        writer.WriteLine("\t\t\t\tName = \"" + subCategory.Key + "\",");
                        writer.WriteLine("\t\t\t\tItems = {" + string.Join(", ", subCategory.Value.Select(i => i.type.ToString())) + "},");
                        writer.WriteLine("\t\t\t},");
                    }
                    writer.WriteLine("\t\t},");
                    writer.WriteLine("\t},");
                }
                writer.WriteLine("}");
                writer.Close();

                Main.NewText("Success!");
                return;
            }
            // ###################################################################################
            IChatCommand chatCommand;
            if (this._commands.TryGetValue(message.CommandId, out chatCommand))
            {
                chatCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
                message.Consume();
                return;
            }

            if (this._defaultCommand != null)
            {
                this._defaultCommand.ProcessIncomingMessage(message.Text, (byte)clientId);
                message.Consume();
            }
        }
        
        private class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new()
        {
            public new TValue this[TKey key]
            {
                get
                {
                    if (!TryGetValue(key, out var val))
                    {
                        val = new TValue();
                        Add(key, val);
                    }
                    return val;
                }
                set => base[key] = value;
            }
        }
    }
}