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
    internal class SSJ8Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 4.0f;
            kiDrainRateWithMastery = 2.0f;
            attackDrainMulti = 1.50f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 3.3f;
                speedMulti = 0.9f;
                baseDefenceBonus = 0;
            }
            else
            {
                damageMulti = 6.6f;
                speedMulti = 3.0f;
                baseDefenceBonus = 100;
            }

            base.SetStaticDefaults();
        }

        public override string FormName() => "Super Saiyan 8";
        public override Color TextColor() => Color.DarkGreen;

        public override string HairTexturePath() => "K7DBTRF/Assets/SSJ8Hair";

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

            return !player.HasBuff<SSJ8Buff>() && !isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.SSJ8Achieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ8Active = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ8Active = false;


        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.statLifeMax2 += 300;
            player.GetAttackSpeed(DamageClass.Generic) += 0.7f;

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                player.GetDamage(DamageClass.Generic) += 3.3f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 6.6f;
            }

            player.lavaImmune = true;
            base.Update(player, ref buffIndex);
        }
    }
}
