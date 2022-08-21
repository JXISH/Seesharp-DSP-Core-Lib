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
        public static void SolveLinearEquations(Matrix<T> input, T[] knownSolution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare)) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (knownSolution.Length != input.Row) { throw (new ArgumentException("The solution vector size is invalid.")); }
            #endregion

            SolveLinearEquations(input._dataRef, input.Row, input.IsSymmetric,knownSolution, 1);
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, T[] known, T[] solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare)) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (known.Length != input.Colum) { throw (new ArgumentException("The known vector size is invalid.")); }
            if (solution.Length != input.Row) { throw (new ArgumentException("The solution vector size is invalid.")); }
            #endregion

            Vector.ArrayCopy(known, solution);

            SolveLinearEquations(input._dataRef, input.Row, input.IsSymmetric, solution, 1);
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, Matrix<T> knownSolution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare)) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (!(knownSolution.IsValid))
            { throw (new ArgumentException("The solution Matrix is invalid.")); }
            #endregion

            SolveLinearEquations(input._dataRef, input.Row, input.IsSymmetric, knownSolution._dataRef, knownSolution.Colum);
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, Matrix<T> known, Matrix<T> solution)
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

            SolveLinearEquations(input._dataRef, input.Row, input.IsSymmetric, solution._dataRef, solution.Colum);
        }

        private static void SolveLinearEquations(T[] matrixInput, int N, bool isSymmetric, T[] dataKnownSolution, int M)
        {
            #region ---- 判断 ----
            if (matrixInput.Length != N * N) { throw (new ArgumentException("The Input Matrix is invalid.")); }
            if (dataKnownSolution.Length != N * M) { throw (new ArgumentException("The solution vector size is invalid.")); }
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
            int exchange = 0;

            if (matrixA is float[])
            {
                if (isSymmetric)
                {
                    error = MatrixCaller.LAPACKE_ssysv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                       N, M, matrixA_address, N, out exchange, matrixB_address, M);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_sgesv(MatrixLayoutEnum.RowMajor, N, M,
                                                       matrixA_address, N, out exchange, matrixB_address, M);
                }
            }
            else if (matrixA is double[])
            {
                if (isSymmetric)
                {
                    error = MatrixCaller.LAPACKE_dsysv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                       N, M, matrixA_address, N, out exchange, matrixB_address, M);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_dgesv(MatrixLayoutEnum.RowMajor, N, M,
                                                       matrixA_address, N, out exchange, matrixB_address, M);
                }
            }
            else if (matrixA is Complex32[])
            {
                if (isSymmetric)
                {
                    error = MatrixCaller.LAPACKE_chesv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                       N, M, matrixA_address, N, out exchange, matrixB_address, M);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_cgesv(MatrixLayoutEnum.RowMajor, N, M,
                                                       matrixA_address, N, out exchange, matrixB_address, M);
                }
            }
            else if (matrixA is Complex[])
            {
                if (isSymmetric)
                {
                    error = MatrixCaller.LAPACKE_zhesv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                       N, M, matrixA_address, N, out exchange, matrixB_address, M);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_zgesv(MatrixLayoutEnum.RowMajor, N, M,
                                                       matrixA_address, N, out exchange, matrixB_address, M);
                }
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            matrixA_GC.Free();
            matrixB_GC.Free();
            #endregion

            if (error != 0 || exchange < 0) { throw (new InvalidOperationException(String.Format("Compute error.Error code = { 0 }", error))); }
        }
    }

    internal partial class MatrixCaller
    {
        #region ---- 一般矩阵 (LAPACK Linear Equation -> Drive -> LAPACKE_?gesv) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 float[,] a, int lda, out int ipiv, float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 double[,] a, int lda, out int ipiv, double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgesv(MatrixLayoutEnum matrix_layout, int n, int nrhs,
                                                 IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);
        #endregion

        #region ---- 对称矩阵 (LAPACK Linear Equation -> Drive -> LAPACKE_?sysv/?hesv) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, float[,] a, int lda, out int ipiv, float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, double[,] a, int lda, out int ipiv, double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_csysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_chesv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zhesv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs, IntPtr a, int lda, out int ipiv, IntPtr b, int ldb);

        #endregion
    }
}
