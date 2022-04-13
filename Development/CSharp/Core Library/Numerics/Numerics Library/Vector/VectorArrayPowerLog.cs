//#define vectorMKL

using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Ln ----

        /// <summary>
        /// double 数组取Ln
        /// inout = ln(inout)
        /// </summary>
        public static void ArrayLn(double[] inout)
        {
#if vectorMKL
            vdLn(inout.Length, inout, inout);
#else
            ippsLn_64f_I(inout, inout.Length);
#endif
        }

        /// <summary>
        /// double 数组取Ln
        /// output = ln (a)
        /// </summary>
        public static void ArrayLn(double[] a, double[] output)
        {
#if vectorMKL
            vdLn(a.Length, a, output);
#else
            ippsLn_64f(a, output, a.Length);
#endif
        }

        /// <summary>
        /// float 数组取Ln
        /// inout = ln(inout)
        /// </summary>
        public static void ArrayLn(float[] inout)
        {
#if vectorMKL
            vsLn(inout.Length, inout, inout);
#else
            ippsLn_32f_I(inout, inout.Length);
#endif
        }

        /// <summary>
        /// float 数组取Ln
        /// inout = ln(inout)
        /// </summary>
        public static void ArrayLn(float[] a, float[] output)
        {
#if vectorMKL
            vsLn(a.Length, a, output);
#else
            ippsLn_32f(a, output, a.Length);
#endif
        }

        #endregion

        #region ---- Log10 ----

        /// <summary>
        /// double 数组取Log10
        /// inout = log10(inout)
        /// </summary>
        public static void ArrayLog10(double[] inout)
        {
#if vectorMKL
            vdLog10(inout.Length, inout, inout);
#else
            ArrayLn(inout);
            ippsMulC_64f_I(Math.Log10(Math.E), inout, inout.Length);
#endif
        }

        /// <summary>
        /// double 数组取Log10
        /// output = log10 (a)
        /// </summary>
        public static void ArrayLog10(double[] a, double[] output)
        {
#if vectorMKL
            vdLog10(a.Length, a, output);
#else
            ArrayLn(a, output); 
            ippsMulC_64f_I(Math.Log10(Math.E), output, output.Length);
#endif
        }

        /// <summary>
        /// float 数组取Log10
        /// inout = log10(inout)
        /// </summary>
        public static void ArrayLog10(float[] inout)
        {
#if vectorMKL
            vsLog10(inout.Length, inout, inout);
#else
            ArrayLn(inout);
            ippsMulC_32f_I((float)(Math.Log10(Math.E)), inout, inout.Length);
#endif
        }

        /// <summary>
        /// float 数组取Log10
        /// inout = log10(inout)
        /// </summary>
        public static void ArrayLog10(float[] a, float[] output)
        {
#if vectorMKL
            vsLog10(a.Length, a, output);
#else
            ArrayLn(a, output);
            ippsMulC_32f_I((float)(Math.Log10(Math.E)), output, output.Length);
#endif
        }

        #endregion

        #region ---- Log2 ----

        /// <summary>
        /// double 数组取Log2
        /// inout = log2(inout)
        /// </summary>
        public static void ArrayLog2(double[] inout)
        {
#if vectorMKL
            vdLog2(inout.Length, inout, inout);
#else
            ArrayLog10(inout);
            ippsMulC_64f_I(1.0 / Math.Log10(2.0f), inout, inout.Length);
#endif
        }

        /// <summary>
        /// double 数组取Log2
        /// output = log2 (a)
        /// </summary>
        public static void ArrayLog2(double[] a, double[] output)
        {
#if vectorMKL
            vdLog2(a.Length, a, output);
#else
            ArrayLog10(a, output);
            ippsMulC_64f_I(1.0 / Math.Log10(2.0), output, output.Length);
#endif
        }

        /// <summary>
        /// float 数组取Log10
        /// inout = log2(inout)
        /// </summary>
        public static void ArrayLog2(float[] inout)
        {
#if vectorMKL
            vsLog2(inout.Length, inout, inout);
#else
            ArrayLog10(inout);
            ippsMulC_32f_I(1.0f / (float)(Math.Log10(2.0f)), inout, inout.Length);
#endif
        }

        /// <summary>
        /// float 数组取Log2
        /// inout = log2(inout)
        /// </summary>
        public static void ArrayLog2(float[] a, float[] output)
        {
#if vectorMKL
            vsLog2(a.Length, a, output);
#else
            ArrayLog10(a, output);
            ippsMulC_32f_I((float)(1.0 / Math.Log10(2.0f)), output, output.Length);
#endif
        }

        #endregion


        #region ---- Exp ----

        /// <summary>
        /// double 数组取Exp
        /// inout = Exp (inout)
        /// </summary>
        public static void ArrayExp(double[] inout)
        {
#if vectorMKL
            vdExp(inout.Length, inout, inout);
#else

            ippsExp_64f_I(inout, inout.Length);
#endif
        }

        /// <summary>
        /// double 数组取Exp
        /// output = Exp(a)
        /// </summary>
        public static void ArrayExp(double[] a, double[] output)
        {
#if vectorMKL
            vdExp(a.Length, a, output);
#else
            ippsExp_64f(a, output, a.Length);
#endif
        }

        /// <summary>
        /// float 数组取Exp
        /// inout = Exp (inout)
        /// </summary>
        public static void ArrayExp(float[] inout)
        {
#if vectorMKL
            vsExp(inout.Length, inout, inout);
#else
            ippsExp_32f_I(inout, inout.Length);
#endif
        }

        /// <summary>
        /// float 数组取Exp
        /// output = Exp (a)
        /// </summary>
        public static void ArrayExp(float[] a, float[] output)
        {
#if vectorMKL
            vsExp(a.Length, a, output);
#else
            ippsExp_32f(a, output, a.Length);
#endif
        }

        #endregion

        #region ---- 10exp ----

        /// <summary>
        /// double 数组取Exp10
        /// inout = Exp10 (inout)
        /// </summary>
        public static void ArrayExp10(double[] inout)
        {
#if vectorMKL
            vdExp10(inout.Length, inout, inout);
#else
            for (int i = 0; i < inout.Length; i++)
            {
                inout[i] = Math.Pow(10.0, inout[i]);
            }
#endif
        }

        /// <summary>
        /// double 数组取Exp10
        /// output = Exp10(a)
        /// </summary>
        public static void ArrayExp10(double[] a, double[] output)
        {
#if vectorMKL
            vdExp10(a.Length, a, output);
#else
            for (int i = 0; i < a.Length; i++)
            {
                output[i] = Math.Pow(10.0, a[i]);
            }
#endif
        }

        /// <summary>
        /// float 数组取Exp10
        /// inout = Exp10 (inout)
        /// </summary>
        public static void ArrayExp10(float[] inout)
        {
#if vectorMKL
            vsExp10(inout.Length, inout, inout);
#else
            for (int i = 0; i < inout.Length; i++)
            {
                inout[i] =(float) Math.Pow(10.0, (double)inout[i]);
            }
#endif
        }

        /// <summary>
        /// float 数组取Exp10
        /// output = Exp10 (a)
        /// </summary>
        public static void ArrayExp10(float[] a, float[] output)
        {
#if vectorMKL
            vsExp10(a.Length, a, output);
#else
            for (int i = 0; i < a.Length; i++)
            {
                output[i] = (float)Math.Pow(10.0, (double)a[i]);
            }
#endif
        }

        #endregion

        #region ---- 2exp ----

        /// <summary>
        /// double 数组取Exp2
        /// inout = Exp2 (inout)
        /// </summary>
        public static void ArrayExp2(double[] inout)
        {
#if vectorMKL
            vdExp2(inout.Length, inout, inout);
#else
            for (int i = 0; i < inout.Length; i++)
            {
                inout[i] = Math.Pow(2.0, inout[i]);
            }
#endif
        }

        /// <summary>
        /// double 数组取Exp2
        /// output = Exp2(a)
        /// </summary>
        public static void ArrayExp2(double[] a, double[] output)
        {
#if vectorMKL
            vdExp2(a.Length, a, output);
#else
            for (int i = 0; i < a.Length; i++)
            {
                output[i] = Math.Pow(2.0, a[i]);
            }
#endif
        }

        /// <summary>
        /// float 数组取Exp2
        /// inout = Exp2 (inout)
        /// </summary>
        public static void ArrayExp2(float[] inout)
        {
#if vectorMKL
            vsExp2(inout.Length, inout, inout);
#else
            for (int i = 0; i < inout.Length; i++)
            {
                inout[i] =(float) Math.Pow(2.0, (double)inout[i]);
            }
#endif
        }

        /// <summary>
        /// float 数组取Exp2
        /// output = Exp2 (a)
        /// </summary>
        public static void ArrayExp2(float[] a, float[] output)
        {
#if vectorMKL
            vsExp10(a.Length, a, output);
#else
            for (int i = 0; i < a.Length; i++)
            {
                output[i] = (float)Math.Pow(2.0, (double)a[i]);
            }
#endif
        }

        #endregion



        #region---- Ln: y = ln(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsLn(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdLn(int n, double[] a, double[] y);

        #endregion

        #region---- Log10: y = log10(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsLog10(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdLog10(int n, double[] a, double[] y);

        #endregion

        #region---- Log2: y = log2(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsLog2(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdLog2(int n, double[] a, double[] y);

        #endregion

        #region---- Ln : pSrcDst = ln (pSrcDst) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsLn_32f_I(float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsLn_64f_I(double[] pSrcDst, int len);

        #endregion

        #region---- Ln : pDst = ln (pSrc) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsLn_32f(float[] pSrc, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsLn_64f(double[] pSrc, double[] pDst, int len);

        #endregion


        #region---- exponential: y = e**(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsExp(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdExp(int n, double[] a, double[] y);

        #endregion

        #region---- base 10 exponential: y = 10**(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsExp10(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdExp10(int n, double[] a, double[] y);

        #endregion

        #region---- base 2 exponential: y = 2**(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsExp2(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdExp2(int n, double[] a, double[] y);

        #endregion

        #region---- Exp : pDst = Exp (pSrc) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsExp_32f(float[] pSrc, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsExp_64f(double[] pSrc, double[] pDst, int len);

        #endregion

        #region---- Exp : pSrcDst = Exp (pSrcDst) ----
        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsExp_32f_I(float[] pSrcDst, int len);
        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsExp_64f_I(double[] pSrcDst, int len);
        #endregion
    }
}
