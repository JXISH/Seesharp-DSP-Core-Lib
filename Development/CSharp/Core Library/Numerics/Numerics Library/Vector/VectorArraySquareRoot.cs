using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Square Templete ----

        /// <summary>
        /// 数组取平方
        /// inout = inout ** 2
        /// </summary>
        public static void ArraySquare<T>(T[] inout)
        {
            if (inout is float[] inout_f32)
            {
                ArraySquare(inout_f32);
            }
            else if (inout is double[] inout_f64)
            {
                ArraySquare(inout_f64);
            }
            else if (inout is Complex32[] inout_fc32)
            {
                ArraySquare(inout_fc32);
            }
            else if (inout is Complex[] inout_fc64)
            {
                ArraySquare(inout_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组取平方
        /// output = a ** 2
        /// </summary>
        public static void ArraySquare<T>(T[] a, T[] output)
        {
            if (a is float[] a_f32 && output is float[] output_f32)
            {
                ArraySquare(a_f32, output_f32);
            }
            else if (a is double[] a_f64 && output is double[] output_f64)
            {
                ArraySquare(a_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && output is Complex32[] output_fc32)
            {
                ArraySquare(a_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && output is Complex[] output_fc64)
            {
                ArraySquare(a_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组取平方
        /// output = a ** 2
        /// </summary>
        public static T[] GetArraySquare<T>(T[] a)
        {
            T[] output = new T[a.Length];
            ArraySquare(a, output);
            return output;
        }

        #endregion

        #region ---- Power Square Templete ----

        /// <summary>
        /// 数组取平方
        /// inout = inout ** 2
        /// </summary>
        public static void ArrayPowerSquare<T>(T[] inout)
        {
            if (inout is float[] inout_f32)
            {
                ArraySquare(inout_f32);
            }
            else if (inout is double[] inout_f64)
            {
                ArraySquare(inout_f64);
            }
            else if (inout is Complex32[] inout_fc32)
            {
                RealImageToComplex(GetComplexPower(inout_fc32), ZeroInit<float>(inout_fc32.Length), inout_fc32);
            }
            else if (inout is Complex[] inout_fc64)
            {
                RealImageToComplex(GetComplexPower(inout_fc64), ZeroInit<double>(inout_fc64.Length), inout_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组功率
        /// output = a x a*
        /// </summary>
        public static void ArrayPowerSquare<T>(T[] a, T[] output)
        {
            if (a is float[] a_f32 && output is float[] output_f32)
            {
                ArraySquare(a_f32, output_f32);
            }
            else if (a is double[] a_f64 && output is double[] output_f64)
            {
                ArraySquare(a_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && output is Complex32[] output_fc32)
            {
                RealImageToComplex(GetComplexPower(a_fc32), ZeroInit<float>(a_fc32.Length), output_fc32);
            }
            else if (a is Complex[] a_fc64 && output is Complex[] output_fc64)
            {
                RealImageToComplex(GetComplexPower(a_fc64), ZeroInit<double>(a_fc64.Length), output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组功率
        /// output = a x a*
        /// </summary>
        public static T[] GetArrayPowerSquare<T>(T[] a)
        {
            T[] output = new T[a.Length];
            ArrayPowerSquare(a, output);
            return output;
        }

        #endregion


        #region ---- Square ----

        #region ---- double ----
        /// <summary>
        /// double 数组取平方
        /// inout = inout ** 2
        /// </summary>
        internal static void ArraySquare(double[] inout)
        {
            ippsSqr_64f_I(inout, inout.Length);
        }

        /// <summary>
        /// double 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static void ArraySquare(double[] a, double[] output)
        {
            ippsSqr_64f(a, output, a.Length);
        }

        /// <summary>
        /// double 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static double[] GetArraySquare(double[] a)
        {
            double[] result = new double[a.Length];
            ArraySquare(a, result);
            return result;
        }

        #endregion

        #region ---- float ----

        /// <summary>
        /// float 数组取平方
        /// inout = inout ** 2
        /// </summary>
        internal static void ArraySquare(float[] inout)
        {
            ippsSqr_32f_I(inout, inout.Length);
        }

        /// <summary>
        /// float 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static void ArraySquare(float[] a, float[] output)
        {
            ippsSqr_32f(a, output, a.Length);
        }

        /// <summary>
        /// float 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static float[] GetArraySquare(float[] a)
        {
            float[] result = new float[a.Length];
            ArraySquare(a, result);
            return result;
        }

        #endregion

        #region ---- Complex ----

        /// <summary>
        /// Complex 数组取平方
        /// inout = inout ** 2
        /// </summary>
        internal static void ArraySquare(Complex[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSqr_64fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static void ArraySquare(Complex[] a, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSqr_64fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static Complex[] GetArraySquare(Complex[] a)
        {
            Complex[] result = new Complex[a.Length];
            ArraySquare(a, result);
            return result;
        }

        #endregion

        #region ---- Complex32 ----

        /// <summary>
        /// Complex32 数组取平方
        /// inout = inout ** 2
        /// </summary>
        internal static void ArraySquare(Complex32[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSqr_32fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static void ArraySquare(Complex32[] a, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSqr_32fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组取平方
        /// output = a ** 2
        /// </summary>
        internal static Complex32[] GetArraySquare(Complex32[] a)
        {
            Complex32[] result = new Complex32[a.Length];
            ArraySquare(a, result);
            return result;
        }
        #endregion

        #endregion


        #region ---- Root Templete ----

        /// <summary>
        /// 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        public static void ArrayRoot<T>(T[] inout)
        {
            if (inout is float[] inout_f32)
            {
                ArrayRoot(inout_f32);
            }
            else if (inout is double[] inout_f64)
            {
                ArrayRoot(inout_f64);
            }
            else if (inout is Complex32[] inout_fc32)
            {
                ArrayRoot(inout_fc32);
            }
            else if (inout is Complex[] inout_fc64)
            {
                ArrayRoot(inout_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static void ArrayRoot<T>(T[] a, T[] output)
        {
            if (a is float[] a_f32 && output is float[] output_f32)
            {
                ArrayRoot(a_f32, output_f32);
            }
            else if (a is double[] a_f64 && output is double[] output_f64)
            {
                ArrayRoot(a_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && output is Complex32[] output_fc32)
            {
                ArrayRoot(a_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && output is Complex[] output_fc64)
            {
                ArrayRoot(a_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static T[] GetArrayRoot<T>(T[] a)
        {
            T[] output = new T[a.Length];
            ArrayRoot(a, output);
            return output;
        }

        #endregion

        #region ---- Root ----

        #region ---- double ----
        /// <summary>
        /// double 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        internal static void ArrayRoot(double[] inout)
        {
            ippsSqrt_64f_I(inout, inout.Length);
        }

        /// <summary>
        /// double 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static void ArrayRoot(double[] a, double[] output)
        {
            ippsSqrt_64f(a, output, a.Length);
        }

        /// <summary>
        /// double 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static double[] GetArrayRoot(double[] a)
        {
            double[] result = new double[a.Length];
            ArrayRoot(a, result);
            return result;
        }
        #endregion

        #region ---- float ----
        /// <summary>
        /// float 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        internal static void ArrayRoot(float[] inout)
        {
            ippsSqrt_32f_I(inout, inout.Length);
        }

        /// <summary>
        /// float 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static void ArrayRoot(float[] a, float[] output)
        {
            ippsSqrt_32f(a, output, a.Length);
        }

        /// <summary>
        /// float 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static float[] GetArrayRoot(float[] a)
        {
            float[] result = new float[a.Length];
            ArrayRoot(a, result);
            return result;
        }
        #endregion

        #region ---- Complex ----
        /// <summary>
        /// Complex 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        internal static void ArrayRoot(Complex[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSqrt_64fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static void ArrayRoot(Complex[] a, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSqrt_64fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static Complex[] GetArrayRoot(Complex[] a)
        {
            Complex[] result = new Complex[a.Length];
            ArrayRoot(a, result);
            return result;
        }
        #endregion

        #region ---- Complex32 ----
        /// <summary>
        /// Complex32 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        internal static void ArrayRoot(Complex32[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsSqrt_32fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static void ArrayRoot(Complex32[] a, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSqrt_32fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组平方根
        /// output = sqrt (a)
        /// </summary>
        internal static Complex32[] GetArrayRoot(Complex32[] a)
        {
            Complex32[] result = new Complex32[a.Length];
            ArrayRoot(a, result);
            return result;
        }
        #endregion

        #endregion


        #region---- Square: pSrcDst = pSrcDst**2 ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_32f_I(float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_64f_I(double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_32fc_I(IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_64fc_I(IntPtr pSrcDst, int len);

        #endregion

        #region---- Square: pDst = pSrc**2 ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_32f(float[] pSrc, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_64f(double[] pSrc, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqr_64fc(IntPtr pSrc, IntPtr pDst, int len);

        #endregion

        #region----  root: pSrcDst = sqrt(pSrcDst) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_32f_I(float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_64f_I(double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_32fc_I(IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_64fc_I(IntPtr pSrcDst, int len);

        #endregion

        #region----  root: pDst = sqrt(pSrc) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_32f(float[] pSrc, float[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_64f(double[] pSrc, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSqrt_64fc(IntPtr pSrc, IntPtr pDst, int len);

        #endregion
    }
}
