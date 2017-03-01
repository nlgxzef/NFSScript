using System;
using System.Collections.Generic;
using NFSScript.Core;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using NFSScript.Math;


namespace NFSScript.World
{
    /// <summary>
    /// A class that represents the game's <see cref="Player"/>.
    /// </summary>
    public static class Player
    {
        /// <summary>
        /// Returns true if the <see cref="Player"/> is in free roam.
        /// </summary>
        public static bool isFreeRoam
        {
            get
            {
                return memory.ReadByte((IntPtr)GameAddrs.STATIC_IS_FREE_ROAM) == 1;
            }
        }

        /// <summary>
        /// <see cref="Player"/>'s cash (Inaccurate read only value).
        /// </summary>
        public static int cash
        {
            get
            {
                return memory.ReadInt32((IntPtr)PlayerAddrs.STATIC_PLAYER_CASH);
            }
        }

        /// <summary>
        /// <see cref="Player"/>'s boost (Inaccurate read only value).
        /// </summary>
        public static int boost
        {
            get
            {
                return memory.ReadInt32((IntPtr)PlayerAddrs.STATIC_PLAYER_BOOST);
            }
        }

        /// <summary>
        /// Returns the currnet amount of gems that the player has.
        /// </summary>
        public static int currentAmountOfGems
        {
            get
            {
                int address = memory.ReadInt32((IntPtr)memory.getBaseAddress + PlayerAddrs.NONSTATIC_GEMS_COLLECTED);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_GEMS_COLLECTED_1);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_GEMS_COLLECTED_2);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_GEMS_COLLECTED_3);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_GEMS_COLLECTED_4);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_GEMS_COLLECTED_5);

                return memory.ReadInt32((IntPtr)address);
            }
        }

        /// <summary>
        /// A class that represents the <see cref="Player"/>'s car.
        /// </summary>
        public static class Car
        {
            /// <summary>
            /// Returns the <see cref="Player"/>'s car speed.
            /// </summary>
            public static float GetSpeed()
            {
                int address = memory.ReadInt32((IntPtr)memory.getBaseAddress + PlayerAddrs.NONSTATIC_PLAYER_SPEED);
                address = memory.ReadInt32((IntPtr)address + PlayerAddrs.PSTATIC_PLAYER_SPEED);

                return memory.ReadFloat((IntPtr)address);
            }
        }
    }
}
