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

namespace K7DBTRF.Buffs
{
    internal class SSJ5Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 3.0f;
            kiDrainRateWithMastery = 1.5f;
            attackDrainMulti = 1.2f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 2.7f;
                speedMulti = 1.5f;
                baseDefenceBonus = 0;
            }
            else
            {
                damageMulti = 5.4f;
                speedMulti = 3.50f;
                baseDefenceBonus = 0;
            }


            base.SetStaticDefaults();
        }

        public override string FormName() => "Super Saiyan 5";
        public override Color TextColor() => Color.White;

        public override string HairTexturePath() => "K7DBTRF/Assets/SSJ5Hair";

        public override bool Stackable() => false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(255, 255, 255)));

        public override Gradient KiBarGradient() => new Gradient(new Color(255, 255, 255)).AddStop(1f, new Color(156, 0, 34));


        // DBZGoatLib.Model
        //public AuraData(string _Path, int _Frames, int _FrameTimerLimit, BlendState _blendState, Color _Color, string _HairPath)

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<SSJ5Buff>() && !isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.SSJ5Achieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ5Active = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ5Active = false;


        }


        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                player.GetDamage(DamageClass.Generic) += 2.7f;
            }
            else
            {
                player.GetDamage(DamageClass.Generic) += 5.4f;
            }
            player.lavaImmune = true;
            base.Update(player, ref buffIndex);
        }
    }
}
