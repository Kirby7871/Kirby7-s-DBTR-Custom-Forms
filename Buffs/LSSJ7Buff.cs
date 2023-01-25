using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBTBalance.Helpers;
using DBTBalance.Model;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using Microsoft.Xna.Framework.Graphics;
using DBTBalance.Buffs;
using log4net.Util;
using DBTBalance;
using K7DBTRF.Assets;
using IL.Terraria.ID;

namespace K7DBTRF.Buffs
{
    internal class LSSJ7Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 4.5f;
            kiDrainRateWithMastery = 2.0f;
            attackDrainMulti = 1.70f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 2.8f;
                speedMulti = 0.8f;
                baseDefenceBonus = 90;
            }
            else
            {
                damageMulti = 5.5f;
                speedMulti = 3.0f;
                baseDefenceBonus = 160;
            }
            
            base.SetStaticDefaults();
        }

        public override string FormName() => "Legendary Super Saiyan 7";
        public override Color TextColor() => Color.DarkBlue;

        public override string HairTexturePath() => "K7DBTRF/Assets/LSSJ7Hair";

        public override bool Stackable() => false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(0, 100, 255)));

        public override Gradient KiBarGradient() => new Gradient(new Color(0, 197, 255)).AddStop(1f, new Color(0, 0, 255));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<LSSJ7Buff>() && isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.LSSJ7Achieved(player);
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ7Active = true;
            

        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ7Active = false;


        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.eyeColor = Color.Black;
            player.gills = true;
            player.statLifeMax2 += 200;
            player.GetAttackSpeed(DamageClass.Generic) += 0.5f;
            player.lavaImmune = true;
            base.Update(player, ref buffIndex);
        }
    }
}
