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
using DBZMODPORT;

namespace K7DBTRF.Buffs
{
    public class LSSJ10FPBuff : Transformation
    {

        


        public override void SetStaticDefaults()
        {
            kiDrainRate = 0.0f;
            kiDrainRateWithMastery = 0.0f;
            attackDrainMulti = 1.0f;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                damageMulti = 4.5f;
                speedMulti = 1.0f;
                baseDefenceBonus = 150;
            }
            else
            {
                damageMulti = 14.0f;
                speedMulti = 1.5f;
                baseDefenceBonus = 300;
            }
            Description.SetDefault("You have 60 seconds to detransform");
            base.SetStaticDefaults();
        }

        public override string FormName() => "Legendary Super Saiyan 10 The Forbidden Power";
        public override Color TextColor() => Color.Gray;

        public override string HairTexturePath() => "K7DBTRF/Assets/LSSJ10FPHair";

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

            return !player.HasBuff<LSSJ10FPBuff>() && !player.HasBuff<LSSJ10FPCooldown>() && isLegendary //check if we aren't already in LSSJ10FP, if we are, we do not transform again obviously, also check if the cooldown is on
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

            modPlayer.LSSJ10FPTimer = 0;

            if (!modPlayer.SSJ10Cheat)
            {
                player.AddBuff(ModContent.BuffType<LSSJ10FPCooldown>(), 1800, false);
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gills = true;
            player.GetAttackSpeed(DamageClass.Generic) += 0.8f;
            player.eyeColor = Color.White;
            KPlayer kplayer = player.GetModPlayer<KPlayer>();
            MyPlayer myplayer = player.GetModPlayer<MyPlayer>();

            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ10FPBuff>()) >= 1f)
                {
                    player.GetDamage(DamageClass.Generic) += 5.0f;
                    myplayer.KiDamage += 1.0f;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) += 4.0f;
                }
                    
            }
            else
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ10FPBuff>()) >= 1f)
                {
                    player.GetDamage(DamageClass.Generic) += 20.0f;
                    myplayer.KiDamage += 6.0f;
                }
                else
                {
                    player.GetDamage(DamageClass.Generic) += 13.0f;
                }
            }

            player.lavaImmune = true;

            kplayer.LSSJ10FPTimer++;

            if (kplayer.SSJ10Cheat)
            {
                kplayer.LSSJ10FPTimer = 0;
            }

            if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ10FPBuff>()) >= 1f)
            {
                if (kplayer.LSSJ10FPTimer >= 7200)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used the forbidden power for too long and banished into mist"), int.MaxValue, 0, false);
                    kplayer.LSSJ10FPTimer = 0;
                }
            }
            else
            {
                if (kplayer.LSSJ10FPTimer >= 3600)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used the forbidden power for too long and banished into mist"), int.MaxValue, 0, false);
                    kplayer.LSSJ10FPTimer = 0;
                }
            }
            

            base.Update(player, ref buffIndex);
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            if (BalanceConfigServer.Instance.SSJTweaks)
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ10FPBuff>()) >= 1f)
                {
                    tip = "True LSSJ10, The Ultimate Forbidden Power\n Damage: +400% | Speed: +100% | Defense: +200\n You can now stay up to 2 minutes";

                    rare = 11;
                }
            }
            else
            {
                if (GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ10FPBuff>()) >= 1f)
                {
                    tip = "True LSSJ10, The Ultimate Forbidden Power\n Damage: +1900% | Speed: +100% | Defense: +400\n You can now stay up to 2 minutes";

                    rare = 11;
                }
            }
            base.ModifyBuffTip(ref tip, ref rare);
        }

    }
}
