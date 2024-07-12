using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace K7DBTRF.Assets
{
    //[Label("Client Settings")]
    internal class BalanceConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static BalanceConfig Instance;

        //[Header("Legendary Saiyan 5 Settings")]
        [DefaultValue(false)]
        public bool UseHair;
    }
    //[Label("Server Settings")]
    internal class BalanceConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static BalanceConfigServer Instance;

        //[Header("Toggleable Balance Adjustments")]
        [DefaultValue(true)]
        public bool ChargeRework;

        

        
        [DefaultValue(true)]
        public bool LongerTransform;

        
        [DefaultValue(true)]
        public bool SSJTweaks;
    }
}
