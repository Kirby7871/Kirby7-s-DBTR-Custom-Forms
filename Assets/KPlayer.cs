using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using DBTBalance.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using DBTBalance.Helpers;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using static Terraria.ModLoader.PlayerDrawLayer;
using DBTBalance.Model;
using DBTBalance;
using K7DBTRF.Buffs;
using System.Reflection;
using DBZGoatLib.Model;
using DBZGoatLib.Layers;
using DBZMODPORT.Buffs.SSJBuffs;


namespace K7DBTRF.Assets
{
    public class KPlayer : ModPlayer
    {
        
        public bool LSSJ5Active; //This is a field
        public bool LSSJ5UnlockMsg;
        public bool LSSJ5Achieved;
        //The method checks its own condition, a field (like LSSJ5Active) requires an external source to switch it to true

        public bool MP_Unlock;

        public bool LSSJ6Active;
        public bool LSSJ6UnlockMsg;
        public bool LSSJ6Achieved;

        public bool LSSJ7Active;
        public bool LSSJ7UnlockMsg;
        public bool LSSJ7Achieved(Player player)
        {
            return GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ6Buff>()) >= 1f;
        }

        public bool LSSJ8Active; //This is a field
        public bool LSSJ8UnlockMsg;
        public bool LSSJ8Achieved;

        public bool LSSJ9Active;
        public bool LSSJ9UnlockMsg;
        public bool LSSJ9Achieved;

        public DateTime? Offset = null;

        public DateTime? PoweringUpTime = null;
        public DateTime? LastPowerUpTick = null;

        private TransformationInfo? Form = null;

        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();
            if (ModLoader.TryGetMod("dbzcalamity", out Mod dbcamod))
            {
                var ModPlayerClass = dbcamod.Code.DefinedTypes.First(x => x.Name.Equals("dbzcalamityPlayer"));
                var getModPlayer = ModPlayerClass.GetMethod("ModPlayer");

                dynamic dbzPlayer = getModPlayer.Invoke(null, new object[] { Player });

                var dodgeChance = ModPlayerClass.GetField("dodgeChange");
                dodgeChance.SetValue(dbzPlayer, (float)dodgeChance.GetValue(dbzPlayer) - .05f);
            }

        }
        public override void PreUpdate()
        {
            if (ModLoader.HasMod("DBZBalance"))
            {
                var transformationHelper = K7DBTRF.DBTBalance.Code.DefinedTypes.First(x => x.Name.Equals("TransformationHelper"));
                var Currentform = TransformationHandler.GetCurrentTransformation(Player);
                if (!(Currentform.Value.buffKeyName == "LSSJ4Buff"))
                    Offset = null;

                if (Currentform.Value.buffKeyName == "LSSJ4Buff" && !Offset.HasValue)
                    Offset = DateTime.Now;

                if (BalanceConfigServer.Instance.KiRework)
                {
                    var MyPlayerClass = K7DBTRF.DBZMOD.Code.DefinedTypes.First(x => x.Name.Equals("MyPlayer"));
                    dynamic myPlayer = MyPlayerClass.GetMethod("ModPlayer").Invoke(null, new object[] { Player });
                    var kiRegenTimer = MyPlayerClass.GetField("kiRegenTimer", DBTBalance.DBTBalance.flagsAll);
                    var kiChargeRate = MyPlayerClass.GetField("kiChargeRate");

                    kiRegenTimer.SetValue(myPlayer, 0);
                    kiChargeRate.SetValue(myPlayer, myPlayer.kiChargeRate + myPlayer.kiRegen);
                }
            }
        }

        public override void PostUpdate()
        {
            var vplayer = new Player();
            if (LSSJ5Achieved && !LSSJ5UnlockMsg) 
            {
                LSSJ5UnlockMsg = true;
                Main.NewText("You went beyond potential, break all limits and go further!\nWhile in Legendary Super Saiyain 4 form press the Transform button once more to reach higher power.", Color.Green);
            }
            if (ModLoader.HasMod("DBZMODPORT"))
            {
                if (BalanceConfigServer.Instance.KiRework)
                {
                    var MyPlayerClass = K7DBTRF.DBZMOD.Code.DefinedTypes.First(x => x.Name.Equals("MyPlayer"));
                    //This is throwing a System.NullReferenceException
                    dynamic myPlayer = MyPlayerClass.GetMethod("ModPlayer").Invoke(null, new object[] { Player });
                    var kiChargeRate = MyPlayerClass.GetField("kiChargeRate");

                    kiChargeRate.SetValue(myPlayer, myPlayer.kiChargeRate + myPlayer.kiRegen);
                }
            }

            if (LSSJ6Achieved && !LSSJ6UnlockMsg)
            {
                LSSJ6UnlockMsg = true;
                Main.NewText("You accepted evil ki, it covers your body, a new form is born!");
            }

            if (LSSJ8Achieved && !LSSJ8UnlockMsg)
            {
                LSSJ8UnlockMsg = true;
                Main.NewText("You stack together LSSJ3 and LSSJ5, the combination of 2 forms creates abysmal power!");
            }
        }




        public static KPlayer ModPlayer(Player player) => player.GetModPlayer<KPlayer>();

        public static bool MasteredLSSJ5(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ5Buff>()) >= 1f;

        public static bool MasteredLSSJ6(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ6Buff>()) >= 1f;

        public static bool MasteredLSSJ7(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ7Buff>()) >= 1f;

        public static bool MasteredLSSJ8(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ8Buff>()) >= 1f;

        public override void SaveData(TagCompound tag)
        {
            var vplayer = new Player();
            tag.Add("K7DBTRF_LSSJ5Achieved", LSSJ5Achieved);
            tag.Add("K7DBTRF_LSSJ5UnlockMsg", LSSJ5UnlockMsg);
            tag.Add("K7DBTRF_LSSJ6Achieved", LSSJ6Achieved);
            tag.Add("K7DBTRF_LSSJ6UnlockMsg", LSSJ6UnlockMsg);
            tag.Add("K7DBTRF_LSSJ7Achieved", LSSJ7Achieved(vplayer));
            tag.Add("K7DBTRF_LSSJ7UnlockMsg", LSSJ7UnlockMsg);
            tag.Add("K7DBTRF_LSSJ8Achieved", LSSJ8Achieved);
            tag.Add("K7DBTRF_LSSJ8UnlockMsg", LSSJ8UnlockMsg);

            tag.Add("K7DBTRF_LSSJ9Achieved", LSSJ9Achieved);
            tag.Add("K7DBTRF_LSSJ9UnlockMsg", LSSJ9UnlockMsg);
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("K7DBTRF_LSSJ5Achieved"))
                LSSJ5Achieved = tag.GetBool("K7DBTRF_LSSJ5Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ5UnlockMsg"))
                LSSJ5UnlockMsg = tag.GetBool("K7DBTRF_LSSJ5UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ6Achieved"))
                LSSJ6Achieved = tag.GetBool("K7DBTRF_LSSJ6Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ5UnlockMsg"))
                LSSJ6UnlockMsg = tag.GetBool("K7DBTRF_LSSJ6UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ8Achieved"))
                LSSJ8Achieved = tag.GetBool("K7DBTRF_LSSJ8Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ8UnlockMsg"))
                LSSJ8UnlockMsg = tag.GetBool("K7DBTRF_LSSJ8UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ9Achieved"))
                LSSJ9Achieved = tag.GetBool("K7DBTRF_LSSJ9Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ8UnlockMsg"))
                LSSJ9UnlockMsg = tag.GetBool("K7DBTRF_LSSJ9UnlockMsg");
        }

        public override void OnRespawn(Player player)
        {
            TransformationHandler.ClearTransformations(player);
            base.OnRespawn(player);
        }

        public override void ResetEffects()
        {
            if (ModLoader.HasMod("DBZMODPORT"))
            {
                if (BalanceConfigServer.Instance.KiRework)
                {
                    var MyPlayerClass = DBTBalance.DBTBalance.DBZMOD.Code.DefinedTypes.First(x => x.Name.Equals("MyPlayer"));
                    dynamic myPlayer = MyPlayerClass.GetMethod("ModPlayer").Invoke(null, new object[] { Player });
                    var kiChargeRate = MyPlayerClass.GetField("kiChargeRate");

                    kiChargeRate.SetValue(myPlayer, myPlayer.kiChargeRate + myPlayer.kiRegen);
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (!Form.HasValue || TransformationHandler.TransformKey.JustPressed)
                Form = Player.GetModPlayer<GPlayer>().FetchTransformation();
            else if (Form.HasValue && !TransformationHandler.TransformKey.Current)
                Form = Player.GetModPlayer<GPlayer>().FetchTransformation();


            if (!Form.HasValue && TransformationHandler.PowerDownKey.JustPressed)
                TransformationHandler.ClearTransformations(Player);
        }






    }



    public class LSSJAFPanel : TransformationTree
    {
        public override bool Complete() => true;

        public override bool Condition(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            var Bplayer = player.GetModPlayer<BPlayer>();
            return Bplayer.LSSJ4Achieved && player.GetModPlayer<GPlayer>().Trait == "Legendary";
        }

        public override Connection[] Connections() => new Connection[]
        {
            new Connection(0,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(1,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(2,2,1,false,new Gradient(Color.Blue).AddStop(0.60f, new Color(0, 255, 0))),
            new Connection(3,2,1,false,new Gradient(Color.Green).AddStop(0.60f, new Color(30, 30, 30)))
        };

        public override string Name() => "Legendary Alternative Future";

        public override Node[] Nodes() => new Node[]
        {
            new Node(0, 2, "LSSJ5Buff", "K7DBTRF/Buffs/LSSJ5Buff", "You did well, but what if you risked the fate of the world again to perfect your skill?",UnlockConditionLSSJ5,DiscoverConditionLSSJ5),
            new Node(1, 2, "LSSJ6Buff", "K7DBTRF/Buffs/LSSJ6Buff", "Consume the 4 star dragon ball combined with evil prideful essence",UnlockConditionLSSJ6,DiscoverConditionLSSJ6),
            new Node(2, 2, "LSSJ7Buff", "K7DBTRF/Buffs/LSSJ7Buff", "I think you should just train harder with your new form",UnlockConditionLSSJ7,DiscoverConditionLSSJ7),
            new Node(3, 2, "LSSJ8Buff", "K7DBTRF/Buffs/LSSJ8Buff", "Can you defeat a crazed cultist after mastering many forms?",UnlockConditionLSSJ8,DiscoverConditionLSSJ8),
            new Node(4, 2, "LSSJ9Buff", "K7DBTRF/Buffs/LSSJ9Buff", "The emblem of the guardian of hell could be infused wth pure ki and unlock new power", UnlockConditionLSSJ9, DiscoverConditionLSSJ9)
        };

        public bool UnlockConditionLSSJ5(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ5Achieved;
        }

        public bool DiscoverConditionLSSJ5(Player player)
        {
            return true;
        }

        public bool UnlockConditionLSSJ6(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ6Achieved;
        }

        public bool DiscoverConditionLSSJ6(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ5Achieved;
        }

        public bool UnlockConditionLSSJ7(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ7Achieved(player);
        }

        public bool DiscoverConditionLSSJ7(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ6Achieved;
        }

        public bool UnlockConditionLSSJ8(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ8Achieved;
        }

        public bool DiscoverConditionLSSJ8(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ6Achieved;
        }

        public bool UnlockConditionLSSJ9(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ9Achieved;
        }

        public bool DiscoverConditionLSSJ9(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ8Achieved;
        }
    }
}
