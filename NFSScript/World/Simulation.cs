using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static NFSScript.World.EASharpBindings;

namespace NFSScript.World
{
    /// <summary>
    /// NFS: World's <see cref="Simulation"/> class.
    /// </summary>
    public static class Simulation
    {
        /// <summary>
        /// 
        /// </summary>
        public static void GetTick()
        {
            CallBinding(_EASharpBinding_438);
        }

        /// <summary>
        /// Returns the tick difference.
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static uint GetTickDifference(long start)
        {
            return (uint)CallBinding<uint>(_EASharpBinding_439, start);
        }

        /// <summary>
        /// Returns the secure tick.
        /// </summary>
        /// <returns></returns>
        public static long GetSecureTick()
        {
            return (long)CallBinding<long>(_EASharpBinding_440);
        }

        /// <summary>
        /// Returns the RDTSC tick.
        /// </summary>
        /// <returns></returns>
        public static long GetRDTSCTick()
        {
            return (long)CallBinding<long>(_EASharpBinding_442);
        }

        /// <summary>
        /// Returns the RDTSC tick difference.
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public static long GetRDTSCTickDifference(long start)
        {
            return (long)CallBinding<long>(_EASharpBinding_443, start);
        }
    }
}
