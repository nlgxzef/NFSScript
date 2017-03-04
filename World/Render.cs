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
    /// Render class.
    /// </summary>
    public static class Render
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void TriggerEffect(EffectType type)
        {
            CallBinding(_EASharpBinding_422, (int)type);
        }
    }
}
