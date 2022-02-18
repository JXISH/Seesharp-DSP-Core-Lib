using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace SeeSharpTools.JXI.MKL
{
    [SuppressUnmanagedCodeSecurity]
    internal static class VMLNative
    {
        #region "Double" number operation

        #region Arithmetic Functions
        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAtan2(int n, double[] a, double[] b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAdd(int n, double[] a, double[] b, double[] y);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAdd(int n, IntPtr a, IntPtr b, IntPtr y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSub(int n, double[] a, double[] b, double[] y);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSub(int n, IntPtr a, IntPtr b, IntPtr y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSqr(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdMul(int n, double[] a, double[] b, double[] y);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdMul(int n, double[] a, IntPtr b, IntPtr y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdMul(int n, double[] a, IntPtr b, double[] y);
        //vdMulByConj

        //vdConj

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAbs(int n, double[] a, double[] r);

        //vdArg

        //vdLinearFrac

        #endregion

        #region Power and Root Functions
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdInv(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdDiv(int n, double[] a, double[] b, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSqrt(int n, double[] a, double[] y);

        //vdSqrt
        //vdInvSqrt
        //vdCbrt
        //vdInvCbrt
        //vdPow2o3
        //vdPow3o2
        //vdDiv
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdPowx(int n, double[] a, double b, double[] y);

        //vdHypot

        #endregion

        #region Exponential and Logarithmic Functions
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdExp(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdExpm1(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdLn(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdLog10(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdLog1p(int n, double[] a, double[] y);

        #endregion

        #region Trigonometric Functions

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdCos(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSin(int n, double[] a, double[] y);

        //vdSinCos
        //vdCIS

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdTan(int n, double[] a, double[] y);

        //vdACos
        //vdASin
        //vdAtan
        //vdAtan2
        #endregion

        #region Hyperbolic Functions
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdCosh(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdSinh(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdTanh(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAcosh(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAsinh(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdAtanh(int n, double[] a, double[] y);


        #endregion

        #region Special Functions

        //vdErf
        //vdErfc
        //vdCdfNorm
        //vdErfInv
        //vdErfcInv
        //vdCdfNormInv
        //vdLGamma
        //vdTgamma
        //vdExpIntl
        #endregion

        #region Rounding Functions
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdFloor(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdCeil(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdTrunc(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdRound(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdNearbyInt(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int Rint(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdModf(int n, double[] a, double[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vdFrac(int n, double[] a, double[] y);


        #endregion

        #endregion

        #region Other data types
        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAbs(int n, float[] a, float[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzAdd(int n, Complex[] a, Complex[] b, Complex[] y);


        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcAdd(int n, Complex32[] a, Complex32[] b, Complex32[] r);


        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcSqr(int n, Complex32[] a, Complex32[] y);


        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsLog10(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzLog10(int n, Complex[] a, Complex[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcLog10(int n, Complex32[] a, Complex32[] y);



        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsAdd(int n, float[] a, float[] b, float[] y);



        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsMul(int n, float[] a, float[] b, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzMul(int n, Complex[] a, Complex[] b, Complex[] y);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcMul(int n, Complex32[] a, Complex32[] b, Complex32[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzAbs(int n, Complex[] a, double[] r);

        //[DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        //public static extern int vcAbs(int n, Complex32[] a, float[] r);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vsSqr(int n, float[] a, float[] y);

        [DllImport(@"mkl_rt.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int vzSqr(int n, Complex[] a, Complex[] y);



        #endregion
    }
}
