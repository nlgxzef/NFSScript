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
        private bool exists = false;

        /// <summary>
        /// 
        /// </summary>
        public EAVector3()
        {
            mSelf = (IntPtr)CallBinding<IntPtr>(_EASharpBinding_627);

            exists = true;
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

            exists = true;
        }

        /// <summary>
        /// Experimental.
        /// </summary>
        public void Destory()
        {
            if (!exists)
            {
                throw new DoesNotExistException(string.Format("An attempt was made to destroy an instance that does not exist inside the game."));
            }

            CallBinding(_EASharpBinding_626, mSelf);
            mSelf = IntPtr.Zero;
        }
    }
}
