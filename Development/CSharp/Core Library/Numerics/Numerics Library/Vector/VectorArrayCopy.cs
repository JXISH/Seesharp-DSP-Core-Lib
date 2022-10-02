using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Rotate Template ----

        /// <summary>
        /// 数组旋转模板
        /// </summary>
        public static void ArrayRotate<T>(T[] source, T[] destination, int lastN)
        {
            int length = source.Length;
            int rotateLength = lastN % length;
            if (rotateLength < 0) { rotateLength += source.Length; }

            Vector.ArrayCopy(source, 0, destination, rotateLength, length - rotateLength);
            Vector.ArrayCopy(source, length - rotateLength, destination, 0, rotateLength);
        }

        /// <summary>
        /// 数组旋转模板
        /// </summary>
        public static void ArrayRotate<T>(T[] inout, int lastN)
        {
            T[] temp = new T[inout.Length];
            Vector.ArrayCopy(inout, temp);
            ArrayRotate(temp, inout, lastN);
        }

        #endregion

        #region ---- Copy Template ----

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, T[] destination)
        {
            if (source is byte[] source_u8 && destination is byte[] destination_u8)
            {
                ArrayCopy(source_u8, destination_u8);
            }
            else if (source is short[] source_i16 && destination is short[] destination_i16)
            {
                ArrayCopy(source_i16, destination_i16);
            }
            else if (source is int[] source_i32 && destination is int[] destination_i32)
            {
                ArrayCopy(source_i32, destination_i32);
            }
            else if (source is float[] source_f32 && destination is float[] destination_f32)
            {
                ArrayCopy(source_f32, destination_f32);
            }
            else if (source is double[] source_f64 && destination is double[] destination_f64)
            {
                ArrayCopy(source_f64, destination_f64);
            }
            else if (source is Complex32[] source_fc32 && destination is Complex32[] destination_fc32)
            {
                ArrayCopy(source_fc32, destination_fc32);
            }
            else if (source is Complex[] source_fc64 && destination is Complex[] destination_fc64)
            {
                ArrayCopy(source_fc64, destination_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, int sourceStart, T[] destination, int destinationStat, int length)
        {
            if (source is byte[] source_u8 && destination is byte[] destination_u8)
            {
                ArrayCopy(source_u8, sourceStart, destination_u8, destinationStat, length);
            }
            else if (source is short[] source_i16 && destination is short[] destination_i16)
            {
                ArrayCopy(source_i16, sourceStart, destination_i16, destinationStat, length);
            }
            else if (source is int[] source_i32 && destination is int[] destination_i32)
            {
                ArrayCopy(source_i32, sourceStart, destination_i32, destinationStat, length);
            }
            else if (source is float[] source_f32 && destination is float[] destination_f32)
            {
                ArrayCopy(source_f32, sourceStart, destination_f32, destinationStat, length);
            }
            else if (source is double[] source_f64 && destination is double[] destination_f64)
            {
                ArrayCopy(source_f64, sourceStart, destination_f64, destinationStat, length);
            }
            else if (source is Complex32[] source_fc32 && destination is Complex32[] destination_fc32)
            {
                ArrayCopy(source_fc32, sourceStart, destination_fc32, destinationStat, length);
            }
            else if (source is Complex[] source_fc64 && destination is Complex[] destination_fc64)
            {
                ArrayCopy(source_fc64, sourceStart, destination_fc64, destinationStat, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 二维数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[,] source, T[,] destination)
        {
            if (source is byte[,] source_u8 && destination is byte[,] destination_u8)
            {
                ArrayCopy(source_u8, destination_u8);
            }
            else if (source is short[,] source_i16 && destination is short[,] destination_i16)
            {
                ArrayCopy(source_i16, destination_i16);
            }
            else if (source is int[,] source_i32 && destination is int[,] destination_i32)
            {
                ArrayCopy(source_i32, destination_i32);
            }
            else if (source is float[,] source_f32 && destination is float[,] destination_f32)
            {
                ArrayCopy(source_f32, destination_f32);
            }
            else if (source is double[,] source_f64 && destination is double[,] destination_f64)
            {
                ArrayCopy(source_f64, destination_f64);
            }
            else if (source is Complex32[,] source_fc32 && destination is Complex32[,] destination_fc32)
            {
                ArrayCopy(source_fc32, destination_fc32);
            }
            else if (source is Complex[,] source_fc64 && destination is Complex[,] destination_fc64)
            {
                ArrayCopy(source_fc64, destination_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, IntPtr destination)
        {
            if (source is byte[] source_u8)
            {
                ArrayCopy(source_u8, destination);
            }
            else if (source is short[] source_i16)
            {
                ArrayCopy(source_i16, destination);
            }
            else if (source is int[] source_i32)
            {
                ArrayCopy(source_i32, destination);
            }
            else if (source is float[] source_f32)
            {
                ArrayCopy(source_f32, destination);
            }
            else if (source is double[] source_f64)
            {
                ArrayCopy(source_f64, destination);
            }
            else if (source is Complex32[] source_fc32)
            {
                ArrayCopy(source_fc32, destination, 0, -1);
            }
            else if (source is Complex[] source_fc64)
            {
                ArrayCopy(source_fc64, destination, 0, -1);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, IntPtr destination, int sourceStart, int length)
        {
            if (source is byte[] source_u8)
            {
                ArrayCopy(source_u8, destination, sourceStart, length);
            }
            else if (source is short[] source_i16)
            {
                ArrayCopy(source_i16, destination, sourceStart, length);
            }
            else if (source is int[] source_i32)
            {
                ArrayCopy(source_i32, destination, sourceStart, length);
            }
            else if (source is float[] source_f32)
            {
                ArrayCopy(source_f32, destination, sourceStart, length);
            }
            else if (source is double[] source_f64)
            {
                ArrayCopy(source_f64, destination, sourceStart, length);
            }
            else if (source is Complex32[] source_fc32)
            {
                ArrayCopy(source_fc32, destination, sourceStart, length);
            }
            else if (source is Complex[] source_fc64)
            {
                ArrayCopy(source_fc64, destination, sourceStart, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(IntPtr source, T[] destination)
        {
            if (destination is byte[] destination_u8)
            {
                ArrayCopy(source, destination_u8);
            }
            else if (destination is short[] destination_i16)
            {
                ArrayCopy(source, destination_i16);
            }
            else if (destination is int[] destination_i32)
            {
                ArrayCopy(source, destination_i32);
            }
            else if (destination is float[] destination_f32)
            {
                ArrayCopy(source, destination_f32);
            }
            else if (destination is double[] destination_f64)
            {
                ArrayCopy(source, destination_f64);
            }
            else if (destination is Complex32[] destination_fc32)
            {
                ArrayCopy(source, destination_fc32, 0, -1);
            }
            else if (destination is Complex[] destination_fc64)
            {
                ArrayCopy(source, destination_fc64, 0, -1);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(IntPtr source, T[] destination, int destinationStat, int length)
        {
            if (destination is byte[] destination_u8)
            {
                ArrayCopy(source, destination_u8, destinationStat, length);
            }
            else if (destination is short[] destination_i16)
            {
                ArrayCopy(source, destination_i16, destinationStat, length);
            }
            else if (destination is int[] destination_i32)
            {
                ArrayCopy(source, destination_i32, destinationStat, length);
            }
            else if (destination is float[] destination_f32)
            {
                ArrayCopy(source, destination_f32, destinationStat, length);
            }
            else if (destination is double[] destination_f64)
            {
                ArrayCopy(source, destination_f64, destinationStat, length);
            }
            else if (destination is Complex32[] destination_fc32)
            {
                ArrayCopy(source, destination_fc32, destinationStat, length);
            }
            else if (destination is Complex[] destination_fc64)
            {
                ArrayCopy(source, destination_fc64, destinationStat, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[,] source, IntPtr destination)
        {
            if (source is byte[,] source_u8)
            {
                ArrayCopy(source_u8, destination);
            }
            else if (source is short[,] source_i16)
            {
                ArrayCopy(source_i16, destination);
            }
            else if (source is int[,] source_i32)
            {
                ArrayCopy(source_i32, destination);
            }
            else if (source is float[,] source_f32)
            {
                ArrayCopy(source_f32, destination);
            }
            else if (source is double[,] source_f64)
            {
                ArrayCopy(source_f64, destination);
            }
            else if (source is Complex32[,] source_fc32)
            {
                ArrayCopy(source_fc32, destination);
            }
            else if (source is Complex[,] source_fc64)
            {
                ArrayCopy(source_fc64, destination);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(IntPtr source, T[,] destination)
        {
            if (destination is byte[,] destination_u8)
            {
                ArrayCopy(source, destination_u8);
            }
            else if (destination is short[,] destination_i16)
            {
                ArrayCopy(source, destination_i16);
            }
            else if (destination is int[,] destination_i32)
            {
                ArrayCopy(source, destination_i32);
            }
            else if (destination is float[,] destination_f32)
            {
                ArrayCopy(source, destination_f32);
            }
            else if (destination is double[,] destination_f64)
            {
                ArrayCopy(source, destination_f64);
            }
            else if (destination is Complex32[,] destination_fc32)
            {
                ArrayCopy(source, destination_fc32);
            }
            else if (destination is Complex[,] destination_fc64)
            {
                ArrayCopy(source, destination_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, byte[] destination)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ArrayCopy(source, destination_address);

            destination_GC.Free();
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(byte[] source, T[] destination)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ArrayCopy(source_address, destination);

            source_GC.Free();
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(IntPtr source, IntPtr destination, int length)
        {
            T[] dataType = new T[1];
            if (dataType is byte[])
            {
                ippsCopy_8u(source, destination, length);
            }
            else if (dataType is short[])
            {
                ippsCopy_16s(source, destination, length);
            }
            else if (dataType is int[])
            {
                ippsCopy_32s(source, destination, length);
            }
            else if (dataType is float[])
            {
                ippsCopy_32f(source, destination, length);
            }
            else if (dataType is double[])
            {
                ippsCopy_64f(source, destination, length);
            }
            else if (dataType is Complex32[])
            {
                ippsCopy_32fc(source, destination, length);
            }
            else if (dataType is Complex[])
            {
                ippsCopy_64fc(source, destination, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        #endregion

        #region ---- Copy Byte ----

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[] source, byte[] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_8u(source, destination, length);
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[] source, int soureStart, byte[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(byte);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(byte);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_8u(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[,] source, byte[,] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_8u(source, destination, length);
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, byte[] destination)
        {
            ippsCopy_8u(source, destination, destination.Length);
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, byte[] destination, int destinationStart, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(byte);

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_8u(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, byte[,] destination)
        {
            ippsCopy_8u(source, destination, destination.Length);
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[] source, IntPtr destination)
        {
            ippsCopy_8u(source, destination, source.Length);
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[] source, IntPtr destination, int soureStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(byte);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_8u(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// byte 数组拷贝
        /// </summary>
        internal static void ArrayCopy(byte[,] source, IntPtr destination)
        {
            ippsCopy_8u(source, destination, source.Length);
        }

        #endregion

        #region ---- Copy Short ----

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, short[] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_16s(source, destination, length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, int soureStart, short[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(short);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(short);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_16s(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[,] source, short[,] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_16s(source, destination, length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, short[] destination)
        {
            ippsCopy_16s(source, destination, destination.Length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, short[] destination, int destinationStart, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(short);

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_16s(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, short[,] destination)
        {
            ippsCopy_16s(source, destination, destination.Length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, IntPtr destination)
        {
            ippsCopy_16s(source, destination, source.Length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, IntPtr destination, int soureStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(short);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_16s(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[,] source, IntPtr destination)
        {
            ippsCopy_16s(source, destination, source.Length);
        }

        #endregion

        #region ---- Copy I32 ----

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, int[] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_32s(source, destination, length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, int soureStart, int[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(int);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(int);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_32s(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[,] source, int[,] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_32s(source, destination, length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, int[] destination)
        {
            ippsCopy_32s(source, destination, destination.Length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, int[] destination, int destinationStart, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(int);

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_32s(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, int[,] destination)
        {
            ippsCopy_32s(source, destination, destination.Length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, IntPtr destination)
        {
            ippsCopy_32s(source, destination, source.Length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, IntPtr destination, int soureStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(int);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_32s(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[,] source, IntPtr destination)
        {
            ippsCopy_32s(source, destination, source.Length);
        }

        #endregion

        #region ---- Copy Double ----

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[] source, double[] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_64f(source, destination, length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[] source, int soureStart, double[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_64f(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[,] source, double[,] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_64f(source, destination, length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(double[] source, IntPtr destination)
        {
            ippsCopy_64f(source, destination, source.Length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[] source, IntPtr destination, int soureStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_64f(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(double[,] source, IntPtr destination)
        {
            ippsCopy_64f(source, destination, source.Length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, double[] destination)
        {
            ippsCopy_64f(source, destination, destination.Length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, double[] destination, int destinationStart, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double);

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_64f(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, double[,] destination)
        {
            ippsCopy_64f(source, destination, destination.Length);
        }

        #endregion

        #region ---- Copy Float ----

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[] source, float[] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_32f(source, destination, length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[] source, int soureStart, float[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_32f(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[,] source, float[,] destination)
        {
            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);
            ippsCopy_32f(source, destination, length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(float[] source, IntPtr destination)
        {
            ippsCopy_32f(source, destination, source.Length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[] source, IntPtr destination, int soureStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float);

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_32f(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(float[,] source, IntPtr destination)
        {
            ippsCopy_32f(source, destination, source.Length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, float[] destination)
        {
            ippsCopy_32f(source, destination, destination.Length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, float[] destination, int destinationStart, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float);

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_32f(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, float[,] destination)
        {
            ippsCopy_32f(source, destination, destination.Length);
        }

        #endregion

        #region ---- Copy Complex ----

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[] source, Complex[] destination)
        {
            ArrayCopy(source, 0, destination, 0);
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[] source, int soureStart, Complex[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double) * 2;
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_64fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[,] source, Complex[,] destination)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);

            ippsCopy_64fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[] source, IntPtr destination, int soureStart = 0, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_64fc(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[,] source, IntPtr destination)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ippsCopy_64fc(source_address, destination, source.Length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex[] destination, int destinationStart = 0, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_64fc(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex[,] destination)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ippsCopy_64fc(source, destination_address, destination.Length);

            destination_GC.Free();
        }

        #endregion

        #region ---- Copy Complex32 ----

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[] source, Complex32[] destination)
        {
            ArrayCopy(source, 0, destination, 0);
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[] source, int soureStart, Complex32[] destination, int destinationStart, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float) * 2;
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, destination.Length - destinationStart, length);

            ippsCopy_32fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[,] source, Complex32[,] destination)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            // 确定拷贝长度
            int length = GetMinArrayLenth(source.Length, destination.Length, -1);

            ippsCopy_32fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[] source, IntPtr destination, int soureStart = 0, int length = -1)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(source.Length - soureStart, length);

            ippsCopy_32fc(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[,] source, IntPtr destination)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ippsCopy_32fc(source_address, destination, source.Length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex32[] destination, int destinationStart = 0, int length = -1)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(destination.Length - destinationStart, length);

            ippsCopy_32fc(source, destination_address, length);

            destination_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex32[,] destination)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ippsCopy_32fc(source, destination_address, destination.Length);

            destination_GC.Free();
        }

        #endregion


        #region---- Copy : pDst = pSrc ----

        // Byte
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] byte[] pSrc, [Out] byte[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] byte[,] pSrc, [Out] byte[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] byte[] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] byte[,] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] IntPtr pSrc, [Out] byte[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] IntPtr pSrc, [Out] byte[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_8u([In] IntPtr pSrc, [Out] IntPtr pDst, int len);

        // Short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[] pSrc, [Out] short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[,] pSrc, [Out] short[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[,] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] IntPtr pSrc, [Out] short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] IntPtr pSrc, [Out] short[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] IntPtr pSrc, [Out] IntPtr pDst, int len);


        // I32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] int[] pSrc, [Out] int[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] int[,] pSrc, [Out] int[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] int[] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] int[,] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] IntPtr pSrc, [Out] int[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] IntPtr pSrc, [Out] int[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32s([In] IntPtr pSrc, [Out] IntPtr pDst, int len);


        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f([In] float[] pSrc, [Out] float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f([In] float[,] pSrc, [Out] float[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f([In] float[] pSrc, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f([In] float[,] pSrc, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f(IntPtr pSrc, [Out] float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f(IntPtr pSrc, [Out] float[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32f(IntPtr pSrc, IntPtr pDst, int len);


        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f([In] double[] pSrc, [Out] double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f([In] double[,] pSrc, [Out] double[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f(IntPtr pSrc, [Out] double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f(IntPtr pSrc, [Out] double[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f([In] double[] pSrc, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f([In] double[,] pSrc, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64f(IntPtr pSrc, IntPtr pDst, int len);


        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_64fc(IntPtr pSrc, IntPtr pDst, int len);

        #endregion

        #region---- Move : pDst = pSrc ----

        // byte
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_8u([In] byte[] pSrc, [Out] byte[] pDst, int len);

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_16s([In] short[] pSrc, [Out] short[] pDst, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_32s([In] int[] pSrc, [Out] int[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_32f([In] float[] pSrc, [Out] float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_64f([In] double[] pSrc, [Out] double[] pDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMove_64fc(IntPtr pSrc, IntPtr pDst, int len);

        #endregion


        #region---- Copy: y = x ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_scopy(int n, float[] x, int incx, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_scopy(int n, IntPtr x, int incx, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_scopy(int n, float[] x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_scopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_dcopy(int n, double[] x, int incx, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_dcopy(int n, IntPtr x, int incx, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_dcopy(int n, double[] x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_dcopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        // comlex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_ccopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        // comlex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_zcopy(int n, IntPtr x, int incx, IntPtr y, int incy);


        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsPackI(int n, float[] a, int inca, float[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsPackI(int n, IntPtr a, int inca, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdPackI(int n, double[] a, int inca, double[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdPackI(int n, IntPtr a, int inca, double[] y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcPackI(int n, IntPtr a, int inca, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzPackI(int n, IntPtr a, int inca, IntPtr y);

        #endregion

    }
}
