using System;

namespace NFSScript.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ExposedBase
    {
        /// <summary>
        /// 
        /// </summary>
        public IntPtr mSelf;

        /// <summary>
        /// 
        /// </summary>
        public ExposedBase()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mSelf"></param>
        public ExposedBase(IntPtr mSelf)
        {
            this.mSelf = mSelf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public unsafe void* _GetRaw()
        {
            return mSelf.ToPointer();
        }

        /// <summary>
        /// Equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static unsafe bool operator ==(ExposedBase a, ExposedBase b)
        {
            if (object.ReferenceEquals((object)a, (object)null) && object.ReferenceEquals((object)b, (object)null))
                return true;
            if (!object.ReferenceEquals((object)a, (object)null) && !object.ReferenceEquals((object)b, (object)null))
                return a._GetRaw() == b._GetRaw();
            return false;
        }

        /// <summary>
        /// Not equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ExposedBase a, ExposedBase b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override unsafe int GetHashCode()
        {
            return (int)mSelf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != GetType())
                return false;
            return this == (ExposedBase)o;
        }
    }
}
