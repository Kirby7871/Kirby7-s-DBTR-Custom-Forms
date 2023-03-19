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
using Terraria.DataStructures;

namespace K7DBTRF.Buffs
{
    internal class LSSJ10FPBuff : Transformation
    {
        int LSSJ10FPTimer;
        public override void SetStaticDefaults()
        {
            kiDrainRate = 0.0f;
            kiDrainRateWithMastery = 0.0f;
            attackDrainMulti = 1.0f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 4.0f;
                speedMulti = 1.0f;
                baseDefenceBonus = 150;
            }
            else
            {
                damageMulti = 13.0f;
                speedMulti = 1.5f;
                baseDefenceBonus = 300;
            }

            base.SetStaticDefaults();
        }

        public override string FormName() => "Legendary Super Saiyan 10 The Forbidden Power";
        public override Color TextColor() => Color.Gray;

        public override string HairTexturePath() => "K7DBTRF/Assets/LSSJ10FPHair";

        public override bool Stackable() => true;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(30, 30, 30)));

        public override Gradient KiBarGradient() => new Gradient(new Color(255, 255, 255)).AddStop(1f, new Color(0, 0, 0));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<LSSJ10FPBuff>() && isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.LSSJ10FPAchieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ10FPActive = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ10FPActive = false;

            LSSJ10FPTimer = 0;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.8f;
            player.eyeColor = Color.White;

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                player.GetDamage(DamageClass.Generic) += 4.0f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 13.0f;
            }

            player.lavaImmune = true;

            LSSJ10FPTimer++;

            if(LSSJ10FPTimer >= 1800)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "used the forbidden power for too long and banished into mist"), int.MaxValue, 0, false);
                LSSJ10FPTimer = 0;
            }

            base.Update(player, ref buffIndex);
        }
    }
}
