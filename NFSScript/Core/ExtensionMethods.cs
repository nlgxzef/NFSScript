using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSScript
{
    /// <summary>
    /// A class for extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a boolean to byte.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static byte ToByte(this bool x)
        {
            if (x)
                return 1;
            return 0;
        }

        /// <summary>
        /// Returns <typeparamref name="T"/> instance of the <paramref name="byteArray"/>.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static T GetObject<T>(this byte[] byteArray)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return (T)(object)BitConverter.ToBoolean(byteArray, 0);
                case TypeCode.Byte:
                    return (T)(object)byteArray[0];
                case TypeCode.Char:
                    return (T)(object)Encoding.UTF8.GetChars(byteArray)[0];
                case TypeCode.Double:
                    return (T)(object)BitConverter.ToDouble(byteArray, 0);
                case TypeCode.Int16:
                    return (T)(object)BitConverter.ToInt16(byteArray, 0);
                case TypeCode.Int32:
                    return (T)(object)BitConverter.ToInt32(byteArray, 0);
                case TypeCode.Int64:
                    return (T)(object)BitConverter.ToInt64(byteArray, 0);
                case TypeCode.Single:
                    return (T)(object)BitConverter.ToSingle(byteArray, 0);
                case TypeCode.UInt16:
                    return (T)(object)BitConverter.ToUInt16(byteArray, 0);
                case TypeCode.UInt32:
                    return (T)(object)BitConverter.ToUInt32(byteArray, 0);
                case TypeCode.UInt64:
                    return (T)(object)BitConverter.ToUInt64(byteArray, 0);
            }

            return default(T);
        }

        /// <summary>
        /// Returns an initialized byte[] of the given <typeparamref name="T"/> object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetBytes<T>(this T obj)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    return BitConverter.GetBytes((bool)(object)obj);
                case TypeCode.Char:
                    return Encoding.UTF8.GetBytes(new[] { (char)(object)obj });
                case TypeCode.Double:
                    return BitConverter.GetBytes((double)(object)obj);
                case TypeCode.Int16:
                    return BitConverter.GetBytes((short)(object)obj);
                case TypeCode.Int32:
                    return BitConverter.GetBytes((int)(object)obj);
                case TypeCode.Int64:
                    return BitConverter.GetBytes((long)(object)obj);
                case TypeCode.Single:
                    return BitConverter.GetBytes((float)(object)obj);
                case TypeCode.UInt16:
                    return BitConverter.GetBytes((ushort)(object)obj);
                case TypeCode.UInt32:
                    return BitConverter.GetBytes((uint)(object)obj);
                case TypeCode.UInt64:
                    return BitConverter.GetBytes((ulong)(object)obj);
            }

            return new byte[1];
        }
    }
    }
