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
    /// Class for the game's net.
    /// </summary>
    public static class Net
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ConnectToRoaming()
        {
            CallBinding(_EASharpBinding_351);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DisconnectFromRoaming()
        {
            CallBinding(_EASharpBinding_352);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void EntrantCrossedFinish()
        {
            CallBinding(_EASharpBinding_353);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">The memory address of the buffer</param>
        /// <param name="length">The length of the buffer</param>
        public static void SendGameplayMessage(uint buffer, uint length)
        {
            CallBinding(_EASharpBinding_354, buffer, length);
        }
    }
}
