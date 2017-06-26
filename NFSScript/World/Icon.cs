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
        private bool exists = false;
        /// <summary>
        /// Creates a new <see cref="Icon"/> instance.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <param name="rotation"></param>
        public Icon(EAVector3 position, uint type, float rotation)
        {
            uint addr = (uint)CallBinding<uint>(_EASharpBinding_109, position.mSelf, type, rotation);
            mSelf = new IntPtr(addr);

            exists = true;
        }

        /// <summary>
        /// Destroys this <see cref="Icon"/> instance from the game. 
        /// </summary>
        public void Destroy()
        {
            if (!exists)
            {
                throw new DoesNotExistException(string.Format("An attempt was made to destroy an instance that does not exist inside the game."));
            }

            CallBinding(_EASharpBinding_110, mSelf);
            mSelf = IntPtr.Zero;
            exists = false;
        }

        /// <summary>
        /// Enables the <see cref="Icon"/>.
        /// </summary>
        public void Enable()
        {
            if (!exists)
                throw new DoesNotExistException(string.Format("The Icon instance does not exist inside the game."));

            CallBinding(_EASharpBinding_111, mSelf);
        }

        /// <summary>
        /// Disables the Icon.
        /// </summary>
        public void Disable()
        {
            if (!exists)
                throw new DoesNotExistException(string.Format("The Icon instance does not exist inside the game."));

            CallBinding(_EASharpBinding_112, mSelf);
        }
    }
}
