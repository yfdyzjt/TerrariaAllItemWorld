// ###################################################################################
// Terraria.Chat > ChatCommandProcessor > ProcessIncomingMessage (OR CreateOutgoingMessage)
using System;
using System.Collections.Generic;
using Terraria.Chat.Commands;
using Terraria.Localization;
// ###################################################################################
using Terraria.GameContent;
using Terraria.ID;
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
                var category = new Dictionary<string, Dictionary<string, List<Item>>>();

                bool[] spears = ItemID.Sets.Factory.CreateBoolSet(ItemID.Spear, ItemID.Trident, ItemID.Swordfish, ItemID.ThunderSpear, ItemID.TheRottedFork, ItemID.DarkLance, ItemID.CobaltNaginata, ItemID.PalladiumPike, ItemID.MythrilHalberd, ItemID.OrichalcumHalberd, ItemID.AdamantiteGlaive, ItemID.TitaniumTrident, ItemID.ObsidianSwordfish, ItemID.Gungnir, ItemID.MushroomSpear, ItemID.MonkStaffT1, ItemID.MonkStaffT2, ItemID.MonkStaffT3, ItemID.ChlorophytePartisan, ItemID.NorthPole);
                int[] sortingPriorityBossSpawns = ItemID.Sets.Factory.CreateIntSet(-1, 43, 1, 560, 2, 70, 3, 1331, 3, 361, 4, 5120, 5, 1133, 5, 4988, 6, 5334, 7, 544, 8, 556, 9, 557, 10, ItemID.PirateMap, 11, 2673, 12, 602, 13, 1844, 14, 1958, 15, 1293, 16, 2767, 17, 4271, 18, 3601, 19, 1291, 20, 109, 21, 29, 22, 50, 23, 3199, 24, 3124, 25, 5437, 26, 5358, 27, 5359, 28, 5360, 29, 5361, 30, 4263, 31, 4819, 32);
                List<int> sortingPriorityBossSpawnsExclusions = new List<int> { ItemID.LifeCrystal, ItemID.ManaCrystal, ItemID.CellPhone, ItemID.IceMirror, ItemID.MagicMirror, ItemID.LifeFruit, ItemID.TreasureMap, ItemID.Shellphone, ItemID.ShellphoneDummy, ItemID.ShellphoneHell, ItemID.ShellphoneOcean, ItemID.ShellphoneSpawn, ItemID.MagicConch, ItemID.DemonConch };

                for (int id = 1; id < TextureAssets.Item.Length; id++)
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
                        { category["Weapons"]["Melee"].Add(item); other = false; } // 近战
                        if (item.magic)
                        { category["Weapons"]["Magic"].Add(item); other = false; } // 魔法
                        if (item.ranged && item.ammo == 0)
                        { category["Weapons"]["Ranged"].Add(item); other = false; } // 远程
                        if (item.summon && !meleeNoSpeed && !item.sentry)
                        { category["Weapons"]["Summon"].Add(item); other = false; } // 召唤
                        if (meleeNoSpeed && !item.sentry)
                        { category["Weapons"]["Whips"].Add(item); other = false; } // 鞭子
                        if (item.noMelee && item.ranged && item.consumable)
                        { category["Weapons"]["Throwing"].Add(item); other = false; } // 投掷 
                        if (item.summon && item.sentry)
                        { category["Weapons"]["Sentry"].Add(item); other = false; } // 炮塔

                        // 工具
                        if (item.pick > 0 && !ItemID.Sets.IsDrill[item.type])
                        { category["Tools"]["Pickaxes"].Add(item); other = false; } // 镐子
                        if (item.axe > 0 && !ItemID.Sets.IsChainsaw[item.type])
                        { category["Tools"]["Axes"].Add(item); other = false; } // 斧头
                        if (item.hammer > 0)
                        { category["Tools"]["Hammers"].Add(item); other = false; } // 锤子
                        if (ItemID.Sets.IsDrill[item.type])
                        { category["Tools"]["Drill"].Add(item); other = false; } // 钻头
                        if (ItemID.Sets.IsChainsaw[item.type])
                        { category["Tools"]["Chainsaw"].Add(item); other = false; } // 链锯

                        // 盔甲
                        if (item.headSlot != -1 && !item.vanity)
                        { category["Armor"]["Head"].Add(item); other = false; } // 头部
                        if (item.bodySlot != -1 && !item.vanity)
                        { category["Armor"]["Body"].Add(item); other = false; } // 身体
                        if (item.legSlot != -1 && !item.vanity)
                        { category["Armor"]["Legs"].Add(item); other = false; } // 腿部
                        if (item.vanity)
                        { category["Armor"]["Vanity"].Add(item); other = false; } // 时装

                        // 放置物
                        if (item.createTile != -1 && !Main.tileFrameImportant[item.createTile])
                        { category["Placeables"]["Tiles"].Add(item); other = false; } // 物块
                        if (item.createWall != -1)
                        { category["Placeables"]["Walls"].Add(item); other = false; } // 墙壁
                        if (item.createTile != -1 && Main.tileFrameImportant[item.createTile])
                        { category["Placeables"]["Furniture"].Add(item); other = false; } // 家具
                        if (ItemID.Sets.IsFishingCrate[item.type])
                        { category["Placeables"]["FishingCrate"].Add(item); other = false; } // 宝匣
                        if (item.createTile != -1 && item.expert)
                        { category["Placeables"]["Relic"].Add(item); other = false; } // 圣物

                        // 饰品
                        if (item.accessory && item.wingSlot <= 0)
                        { category["Accessories"]["Accessories"].Add(item); other = false; } // 饰品
                        if (item.accessory && item.wingSlot > 0)
                        { category["Accessories"]["Wings"].Add(item); other = false; } // 翅膀
                        if (Main.projHook[item.shoot])
                        { category["Accessories"]["Hooks"].Add(item); other = false; } // 钩爪
                        if (Main.vanityPet[item.buffType])
                        { category["Accessories"]["Pets"].Add(item); other = false; } // 宠物
                        if (Main.lightPet[item.buffType])
                        { category["Accessories"]["LightPets"].Add(item); other = false; } // 照明宠物
                        if (item.mountType != -1 && !MountID.Sets.Cart[item.mountType])
                        { category["Accessories"]["Mounts"].Add(item); other = false; } // 坐骑
                        if (item.mountType != -1 && MountID.Sets.Cart[item.mountType])
                        { category["Accessories"]["Carts"].Add(item); other = false; } // 矿车

                        // 消耗品
                        if (item.ammo != 0)
                        { category["Consumables"]["Ammo"].Add(item); other = false; } // 弹药
                        if (item.UseSound == SoundID.Item3 && !ItemID.Sets.IsFood[item.type])
                        { category["Consumables"]["Potions"].Add(item); other = false; } // 药水
                        if (ItemID.Sets.IsFood[item.type])
                        { category["Consumables"]["Food"].Add(item); other = false; } // 食物
                        if (sortingPriorityBossSpawns[item.type] != -1 && !sortingPriorityBossSpawnsExclusions.Contains(item.type) || item.type == ItemID.PirateMap || item.type == ItemID.SnowGlobe || item.type == ItemID.DD2ElderCrystal)
                        { category["Consumables"]["BossSummons"].Add(item); other = false; } // Boss召唤物
                        if (ItemID.Sets.BossBag[item.type] && item.expert)
                        { category["Consumables"]["BossBag"].Add(item); other = false; } // 宝藏袋
                        if (item.bait > 0)
                        { category["Consumables"]["Bait"].Add(item); other = false; } // 鱼饵
                        if (item.questItem)
                        { category["Consumables"]["QuestFish"].Add(item); other = false; } // 任务鱼
                        if (ItemID.Sets.IsAPickup[item.type])
                        { category["Consumables"]["Pickup"].Add(item); other = false; } // 拾取物

                        // 其它
                        if (item.material)
                        { category["Other"]["Materials"].Add(item); other = false; } // 材料
                        if (ItemID.Sets.IsAKite[item.type])
                        { category["Other"]["Kites"].Add(item); other = false; } // 风筝
                        if (item.fishingPole > 0)
                        { category["Other"]["Poles"].Add(item); other = false; } // 鱼竿
                        if (item.paint > 0 || ItemID.Sets.IsPaintScraper[item.type])
                        { category["Other"]["Paint"].Add(item); other = false; } // 油漆
                        if (item.dye != 0)
                        { category["Other"]["Dyes"].Add(item); other = false; } // 染料
                        if (other)
                        { category["Other"]["Other"].Add(item); } // 其他
                    }
                }


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
    }
}