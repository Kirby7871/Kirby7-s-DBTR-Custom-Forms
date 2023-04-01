﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace K7DBTRF.Assets
{
    [Label("Client Settings")]
    internal class BalanceConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static BalanceConfig Instance;

        [Header("Legendary Saiyan 5 Settings")]
        [Label("Replace Hair/Arm with hair:")]
        [Tooltip("(Janky) Replace the player's chest piece with red-ish hair.")]
        [DefaultValue(false)]
        public bool UseHair;
    }
    [Label("Server Settings")]
    internal class BalanceConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static BalanceConfigServer Instance;

        [Header("Toggleable Balance Adjustments")]
        [Label("Beam weapon rework")]
        [Tooltip("Changes beam ki weapons to always take 3 seconds to charge.\nMore Charges still mean more damage.")]
        [DefaultValue(true)]
        public bool ChargeRework;

        

        [Label("Longer transformation times")]
        [Tooltip("Makes transforming into SSJ forms require holding the button for a small duration.")]
        [DefaultValue(true)]
        public bool LongerTransform;

        [Label("Super Saiyan Transformation Tweaks")]
        [Tooltip("Rebalances the bonuses granted by transformations to reduce power creep.")]
        [DefaultValue(true)]
        public bool SSJTweaks;
    }
}
