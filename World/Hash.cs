using System;
using static NFSScript.World.EASharpBindings;

namespace NFSScript.World
{
    /// <summary>
    /// Hash class.
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// Returns a hash.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public unsafe static uint Hash32(string text)
        {
            return (uint)CallBinding<uint>(_EASharpBinding_94, text);
        }
    }
}
