using System;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using NFSScript.Math;

namespace NFSScript.World
{
    /// <summary>
    /// A class that represents the game's UI.
    /// </summary>
    public static class UI
    {
        /// <summary>
        /// Returns the point of where the game's cursor is located on screen. (Might be inaccurate)
        /// </summary>
        public static Point cursorPosition
        {
            get
            {
                ushort x = memory.ReadUShort((IntPtr)memory.getBaseAddress + UIAddrs.NON_STATIC_CURSOR_POS_X);
                ushort y = memory.ReadUShort((IntPtr)memory.getBaseAddress + UIAddrs.NON_STATIC_CURSOR_POS_Y);

                return new Point(x, y);
            }
        }
    }
}
