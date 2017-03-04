using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NFSScript.Core;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using static NFSScript.WorldFunctions;
using static NFSScript.World.EASharpBindings;
using NFSScript.Math;

namespace NFSScript.World
{
    /// <summary>
    /// A class that represents the game's HUD.
    /// </summary>
    public static class HUD
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enable"></param>
        public static void EnableMinimapTrackLayout(bool enable)
        {
            CallBinding(_EASharpBinding_104, enable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gadget"></param>
        /// <param name="enable"></param>
        public static void EnableGadget(string gadget, bool enable)
        {
            CallBinding(_EASharpBinding_105, gadget, enable);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gadget"></param>
        public static void DisposeGadget(string gadget)
        {
            CallBinding(_EASharpBinding_106, gadget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streak"></param>
        /// <param name="isStreakBroken"></param>
        public static void ShowTreasureStreakBuff(int streak, bool isStreakBroken)
        {
            CallBinding(_EASharpBinding_107, streak, isStreakBroken);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearAllBuff()
        {
            CallBinding(_EASharpBinding_108);
        }
    }
}
