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

namespace K7DBTRF.Items.Consumables
{
    internal class SSJ5Unlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Saiyan Soul");
            Tooltip.SetDefault("Go overdrive,above the known SSJ forms");
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
            var bplayer = player.GetModPlayer<BPlayer>();
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";
            if (isLegendary)
            {
                return bplayer.LSSJ4Achieved;
            }
            else
            {
                return true;
            }

        }

        public override void OnConsumeItem(Player player)
        {
            var kplayer = player.GetModPlayer<KPlayer>();
            if (!kplayer.LSSJ5Achieved)
            {
                kplayer.LSSJ5Achieved = true;
                Main.NewText("You unlocked an unknown form of legend, select the form and transform as usual", 255, 0, 0);
            }
            if (!kplayer.SSJ5Achieved)
            {
                kplayer.SSJ5Achieved = true;
                Main.NewText("You unlocked a form unknown to all, select the form and transform as usual", 255, 0, 0);
            }
        }
    }
}
