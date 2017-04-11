using System;
using NFSScript.Core;

namespace NFSScript
{
    /// <summary>
    /// A class for NFSScript related funcions and variables.
    /// </summary>
    public static class NFSScript
    {
        /// <summary>
        /// DEBUG.
        /// </summary>
        public static bool DEBUG = false;

        /// <summary>
        /// 
        /// </summary>
        public static NFSGame currentLoadedNFSGame;

        /// <summary>
        /// The directory of the script loader (NFSScriptLoader.exe).
        /// </summary>
        public static string Directory { get { return AppDomain.CurrentDomain.BaseDirectory; } }
    }

    /// <summary>
    /// The class that represents the current game.
    /// </summary>
    public static class CurrentGame
    {
        /// <summary>
        /// Returns whether the game is minimized or not.
        /// </summary>
        public static bool IsMinimized { get { return NativeMethods.IsIconic(GameMemory.memory.GetMainProcess().MainWindowHandle); } }
    }

    /// <summary>
    /// NFSGame enum.
    /// </summary>
    public enum NFSGame : byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,
        /// <summary>
        /// The game Need for Speed: Underground.
        /// </summary>
        Underground = 1,
        /// <summary>
        /// The game Need for Speed: Underground 2.
        /// </summary>
        Underground2 = 2,
        /// <summary>
        /// The game Need for Speed: Most Wanted.
        /// </summary>
        MW = 3,
        /// <summary>
        /// The game Need for Speed: Carbon.
        /// </summary>
        Carbon = 4,
        /// <summary>
        /// The game Need for Speed: ProStreet.
        /// </summary>
        ProStreet = 5,
        /// <summary>
        /// The game Need for Speed: Undercover.
        /// </summary>
        Undercover = 6,
        /// <summary>
        /// The game Need for Speed: World.
        /// </summary>
        World = 7,
        /// <summary>
        /// Undetermined game.
        /// </summary>
        Undetermined = 255
    }
}
