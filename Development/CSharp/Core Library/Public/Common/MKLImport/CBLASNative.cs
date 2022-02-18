using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SeeSharpTools.JXI.MKL
{
    /** CBLAS native declarations */

    [SuppressUnmanagedCodeSecurity]
    internal static class CBLASNative
    {

        /** CBLAS native LAPACKE_zgesv declaration */

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl,ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, double[] X, int incX, double[] Y, int incY);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, IntPtr X, int incX, double[] Y, int incY);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, double[] X, int incX, IntPtr Y, int incY);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dcopy(int n, IntPtr X, int incX, IntPtr Y, int incY);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_zcopy(int n, Complex[] X, int incX, Complex[] Y, int incY);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dscal(int n, double alpha, double[] X, int incX);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int cblas_dscal(int n, double alpha, IntPtr X, int incX);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern double cblas_dasum(int n,  double[] x, int incx);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern double cblas_idamin(int n, double[] x, int incx);
    }
}
