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
    public class SSJ8Unlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cultist knowledge of Saiyans");
            Tooltip.SetDefault("Consume the knowledge of how to fuse SSJ3/LSSJ3 and SSJ5/LSSJ5");
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
            return kplayer.SSJ6Achieved || kplayer.LSSJ6Achieved;
        }

        public override void OnConsumeItem(Player player)
        {
            var kplayer = player.GetModPlayer<KPlayer>();
            if (!kplayer.LSSJ8Achieved)
            {
                kplayer.LSSJ8Achieved = true;
                Main.NewText("You successfully add 2 forms together! The new power of LSSJ8 covers you!", 255, 255, 0);
            }
            if (!kplayer.SSJ8Achieved)
            {
                kplayer.SSJ8Achieved = true;
                Main.NewText("You successfully add 2 forms together! The new power of SSJ8 covers you!", 255, 255, 0);
            }
        }
    }
}
