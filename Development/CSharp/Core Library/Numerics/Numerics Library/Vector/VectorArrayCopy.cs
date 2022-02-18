using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Copy Template ----

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
            if (source is short[] source_i16 && destination is short[] destination_i16)
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
            if (source is short[] source_i16 && destination is short[] destination_i16)
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
            if (source is short[,] source_i16 && destination is short[,] destination_i16)
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
        public static void ArrayCopy<T>(T[] source, IntPtr destination, int length = 0)
        {
            if (source is short[] source_i16)
            {
                ArrayCopy(source_i16, destination, length);
            }
            else if (source is int[] source_i32)
            {
                ArrayCopy(source_i32, destination, length);
            }
            else if (source is float[] source_f32)
            {
                ArrayCopy(source_f32, destination, length);
            }
            else if (source is double[] source_f64)
            {
                ArrayCopy(source_f64, destination, length);
            }
            else if (source is Complex32[] source_fc32)
            {
                ArrayCopy(source_fc32, destination, length);
            }
            else if (source is Complex[] source_fc64)
            {
                ArrayCopy(source_fc64, destination, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(T[] source, byte[] destination, int length = 0)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ArrayCopy(source, destination_address,length);

            destination_GC.Free();
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(IntPtr source, T[] destination, int length = 0)
        {
            if (destination is short[] destination_i16)
            {
                ArrayCopy(source, destination_i16, length);
            }
            else if (destination is int[] destination_i32)
            {
                ArrayCopy(source, destination_i32, length);
            }
            else if (destination is float[] destination_f32)
            {
                ArrayCopy(source, destination_f32, length);
            }
            else if (destination is double[] destination_f64)
            {
                ArrayCopy(source, destination_f64, length);
            }
            else if (destination is Complex32[] destination_fc32)
            {
                ArrayCopy(source, destination_fc32, length);
            }
            else if (destination is Complex[] destination_fc64)
            {
                ArrayCopy(source, destination_fc64, length);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组拷贝模板
        /// </summary>
        public static void ArrayCopy<T>(byte[] source, T[] destination, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ArrayCopy(source_address, destination, length);

            source_GC.Free();
        }

        #endregion

        #region ---- Copy Short ----

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, short[] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_16s(source, destination, length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, int soureStart, short[] destination, int destinationStart, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(short);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(short);

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

            ippsCopy_16s(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[,] source, short[,] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_16s(source, destination, length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, short[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }
            ippsCopy_16s(source, destination, length);
        }

        /// <summary>
        /// short 数组拷贝
        /// </summary>
        internal static void ArrayCopy(short[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }
            ippsCopy_16s(source, destination, length);
        }

        #endregion

        #region ---- Copy I32 ----

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, int[] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_32s(source, destination, length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, int soureStart, int[] destination, int destinationStart, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(int);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(int);

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

            ippsCopy_32s(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[,] source, int[,] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_32s(source, destination, length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, int[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }
            ippsCopy_32s(source, destination, length);
        }

        /// <summary>
        /// I32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(int[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }
            ippsCopy_32s(source, destination, length);
        }

        #endregion

        #region ---- Copy Double ----

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[] source, double[] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_64f(source, destination, length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[] source, int soureStart, double[] destination, int destinationStart, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double);

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

            ippsCopy_64f(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 double 数组拷贝
        /// </summary>
        internal static void ArrayCopy(double[,] source, double[,] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_64f(source, destination, length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(double[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }
            ippsCopy_64f(source, destination, length);
        }

        /// <summary>
        /// double 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, double[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }
            ippsCopy_64f(source, destination, length);
        }

        #endregion

        #region ---- Copy Float ----

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[] source, float[] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_32f(source, destination, length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[] source, int soureStart, float[] destination, int destinationStart, int length = 0)
        {

            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float);
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float);

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

            ippsCopy_32f(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// 二维 float 数组拷贝
        /// </summary>
        internal static void ArrayCopy(float[,] source, float[,] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            ippsCopy_32f(source, destination, length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(float[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }
            ippsCopy_32f(source, destination, length);
        }

        /// <summary>
        /// float 数组拷贝
        /// </summary>
        public static void ArrayCopy(IntPtr source, float[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }
            ippsCopy_32f(source, destination, length);
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
        internal static void ArrayCopy(Complex[] source, int soureStart, Complex[] destination, int destinationStart, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(double) * 2;
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(double) * 2;

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

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

            int length = Math.Min(source.Length, destination.Length);

            ippsCopy_64fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }

            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ippsCopy_64fc(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }

            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ippsCopy_64fc(source, destination_address, length);

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
        internal static void ArrayCopy(Complex32[] source, int soureStart, Complex32[] destination, int destinationStart, int length = 0)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject() + soureStart * sizeof(float) * 2;
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject() + destinationStart * sizeof(float) * 2;

            if (length <= 0) { length = Math.Min(source.Length - soureStart, destination.Length - destinationStart); }
            else { length = Math.Min(length, Math.Min(source.Length - soureStart, destination.Length - destinationStart)); }

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

            int length = Math.Min(source.Length, destination.Length);

            ippsCopy_32fc(source_address, destination_address, length);

            source_GC.Free();
            destination_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(Complex32[] source, IntPtr destination, int length = 0)
        {
            if (length <= 0) { length = source.Length; }
            else { length = Math.Min(length, source.Length); }

            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            ippsCopy_32fc(source_address, destination, length);

            source_GC.Free();
        }

        /// <summary>
        /// Complex32 数组拷贝
        /// </summary>
        internal static void ArrayCopy(IntPtr source, Complex32[] destination, int length = 0)
        {
            if (length <= 0) { length = destination.Length; }
            else { length = Math.Min(length, destination.Length); }

            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            ippsCopy_32fc(source, destination_address, length);

            destination_GC.Free();
        }

        #endregion


        #region---- Copy : pDst = pSrc ----

        // Short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[] pSrc, [Out] short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[,] pSrc, [Out] short[,] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] short[] pSrc, [Out] IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCopy_16s([In] IntPtr pSrc, [Out] short[] pDst, int len);

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
        private static extern int ippsCopy_32s([In] IntPtr pSrc, [Out] int[] pDst, int len);

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
        private static extern int ippsCopy_32f(IntPtr pSrc, [Out] float[] pDst, int len);

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
        private static extern int ippsCopy_64f([In] double[] pSrc, IntPtr pDst, int len);

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
