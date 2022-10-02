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
            if (!(input.IsValid && input.IsSquare)) { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (knownSolution.Length != input.Row) { throw (new System.Exception("The solution vector size is invalid.")); }
            #endregion

            #region ---- 准备 ----

            GCHandle knownSolution_GC = GCHandle.Alloc(knownSolution, GCHandleType.Pinned);
            IntPtr knownSolution_address = knownSolution_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----

            SolveLinearEquations<T>(input._dataAddress, knownSolution_address, knownSolution_address,
                                    input.Row, 1, input.IsSymmetric);

            #endregion

            #region ---- 收尾 ----

            knownSolution_GC.Free();

            #endregion
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, T[] known, T[] solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare)) { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (known.Length != input.Colum) { throw (new System.Exception("The known vector size is invalid.")); }
            if (solution.Length != input.Row) { throw (new System.Exception("The solution vector size is invalid.")); }
            #endregion

            #region ---- 准备 ----

            GCHandle known_GC = GCHandle.Alloc(known, GCHandleType.Pinned);
            IntPtr known_address = known_GC.AddrOfPinnedObject();

            GCHandle solution_GC = GCHandle.Alloc(solution, GCHandleType.Pinned);
            IntPtr solution_address = solution_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----

            SolveLinearEquations<T>(input._dataAddress, known_address, solution_address,
                                    input.Row, 1, input.IsSymmetric);

            #endregion

            #region ---- 收尾 ----

            known_GC.Free();
            solution_GC.Free();

            #endregion
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, Matrix<T> knownSolution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare))
            { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (!(knownSolution.IsValid && knownSolution.Row == input.Row))
            { throw (new System.Exception("The solution Matrix is invalid.")); }
            #endregion

            SolveLinearEquations<T>(input._dataAddress, knownSolution._dataAddress, knownSolution._dataAddress,
                                    input.Row, knownSolution.Colum, input.IsSymmetric);
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquations(Matrix<T> input, Matrix<T> known, Matrix<T> solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare))
            { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (!(known.IsValid && known.Row == input.Row))
            { throw (new System.Exception("The known Matrix is invalid.")); }
            if (!(solution.IsValid && solution.Row == input.Colum && solution.Colum == known.Colum))
            { throw (new System.Exception("The solution Matrix is invalid.")); }
            #endregion

            SolveLinearEquations<T>(input._dataAddress, known._dataAddress, solution._dataAddress,
                                    input.Row, solution.Colum, input.IsSymmetric);
        }

        /// <summary>
        /// Solve Equations: matrixInput  * dataSolution = dataKnown
        /// </summary>
        /// <param name="matrixInput">Row = matrixOrder, Colum = matrixOrder</param>
        /// <param name="dataKnown">Row = matrixOrder, Colum = dataSize</param>
        /// <param name="dataSolution">Row = matrixOrder, Colum = dataSize</param>
        private static void SolveLinearEquations<T>(IntPtr matrixInput, IntPtr dataKnown, IntPtr dataSolution,
                                                    int matrixOrder, int dataSize, bool isSymmetric)
        {
            #region ---- 判断 ----
            #endregion

            #region ---- 准备 ----

            bool inplace = (dataKnown == dataSolution);
            if (!inplace)
            {
                Vector.ArrayCopy<T>(dataKnown, dataSolution, matrixOrder * dataSize);
            }

            // 复制数组, 保留matrixInput
            T[] matrixA = new T[matrixOrder * matrixOrder];
            Vector.ArrayCopy<T>(matrixInput, matrixA);

            GCHandle matrixA_GC = GCHandle.Alloc(matrixA, GCHandleType.Pinned);
            IntPtr matrixA_address = matrixA_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----

            if (isSymmetric)
            {
                SolveLinearEquationsSymmetric<T>(matrixA_address, dataSolution, matrixOrder, dataSize);
            }
            else
            {
                SolveLinearEquationsGeneral<T>(matrixA_address, dataSolution, matrixOrder, dataSize);
            }

            #endregion

            #region ---- 收尾 ----

            matrixA_GC.Free();

            #endregion

        }

        /// <summary>
        /// 求解线性方程, matrixA为一般方阵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为m </param>
        /// <param name="matrixB"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="m"> matrixA行数 / matrixA列数 / matrixB行数 </param>
        /// <param name="n"> matrixB列数 </param>
        private static void SolveLinearEquationsGeneral<T>(IntPtr matrixA, IntPtr matrixB, int m, int n)
        {
            #region ---- 准备 ----
            int error = 0;
            T[] dataType = new T[1];

            int rowA = m;
            int columA = rowA;
            int rowB = m;
            int columB = n;
            int order = m;

            #endregion

            #region ---- 调用API ----
            int[] exchange = new int[order];

            if (dataType is float[])
            {
                error = MatrixCaller.LAPACKE_sgesv(MatrixLayoutEnum.RowMajor,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);

            }
            else if (dataType is double[])
            {
                error = MatrixCaller.LAPACKE_dgesv(MatrixLayoutEnum.RowMajor,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);
            }
            else if (dataType is Complex32[])
            {

                error = MatrixCaller.LAPACKE_cgesv(MatrixLayoutEnum.RowMajor,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);
            }
            else if (dataType is Complex[])
            {

                error = MatrixCaller.LAPACKE_zgesv(MatrixLayoutEnum.RowMajor,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);

            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            if (error != 0) { throw (new System.Exception(String.Format("Compute error.Error code = { 0 }", error))); }
        }

        /// <summary>
        /// 求解线性方程, matrixA为对称矩阵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为m </param>
        /// <param name="matrixB"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="m"> matrixA行数 / matrixA列数 / matrixB行数 </param>
        /// <param name="n"> matrixB列数 </param>
        private static void SolveLinearEquationsSymmetric<T>(IntPtr matrixA, IntPtr matrixB, int m, int n)
        {
            #region ---- 准备 ----
            int error = 0;
            T[] dataType = new T[1];

            int rowA = m;
            int columA = rowA;
            int rowB = m;
            int columB = n;
            int order = m;

            #endregion

            #region ---- 调用API ----
            int[] exchange = new int[order];

            if (dataType is float[])
            {
                error = MatrixCaller.LAPACKE_ssysv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);
            }
            else if (dataType is double[])
            {
                error = MatrixCaller.LAPACKE_dsysv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);

            }
            else if (dataType is Complex32[])
            {

                error = MatrixCaller.LAPACKE_chesv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);

            }
            else if (dataType is Complex[])
            {

                error = MatrixCaller.LAPACKE_zhesv(MatrixLayoutEnum.RowMajor, MatrixTriangularChar.UpTriangular,
                                                   order, columB,
                                                   matrixA, columA, exchange,
                                                   matrixB, columB);

            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            if (error != 0) { throw (new System.Exception(String.Format("Compute error.Error code = { 0 }", error))); }
        }
    }

    internal partial class MatrixCaller
    {
        #region ---- 一般矩阵 (LAPACK Linear Equation -> Drive -> LAPACKE_?gesv) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 float[,] a, int lda, int[] ipiv,
                                                 float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 double[,] a, int lda, int[] ipiv,
                                                 double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgesv(MatrixLayoutEnum matrix_layout,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);
        #endregion

        #region ---- 对称矩阵 (LAPACK Linear Equation -> Drive -> LAPACKE_?sysv/?hesv) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 float[,] a, int lda, int[] ipiv,
                                                 float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 double[,] a, int lda, int[] ipiv,
                                                 double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_csysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zsysv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_chesv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zhesv(MatrixLayoutEnum matrix_layout, MatrixTriangularChar uplo,
                                                 int n, int nrhs,
                                                 IntPtr a, int lda, int[] ipiv,
                                                 IntPtr b, int ldb);

        #endregion
    }
}
