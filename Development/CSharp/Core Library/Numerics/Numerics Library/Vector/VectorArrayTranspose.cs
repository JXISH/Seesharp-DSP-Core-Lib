using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Transpose ----

        /// <summary>
        /// 二维数组转置
        /// </summary>
        public static void ArrayTranspose<T>(T[][] input, T[][] output)
        {
            int row = input.Length;
            int colum = input[0].Length;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    output[j][i] = input[i][j];
                }
            }
        }

        #endregion

        #region---- Transfer: B = A' ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_somatcopy(byte ordering, byte trans, int rows, int cols, float alpha, IntPtr A, int lda, IntPtr B, int ldb);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_domatcopy(byte ordering, byte trans, int rows, int cols, double alpha, IntPtr A, int lda, IntPtr B, int ldb);

        // Complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_comatcopy(byte ordering, byte trans, int rows, int cols, Complex32 alpha, IntPtr A, int lda, IntPtr B, int ldb);

        // Complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_zomatcopy(byte ordering, byte trans, int rows, int cols, Complex alpha, IntPtr A, int lda, IntPtr B, int ldb);

        #endregion

        #region---- Transfer: AB = AB' ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_simatcopy(byte ordering, byte trans, int rows, int cols, float alpha, IntPtr AB, int lda, int ldb);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_dimatcopy(byte ordering, byte trans, int rows, int cols, double alpha, IntPtr AB, int lda, int ldb);

        // Complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_cimatcopy(byte ordering, byte trans, int rows, int cols, Complex32 alpha, IntPtr AB, int lda, int ldb);

        // Complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void mkl_zimatcopy(byte ordering, byte trans, int rows, int cols, Complex alpha, IntPtr AB, int lda, int ldb);

        #endregion

        #region---- Note ----
        /***********************************************
        ordering:   Ordering of the matrix storage.
                    If ordering = 'R' or 'r', the ordering is row-major.
                    If ordering = 'C' or 'c', the ordering is column-major.
        trans:      Parameter that specifies the operation type.
                    If trans = 'N' or 'n', op(AB)= AB and the matrix AB is assumed unchanged on input.
                    If trans = 'T' or 't', it is assumed that AB should be transposed.
                    If trans = 'C' or 'c', it is assumed that AB should be conjugate transposed.
                    If trans = 'R' or 'r', it is assumed that AB should be only conjugated.
                    If the data is real, then trans = 'R' is the same as trans = 'N', and trans = 'C' is the same as trans = 'T'.
          *********************************************************/
        #endregion
    }
}
