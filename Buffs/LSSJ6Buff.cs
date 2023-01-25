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
    public class LSSJ6Buff : Transformation
    {
        public override void SetStaticDefaults()
        {
            kiDrainRate = 5.0f;
            kiDrainRateWithMastery = 2.5f;
            attackDrainMulti = 1.80f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 2.5f;
                speedMulti = 1.0f;
                baseDefenceBonus = 82;
            }
            else
            {
                damageMulti = 5.0f;
                speedMulti = 3.0f;
                baseDefenceBonus = 140;
            }


            base.SetStaticDefaults();
        }

        public override string FormName() => "Legendary Super Saiyan 6";
        public override Color TextColor() => Color.White;

        public override string HairTexturePath() => "K7DBTRF/Assets/LSSJ6Hair";

        public override bool Stackable() => false;

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ3", 260);

        public override AuraData AuraData() => (new AuraData("K7DBTRF/Assets/LSSJ5Aura", 4, BlendState.AlphaBlend, new Color(218, 255, 202)));

        public override Gradient KiBarGradient() => new Gradient(new Color(255, 255, 255)).AddStop(1f, new Color(156, 0, 34));


        // DBZGoatLib.Model
        //public AuraData(string _Path, int _Frames, int _FrameTimerLimit, BlendState _blendState, Color _Color, string _HairPath)

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            var currentForm = TransformationHandler.GetCurrentTransformation(player);
            bool isLegendary = player.GetModPlayer<GPlayer>().Trait == "Legendary";

            return !player.HasBuff<LSSJ6Buff>() && isLegendary //check if we aren't already in LSSJ5, if we are, we do not transform again obviously
                 && modPlayer.LSSJ6Achieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ6Active = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.LSSJ6Active = false;


        }
    }
}
