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
                string weapons = "Weapons 武器", tools = "Tools 工具", armor = "Armor 盔甲", accessories = "Accessories 饰品", placeables = "Placeables 放置物", consumables = "Consumables 消耗品", boss = "Boss 老板", others = "Others 其他";
                string melee = "Melee 近战", magic = "Magic 魔法", ranged = "Ranged 远程", summon = "Summon 召唤", whips = "Whips 鞭子", throwing = "Throwing 投掷", sentry = "Sentry 炮塔";
                string pickaxes = "Pickaxes 镐子", axes = "Axes 斧头", hammers = "Hammers 锤子", drill = "Drill 钻头", chainsaw = "Chainsaw 链锯";
                string head = "Head 头部", body = "Body 身体", legs = "Legs 腿部", vanity = "Vanity 时装";
                string aaccessories = "Accessories 饰品", wings = "Wings 翅膀", hooks = "Hooks 钩爪", pets = "Pets 宠物", lightPets = "LightPets 照明宠物", mounts = "Mounts 坐骑", carts = "Carts 矿车";
                string tiles = "Tiles 物块", walls = "Walls 墙壁", furniture = "Furniture 家具";
                string ammo = "Ammo 弹药", potions = "Potions 药水", food = "Food 食物", pickup = "Pickup 拾取物";
                string bossSummons = "BossSummons 老板召唤物", bossBag = "BossBag 宝藏袋", bait = "Bait 鱼饵", questFish = "QuestFish 任务鱼", relic = "Relic 圣物", fishingCrate = "FishingCrate 宝匣";
                string materials = "Materials 材料", kites = "Kites 风筝", poles = "Poles 鱼竿", paint = "Paint 油漆", dyes = "Dyes 染料", oothers = "Others 其它";

                var categories = new DefaultDictionary<string, DefaultDictionary<string, List<Item>>>();

                bool[] spears = ItemID.Sets.Factory.CreateBoolSet(ItemID.Spear, ItemID.Trident, ItemID.Swordfish, ItemID.ThunderSpear, ItemID.TheRottedFork, ItemID.DarkLance, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.ObsidianSwordfish, ItemID.Gungnir, ItemID.MushroomSpear, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.MonkStaffT3, ItemID.ChlorophytePartisan, ItemID.NorthPole);
                int[] sortingPriorityBossSpawns = ItemID.Sets.Factory.CreateIntSet(-1, 43, 1, 560, 2, 70, 3, 1331, 3, 361, 4, 5120, 5, 1133, 5, 4988, 6, 5334, 7, 544, 8, 556, 9, 557, 10, ItemID.PirateMap, 11, 2673, 12, 602, 13, 1844, 14, 1958, 15, 1293, 16, 2767, 17, 4271, 18, 3601, 19, 1291, 20, 109, 21, 29, 22, 50, 23, 3199, 24, 3124, 25, 5437, 26, 5358, 27, 5359, 28, 5360, 29, 5361, 30, 4263, 31, 4819, 32);
                List<int> sortingPriorityBossSpawnsExclusions = new List<int> { ItemID.LifeCrystal, ItemID.ManaCrystal, ItemID.CellPhone, ItemID.IceMirror, ItemID.MagicMirror, ItemID.LifeFruit, ItemID.TreasureMap, ItemID.Shellphone, ItemID.ShellphoneDummy, ItemID.ShellphoneHell, ItemID.ShellphoneOcean, ItemID.ShellphoneSpawn, ItemID.MagicConch, ItemID.DemonConch };

                // 初始化
                Main.NewText("Load");
                for (int id = 1; id < ItemID.Count; id++)
                {
                    if (!ItemID.Sets.Deprecated[id])
                    {
                        Item item = new Item();
                        item.SetDefaults(id);
                        bool other = true;

                        bool isTool = item.pick > 0 || item.axe > 0 || item.hammer > 0 || item.type == ItemID.GravediggerShovel;
                        bool meleeNoSpeed = item.noMelee && (item.melee && (item.shoot > 0 && !spears[item.type] && !item.shootsEveryUse || isTool));

                        // 武器
                        if (item.melee && !isTool)
                        { categories[weapons][melee].Add(item); other = false; } // 近战
                        if (item.magic)
                        { categories[weapons][magic].Add(item); other = false; } // 魔法
                        if (item.ranged && item.ammo == 0)
                        { categories[weapons][ranged].Add(item); other = false; } // 远程
                        if (item.summon && !meleeNoSpeed && !item.sentry)
                        { categories[weapons][summon].Add(item); other = false; } // 召唤
                        if (meleeNoSpeed && !item.sentry)
                        { categories[weapons][whips].Add(item); other = false; } // 鞭子
                        if (item.noMelee && item.ranged && item.consumable)
                        { categories[weapons][throwing].Add(item); other = false; } // 投掷 
                        if (item.summon && item.sentry)
                        { categories[weapons][sentry].Add(item); other = false; } // 炮塔

                        // 工具
                        if (item.pick > 0 && !ItemID.Sets.IsDrill[item.type])
                        { categories[tools][pickaxes].Add(item); other = false; } // 镐子
                        if (item.axe > 0 && !ItemID.Sets.IsChainsaw[item.type])
                        { categories[tools][axes].Add(item); other = false; } // 斧头
                        if (item.hammer > 0)
                        { categories[tools][hammers].Add(item); other = false; } // 锤子
                        if (ItemID.Sets.IsDrill[item.type])
                        { categories[tools][drill].Add(item); other = false; } // 钻头
                        if (ItemID.Sets.IsChainsaw[item.type])
                        { categories[tools][chainsaw].Add(item); other = false; } // 链锯

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

                        // 消耗品
                        if (item.ammo != 0)
                        { categories[consumables][ammo].Add(item); other = false; } // 弹药
                        if (item.UseSound == SoundID.Item3 && !ItemID.Sets.IsFood[item.type])
                        { categories[consumables][potions].Add(item); other = false; } // 药水
                        if (ItemID.Sets.IsFood[item.type])
                        { categories[consumables][food].Add(item); other = false; } // 食物
                        if (ItemID.Sets.IsAPickup[item.type])
                        { categories[consumables][pickup].Add(item); other = false; } // 拾取物

                        // 老板
                        if (sortingPriorityBossSpawns[item.type] != -1 && !sortingPriorityBossSpawnsExclusions.Contains(item.type) || item.type == ItemID.PirateMap || item.type == ItemID.SnowGlobe || item.type == ItemID.DD2ElderCrystal)
                        { categories[boss][bossSummons].Add(item); other = false; } // Boss召唤物
                        if (ItemID.Sets.BossBag[item.type] && item.expert)
                        { categories[boss][bossBag].Add(item); other = false; } // 宝藏袋
                        if (item.createTile != -1 && item.rare == -13)
                        { categories[boss][relic].Add(item); other = false; } // 圣物
                        if (item.fishingPole > 0)
                        { categories[boss][poles].Add(item); other = false; } // 鱼竿
                        if (item.bait > 0)
                        { categories[boss][bait].Add(item); other = false; } // 鱼饵
                        if (item.questItem)
                        { categories[boss][questFish].Add(item); other = false; } // 任务鱼
                        if (ItemID.Sets.IsFishingCrate[item.type])
                        { categories[boss][fishingCrate].Add(item); other = false; } // 宝匣

                        // 其它
                        if (item.material)
                        { categories[others][materials].Add(item); other = false; } // 材料
                        if (ItemID.Sets.IsAKite[item.type])
                        { categories[others][kites].Add(item); other = false; } // 风筝
                        if (item.paint > 0 || ItemID.Sets.IsPaintScraper[item.type])
                        { categories[others][paint].Add(item); other = false; } // 油漆
                        if (item.dye != 0)
                        { categories[others][dyes].Add(item); other = false; } // 染料
                        if (other)
                        { categories[others][oothers].Add(item); } // 其他
                    }
                }

                // 排序
                Main.NewText("Sort");
                foreach (var category in categories)
                {
                    foreach (var subCategory in category.Value)
                    {
                        if (category.Key == weapons)
                        {
                            subCategory.Value.Sort((x, y) => x.damage.CompareTo(y.damage));
                        }
                        else if (category.Key == tools)
                        {
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
                        }
                        else if (category.Key == armor)
                        {
                            subCategory.Value.Sort((x, y) => x.defense.CompareTo(y.defense));
                        }
                        else if (category.Key == accessories)
                        {
                            if (subCategory.Key == wings)
                            {
                                subCategory.Value.Sort((x, y) => x.wingSlot.CompareTo(y.wingSlot));
                            }
                        }
                        else if (category.Key == placeables)
                        {
                        }
                        else if (category.Key == consumables)
                        {
                            if (subCategory.Key == ammo)
                            {
                                subCategory.Value.Sort((x, y) => x.ammo.CompareTo(y.ammo));
                            }
                            else if (subCategory.Key == food)
                            {
                                subCategory.Value.Sort((x, y) => ItemID.Sets.IsFood[x.type].CompareTo(ItemID.Sets.IsFood[y.type]));
                            }
                        }
                        else if (category.Key == boss)
                        {
                            if (subCategory.Key == poles)
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
                            else if (subCategory.Key == bait)
                            {
                                subCategory.Value.Sort((x, y) => x.bait.CompareTo(y.bait));
                            }
                        }
                        else if (category.Key == others)
                        {
                        }
                    }
                }

                // 写入
                Main.NewText("Write");
                var writer = new StreamWriter("categories.lua");
                writer.WriteLine("Categories = {");
                foreach (var category in categories)
                {
                    writer.WriteLine("\t{");
                    writer.WriteLine("\t\tName = \"" + category.Key + "\",");
                    writer.WriteLine("\t\tSubs = {");

                    var subCategories = category.Value.ToList().OrderByDescending(s => s.Value.Count);
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