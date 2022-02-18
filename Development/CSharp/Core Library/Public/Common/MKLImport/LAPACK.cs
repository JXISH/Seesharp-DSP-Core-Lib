using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace SeeSharpTools.JXI.MKL
{
    internal static class LAPACK
    {
        public static int LAPACK_ROW_MAJOR = 101;
        public static int LAPACK_COL_MAJOR = 102;

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int LAPACKE_dgelsd(int matrix_layout, int m, int n, int nrhs, double [] a, int lda, double [] b, int ldb,
                                                double [] s, double rcond, out int rank);

        [DllImport(Constants.MKLCoreFilePath, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int LAPACKE_dgels(int matrix_layout, char trans, int m, int n,
                                               int nrhs, double[] a, int lda, double [] b, int ldb);

    }
}
