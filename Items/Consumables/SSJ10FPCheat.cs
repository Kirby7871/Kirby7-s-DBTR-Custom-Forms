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
    internal class SSJ10FPCheat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SSJ10Cheat");
            Tooltip.SetDefault("Removes the time limit of SSJ10 and LSSJ10");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.potion = false;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.scale = 1f;
            base.SetDefaults();
        }

        public override bool? UseItem(Player player)
        {
            KPlayer kplayer = player.GetModPlayer<KPlayer>();
            if (kplayer.SSJ10FPAchieved || kplayer.LSSJ10FPAchieved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnConsumeItem(Player player)
        {
            KPlayer kplayer = player.GetModPlayer<KPlayer>();
            
            if (!kplayer.SSJ10Cheat)
            {
                kplayer.SSJ10Cheat = true;
                Main.NewText("You have now unlimited SSJ10/LSSJ10 usage");
            }
            else
            {
                kplayer.SSJ10Cheat = false;
                Main.NewText("You no longer have unlimited SSJ10/LSSJ10 usage");
            }

            base.OnConsumeItem(player);
        }


    }
}
