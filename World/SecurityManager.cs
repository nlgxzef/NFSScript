using static NFSScript.World.EASharpBindings;

namespace NFSScript.World
{
    /// <summary>
    /// NFS: World's security manager class.
    /// </summary>
    public static class SecurityManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static void CheckLoadedLibraries()
        {
            CallBinding(_EASharpBinding_427);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void EventReset()
        {
            CallBinding(_EASharpBinding_428);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static uint GetHacks()
        {
            return (uint)CallBinding<uint>(_EASharpBinding_429);
        }
    }
}
