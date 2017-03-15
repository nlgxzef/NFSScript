using System;
using System.Runtime.InteropServices;
using NFSScript.Core;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using static NFSScript.WorldFunctions;
using static NFSScript.World.EASharpBindings;
using NFSScript.Math;

namespace NFSScript.World
{
    /// <summary>
    /// 
    /// </summary>
    public class EAVector3 : ExposedBase
    {
        /// <summary>
        /// 
        /// </summary>
        public EAVector3()
        {
            mSelf = (IntPtr)CallBinding<IntPtr>(_EASharpBinding_627);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public EAVector3(float x, float y, float z) : base()
        {
            mSelf = (IntPtr)CallBinding<IntPtr>(_EASharpBinding_628, x, y, z);
        }

        /// <summary>
        /// Experimental.
        /// </summary>
        public void Destory()
        {
            CallBinding(_EASharpBinding_626, mSelf);
            mSelf = IntPtr.Zero;
        }
    }
}
