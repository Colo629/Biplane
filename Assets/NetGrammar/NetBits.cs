using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetGrammar
{
    public static class NetBits
    {
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(bool value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        
        #region GetBytes Overloads
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(char value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(double value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(float value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(long value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(short value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(uint value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ulong value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }
        /// <summary>
        /// Same as BitConverter.GetBytes, but enforces big-endian.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ushort value)
        {
            return BitConverter.IsLittleEndian ? BitConverter.GetBytes(value).Reverse().ToArray() : BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Same as Encoding.ASCII.GetBytes, or Encoding.UTF8.GetBytes according to parameter.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isUTF8"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string value, bool isUTF8 = false)
        {
            return isUTF8 ? Encoding.UTF8.GetBytes(value) : Encoding.ASCII.GetBytes(value);
        }
        #endregion

        
        #region Decoders

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static bool ToBoolean(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToBoolean(BitConverter.IsLittleEndian ? ((byte[])bytes.Clone()).Reverse().ToArray() : bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static char ToChar(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToChar(BitConverter.IsLittleEndian ? ((byte[])bytes.Clone()).Reverse().ToArray() : bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static double ToDouble(byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, sizeof(double));
            }
            return BitConverter.ToDouble(bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static float ToSingle(byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, sizeof(float));
            }
            return BitConverter.ToSingle(bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int ToInt32(byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, sizeof(int));
            }
            return BitConverter.ToInt32(bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static long ToInt64(byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, sizeof(long));
            }
            return BitConverter.ToInt64(bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static short ToInt16(byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, sizeof(short));
            }
            return BitConverter.ToInt16(bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static uint ToUInt32(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToUInt32(BitConverter.IsLittleEndian ? ((byte[])bytes.Clone()).Reverse().ToArray() : bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static ulong ToUInt64(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToUInt64(BitConverter.IsLittleEndian ? ((byte[])bytes.Clone()).Reverse().ToArray() : bytes, startIndex);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from a big-endian byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static ushort ToUInt16(byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToUInt16(BitConverter.IsLittleEndian ? ((byte[])bytes.Clone()).Reverse().ToArray() : bytes, startIndex);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static bool[] ToBooleans(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<bool>();
            var elements = new bool[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToBoolean(bytes, startIndex);
                startIndex += sizeof(bool);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static bool[] ToBooleans(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(bool);
            return ToBooleans(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a char array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static char[] ToChars(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<char>();
            var elements = new char[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToChar(bytes, startIndex);
                startIndex += sizeof(char);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static char[] ToChars(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(char);
            return ToChars(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static float[] ToSingles(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<float>();
            var elements = new float[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length && ePointer < numberToConvert)
            {
                elements[ePointer] = ToSingle(bytes, startIndex);
                startIndex += sizeof(float);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static float[] ToSingles(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(float);
            return ToSingles(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static double[] ToDoubles(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<double>();
            var elements = new double[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToDouble(bytes, startIndex);
                startIndex += sizeof(double);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static double[] ToDoubles(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(double);
            return ToDoubles(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static int[] ToInt32s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<int>();
            var elements = new int[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToInt32(bytes, startIndex);
                startIndex += sizeof(int);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int[] ToInt32s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(int);
            return ToInt32s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static long[] ToInt64s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<long>();
            var elements = new long[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToInt64(bytes, startIndex);
                startIndex += sizeof(long);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static long[] ToInt64s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(long);
            return ToInt64s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static short[] ToInt16s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<short>();
            var elements = new short[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToInt16(bytes, startIndex);
                startIndex += sizeof(short);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static short[] ToInt16s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(short);
            return ToInt16s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static uint[] ToUInt32s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<uint>();
            var elements = new uint[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToUInt32(bytes, startIndex);
                startIndex += sizeof(uint);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static uint[] ToUInt32s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(uint);
            return ToUInt32s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static ulong[] ToUInt64s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<ulong>();
            var elements = new ulong[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToUInt64(bytes, startIndex);
                startIndex += sizeof(ulong);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static ulong[] ToUInt64s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(ulong);
            return ToUInt64s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Get a bool array of specified size from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <param name="numberToConvert"></param>
        /// <returns></returns>
        public static ushort[] ToUInt16s(byte[] bytes, int startIndex, int numberToConvert)
        {
            if (numberToConvert <= 0) return Array.Empty<ushort>();
            var elements = new ushort[numberToConvert];
            var ePointer = 0;
            while (startIndex < bytes.Length)
            {
                elements[ePointer] = ToUInt16(bytes, startIndex);
                startIndex += sizeof(ushort);
                ePointer++;
            }
            return elements;
        }
        /// <summary>
        /// Get a boolean array from the given bytes. 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static ushort[] ToUInt16s(byte[] bytes, int startIndex = 0)
        {
            int numberToConvert = bytes.Length / sizeof(ushort);
            return ToUInt16s(bytes, startIndex, numberToConvert);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from an ASCII-encoded byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToASCII(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// Returns a ready-to-use boolean from an ASCII-encoded byte array,
        /// starting at specified index and taking specified char count.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ToASCII(byte[] bytes, int index, int count)
        {
            return Encoding.ASCII.GetString(bytes, index, count);
        }
        #endregion
        
        private static int InsetBytes(IReadOnlyList<byte> subject, IList<byte> writeTo, int startIndex)
        {
            //Debug.Log($"{subject.Count}//{writeTo.Count}//{startIndex}");
            for (var i = 0; i < subject.Count; i++) writeTo[startIndex + i] = subject[i];
            return subject.Count;
        }

        #region AssemblePacket
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(byte[] dataElements, ushort prefix)
        {
            const int sizeOfElement = sizeof(byte);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                encoded[sizeof(int) + sizeof(ushort) + sizeOfElement * i] = dataElements[i];
            return encoded;
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, byte[] dataElements)
        {
            const int sizeOfElement = sizeof(byte);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, byte[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }

        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params bool[] dataElements)
        {
            const int sizeOfElement = sizeof(bool);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params bool[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params char[] dataElements)
        {
            const int sizeOfElement = sizeof(char);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params char[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params double[] dataElements)
        {
            const int sizeOfElement = sizeof(double);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params double[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params float[] dataElements)
        {
            const int sizeOfElement = sizeof(float);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params float[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params int[] dataElements)
        {
            const int sizeOfElement = sizeof(int);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params int[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params long[] dataElements)
        {
            const int sizeOfElement = sizeof(long);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params long[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params short[] dataElements)
        {
            const int sizeOfElement = sizeof(short);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params short[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params uint[] dataElements)
        {
            const int sizeOfElement = sizeof(uint);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params uint[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params ulong[] dataElements)
        {
            const int sizeOfElement = sizeof(ulong);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params ulong[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params ushort[] dataElements)
        {
            const int sizeOfElement = sizeof(ushort);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + sizeOfElement * dataElements.Length];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            for (int i = 0; i < dataElements.Length; i++)
                InsetBytes(GetBytes(dataElements[i]), encoded, sizeof(int) + sizeof(ushort) + sizeOfElement * i);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and data elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements"></param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params ushort[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        
        /// <summary>
        /// Assembles a packet with a given prefix and ASCII string elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements">ASCII-formatted strings</param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, params string[] dataElements)
        {
            int dataSize = dataElements.Sum(str => str.Length);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + dataSize];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            int writeIndex = sizeof(int) + sizeof(ushort);
            foreach (string str in dataElements)
                writeIndex += InsetBytes(GetBytes(str), encoded, writeIndex);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and ASCII string elements.
        /// Optionally skips all strings with non-ASCII characters. 
        /// This is useful in cases like user input where input strings may contain non-ASCII characters,
        /// but is somewhat slower overall.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="skipNonASCII"></param>
        /// <param name="dataElements">ASCII-formatted strings</param>
        /// <returns></returns>
        public static byte[] AssemblePacket(ushort prefix, bool skipNonASCII, params string[] dataElements)
        {
            if (!skipNonASCII) return AssemblePacket(prefix, dataElements);
            int dataSize = dataElements.Sum(str => str.Length);
            var encoded = new byte[sizeof(int) + sizeof(ushort) + dataSize];
            InsetBytes(GetBytes(encoded.Length), encoded, 0);
            InsetBytes(GetBytes(prefix), encoded, sizeof(int));
            int writeIndex = sizeof(int) + sizeof(ushort);
            foreach (string str in dataElements.Where(str => str.All(chr => chr <= 127)))  // I have no idea if the reSharper of this also works
                writeIndex += InsetBytes(GetBytes(str), encoded, writeIndex);
            return encoded;
        }
        /// <summary>
        /// Assembles a packet with a given prefix and ASCII string elements.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="dataElements">ASCII-formatted strings</param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, params string[] dataElements)
        {
            return AssemblePacket((ushort)prefix, dataElements);
        }
        /// <summary>
        /// Assembles a packet with a given prefix and ASCII string elements.
        /// Optionally skips all strings with non-ASCII characters. 
        /// This is useful in cases like user input where input strings may contain non-ASCII characters,
        /// but is somewhat slower overall.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="skipNonASCII"></param>
        /// <param name="dataElements">ASCII-formatted strings</param>
        /// <returns></returns>
        public static byte[] AssemblePacket(NetDefs.MailPrefix prefix, bool skipNonASCII, params string[] dataElements)
        {
            return AssemblePacket((ushort)prefix, skipNonASCII, dataElements);
        }
        
        #endregion
    }
}
