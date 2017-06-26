using static NFSScript.World.EASharpBindings;

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
        /// <param name="buffer">The memory address of the buffer.</param>
        /// <param name="length">The length of the buffer.</param>
        public static void SendGameplayMessage(uint buffer, uint length)
        {
            CallBinding(_EASharpBinding_354, buffer, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixedBuffer">The memory address of the fixed buffer.</param>
        /// <param name="length">The length of the fixed buffer.</param>
        /// <returns></returns>
        public static uint RecvGameplayMessage(uint fixedBuffer, uint length)
        {
            return (uint)CallBinding<uint>(_EASharpBinding_355, fixedBuffer, length);
        }
    }
}
