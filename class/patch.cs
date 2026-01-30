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
                string melee = "Melee 近战", magic = "Magic 魔法", ranged = "Ranged 远程", summon = "Summon 召唤", whips = "Whips 鞭子", throwing = "Throwing 投掷", sentry = "Sentry 炮塔";
                string pickaxes = "Pickaxes 镐子", axes = "Axes 斧头", hammers = "Hammers 锤子", drill = "Drill 钻头", chainsaw = "Chainsaw 链锯", tother = "Other 其他";
                string head = "Head 头部", body = "Body 身体", legs = "Legs 腿部", vanity = "Vanity 时装";
                string aaccessories = "Accessories 饰品", wings = "Wings 翅膀", hooks = "Hooks 钩爪", pets = "Pets 宠物", lightPets = "LightPets 照明宠物", mounts = "Mounts 坐骑", carts = "Carts 矿车";
                string tiles = "Tiles 物块", walls = "Walls 墙壁", furniture = "Furniture 家具", pother = "Other 其他";
                string ammo = "Ammo 弹药", potions = "Potions 药水", food = "Food 食物", pickup = "Pickup 拾取物", cother = "Other 其他", environment = "Environment";
                string bossSummons = "BossSummons 首领召唤物", bossBag = "BossBag 宝藏袋", bait = "Bait 鱼饵", questFish = "QuestFish 任务鱼", relic = "Relic 圣物", fishingCrate = "FishingCrate 宝匣", poles = "Poles 鱼竿";
                string materials = "Materials 材料", kites = "Kites 风筝", paint = "Paint 油漆", paintTool = "PaintTool 油漆工具", dyes = "Dyes 染料", oothers = "Games 娱乐", gameplay = "Others 其它";

                var orderList = new List<string>() { bossAndFish, accessories, armor, weapons, tools, placeables, consumables, others };

                var categories = new DefaultDictionary<string, DefaultDictionary<string, List<Item>>>();

                var isSpear = ItemID.Sets.Factory.CreateBoolSet(ItemID.Spear, ItemID.Trident, ItemID.Swordfish, ItemID.ThunderSpear, ItemID.TheRottedFork, ItemID.DarkLance, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.ObsidianSwordfish, ItemID.Gungnir, ItemID.MushroomSpear, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.MonkStaffT3, ItemID.ChlorophytePartisan, ItemID.NorthPole);
                var isWhip = ItemID.Sets.Factory.CreateBoolSet(4672, 4678, 4679, 4680, 4911, 4912, 4913, 4914, 5074, 5473, 5474, 5475, 5476, 5477, 5478, 5479, 5480, 5688);
                var _itemIdsThatAreAccepted = new HashSet<int> { 213, 5295, 509, 850, 851, 3612, 3625, 3611, 510, 849, 3620, 1071, 1543, 1072, 1544, 1100, 1545, 50, 3199, 3124, 5358, 5359, 5360, 5361, 5437, 1326, 5335, 3384, 4263, 4819, 4262, 946, 4707, 205, 206, 207, 1128, 3031, 4820, 5302, 5364, 4460, 4608, 4872, 3032, 5303, 5304, 1991, 4821, 3183, 779, 5134, 1299, 4711, 4049, 114, 5667 };
                var sortingPriorityBossSpawns = ItemID.Sets.Factory.CreateIntSet(-1, 43, 1, 560, 2, 70, 3, 1331, 3, 361, 4, 5120, 5, 1133, 5, 4988, 6, 5334, 7, 544, 8, 556, 9, 557, 10, ItemID.PirateMap, 11, 2673, 12, 602, 13, 1844, 14, 1958, 15, 1293, 16, 2767, 17, 4271, 18, 3601, 19, 1291, 20, 109, 21, 29, 22, 50, 23, 3199, 24, 3124, 25, 5437, 26, 5358, 27, 5359, 28, 5360, 29, 5361, 30, 4263, 31, 4819, 32);
                var sortingPriorityBossSpawnsExclusions = new List<int> { ItemID.LifeCrystal, ItemID.ManaCrystal, ItemID.CellPhone, ItemID.IceMirror, ItemID.MagicMirror, ItemID.LifeFruit, ItemID.TreasureMap, ItemID.Shellphone, ItemID.ShellphoneDummy, ItemID.ShellphoneHell, ItemID.ShellphoneOcean, ItemID.ShellphoneSpawn, ItemID.MagicConch, ItemID.DemonConch };
                var consumablesOtherAdded = ItemID.Sets.Factory.CreateBoolSet(71, 72, 73, 74, 3822, 3817, 327, 329, 1141, 1169, 1533, 1534, 1535, 1536, 1537, 4714);
                var consumablesEnvironmentAdded = ItemID.Sets.Factory.CreateBoolSet(62, 59, 195, 194, 369, 2171, 5241, 307, 308, 309, 310, 311, 312, 2357, 4041, 4042, 4043, 4044, 4045, 4046, 4047, 4048, 4241, 1828, 27, 4851, 4852, 4853, 4854, 4855, 4856, 4857, 4400, 779, 5134, 780, 781, 782, 783, 784, 5392, 5393, 5394, 422, 423, 3477, 2886, 2887, 66, 67, 209, 3031, 3032, 3182, 4447, 4824, 4827, 3186, 207, 3184, 4448, 4820, 4872, 4825, 1128, 3185, 4449, 4826, 5302, 5303, 5364, 5304, 2015, 2016, 3194, 2019, 1338, 4838, 4839, 4840, 4841, 4842, 4843, 4844, 2017, 5312, 5313, 2123, 2122, 3191, 5350, 4070, 4069, 4068, 1992, 2121, 2007, 261, 2740, 4374, 3192, 4361, 4847, 2004, 5212, 5300, 4363, 4849, 2003, 4395, 2205, 4373, 4375, 2157, 2156, 4359, 4480, 3193, 2006, 2018, 3563, 4831, 4832, 4833, 4834, 4835, 4836, 4837, 5132, 5311, 4464, 4465, 4418, 2002, 2001, 1994, 1995, 1996, 1998, 1999, 1997, 2000, 4845, 4961, 4339, 4338, 4334, 4334, 4337, 4336, 2889, 2890, 2891, 4340, 2892, 4274, 2893, 4362, 2894, 4482, 3564, 4419, 2895, 4961, 2673);

                // 初始化
                for (int id = 1; id < ItemID.Count; id++)
                {
                    if (!ItemID.Sets.Deprecated[id])
                    {
                        Item item = new Item();
                        item.SetDefaults(id);
                        bool other = true;

                        bool isConsumable = (item.type == 267 || item.type == 1307) || (item.consumable && !(item.createTile != -1 || item.createWall != -1 || item.tileWand != -1));
                        bool isPotions = (item.UseSound == SoundID.Item3 && !ItemID.Sets.IsFood[item.type]) || item.type == ItemID.Mushroom || item.type == ItemID.LovePotion || item.type == ItemID.GenderChangePotion || item.type == ItemID.WormholePotion || item.type == ItemID.RecallPotion || item.type == ItemID.StinkPotion || item.type == ItemID.TeleportationPotion || item.type == ItemID.PotionOfReturn || item.type == ItemID.HerbBag;
                        bool isBossSummons = (sortingPriorityBossSpawns[item.type] != -1 && !sortingPriorityBossSpawnsExclusions.Contains(item.type)) || item.type == ItemID.PirateMap || item.type == ItemID.SnowGlobe || item.type == ItemID.DD2ElderCrystal || item.type == ItemID.GuideVoodooDoll || item.type == ItemID.ClothierVoodooDoll || item.type == ItemID.NightKey || item.type == ItemID.LightKey;
                        bool isTool = item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.fishingPole > 0 || item.tileWand != -1 || _itemIdsThatAreAccepted.Contains(item.type) || item.type == ItemID.GravediggerShovel || item.type == ItemID.SpectreGoggles || item.type == ItemID.ClosedVoidBag || item.type == ItemID.VoidLens || item.type == ItemID.MoneyTrough || item.type == ItemID.ChesterPetItem || item.type == ItemID.DontHurtCrittersBook || item.type == ItemID.DontHurtNatureBook || item.type == ItemID.DontHurtComboBook || item.type == ItemID.UncumberingStone || item.type == ItemID.EncumberingStone || item.type == ItemID.DirtRod;
                        bool isPaintScraper = item.type == ItemID.PaintSprayer || item.type == ItemID.PaintScraper || item.type == ItemID.Paintbrush || item.type == ItemID.PaintRoller || item.type == ItemID.SpectrePaintbrush || item.type == ItemID.SpectrePaintScraper || item.type == ItemID.SpectrePaintRoller;

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
                        if (item.createTile != -1 && !Main.tileFrameImportant[item.createTile])
                        { categories[placeables][tiles].Add(item); other = false; } // 物块
                        if (item.createWall != -1)
                        { categories[placeables][walls].Add(item); other = false; } // 墙壁
                        if (item.createTile != -1 && Main.tileFrameImportant[item.createTile])
                        { categories[placeables][furniture].Add(item); other = false; } // 家具
                        if (item.tileWand != -1)
                        { categories[placeables][pother].Add(item); other = false; } // 其他

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
                        if (consumablesEnvironmentAdded[item.type])
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
                        { categories[others][materials].Add(item); other = false; } // 材料
                        if (ItemID.Sets.IsAKite[item.type])
                        { categories[others][kites].Add(item); other = false; } // 风筝
                        if (item.dye != 0)
                        { categories[others][dyes].Add(item); other = false; } // 染料
                        if (ItemID.Sets.SortingPriorityMiscImportants[item.type] != -1)
                        { categories[others][gameplay].Add(item); other = false; } // 娱乐
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