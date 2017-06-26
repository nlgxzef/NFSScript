using System;

namespace NFSScript.Core
{
    /// <summary>
    /// Where the magic happens.
    /// </summary>
    public static class GameMemory
    {        
        /// <summary>
        /// Where the magic happens.
        /// </summary>
        public static VAMemory memory;

        /// <summary>
        /// A generic memory.
        /// </summary>
        public static GMemory genericMemory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IntPtr WriteData(byte[] data)
        {
            uint size = (uint)data.Length;

            IntPtr hAlloc = NativeMethods.VirtualAllocEx(memory.ProcessHandle, IntPtr.Zero, size, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

            memory.WriteByteArray(hAlloc, data);

            return hAlloc;
        }
    }
}
