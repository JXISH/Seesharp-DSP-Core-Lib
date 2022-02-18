using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- add Templete ----

        /// <summary>
        /// 数组加法
        /// inout = inout + b
        /// </summary>
        public static void ArrayAdd<T>(T[] inout, T[] b)
        {
            if (inout is short[] inout_i16 && b is short[] b_i16)
            {
                ArrayAdd(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArrayAdd(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArrayAdd(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32[] b_fc32)
            {
                ArrayAdd(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex[] b_fc64)
            {
                ArrayAdd(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组加法
        /// output = a + b
        /// </summary>
        public static void ArrayAdd<T>(T[] a, T[] b, T[] output)
        {
            if (a is short[] a_i16 && b is short[] b_i16 && output is short[] output_i16)
            {
                ArrayAdd(a_i16, b_i16, output_i16);
            }
            else if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                 ArrayAdd(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArrayAdd(a_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32[] b_fc32 && output is Complex32[] output_fc32)
            {
                ArrayAdd(a_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex[] b_fc64 && output is Complex[] output_fc64)
            {
                ArrayAdd(a_fc64, b_fc64, output_fc64); 
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组加法
        /// return = a + b
        /// </summary>
        public static T[] GetArrayAdd<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArrayAdd(a, b, output);
            return output;
        }

        #endregion

        #region ---- add ----

        /// <summary>
        /// I16数组加法
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(short[] inout, short[] b)
        {
            ippsAdd_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16数组加法
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(short[] a, short[] b, short[] output)
        {
            ippsAdd_16s(a, b, output, a.Length);
        }

        /// <summary>
        /// double数组加法
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(double[] inout, double[] b)
        {
            ippsAdd_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double数组加法
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(double[] a, double[] b, double[] output)
        {
            ippsAdd_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float数组加法
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(float[] inout, float[] b)
        {
            ippsAdd_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float数组加法
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(float[] a, float[] b, float[] output)
        {
            ippsAdd_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex数组加法
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(Complex[] inout, Complex[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsAdd_64fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex数组加法
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(Complex[] a, Complex[] b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsAdd_64fc(a_address, b_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32数组加法
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(Complex32[] inout, Complex32[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsAdd_32fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex32数组加法
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(Complex32[] a, Complex32[] b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsAdd_32fc(a_address, b_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- add C Templete ----

        /// <summary>
        /// 数组加常数
        /// inout = inout + b
        /// </summary>
        public static void ArrayAdd<T>(T[] inout, T b)
        {
            if (inout is short[] inout_i16 && b is short b_i16)
            {
                ArrayAdd(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float b_f32)
            {
                ArrayAdd(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double b_f64)
            {
                ArrayAdd(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32 b_fc32)
            {
                ArrayAdd(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex b_fc64)
            {
                ArrayAdd(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组加常数
        /// output = a + b
        /// </summary>
        public static void ArrayAdd<T>(T[] a, T b, T[] output)
        {
            if (a is short[] a_i16 && b is short b_i16 && output is short[] output_i16)
            {
                ArrayAdd(a_i16, b_i16, output_i16);
            }
            else if (a is float[] inout_f32 && b is float b_f32 && output is float[] output_f32)
            {
                ArrayAdd(inout_f32, b_f32, output_f32);
            }
            else if (a is double[] inout_f64 && b is double b_f64 && output is double[] output_f64)
            {
                ArrayAdd(inout_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] inout_fc32 && b is Complex32 b_fc32 && output is Complex32[] output_fc32)
            {
                ArrayAdd(inout_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] inout_fc64 && b is Complex b_fc64 && output is Complex[] output_fc64)
            {
                ArrayAdd(inout_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组加常数
        /// return = a + b
        /// </summary>
        public static T[] GetArrayAdd<T>(T[] a, T b)
        {
            T[] output = new T[a.Length];
            ArrayAdd(a, b, output);
            return output;
        }

        #endregion

        #region ---- add C ----
        /// <summary>
        /// I16 数组加常数
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(short[] inout, short b)
        {
            ippsAddC_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16 数组加常数
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(short[] a, short b, short[] output)
        {
            Vector.ArrayCopy(a, output);
            ippsAddC_16s_I(b, output, a.Length);
        }

        /// <summary>
        /// double 数组加常数
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(double[] inout, double b)
        {
            ippsAddC_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 数组加常数
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(double[] a, double b, double[] output)
        {
            ippsAddC_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float 数组加常数
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(float[] inout, float b)
        {
            ippsAddC_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 数组加常数
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(float[] a, float b, float[] output)
        {
            ippsAddC_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex 数组加常数
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(Complex[] inout, Complex b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsAddC_64fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex 数组加常数
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(Complex[] a, Complex b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsAddC_64fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组加常数
        /// inout = inout + b
        /// </summary>
        internal static void ArrayAdd(Complex32[] inout, Complex32 b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsAddC_32fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 数组加常数
        /// output = a + b
        /// </summary>
        internal static void ArrayAdd(Complex32[] a, Complex32 b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsAddC_32fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- Add : pSrcDst =  pSrc + pSrcDst -----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_16s_I(short[] pSrc, short[] pSrcDst, int len);

        // U32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32u_I(uint[] pSrc, uint[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32f_I(float[] pSrc, float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_64f_I(double[] pSrc, double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_64fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        #endregion

        #region ---- Add : pDst =  pSrc1 + pSrc2 -----

        // short 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_16s(short[] pSrc1, short[] pSrc2, short[] pDst, int len);

        // U32 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32u(uint[] pSrc1, uint[] pSrc2, uint[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_32fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAdd_64fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        #endregion

        #region ---- AddC : pSrcDst =  val + pSrcDst -----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_16s_I(short val, short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_32f_I(float val, float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_64f_I(double val, double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_32fc_I(Complex32 val, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_64fc_I(Complex val, IntPtr pSrcDst, int len);

        #endregion

        #region ---- AddC : pDst =  val + pSrc -----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_32f(float[] pSrc, float val, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_64f(double[] pSrc, double val, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_32fc(IntPtr pSrc, Complex32 val, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddC_64fc(IntPtr pSrc, Complex val, IntPtr pDst, int len);

        #endregion
    }
}
