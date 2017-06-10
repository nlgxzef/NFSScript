using System;
using NFSScript.Core;
using static NFSScript.World.EASharpBindings;

namespace NFSScript.World
{
    /// <summary>
    /// Icon class.
    /// </summary>
    public class Icon : ExposedBase
    {
        /// <summary>
        /// Creates a new <see cref="Icon"/> instance.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <param name="rotation"></param>
        public Icon(EAVector3 position, uint type, float rotation)
        {
            mSelf = (IntPtr)CallBinding<uint>(_EASharpBinding_109, position.mSelf, type, rotation);
        }

        /// <summary>
        /// Destroys this <see cref="Icon"/> instance. 
        /// </summary>
        public void Destroy()
        {
            CallBinding(_EASharpBinding_110, mSelf);
            mSelf = IntPtr.Zero;
        }

        /// <summary>
        /// Enables the <see cref="Icon"/>.
        /// </summary>
        public void Enable()
        {
            CallBinding(_EASharpBinding_111, mSelf);
        }

        /// <summary>
        /// Disables the Icon.
        /// </summary>
        public void Disable()
        {
            CallBinding(_EASharpBinding_112, mSelf);
        }
    }
}
