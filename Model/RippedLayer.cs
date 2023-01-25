using DBTBalance;
using K7DBTRF.Assets;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.ModLoader;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Terraria;
using System.Dynamic;
using DBTBalance.Buffs;
using DBTBalance.Helpers;
using System.IO;
using DBZGoatLib;
using DBTBalance.Model;
using Terraria.ModLoader.Config;
using K7DBTRF.Buffs;
using DBZGoatLib.Handlers;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace K7DBTRF.Model
{
    public class RippedLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.ArmOverItem);
        }
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            var drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.GetModPlayer<KPlayer>().LSSJ5Active && BalanceConfig.Instance.UseHair)
            {
                Color alphaArms = drawInfo.drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)(drawInfo.Position.X + drawInfo.drawPlayer.width * 0.5) / 16, (int)((drawInfo.Position.Y + drawInfo.drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);

                var xArms = (int)(drawInfo.Position.X - Main.screenPosition.X - (drawPlayer.bodyFrame.Width / 2) + (drawPlayer.width / 2));
                var yArms = (int)(drawInfo.Position.Y - Main.screenPosition.Y + drawPlayer.height - drawPlayer.bodyFrame.Height + 4f);

                Vector2 vel = drawPlayer.velocity - drawPlayer.oldVelocity;
                float offset = drawPlayer.wings != 0 ? (vel.Y > 0 ? -4.3f : 0) : (drawPlayer.velocity.Y != 0 ? -4.3f : 0);
                DrawData dataArms = new DrawData(ModContent.Request<Texture2D>("K7DBTRF/Assets/LSSJ5Arms", AssetRequestMode.AsyncLoad).Value,
                    new Vector2(xArms, yArms + offset) + drawPlayer.bodyPosition,
                    drawPlayer.bodyFrame,
                    alphaArms,
                    drawPlayer.bodyRotation,
                    drawPlayer.bodyPosition,
                    1f,
                    drawInfo.playerEffect, 0);

                drawInfo.DrawDataCache.Add(dataArms);
            }
        }
    }
}
