using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using K7DBTRF.Assets;
using K7DBTRF.Helpers;
using Terraria.ID;
using DBZGoatLib;
using K7DBTRF.Buffs;
using DBZMODPORT.Buffs.SSJBuffs;
using DBTBalance;
using IL.Terraria.Map;

namespace K7DBTRF.NPCs
{
    internal class KNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Player player = Main.CurrentPlayer;
                if (npc.type == NPCID.CultistBoss)
                {
                    var modPlayer = player.GetModPlayer<KPlayer>();
                    KPlayer kplayer = Main.CurrentPlayer.GetModPlayer<KPlayer>();


                    float LSSJ3Mastery = GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ3Buff>());
                    float LSSJ5Mastery = GPlayer.ModPlayer(player).GetMastery(ModContent.BuffType<LSSJ5Buff>());
                    if ((!modPlayer.LSSJ8Achieved) && modPlayer.LSSJ6Achieved)
                    {
                        modPlayer.LSSJ8Achieved = true;
                        return;
                    }
                }

                if (npc.type == NPCID.MoonLordCore)
                {
                    var modPlayer = player.GetModPlayer<KPlayer>();
                    KPlayer kplayer = Main.CurrentPlayer.GetModPlayer<KPlayer>();
                    var bplayer = player.GetModPlayer<BPlayer>();

                    if ((!kplayer.LSSJ5Achieved) && bplayer.LSSJ4Achieved)
                    {
                        modPlayer.LSSJ5Achieved = true;
                        return;
                    }
                }
            }
            else
            {
                if (npc.type == NPCID.CultistBoss)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        Player player = Main.player[i];
                        if (player != null)
                        {
                            if (player.active)
                            {
                                KPlayer modPlayer = player.GetModPlayer<KPlayer>();

                                if (!modPlayer.LSSJ8Achieved)
                                {
                                    if (npc.type == NPCID.CultistBoss)
                                    {
                                        modPlayer.MP_Unlock = true;
                                        modPlayer.LSSJ8Achieved = true;
                                        KNetworkHandler.SendUnlockStatus(i, true);
                                    }
                                }
                            }
                        }
                    }

                    if (npc.type == NPCID.MoonLordCore)
                    {
                        for (int i = 0; i < 255; i++)
                        {
                            Player player = Main.player[i];
                            if (player != null)
                            {
                                if (player.active)
                                {
                                    KPlayer modPlayer = player.GetModPlayer<KPlayer>();

                                    if (!modPlayer.LSSJ5Achieved)
                                    {
                                        if (npc.type == NPCID.MoonLordCore)
                                        {
                                            modPlayer.MP_Unlock = true;
                                            modPlayer.LSSJ5Achieved = true;
                                            KNetworkHandler.SendUnlockStatus(i, true);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
