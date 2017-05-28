using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using static NFSScript.Core.NativeMethods;

namespace NFSScript.Core
{
    /// <summary>
    /// A memory class with generic functions.
    /// </summary>
    public class GMemory
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool debugMode;
        private IntPtr baseAddress;
        private ProcessModule processModule;
        private Process[] mainProcess;
        private IntPtr processHandle;

        /// <summary>
        /// 
        /// </summary>
        public string processName { get; set; }

        /// <summary>
        /// Get the process handle.
        /// </summary>
        public IntPtr ProcessHandle { get { return processHandle; } }

        /// <summary>
        /// 
        /// </summary>
        public long getBaseAddress
        {
            get
            {
                this.baseAddress = (IntPtr)0;
                this.processModule = this.mainProcess[0].MainModule;
                this.baseAddress = this.processModule.BaseAddress;
                return (long)this.baseAddress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GMemory()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public GMemory(string pProcessName)
        {
            this.processName = pProcessName;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckProcess()
        {
            if (this.processName != null)
            {
                this.mainProcess = Process.GetProcessesByName(this.processName);
                if (this.mainProcess.Length == 0)
                {
                    this.ErrorProcessNotFound(this.processName);
                    return false;
                }
                this.processHandle = OpenProcess(2035711U, false, this.mainProcess[0].Id);
                if (!(this.processHandle == IntPtr.Zero))
                    return true;
                this.ErrorProcessNotFound(this.processName);
                return false;
            }
            //int num = (int)MessageBox.Show("Programmer, define process name first!");
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <param name="isRelativeToMemoryBase"></param>
        public byte[] ReadByteArray(int address, uint size, bool isRelativeToMemoryBase)
        {
            if (processHandle == IntPtr.Zero)
                CheckProcess();
            try
            {
                if (isRelativeToMemoryBase)
                    address += (int)baseAddress;

                IntPtr addr = (IntPtr)address;
                uint lpflOldProtect;
                byte[] lpBuffer = new byte[size];

                VirtualProtectEx(processHandle, addr, (UIntPtr)size, 0x4, out lpflOldProtect);
                bool read = ReadProcessMemory(processHandle, addr, lpBuffer, size, 0x0);
                VirtualProtectEx(processHandle, addr, (UIntPtr)size, lpflOldProtect, out lpflOldProtect);

                if (read)
                    return lpBuffer;
                throw new Exception("ReadProcessMemory returned false.");
            }
            catch (Exception ex)
            {
                if (debugMode)
                    Log.Print("ERROR", string.Format(
                        "Error during ReadByteArray:\r\nAddress: {0}, Size: {1}, isRelativeToMemoryBase: {2}\r\n{3}",
                        address.ToString("X"), size, isRelativeToMemoryBase, ex.ToString()));
                return new byte[1];
            }
        }

        /// <summary>
        /// Reads the value from the memory and returns the <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <param name="isRelativeToMemoryBase">Whether the address is relevant to the module's base address.</param>
        public T Read<T>(int address, bool isRelativeToMemoryBase = false)
        {
            return ReadByteArray(address, typeof(T) == typeof(bool) ? 1 : (uint)Marshal.SizeOf(typeof(T)), isRelativeToMemoryBase)
                .GetObject<T>();
        }

        /// <summary>
        /// Reads a string from the memory with the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="address"></param>
        /// <param name="length">Length of the string.</param>
        /// <param name="isRelativeToMemoryBase">Whether the address is relevant to the module's base address.</param>
        public string ReadString(Encoding encoding, int address, uint length, bool isRelativeToMemoryBase = false)
        {
            try
            {
                return encoding.GetString(ReadByteArray(address, length, isRelativeToMemoryBase));
            }
            catch (Exception ex)
            {
                if (debugMode)
                    Log.Print("ERROR", string.Format(
                        "Error during ReadString:\r\nEncoding: {0}, Address: {1}, Length: {2}, isRelativeToMemoryBase: {3}\r\n{4}",
                        encoding.BodyName, address.ToString("X"), length, isRelativeToMemoryBase, ex.ToString()));
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="bytes"></param>
        /// <param name="isRelativeToMemoryBase"></param>
        public bool WriteByteArray(int address, byte[] bytes, bool isRelativeToMemoryBase)
        {
            if (processHandle == IntPtr.Zero)
                CheckProcess();
            try
            {
                if (isRelativeToMemoryBase)
                    address += (int)baseAddress;

                IntPtr addr = (IntPtr)address;
                uint lpflOldProtect;

                VirtualProtectEx(processHandle, addr, (UIntPtr)(bytes.Length), 0x4, out lpflOldProtect);
                bool flag = WriteProcessMemory(processHandle, addr, bytes, (uint)bytes.Length, 0x0);
                VirtualProtectEx(processHandle, addr, (UIntPtr)(bytes.Length), lpflOldProtect, out lpflOldProtect);

                return flag;
            }
            catch (Exception ex)
            {
                if (debugMode)
                    Log.Print("ERROR", string.Format(
                        "Error during WriteByteArray:\r\nAddress: {0}, bytes.Length: {1}, isRelativeToMemoryBase: {2}\r\n{3}",
                        address.ToString("X"), bytes.Length, isRelativeToMemoryBase, ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// Writes the <paramref name="value"/> to the memory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address"></param>
        /// <param name="value">The value to write.</param>
        /// <param name="isRelativeToMemoryBase">Whether the address is relevant to the module's base address.</param>
        public bool Write<T>(int address, T value, bool isRelativeToMemoryBase = false)
        {
            return WriteByteArray(address, value.GetBytes(), isRelativeToMemoryBase);
        }

        /// <summary>
        /// Writes '<paramref name="str"/>' to the memory using specified <paramref name="encoding"/> to get bytes.
        /// </summary>
        /// <remarks>
        /// Automatically converts str into a null-terminated string.
        /// </remarks>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="address"></param>
        /// <param name="str">The string to write.</param>
        /// <param name="isRelativeToMemoryBase">Whether the address is relevant to the module's base address.</param>
        public bool WriteString(Encoding encoding, int address, string str, bool isRelativeToMemoryBase = false)
        {
            try
            {
                return WriteByteArray(address, encoding.GetBytes(str + '\0'), isRelativeToMemoryBase);
            }
            catch (Exception ex)
            {
                if (debugMode)
                    Log.Print("ERROR",
                        string.Format("Error during WriteString:\r\nEncoding: {0}, Address: {1}, String: {2}, isRelativeToMemoryBase: {3}\r\n{4}",
                        encoding.BodyName, address.ToString("X"), str, isRelativeToMemoryBase, ex.ToString()));
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Process GetMainProcess()
        {
            return this.mainProcess[0];
        }

        private void ErrorProcessNotFound(string pProcessName)
        {
            Log.Print("ERROR", string.Format("{0} {1}", processName, "is not running or has not been found. Try to open the loader as an administrator."));
            Environment.Exit(0);
        }
    }
}