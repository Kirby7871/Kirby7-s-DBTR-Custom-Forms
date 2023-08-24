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
using K7DBTRF.Buffs.Debuffs;
using MonoMod.RuntimeDetour;
using IL.Terraria.Audio;
using DBZGoatLib.Layers;
using DBZGoatLib.UI;
using DBZMODPORT;

namespace K7DBTRF.Buffs
{
    public class SSJ10FPBuff : Transformation
    {


        

        public override void SetStaticDefaults()
        {
            Player player = Main.CurrentPlayer;
            kiDrainRate = 0.0f;
            kiDrainRateWithMastery = 0.0f;
            attackDrainMulti = 1.0f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 6.0f;
                speedMulti = 1.5f;
                baseDefenceBonus = 75;
            }
            else
            {
                damageMulti = 15.0f;
                speedMulti = 1.5f;
                baseDefenceBonus = 150;
            }
            Description.SetDefault("You have 60 seconds to detransform");
            base.SetStaticDefaults();
        }

        public override string FormName() => "Super Saiyan 10 The Forbidden Power";
        public override Color TextColor() => Color.Gray;

        public override string HairTexturePath() => "K7DBTRF/Assets/SSJ10FPHair";

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

            return !player.HasBuff<SSJ10FPBuff>() && !player.HasBuff<LSSJ10FPCooldown>() && !isLegendary //check if we aren't already in LSSJ10FP, if we are, we do not transform again obviously, also check if the cooldown is on
                 && modPlayer.SSJ10FPAchieved;
        }

        public override void OnTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ10FPActive = true;


        }
        public override void PostTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<KPlayer>();
            modPlayer.SSJ10FPActive = false;

            modPlayer.SSJ10FPTimer = 0;

            if (!modPlayer.SSJ10Cheat)
            {
                player.AddBuff(ModContent.BuffType<LSSJ10FPCooldown>(), 1800, false);
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 1.5f;
            player.eyeColor = Color.White;
            if(player.wet)
            {
                player.breath = player.breathMax;
            }
            KPlayer kplayer = player.GetModPlayer<KPlayer>();
            MyPlayer myplayer = player.GetModPlayer<MyPlayer>();

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ10FPBuff>()) >= 1f)
                {
                    player.GetDamage(DamageClass.Generic) += 7.5f;
                    myplayer.KiDamage += 1.5f;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) += 6.0f;
                }
                
            }
            else
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ10FPBuff>()) >= 1f)
                {
                    player.GetDamage(DamageClass.Generic) += 21.0f;
                    myplayer.kiDamage += 6.0f;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) += 15.0f;
                }
            }

            player.lavaImmune = true;

            kplayer.SSJ10FPTimer++;

            if (kplayer.SSJ10Cheat)
            {
                kplayer.SSJ10FPTimer = 0;
            }

            
            if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ10FPBuff>()) >= 1f)
            {
                if (kplayer.SSJ10FPTimer >= 7200)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used the forbidden power for too long and banished into mist"), int.MaxValue, 0, false);
                    kplayer.SSJ10FPTimer = 0;
                }
            }
            else
            {
                if (kplayer.SSJ10FPTimer >= 3600)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used the forbidden power for too long and banished into mist"), int.MaxValue, 0, false);
                    kplayer.SSJ10FPTimer = 0;
                }
            }
            


            base.Update(player, ref buffIndex);
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ10FPBuff>()) >= 1f)
                {
                    tip = "True SSJ10, The Ultimate Forbidden Power\n Damage: +650% | Speed: +100%\n You can now stay up to 2 minutes";

                    rare = 11;
                }
            }
            else
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ10FPBuff>()) >= 1f)
                {
                    tip = "True SSJ10, The Ultimate Forbidden Power\n Damage: +2000% | Speed: +100%\n You can now stay up to 2 minutes";

                    rare = 11;
                }
            }
            base.ModifyBuffTip(ref tip, ref rare);
        }


    }
}
