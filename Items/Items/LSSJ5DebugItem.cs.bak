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

namespace K7DBTRF.Items
{
    internal class LSSJ5DebugItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LSSJ5 Unlock Item");
            Tooltip.SetDefault("Go Overdrive");
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
            return bplayer.LSSJ4Achieved;

        }

        public override void OnConsumeItem(Player player)
        {
            var bplayer = player.GetModPlayer<KPlayer>();
            if (!bplayer.LSSJ5Achieved)
            {
                bplayer.LSSJ5Achieved = true;
            }
            else
            {
                bplayer.LSSJ5Achieved = false;
            }
            

        }
    }
}
