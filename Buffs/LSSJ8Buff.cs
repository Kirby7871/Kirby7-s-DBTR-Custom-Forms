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
    internal class LSSJ8Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 5.0f;
            kiDrainRateWithMastery = 2.5f;
            attackDrainMulti = 1.50f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 3.1f;
                speedMulti = 0.9f;
                baseDefenceBonus = 100;
            }
            else
            {
                damageMulti = 6.2f;
                speedMulti = 3.0f;
                baseDefenceBonus = 200;
            }

            base.SetStaticDefaults();
        }

        public override string FormName() => "Legendary Super Saiyan 8";
        public override Color TextColor() => Color.DarkGreen;

        public override string HairTexturePath() => "K7DBTRF/Assets/LSSJ8Hair";

        public override bool Stackable() => false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(0, 255, 0)));

        public override Gradient KiBarGradient() => new Gradient(new Color(0, 255, 0)).AddStop(1f, new Color(0, 155, 0));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<LSSJ8Buff>() && isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.LSSJ6Achieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ8Active = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ8Active = false;


        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.statLifeMax2 += 300;
            player.GetAttackSpeed(DamageClass.Generic) += 0.7f;
            player.lavaImmune = true;
            base.Update(player, ref buffIndex);
        }
    }
}
