using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Square ----

        #region ---- double ----
        /// <summary>
        /// double 数组取平方
        /// inout = inout ** 2
        /// </summary>
        public static void ArraySquare(double[] inout)
        {
            ippsSqr_64f_I(inout, inout.Length);
        }
        
        /// <summary>
        /// double 数组取平方
        /// output = a ** 2
        /// </summary>
        public static void ArraySquare(double[] a, double[] output)
        {
            ippsSqr_64f(a, output, a.Length);
        }

        /// <summary>
        /// double 数组取平方
        /// output = a ** 2
        /// </summary>
        public static double[] GetArraySquare(double[] a)
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
        public static void ArraySquare(float[] inout)
        {
            ippsSqr_32f_I(inout, inout.Length);
        }

        /// <summary>
        /// float 数组取平方
        /// output = a ** 2
        /// </summary>
        public static void ArraySquare(float[] a, float[] output)
        {
            ippsSqr_32f(a, output, a.Length);
        }

        /// <summary>
        /// float 数组取平方
        /// output = a ** 2
        /// </summary>
        public static float[] GetArraySquare(float[] a)
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
        public static void ArraySquare(Complex[] inout)
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
        public static void ArraySquare(Complex[] a, Complex[] output)
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
        public static Complex[] GetArraySquare(Complex[] a)
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
        public static void ArraySquare(Complex32[] inout)
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
        public static void ArraySquare(Complex32[] a, Complex32[] output)
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
        public static Complex32[] GetArraySquare(Complex32[] a)
        {
            Complex32[] result = new Complex32[a.Length];
            ArraySquare(a, result);
            return result;
        }
        #endregion

        #endregion

        #region ---- Root ----

        #region ---- double ----
        /// <summary>
        /// double 数组平方根
        /// inout = sqrt (inout)
        /// </summary>
        public static void ArrayRoot(double[] inout)
        {
            ippsSqrt_64f_I(inout, inout.Length);
        }

        /// <summary>
        /// double 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static void ArrayRoot(double[] a, double[] output)
        {
            ippsSqrt_64f(a, output, a.Length);
        }

        /// <summary>
        /// double 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static double[] GetArrayRoot(double[] a)
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
        public static void ArrayRoot(float[] inout)
        {
            ippsSqrt_32f_I(inout, inout.Length);
        }

        /// <summary>
        /// float 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static void ArrayRoot(float[] a, float[] output)
        {
            ippsSqrt_32f(a, output, a.Length);
        }

        /// <summary>
        /// float 数组平方根
        /// output = sqrt (a)
        /// </summary>
        public static float[] GetArrayRoot(float[] a)
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
        public static void ArrayRoot(Complex[] inout)
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
        public static void ArrayRoot(Complex[] a, Complex[] output)
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
        public static Complex[] GetArrayRoot(Complex[] a)
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
        public static void ArrayRoot(Complex32[] inout)
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
        public static void ArrayRoot(Complex32[] a, Complex32[] output)
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
        public static Complex32[] GetArrayRoot(Complex32[] a)
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
