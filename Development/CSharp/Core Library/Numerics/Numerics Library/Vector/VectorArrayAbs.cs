using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- abs Templete ----

        /// <summary>
        /// 数组取模
        /// inout = Abs (inout)
        /// </summary>
        public static void ArrayAbs<T>(T [] inout)
        {
            if (inout is short[] inout_i16)
            {
                ArrayAbs(inout_i16);
            }
            else if (inout is int[] inout_i32)
            {
                ArrayAbs(inout_i32);
            }
            else if (inout is float[] inout_f32)
            {
                ArrayAbs(inout_f32);
            }
            else if (inout is double[] inout_f64)
            {
                ArrayAbs(inout_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组取模
        /// output = Abs (a)
        /// </summary>
        public static void ArrayAbs<T>(T[] a, T[] output)
        {
            if (a is short[] a_i16 && output is short[] output_i16)
            {
               ArrayAbs(a_i16, output_i16);
            }
            else if (a is int[] a_i32 && output is int[] output_i32)
            {
                ArrayAbs(a_i32, output_i32);
            }
            else if (a is float[] a_f32 && output is float[] output_f32)
            {
                ArrayAbs(a_f32, output_f32);
            }
            else if (a is double[] a_f64 && output is double[] output_f64)
            {
                ArrayAbs(a_f64, output_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组取模
        /// return = Abs (a)
        /// </summary>
        public static T[] GetArrayAbs<T>(T[] a)
        {
            T[] output = new T[a.Length];
            ArrayAbs(a, output);
            return output;
        }

        #endregion

        #region ---- abs ----

        /// <summary>
        /// short 数组取模
        /// inout = Abs (inout)
        /// </summary>
        internal static void ArrayAbs(short[] inout)
        {
            ippsAbs_16s_I(inout, inout.Length);
        }

        /// <summary>
        /// short 数组取模
        /// output = Abs (a)
        /// </summary>
        internal static void ArrayAbs(short[] a, short[] output)
        {
            ippsAbs_16s(a, output, a.Length);
        }

        /// <summary>
        /// I32 数组取模
        /// inout = Abs (inout)
        /// </summary>
        internal static void ArrayAbs(int[] inout)
        {
            ippsAbs_32s_I(inout, inout.Length);
        }

        /// <summary>
        /// I32 数组取模
        /// output = Abs (a)
        /// </summary>
        internal static void ArrayAbs(int[] a, int[] output)
        {
            ippsAbs_32s(a, output, a.Length);
        }

        /// <summary>
        /// float数组取模
        /// inout = Abs (inout)
        /// </summary>
        internal static void ArrayAbs(float[] inout)
        {
            ippsAbs_32f_I(inout, inout.Length);
        }

        /// <summary>
        /// float数组取模
        /// output = Abs (a)
        /// </summary>
        internal static void ArrayAbs(float[] a, float[] output)
        {
            ippsAbs_32f(a, output, a.Length);
        }

        /// <summary>
        /// double数组取模
        /// inout = Abs (inout)
        /// </summary>
        internal static void ArrayAbs(double[] inout)
        {
            ippsAbs_64f_I(inout, inout.Length);
        }

        /// <summary>
        /// double数组取模
        /// output = Abs (a)
        /// </summary>
        internal static void ArrayAbs(double[] a, double[] output)
        {
            ippsAbs_64f(a, output, a.Length);
        }

        #endregion

        #region---- Abs: pSrcDst = abs(pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_16s_I(short[] pSrcDst, int len);

        // I32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_32s_I(int[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_32f_I(float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_64f_I(double[] pSrcDst, int len);

        #endregion

        #region---- Abs: pDst = abs(pSrc) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_16s(short[] pSrc, short[] pDst, int len);
        
        // I32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_32s(int[] pSrc, int[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_32f(float[] pSrc, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAbs_64f(double[] pSrc, double[] pDst, int len);

        #endregion
    }
}
