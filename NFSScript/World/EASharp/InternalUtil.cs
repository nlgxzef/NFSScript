using System;
using System.Text;

namespace NFSScript.World.EASharp
{
    /// <summary>
    /// InternalUtil class.
    /// </summary>
    public class InternalUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static unsafe string ConvertFromCString(sbyte* str)
        {
            if ((IntPtr)str == IntPtr.Zero)
                return null;
            return new string(str, 0, -1, Encoding.ASCII);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static unsafe string ConvertFromWCString(char* str)
        {
            if ((IntPtr)str == IntPtr.Zero)
                return null;
            return new string(str);
        }
    }
}
