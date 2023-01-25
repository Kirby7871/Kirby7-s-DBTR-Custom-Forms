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

namespace K7DBTRF.Items.Consumables
{
    internal class LSSJ6Unlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evil Infused Dragon Ball");
            Tooltip.SetDefault("May be the key for a new power?");
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
            return kplayer.LSSJ5Achieved && !kplayer.LSSJ6Achieved;
        }

        public override void OnConsumeItem(Player player)
        {
            var kplayer = player.GetModPlayer<KPlayer>();
            if (!kplayer.LSSJ6Achieved)
            {
                kplayer.LSSJ6Achieved = true;
            }


        }

        public override void AddRecipes()
        {
            Mod DBT = ModLoader.GetMod("DBZMODPORT");
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(DBT, "PridefulKiCrystal", 50);
            recipe.AddIngredient(DBT, "FourStarDB", 1);
            recipe.AddTile(DBT.Find<ModTile>("KaiTable"));
            recipe.Register();
        }
    }
}
