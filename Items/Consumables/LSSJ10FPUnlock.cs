using DBTBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using DBTBalance.Helpers;
using DBTBalance.Model;
using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using Microsoft.Xna.Framework.Graphics;
using DBZMODPORT;
using System.Security.Cryptography.X509Certificates;
using K7DBTRF.Assets;
using DBZMODPORT.Items.Materials;
using DBZMODPORT.Items;
using DBZMODPORT.Items.DragonBalls;
using IL.Terraria.GameContent;
using Terraria.DataStructures;

namespace K7DBTRF.Items.Consumables
{
    public class LSSJ10FPUnlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voodoo Doll on a plate");
            Tooltip.SetDefault("Such macabre action would of course give you an humongous yet dangerous power, be careful how you use it");
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.potion = false;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.scale = 1f;
        }
        public override bool? UseItem(Player player)
        {
            var kplayer = player.GetModPlayer<KPlayer>();
            return !kplayer.LSSJ10FPAchieved || !kplayer.SSJ10FPAchieved && NPC.AnyNPCs(NPCID.Guide);
        }

        public override void OnConsumeItem(Player player)
        {
            var kplayer = player.GetModPlayer<KPlayer>();
            var isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";
            if (!kplayer.LSSJ9Achieved ^ !kplayer.SSJ9Achieved)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "could not attain the forbidden power and died"), 2147000000, 0, false);
            }
            else if (!kplayer.LSSJ10FPAchieved | !kplayer.SSJ10FPAchieved)
            {
                if (isLegendary)
                {
                    kplayer.LSSJ10FPAchieved = true;
                }
                else
                {
                    if (!kplayer.SSJ10FPAchieved)
                    {
                        kplayer.SSJ10FPAchieved = true;
                    }
                }
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active) continue;
                if (npc.type == NPCID.Guide)
                {
                    npc.StrikeNPCNoInteraction(2000000000, 0.0f, 0, false, false, false);
                }
            }
        }

        public override void AddRecipes()
        {
            Mod DBT = ModLoader.GetMod("DBZMODPORT");
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.GuideVoodooDoll);
            recipe.AddIngredient(ItemID.FancyDishes);
            recipe.AddTile(DBT.Find<ModTile>("KaiTable"));
            recipe.Register();
        }
    }
}
