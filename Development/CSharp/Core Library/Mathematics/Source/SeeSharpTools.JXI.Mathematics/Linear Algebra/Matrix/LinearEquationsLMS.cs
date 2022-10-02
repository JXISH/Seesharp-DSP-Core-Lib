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
            if (!(input.IsValid)) { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (known.Length != input.Row) { throw (new System.Exception("The known vector size is invalid.")); }
            if (solution.Length != input.Colum) { throw (new System.Exception("The solution vector size is invalid.")); }
            #endregion

            #region ---- 准备 ----

            GCHandle known_GC = GCHandle.Alloc(known, GCHandleType.Pinned);
            IntPtr known_address = known_GC.AddrOfPinnedObject();

            GCHandle solution_GC = GCHandle.Alloc(solution, GCHandleType.Pinned);
            IntPtr solution_address = solution_GC.AddrOfPinnedObject();

            #endregion

            SolveLinearEquationsLMS<T>(input._dataAddress, known_address, solution_address,
                                       input.Row, input.Colum, 1);

            #region ---- 收尾 ----
            known_GC.Free();
            solution_GC.Free();
            #endregion
        }

        /// <summary>
        /// 求解线性方程
        /// </summary>
        public static void SolveLinearEquationsLMS(Matrix<T> input, Matrix<T> known, Matrix<T> solution)
        {
            #region ---- 判断 ----
            if (!(input.IsValid)) { throw (new System.Exception("The Input Matrix is invalid.")); }
            if (!(known.IsValid && known.Row == input.Row))
            { throw (new System.Exception("The known Matrix is invalid.")); }
            if (!(solution.IsValid && solution.Row == input.Colum && solution.Colum == known.Colum))
            { throw (new System.Exception("The solution Matrix is invalid.")); }
            #endregion

            SolveLinearEquationsLMS<T>(input._dataAddress, known._dataAddress, solution._dataAddress,
                                       input.Row, input.Colum, solution.Colum);
        }

        /// <summary>
        /// matrixInput * dataSolution = dataKnown
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixInput">Row = M, Colum = N </param>
        /// <param name="dataKnown">Row = M, Colum = L</param>
        /// <param name="dataSolution">Row = N, Colum = L</param>
        private static void SolveLinearEquationsLMS<T>(IntPtr matrixInput, IntPtr dataKnown, IntPtr dataSolution,
                                                       int M, int N, int L)
        {
            #region ---- 判断 ----
            #endregion

            #region ---- 准备 ----

            bool inplace = (dataKnown == dataSolution);
            bool overdetermined = (M > N);
            IntPtr knownSolution_address;
            GCHandle knownSolution_GC = new GCHandle();
            if (!inplace)
            {
                if (overdetermined)
                {
                    // dataSolution 的空间比 dataKnown 小, 申请新的内存
                    T[] dataKnownSolution = new T[M * L];
                    knownSolution_GC = GCHandle.Alloc(dataKnownSolution, GCHandleType.Pinned);
                    knownSolution_address = knownSolution_GC.AddrOfPinnedObject();
                    Vector.ArrayCopy<T>(dataKnown, dataKnownSolution);
                }
                else
                {
                    Vector.ArrayCopy<T>(dataKnown, dataSolution, M * L);
                    knownSolution_address = dataSolution;
                }
            }
            else
            {
                /* dataSolution和dataKnow 的空间需要调用者自己掌握 */
                knownSolution_address = dataSolution;
            }

            // 复制数组, 保留matrixInput
            T[] matrixA = new T[M * N];
            Vector.ArrayCopy<T>(matrixInput, matrixA);

            GCHandle matrixA_GC = GCHandle.Alloc(matrixA, GCHandleType.Pinned);
            IntPtr matrixA_address = matrixA_GC.AddrOfPinnedObject();

            #endregion

            #region ---- 计算 ----

            if (overdetermined)
            {
                SolveLinearEquationsQR<T>(matrixA_address, knownSolution_address, M, N, L);
                //SolveLinearEquationsSVD<T>(matrixA_address, knownSolution_address, M, N, L);
            }
            else
            {
                SolveLinearEquationsSVD<T>(matrixA_address, knownSolution_address, M, N, L);
            }

            #endregion

            #region ---- 收尾 ----

            matrixA_GC.Free();

            if (!inplace && overdetermined)
            {
                // 将计算结果赋值输入空间
                Vector.ArrayCopy<T>(knownSolution_address, dataSolution, N * L);
                knownSolution_GC.Free();
            }

            #endregion

        }

        /// <summary>
        /// 求解线性方程, matrixA为一般方阵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="matrixKnownSolution"> 行排列, 行首元素存储间隔为k, 行数为 MAX(m,n) </param>
        /// <param name="m"> matrixA行数 / matrixKnow行数 </param>
        /// <param name="n"> matrixA列数 / matrixSolution行数 </param>
        /// <param name="k"> matrixB列数 </param>
        private static void SolveLinearEquationsQR<T>(IntPtr matrixA, IntPtr matrixKnownSolution,
                                                       int m, int n, int k)
        {
            #region ---- 准备 ----
            int error = 0;
            T[] dataType = new T[1];

            int rowA = m;
            int columA = n;
            int rowKnownSolution = Math.Max(m, n);
            int columKnownSolution = k;

            bool overdetermined = (m > n);

            #endregion

            #region ---- 调用API ----

            int rank;
            if (dataType is float[])
            {
                error = MatrixCaller.LAPACKE_sgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   rowA, columA, columKnownSolution,
                                                   matrixA, columA,
                                                   matrixKnownSolution, columKnownSolution);
            }
            else if (dataType is double[])
            {
                error = MatrixCaller.LAPACKE_dgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   rowA, columA, columKnownSolution,
                                                   matrixA, columA,
                                                   matrixKnownSolution, columKnownSolution);
            }
            else if (dataType is Complex32[])
            {
                error = MatrixCaller.LAPACKE_cgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   rowA, columA, columKnownSolution,
                                                   matrixA, columA,
                                                   matrixKnownSolution, columKnownSolution);
            }
            else if (dataType is Complex[])
            {
                error = MatrixCaller.LAPACKE_zgels(MatrixLayoutEnum.RowMajor, MatrixTransposeChar.NoTranspose,
                                                   rowA, columA, columKnownSolution,
                                                   matrixA, columA,
                                                   matrixKnownSolution, columKnownSolution);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----

            if (overdetermined)
            {
                T[] residualSum = ComputeResidualSum<T>(matrixKnownSolution, m, n, k);
            }

            #endregion

            if (error != 0) { throw (new System.Exception(String.Format("Compute error.Error code = { 0 }", error))); }
        }


        /// <summary>
        /// 求解线性方程, matrixA为一般方阵
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="matrixKnownSolution"> 行排列, 行首元素存储间隔为k, 行数为 MAX(m,n) </param>
        /// <param name="m"> matrixA行数 / matrixKnow行数 </param>
        /// <param name="n"> matrixA列数 / matrixSolution行数 </param>
        /// <param name="k"> matrixB列数 </param>
        private static void SolveLinearEquationsSVD<T>(IntPtr matrixA, IntPtr matrixKnownSolution,
                                                   int m, int n, int k)
        {

            #region ---- 判断 ----
            // if (m > n) { throw (new System.Exception("The Input Matrix is invalid.")); }
            #endregion

            #region ---- 准备 ----
            int error = 0;
            T[] dataType = new T[1];

            int rowA = m;
            int columA = n;
            int rowKnownSolution = Math.Max(m, n); // rowKnowSolution = n;
            int columKnownSolution = k;
            int singularSize = Math.Min(m, n); // singularSize = m;

            bool overdetermined = (m > n);

            #endregion

            #region ---- 调用API ----

            int rank;
            if (dataType is float[])
            {

                float[] singular = new float[singularSize];
                error = MatrixCaller.LAPACKE_sgelsd(MatrixLayoutEnum.RowMajor,
                                                    rowA, columA, columKnownSolution,
                                                    matrixA, columA,
                                                    matrixKnownSolution, columKnownSolution,
                                                    singular, -1, out rank);

            }
            else if (dataType is double[])
            {
                double[] singular = new double[singularSize];
                error = MatrixCaller.LAPACKE_dgelsd(MatrixLayoutEnum.RowMajor,
                                                    rowA, columA, columKnownSolution,
                                                    matrixA, columA,
                                                    matrixKnownSolution, columKnownSolution,
                                                    singular, -1, out rank);
            }
            else if (dataType is Complex32[])
            {

                float[] singular = new float[singularSize];
                error = MatrixCaller.LAPACKE_cgelsd(MatrixLayoutEnum.RowMajor,
                                                    rowA, columA, columKnownSolution,
                                                    matrixA, columA,
                                                    matrixKnownSolution, columKnownSolution,
                                                    singular, -1, out rank);
            }
            else if (dataType is Complex[])
            {
                double[] singular = new double[singularSize];
                error = MatrixCaller.LAPACKE_zgelsd(MatrixLayoutEnum.RowMajor,
                                                    rowA, columA, columKnownSolution,
                                                    matrixA, columA,
                                                    matrixKnownSolution, columKnownSolution,
                                                    singular, -1, out rank);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----

            if (overdetermined)
            {
                T[] residualSum = ComputeResidualSum<T>(matrixKnownSolution, m, n, k);
            }

            #endregion

            if (error != 0) { throw (new System.Exception(String.Format("Compute error.Error code = { 0 }", error))); }
        }

        private static T[] ComputeResidualSum<T>(IntPtr matrixKnownSolution, int m, int n, int k)
        {
            IntPtr residualAdress = matrixKnownSolution + n * k * ElementSize<T>();

            T[] residualSum = new T[k];
            T[][] residual = new T[k][];
            for (int i = 0; i < k; i++)
            {
                residual[i] = new T[m - n];
                MatrixCaller.ArrayCopy<T>(residualAdress, residual[i], k);
                residualAdress += ElementSize<T>();
                Vector.ArrayPowerSquare(residual[i]);
                residualSum[i] = Vector.ArraySum(residual[i]);
            }

            return residualSum;
        }
    }

    internal partial class MatrixCaller
    {
        #region ---- QR分解 (Linear Least Squares (LLS) Problems -> Drive -> LAPACKE_?gels) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 float[,] a, int lda,
                                                 float[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 IntPtr a, int lda,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 double[,] a, int lda,
                                                 double[,] b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 IntPtr a, int lda,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 IntPtr a, int lda,
                                                 IntPtr b, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgels(MatrixLayoutEnum matrix_layout, MatrixTransposeChar trans,
                                                 int m, int n, int nrhs,
                                                 IntPtr a, int lda,
                                                 IntPtr b, int ldb);

        #endregion

        #region ---- SVD分解 (Linear Least Squares (LLS) Problems -> Drive -> LAPACKE_?gelsd) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs,
                                                  float[,] a, int lda,
                                                  float[,] b, int ldb,
                                                  float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgelsd(MatrixLayoutEnum matrix_layout,
                                                 int m, int n, int nrhs,
                                                 IntPtr a, int lda,
                                                 IntPtr b, int ldb,
                                                 float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs,
                                                  double[,] a, int lda,
                                                  double[,] b, int ldb,
                                                  double[] s, double rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs,
                                                  IntPtr a, int lda,
                                                  IntPtr b, int ldb,
                                                  double[] s, double rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs,
                                                  IntPtr a, int lda,
                                                  IntPtr b, int ldb,
                                                  float[] s, float rcond, out int rank);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgelsd(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, int nrhs,
                                                  IntPtr a, int lda,
                                                  IntPtr b, int ldb,
                                                  double[] s, double rcond, out int rank);
        #endregion
    }
}
