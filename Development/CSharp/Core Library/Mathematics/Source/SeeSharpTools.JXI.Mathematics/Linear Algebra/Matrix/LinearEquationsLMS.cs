//#define SVD_Solution

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;

using SeeSharpTools.JXI.Numerics;

namespace SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix
{
    public partial class Matrix<T>
    {
        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquationsLMS(Matrix<T> input, T[] known, T[] solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid)) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (known.Length != input.Colum) { throw (new ArgumentException("The known vector size is invalid.")); }
            if (solution.Length != input.Row) { throw (new ArgumentException("The solution vector size is invalid.")); }
            #endregion

            Vector.ArrayCopy(known, solution);

            SolveLinearEquationsLMS(input._dataRef, input.Row, input.Colum, solution, 1);
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquationsLMS(Matrix<T> input, Matrix<T> known, Matrix<T> solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid)) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (!(known.IsValid && known.Row == input.Row))
            { throw (new ArgumentException("The known Matrix is invalid.")); }
            if (!(solution.IsValid && solution.Row == input.Colum && solution.Colum == known.Colum))
            { throw (new ArgumentException("The solution Matrix is invalid.")); }
            #endregion

            #region Outplace
            Vector.ArrayCopy(known._dataRef, solution._dataRef);
            #endregion

            SolveLinearEquationsLMS(input._dataRef, input.Row, input.Colum, solution._dataRef, solution.Colum);
        }

        private static void SolveLinearEquationsLMS(T[] matrixInput, int M, int N, T[] dataKnownSolution, int L)
        {
            #region ---- 判断 ----
            if (matrixInput.Length != M * N) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (dataKnownSolution.Length != M * L) { throw (new ArgumentException("The solution vector size is invalid.")); }
            #endregion

            #region ---- 准备 ----
            int error = 0;

            T[] matrixA = new T[matrixInput.Length];
            GCHandle matrixA_GC = GCHandle.Alloc(matrixA, GCHandleType.Pinned);
            IntPtr matrixA_address = matrixA_GC.AddrOfPinnedObject();
            Vector.ArrayCopy(matrixInput, matrixA_address);

            GCHandle matrixB_GC = GCHandle.Alloc(dataKnownSolution, GCHandleType.Pinned);
            IntPtr matrixB_address = matrixB_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 调用API ----

            int rank;
            if (matrixA is float[])
            {
#if SVD_Solution
                float[] singular = new float[Math.Min(M, N)];
                error = MatrixCaller.LAPACKE_sgelsd(MatrixLayoutEnum.RowMajor,
                                                    M, N, L, matrixA_address, N, matrixB_address, L,
                                                    singular, -1, out rank);
#else
                error = MatrixCaller.LAPACKE_sgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   M, N, L, matrixA_address, N, matrixB_address, L);
#endif

            }
            else if (matrixA is double[])
            {
#if SVD_Solution
                double[] singular = new double[Math.Min(M, N)];
                error = MatrixCaller.LAPACKE_dgelsd(MatrixLayoutEnum.RowMajor,
                                                    M, N, L, matrixA_address, N, matrixB_address, L,
                                                    singular, -1, out rank);
#else
                error = MatrixCaller.LAPACKE_dgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   M, N, L, matrixA_address, N, matrixB_address, L);
#endif
            }
            else if (matrixA is Complex32[])
            {
#if SVD_Solution
                float[] singular = new float[Math.Min(M, N)];
                error = MatrixCaller.LAPACKE_cgelsd(MatrixLayoutEnum.RowMajor,
                                                    M, N, L, matrixA_address, N, matrixB_address, L,
                                                    singular, -1, out rank);
#else
                error = MatrixCaller.LAPACKE_cgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   M, N, L, matrixA_address, N, matrixB_address, L);
#endif
            }
            else if (matrixA is Complex[])
            {
#if SVD_Solution
                double[] singular = new double[Math.Min(M, N)];
                error = MatrixCaller.LAPACKE_zgelsd(MatrixLayoutEnum.RowMajor,
                                                    M, N, L, matrixA_address, N, matrixB_address, L,
                                                    singular, -1, out rank);
#else
                error = MatrixCaller.LAPACKE_zgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   M, N, L, matrixA_address, N, matrixB_address, L);
#endif
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            matrixA_GC.Free();
            matrixB_GC.Free();
            #endregion

            if (error != 0) { throw (new InvalidOperationException(String.Format("Compute error.Error code = { 0 }", error))); }
        }

    }

    internal partial class MatrixCaller
    {
        #region ---- QR分解 (Linear Least Squares (LLS) Problems -> Drive -> LAPACKE_?gels) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, float[,] a, int lda, float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, double[,] a, int lda, double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb);

        #endregion

        #region ---- SVD分解 (Linear Least Squares (LLS) Problems -> Drive -> LAPACKE_?gelsd) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs, float[,] a, int lda, float[,] b, int ldb,
                                                  float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgelsd(MatrixLayoutEnum matrix_layout,
                                                 int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb,
                                                 float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs, double[,] a, int lda, double[,] b, int ldb,
                                                  double[] s, double rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb,
                                                  double[] s, double rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb,
                                                  float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs, IntPtr a, int lda, IntPtr b, int ldb,
                                                  double[] s, double rcond, out int rank);
        #endregion
    }
}
