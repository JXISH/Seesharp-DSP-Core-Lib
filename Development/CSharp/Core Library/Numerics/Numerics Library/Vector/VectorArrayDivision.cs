using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Division Templete ----

        /// <summary>
        /// 数组除法
        /// inout = inout / b
        /// </summary>
        public static void ArrayDivision<T>(T[] inout, T[] b)
        {
            if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArrayDivision(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArrayDivision(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32[] b_fc32)
            {
                ArrayDivision(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex[] b_fc64)
            {
                ArrayDivision(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组除法
        /// output = a / b
        /// </summary>
        public static void ArrayDivision<T>(T[] a, T[] b, T[] output)
        {
            if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                ArrayDivision(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArrayDivision(a_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32[] b_fc32 && output is Complex32[] output_fc32)
            {
                ArrayDivision(a_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex[] b_fc64 && output is Complex[] output_fc64)
            {
                ArrayDivision(a_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组除法
        /// return = a / b
        /// </summary>
        public static T[] GetArrayDivision<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArrayDivision(a, b, output);
            return output;
        }


        #endregion

        #region ---- Division ----

        /// <summary>
        /// double 数组除法
        /// inout = inout / b
        /// </summary>
        internal static void ArrayDivision(double[] inout, double[] b)
        {
            ippsDiv_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 数组除法
        /// output = a / b
        /// </summary>
        internal static void ArrayDivision(double[] a, double[] b, double[] output)
        {
            ippsDiv_64f(b, a, output, a.Length);
        }

        /// <summary>
        /// float 数组除法
        /// inout = inout / b
        /// </summary>
        internal static void ArrayDivision(float[] inout, float[] b)
        {
            ippsDiv_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 数组除法
        /// output = a / b
        /// </summary>
        internal static void ArrayDivision(float[] a, float[] b, float[] output)
        {
            ippsDiv_32f(b, a, output, a.Length);
        }

        /// <summary>
        /// Complex 数组除法
        /// inout = inout / b
        /// </summary>
        internal static void ArrayDivision(Complex[] inout, Complex[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsDiv_64fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex 数组除法
        /// output = a / b
        /// </summary>
        internal static void ArrayDivision(Complex[] a, Complex[] b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsDiv_64fc(b_address,a_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组除法
        /// inout = inout / b
        /// </summary>
        internal static void ArrayDivision(Complex32[] inout, Complex32[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsDiv_32fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex32 数组除法
        /// output = a / b
        /// </summary>
        internal static void ArrayDivision(Complex32[] a, Complex32[] b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsDiv_32fc(b_address, a_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- Division : pSrcDst = pSrcDst / pSrc ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_32f_I(float[] pSrc, float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_64f_I(double[] pSrc, double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_32fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_64fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        #endregion

        #region ---- Division : pDst = pSrc2 / pSrc1 ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_32fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDiv_64fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);
        #endregion
    }
}
