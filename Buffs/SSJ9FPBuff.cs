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
    internal class SSJ9FPBuff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 6.0f;
            kiDrainRateWithMastery = 2.5f;
            attackDrainMulti = 1.50f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 3.8f;
                speedMulti = 1.2f;
                baseDefenceBonus = 30;
            }
            else
            {
                damageMulti = 9.0f;
                speedMulti = 2.0f;
                baseDefenceBonus = 60;
            }

            base.SetStaticDefaults();
        }

        public override string FormName() => "Super Saiyan 9 Full Power";
        public override Color TextColor() => Color.Black;

        public override string HairTexturePath() => "K7DBTRF/Assets/SSJ9FPHair";

        public override bool Stackable() => false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(30, 30, 30)));

        public override Gradient KiBarGradient() => new Gradient(new Color(255, 255, 255)).AddStop(1f, new Color(0, 0, 0));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<SSJ9FPBuff>() && !isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.SSJ9FPAchieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ9FPActive = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ9FPActive = false;


        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.8f;

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                player.GetDamage(DamageClass.Generic) += 3.8f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 9.0f;
            }

            player.lavaImmune = true;
            player.statLifeMax2 /= 2;
            base.Update(player, ref buffIndex);
        }
    }
}
