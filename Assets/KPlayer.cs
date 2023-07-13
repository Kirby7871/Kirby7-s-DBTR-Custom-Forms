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

        public bool LSSJ9FPActive;
        public bool LSSJ9FPUnlockMsg;
        public bool LSSJ9FPAchieved;

        public bool LSSJ10FPActive;
        public bool LSSJ10FPUnlockMsg;
        public bool LSSJ10FPAchieved;

        public int LSSJ10FPTimer;

        public bool SSJ5Active; //This is a field
        public bool SSJ5UnlockMsg;
        public bool SSJ5Achieved;

        public bool SSJ6Active; //This is a field
        public bool SSJ6UnlockMsg;
        public bool SSJ6Achieved;

        public bool SSJ7Active;
        public bool SSJ7UnlockMsg;
        public bool SSJ7Achieved(Player player)
        {
            return GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<SSJ6Buff>()) >= 1f;
        } //This is a method

        public bool SSJ8Active; //This is a field
        public bool SSJ8UnlockMsg;
        public bool SSJ8Achieved;

        public bool SSJ9Active; //This is a field
        public bool SSJ9UnlockMsg;
        public bool SSJ9Achieved;

        public bool SSJ9FPActive;
        public bool SSJ9FPUnlockMsg;
        public bool SSJ9FPAchieved;

        public bool SSJ10FPActive; //This is a field
        public bool SSJ10FPUnlockMsg;
        public bool SSJ10FPAchieved;

        public int SSJ10FPTimer;

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
            base.PostUpdateEquips();

        }
        public override void PreUpdate()
        {
            
            base.PreUpdate();
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

            if (LSSJ10FPAchieved && !LSSJ10FPUnlockMsg)
            {
                LSSJ10FPUnlockMsg = true;
                Main.NewText("You achieve the most dangerous power a saiyan can have, beware of it");
            }
        }




        public static KPlayer ModPlayer(Player player) => player.GetModPlayer<KPlayer>();

        public static bool MasteredLSSJ5(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ5Buff>()) >= 1f;

        public static bool MasteredLSSJ6(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ6Buff>()) >= 1f;

        public static bool MasteredLSSJ7(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ7Buff>()) >= 1f;

        public static bool MasteredLSSJ8(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.LSSJ8Buff>()) >= 1f;



        public static bool MasteredSSJ6(Player player) => GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<Buffs.SSJ6Buff>()) >= 1f;

        public override void SaveData(TagCompound tag)
        {
            var vplayer = Main.CurrentPlayer;
            tag.Add("K7DBTRF_LSSJ5Achieved", LSSJ5Achieved);
            tag.Add("K7DBTRF_LSSJ5UnlockMsg", LSSJ5UnlockMsg);
            tag.Add("K7DBTRF_LSSJ6Achieved", LSSJ6Achieved);
            tag.Add("K7DBTRF_LSSJ6UnlockMsg", LSSJ6UnlockMsg);
            tag.Add("K7DBTRF_LSSJ7UnlockMsg", LSSJ7UnlockMsg);
            tag.Add("K7DBTRF_LSSJ8Achieved", LSSJ8Achieved);
            tag.Add("K7DBTRF_LSSJ8UnlockMsg", LSSJ8UnlockMsg);

            tag.Add("K7DBTRF_LSSJ9Achieved", LSSJ9Achieved);
            tag.Add("K7DBTRF_LSSJ9UnlockMsg", LSSJ9UnlockMsg);

            tag.Add("K7DBTRF_LSSJ9FPAchieved", LSSJ9FPAchieved);
            tag.Add("K7DBTRF_LSSJ9FPUnlockMsg", LSSJ9FPUnlockMsg);

            tag.Add("K7DBTRF_LSSJ10FPAchieved", LSSJ10FPAchieved);
            tag.Add("K7DBTRF_LSSJ10FPUnlockMsg", LSSJ10FPUnlockMsg);



            tag.Add("K7DBTRF_SSJ5Achieved", SSJ5Achieved);
            tag.Add("K7DBTRF_SSJ5UnlockMsg", SSJ5UnlockMsg);

            tag.Add("K7DBTRF_SSJ6Achieved", SSJ6Achieved);
            tag.Add("K7DBTRF_SSJ6UnlockMsg", SSJ6UnlockMsg);

            tag.Add("K7DBTRF_SSJ7UnlockMsg", SSJ7UnlockMsg);

            tag.Add("K7DBTRF_SSJ8Achieved", SSJ8Achieved);
            tag.Add("K7DBTRF_SSJ8UnlockMsg", SSJ8UnlockMsg);

            tag.Add("K7DBTRF_SSJ9Achieved", SSJ9Achieved);
            tag.Add("K7DBTRF_SSJ9UnlockMsg", SSJ9UnlockMsg);

            tag.Add("K7DBTRF_SSJ10FPAchieved", SSJ10FPAchieved);
            tag.Add("K7DBTRF_SSJ10FPUnlockMsg", SSJ10FPUnlockMsg);
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("K7DBTRF_LSSJ5Achieved"))
                LSSJ5Achieved = tag.GetBool("K7DBTRF_LSSJ5Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ5UnlockMsg"))
                LSSJ5UnlockMsg = tag.GetBool("K7DBTRF_LSSJ5UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ6Achieved"))
                LSSJ6Achieved = tag.GetBool("K7DBTRF_LSSJ6Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ6UnlockMsg"))
                LSSJ6UnlockMsg = tag.GetBool("K7DBTRF_LSSJ6UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ8Achieved"))
                LSSJ8Achieved = tag.GetBool("K7DBTRF_LSSJ8Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ8UnlockMsg"))
                LSSJ8UnlockMsg = tag.GetBool("K7DBTRF_LSSJ8UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ9Achieved"))
                LSSJ9Achieved = tag.GetBool("K7DBTRF_LSSJ9Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ9UnlockMsg"))
                LSSJ9UnlockMsg = tag.GetBool("K7DBTRF_LSSJ9UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ9FPAchieved"))
                LSSJ9FPAchieved = tag.GetBool("K7DBTRF_LSSJ9FPAchieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ9FPUnlockMsg"))
                LSSJ9FPUnlockMsg = tag.GetBool("K7DBTRF_LSSJ9FPUnlockMsg");

            if (tag.ContainsKey("K7DBTRF_LSSJ10FPAchieved"))
                LSSJ10FPAchieved = tag.GetBool("K7DBTRF_LSSJ10FPAchieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ10FPUnlockMsg"))
                LSSJ10FPUnlockMsg = tag.GetBool("K7DBTRF_LSSJ10FPUnlockMsg");



            if (tag.ContainsKey("K7DBTRF_SSJ5Achieved"))
                SSJ5Achieved = tag.GetBool("K7DBTRF_SSJ5Achieved");
            if (tag.ContainsKey("K7DBTRF_SSJ5UnlockMsg"))
                SSJ5UnlockMsg = tag.GetBool("K7DBTRF_SSJ5UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_SSJ6Achieved"))
                SSJ6Achieved = tag.GetBool("K7DBTRF_SSJ6Achieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ5UnlockMsg"))
                SSJ6UnlockMsg = tag.GetBool("K7DBTRF_SSJ6UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_SSJ8Achieved"))
                SSJ8Achieved = tag.GetBool("K7DBTRF_SSJ8Achieved");
            if (tag.ContainsKey("K7DBTRF_SSJ8UnlockMsg"))
                SSJ8UnlockMsg = tag.GetBool("K7DBTRF_SSJ8UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_SSJ9Achieved"))
                SSJ9Achieved = tag.GetBool("K7DBTRF_SSJ9Achieved");
            if (tag.ContainsKey("K7DBTRF_SSJ9UnlockMsg"))
                SSJ9UnlockMsg = tag.GetBool("K7DBTRF_SSJ9UnlockMsg");

            if (tag.ContainsKey("K7DBTRF_SSJ9FPAchieved"))
                SSJ9FPAchieved = tag.GetBool("K7DBTRF_SSJ9FPAchieved");
            if (tag.ContainsKey("K7DBTRF_LSSJ9FPUnlockMsg"))
                SSJ9FPUnlockMsg = tag.GetBool("K7DBTRF_SSJ9FPUnlockMsg");

            if (tag.ContainsKey("K7DBTRF_SSJ10FPAchieved"))
                SSJ10FPAchieved = tag.GetBool("K7DBTRF_SSJ10FPAchieved");
            if (tag.ContainsKey("K7DBTRF_SSJ10FPUnlockMsg"))
                SSJ10FPUnlockMsg = tag.GetBool("K7DBTRF_SSJ10FPUnlockMsg");
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

            }
            base.ResetEffects();
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
            return player.GetModPlayer<GPlayer>().Trait == "Legendary";
        }

        public override Connection[] Connections() => new Connection[]
        {
            new Connection(0,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(1,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(2,2,1,false,new Gradient(Color.Blue).AddStop(0.60f, new Color(0, 255, 0))),
            new Connection(3,2,1,false,new Gradient(Color.Green).AddStop(0.60f, new Color(30, 30, 30))),
            new Connection(4,2,1,false,new Gradient(Color.Green).AddStop(0.60f, new Color(30, 30, 30))),
            new Connection(5,2,1,false,new Gradient(Color.Green).AddStop(0.60f, new Color(30, 30, 30)))
        };

        public override string Name() => "Legendary Alternative Future";

        public override Node[] Nodes() => new Node[]
        {
            new Node(0, 2, "LSSJ5Buff", "K7DBTRF/Buffs/LSSJ5Buff", "You did well, but what if you risked the fate of the world again to perfect your skill?",UnlockConditionLSSJ5,DiscoverConditionLSSJ5),
            new Node(1, 2, "LSSJ6Buff", "K7DBTRF/Buffs/LSSJ6Buff", "Consume the 4 star dragon ball combined with evil prideful essence",UnlockConditionLSSJ6,DiscoverConditionLSSJ6),
            new Node(2, 2, "LSSJ7Buff", "K7DBTRF/Buffs/LSSJ7Buff", "I think you should just train harder with your new form",UnlockConditionLSSJ7,DiscoverConditionLSSJ7),
            new Node(3, 2, "LSSJ8Buff", "K7DBTRF/Buffs/LSSJ8Buff", "Can you defeat a crazed cultist after mastering many forms?",UnlockConditionLSSJ8,DiscoverConditionLSSJ8),
            new Node(4, 2, "LSSJ9Buff", "K7DBTRF/Buffs/LSSJ9Buff", "The emblem of the guardian of hell could be infused wth pure ki and unlock new power", UnlockConditionLSSJ9, DiscoverConditionLSSJ9),
            new Node(5, 2, "LSSJ9FPBuff", "K7DBTRF/Buffs/LSSJ9FPBuff", "Consume the pure emblem and it also gets unlocked with LSSJ9", UnlockConditionLSSJ9FP, DiscoverConditionLSSJ9FP),
            new Node(6, 2, "LSSJ10FPBuff", "K7DBTRF/Buffs/LSSJ10FPBuff", "Eat the voodoo doll of your first friend, and you will get dark power", UnlockConditionLSSJ10FP, DiscoverConditionLSSJ10FP)
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

        public bool UnlockConditionLSSJ9FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ9FPAchieved;
        }

        public bool DiscoverConditionLSSJ9FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ8Achieved;
        }

        public bool UnlockConditionLSSJ10FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ10FPAchieved;
        }

        public bool DiscoverConditionLSSJ10FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.LSSJ9Achieved;
        }
    }



    public class SSJAFPanel : TransformationTree
    {
        public override bool Complete() => true;

        public override bool Condition(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            var Bplayer = player.GetModPlayer<BPlayer>();
            return player.GetModPlayer<GPlayer>().Trait != "Legendary";
        }

        public override Connection[] Connections() => new Connection[]
        {
            new Connection(0,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(1,2,1,false,new Gradient(Color.White).AddStop(0.60f, new Color(0, 0, 255))),
            new Connection(2,2,1,false,new Gradient(Color.Blue).AddStop(0.60f, new Color(0, 255, 0))),
            new Connection(3,2,1,false,new Gradient(Color.Yellow).AddStop(0.60f, new Color(30, 30, 30))),
            new Connection(4,2,1,false,new Gradient(Color.Yellow).AddStop(0.60f, new Color(30, 30, 30))),
            new Connection(5,2,1,false,new Gradient(Color.Yellow).AddStop(0.60f, new Color(30, 30, 30)))
        };

        public override string Name() => "Alternative Future";

        public override Node[] Nodes() => new Node[]
        {
            new Node(0, 2, "SSJ5Buff", "K7DBTRF/Buffs/SSJ5Buff", "You should kill the moon lord again to improve your new power",UnlockConditionSSJ5,DiscoverConditionSSJ5),
            new Node(1, 2, "SSJ6Buff", "K7DBTRF/Buffs/SSJ6Buff", "Consume the 4 star dragon ball combined with evil prideful essence",UnlockConditionSSJ6,DiscoverConditionSSJ6),
            new Node(2, 2, "SSJ7Buff", "K7DBTRF/Buffs/SSJ7Buff", "I think you should just train harder with your new form",UnlockConditionSSJ7,DiscoverConditionSSJ7),
            new Node(3, 2, "SSJ8Buff", "K7DBTRF/Buffs/SSJ8Buff", "Can you defeat a crazed cultist after mastering many forms?",UnlockConditionSSJ8,DiscoverConditionSSJ8),
            new Node(4, 2, "SSJ9Buff", "K7DBTRF/Buffs/SSJ9Buff", "The emblem of the guardian of hell could be infused wth pure ki and unlock new power", UnlockConditionSSJ9, DiscoverConditionSSJ9),
            new Node(5, 2, "SSJ9FPBuff", "K7DBTRF/Buffs/SSJ9FPBuff", "See SSJ9", UnlockConditionSSJ9FP, DiscoverConditionSSJ9FP),
            new Node(6, 2, "SSJ10FPBuff", "K7DBTRF/Buffs/SSJ10FPBuff", "Eat the voodoo doll of your first friend, and you will get dark power", UnlockConditionSSJ10FP, DiscoverConditionSSJ10FP)
        };

        public bool UnlockConditionSSJ5(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ5Achieved;
        }

        public bool DiscoverConditionSSJ5(Player player)
        {
            return true;
        }

        public bool UnlockConditionSSJ6(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ6Achieved;
        }

        public bool DiscoverConditionSSJ6(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ5Achieved;
        }

        public bool UnlockConditionSSJ7(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ7Achieved(player);
        }

        public bool DiscoverConditionSSJ7(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ6Achieved;
        }

        public bool UnlockConditionSSJ8(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ8Achieved;
        }

        public bool DiscoverConditionSSJ8(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ6Achieved;
        }

        public bool UnlockConditionSSJ9(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ9Achieved;
        }

        public bool DiscoverConditionSSJ9(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ8Achieved;
        }

        public bool UnlockConditionSSJ9FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ9FPAchieved;
        }

        public bool DiscoverConditionSSJ9FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ8Achieved;
        }

        public bool UnlockConditionSSJ10FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ10FPAchieved;
        }

        public bool DiscoverConditionSSJ10FP(Player player)
        {
            var Kplayer = player.GetModPlayer<KPlayer>();
            return Kplayer.SSJ9Achieved || Kplayer.SSJ9FPAchieved;
        }
    }
}
