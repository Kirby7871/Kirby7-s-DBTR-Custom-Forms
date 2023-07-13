﻿using System;
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
    internal class SSJ9Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 4.0f;
            kiDrainRateWithMastery = 2.0f;
            attackDrainMulti = 1.50f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 3.7f;
                speedMulti = 1.0f;
                baseDefenceBonus = 0;
            }
            else
            {
                damageMulti = 7.4f;
                speedMulti = 2.5f;
                baseDefenceBonus = 50;
            }

            base.SetStaticDefaults();
        }

        public override string FormName() => "Super Saiyan 9";
        public override Color TextColor() => Color.Black;

        public override string HairTexturePath() => "K7DBTRF/Assets/SSJ9Hair";

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

            return !player.HasBuff<SSJ9Buff>() && !isLegendary //check if we aren't already in SSJ9, if we are, we do not transform again obviously
                 && modPlayer.SSJ9Achieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ9Active = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ9Active = false;


        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.8f;

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                player.GetDamage(DamageClass.Generic) += 3.7f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 7.4f;
            }

            player.lavaImmune = true;
            base.Update(player, ref buffIndex);
        }
    }
}
