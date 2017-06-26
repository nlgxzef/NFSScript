using static NFSScript.Core.GameMemory;

namespace NFSScript.World
{
    /// <summary>
    /// A class that makes life easy.
    /// </summary>
    internal static class MemoryBase
    {
        internal static uint FunctionBase
        {
            get { return ((uint)memory.getBaseAddress - 0x400000); }
        }
    }
}
