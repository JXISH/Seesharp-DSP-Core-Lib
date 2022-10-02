using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        /// <summary>
        /// 数组平移自减
        /// </summary>
        public static T[] SelfDiffer<T>(T[] data, int delta = 1, bool rotate = false)
        {
            int length = data.Length;
            T[] dataDiffer = new T[length];

            Vector.ArrayRotate(data, dataDiffer, delta);

            // 数组减法
            Vector.ArraySub(dataDiffer, data);

            if (!rotate)
            {
                T[] init = new T[delta];
                Vector.ArrayCopy(init, dataDiffer);
            }

            return dataDiffer;
        }

        #region ---- sub Templete ----
        /// <summary>
        /// 数组减法
        /// inout = inout - b
        /// </summary>
        public static void ArraySub<T>(T[] inout, T[] b)
        {
            if (inout is short[] inout_i16 && b is short[] b_i16)
            {
                ArraySub(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArraySub(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArraySub(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32[] b_fc32)
            {
                ArraySub(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex[] b_fc64)
            {
                ArraySub(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减法
        /// output = a - b
        /// </summary>
        public static void ArraySub<T>(T[] a, T[] b, T[] output)
        {
            if (a is short[] a_i16 && b is short[] b_i16 && output is short[] output_i16)
            {
                ArraySub(a_i16, b_i16, output_i16);
            }
            else if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                ArraySub(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArraySub(a_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32[] b_fc32 && output is Complex32[] output_fc32)
            {
                ArraySub(a_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex[] b_fc64 && output is Complex[] output_fc64)
            {
                ArraySub(a_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减法
        /// return = a - b
        /// </summary>
        public static T[] GetArraySub<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArraySub(a, b, output);
            return output;
        }

        #endregion

        #region ---- sub ----

        /// <summary>
        /// I16 数组减法
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(short[] inout, short[] b)
        {
            ippsSub_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16 数组减法
        /// output = a - b
        /// </summary>
        internal static void ArraySub(short[] a, short[] b, short[] output)
        {
            ippsSub_16s(b, a, output, a.Length);
        }

        /// <summary>
        /// double 数组减法
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(double[] inout, double[] b)
        {
            ippsSub_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 数组减法
        /// output = a - b
        /// </summary>
        internal static void ArraySub(double[] a, double[] b, double[] output)
        {
            ippsSub_64f(b, a, output, a.Length);
        }

        /// <summary>
        /// float 数组减法
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(float[] inout, float[] b)
        {
            ippsSub_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 数组减法
        /// output = a - b
        /// </summary>
        internal static void ArraySub(float[] a, float[] b, float[] output)
        {
            ippsSub_32f(b, a, output, a.Length);
        }

        /// <summary>
        /// Complex 数组减法
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(Complex[] inout, Complex[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsSub_64fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex 数组减法
        /// output = a - b
        /// </summary>
        internal static void ArraySub(Complex[] a, Complex[] b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSub_64fc(b_address, a_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组减法
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(Complex32[] inout, Complex32[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsSub_32fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex32 数组减法
        /// output = a - b
        /// </summary>
        internal static void ArraySub(Complex32[] a, Complex32[] b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSub_32fc(b_address, a_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- sub C Templete ----

        /// <summary>
        /// 数组减常数
        /// inout = inout - b
        /// </summary>
        public static void ArraySub<T>(T[] inout, T b)
        {
            if (inout is short[] inout_i16 && b is short b_i16)
            {
                ArraySub(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float b_f32)
            {
                ArraySub(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double b_f64)
            {
                ArraySub(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32 b_fc32)
            {
                ArraySub(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex b_fc64)
            {
                ArraySub(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减常数
        /// output = a - b
        /// </summary>
        public static void ArraySub<T>(T[] a, T b, T[] output)
        {
            if (a is short[] a_i16 && b is short b_i16 && output is short[] output_i16)
            {
                ArraySub(a_i16, b_i16, output_i16);
            }
            else if (a is float[] inout_f32 && b is float b_f32 && output is float[] output_f32)
            {
                ArraySub(inout_f32, b_f32, output_f32);
            }
            else if (a is double[] inout_f64 && b is double b_f64 && output is double[] output_f64)
            {
                ArraySub(inout_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] inout_fc32 && b is Complex32 b_fc32 && output is Complex32[] output_fc32)
            {
                ArraySub(inout_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] inout_fc64 && b is Complex b_fc64 && output is Complex[] output_fc64)
            {
                ArraySub(inout_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减常数
        /// return = a - b
        /// </summary>
        public static T[] GetArraySub<T>(T[] a, T b)
        {
            T[] output = new T[a.Length];
            ArraySub(a, b, output);
            return output;
        }

        #endregion

        #region ---- sub C ----

        /// <summary>
        /// I16 数组减常数
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(short[] inout, short b)
        {
            ippsSubC_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16 数组减常数
        /// output = a - b
        /// </summary>
        internal static void ArraySub(short[] a, short b, short[] output)
        {
            Vector.ArrayCopy(a, output);
            ippsSubC_16s_I(b, output, a.Length);
        }

        /// <summary>
        /// double 数组减常数
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(double[] inout, double b)
        {
            ippsSubC_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 数组减常数
        /// output = a - b
        /// </summary>
        internal static void ArraySub(double[] a, double b, double[] output)
        {
            ippsSubC_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float 数组减常数
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(float[] inout, float b)
        {
            ippsSubC_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 数组减常数
        /// output = a - b
        /// </summary>
        internal static void ArraySub(float[] a, float b, float[] output)
        {
            ippsSubC_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex 数组减常数
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(Complex[] inout, Complex b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSubC_64fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex 数组减常数
        /// output = a - b
        /// </summary>
        internal static void ArraySub(Complex[] a, Complex b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSubC_64fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组减常数
        /// inout = inout - b
        /// </summary>
        internal static void ArraySub(Complex32[] inout, Complex32 b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSubC_32fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 数组减常数
        /// output = a - b
        /// </summary>
        internal static void ArraySub(Complex32[] a, Complex32 b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSubC_32fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- sub C Reverse Templete ----

        /// <summary>
        /// 常数减数组
        /// inout = b - inout
        /// </summary>
        public static void ArraySub<T>(T b, T[] inout)
        {
            if (inout is float[] inout_f32 && b is float b_f32)
            {
                ArraySub(b_f32, inout_f32);
            }
            else if (inout is double[] inout_f64 && b is double b_f64)
            {
                ArraySub(b_f64, inout_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32 b_fc32)
            {
                ArraySub(b_fc32, inout_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex b_fc64)
            {
                ArraySub(b_fc64, inout_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减常数
        /// output = b - a
        /// </summary>
        public static void ArraySub<T>(T b, T[] a, T[] output)
        {
            if (a is float[] inout_f32 && b is float b_f32 && output is float[] output_f32)
            {
                ArraySub(b_f32, inout_f32, output_f32);
            }
            else if (a is double[] inout_f64 && b is double b_f64 && output is double[] output_f64)
            {
                ArraySub(b_f64, inout_f64, output_f64);
            }
            else if (a is Complex32[] inout_fc32 && b is Complex32 b_fc32 && output is Complex32[] output_fc32)
            {
                ArraySub(b_fc32, inout_fc32, output_fc32);
            }
            else if (a is Complex[] inout_fc64 && b is Complex b_fc64 && output is Complex[] output_fc64)
            {
                ArraySub(b_fc64, inout_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组减常数
        /// return = b - a
        /// </summary>
        public static T[] GetArraySub<T>(T b, T[] a)
        {
            T[] output = new T[a.Length];
            ArraySub(b, a, output);
            return output;
        }

        #endregion

        #region ---- sub C Reverse ----
        /// <summary>
        /// double 常数减数组
        /// inout =  b - inout
        /// </summary>
        internal static void ArraySub(double b, double[] inout)
        {
            ippsSubCRev_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 常数减数组
        /// output = b - a
        /// </summary>
        internal static void ArraySub(double b, double[] a, double[] output)
        {
            ippsSubCRev_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float 常数减数组
        /// inout =  b - inout
        /// </summary>
        internal static void ArraySub(float b, float[] inout)
        {
            ippsSubCRev_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 常数减数组
        /// output = b - a
        /// </summary>
        internal static void ArraySub(float b, float[] a, float[] output)
        {
            ippsSubCRev_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex 常数减数组
        /// inout = b - inout
        /// </summary>
        internal static void ArraySub(Complex b, Complex[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSubCRev_64fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex 常数减数组
        /// output = b - a
        /// </summary>
        internal static void ArraySub(Complex b, Complex[] a, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSubCRev_64fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 常数减数组
        /// inout = b - inout
        /// </summary>
        internal static void ArraySub(Complex32 b, Complex32[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSubCRev_32fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 常数减数组
        /// output = b - a
        /// </summary>
        internal static void ArraySub(Complex32 b, Complex32[] a, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSubCRev_32fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- Sub : pSrcDst =  pSrcDst - pSrc -----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_16s_I(short[] pSrc, short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_32f_I(float[] pSrc, float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_64f_I(double[] pSrc, double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_32fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_64fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        #endregion

        #region ---- Sub : pDst = pSrc2 - pSrc1 -----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_16s(short[] pSrc1, short[] pSrc2, short[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_32fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSub_64fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        #endregion

        #region ---- SubC : pSrcDst = pSrcDst - val -----
        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_16s_I(short val, short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_32f_I(float val, float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_64f_I(double val, double[] pSrcDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_32fc_I(Complex32 val, IntPtr pSrcDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_64fc_I(Complex val, IntPtr pSrcDst, int len);

        #endregion

        #region ---- SubC : pDst = pSrc - val -----
        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_32f(float[] pSrc, float val, float[] pDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_32fc(IntPtr pSrc, Complex32 val, IntPtr pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_64f(double[] pSrc, double val, double[] pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubC_64fc(IntPtr pSrc, Complex val, IntPtr pDst, int len);
        #endregion

        #region ---- SubC Rev : pSrcDst = val - pSrcDst -----
        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_32f_I(float val, float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_64f_I(double val, double[] pSrcDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_32fc_I(Complex32 val, IntPtr pSrcDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_64fc_I(Complex val, IntPtr pSrcDst, int len);
        #endregion

        #region ---- SubC Rev : pDst = val - pSrc -----
        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_32f(float[] pSrc, float val, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_64f(double[] pSrc, double val, double[] pDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_32fc(IntPtr pSrc, Complex32 val, IntPtr pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSubCRev_64fc(IntPtr pSrc, Complex val, IntPtr pDst, int len);
        #endregion
    }
}

