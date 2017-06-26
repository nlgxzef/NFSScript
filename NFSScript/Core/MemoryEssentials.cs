using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace NFSScript.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryAllocMap : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public IntPtr storedAddress { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint calledAddress { get; private set; }

        /// <summary>
        /// Returns the amount of usages.
        /// </summary>
        public int usages { get; private set; }
        bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storedAddress"></param>
        /// <param name="calledAddress"></param>
        public MemoryAllocMap(IntPtr storedAddress, uint calledAddress)
        {
            this.storedAddress = storedAddress;
            this.calledAddress = calledAddress;
            usages = 0;
        }

        void Use()
        {
            usages++;
        }
       
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calledAddressValue"></param>
        /// <returns></returns>
        public static bool ExistsInMemoryReturnAlloc(uint calledAddressValue)
        {
            return ASM.memoryReturnAllocation.Find(x => x.calledAddress == calledAddressValue) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calledAddressValue"></param>
        /// <returns></returns>
        public static MemoryAllocMap GetMemoryAllocMapByCalledAddress(uint calledAddressValue)
        {
            return ASM.memoryReturnAllocation.Find(x => x.calledAddress == calledAddressValue);
        }
    }

}
