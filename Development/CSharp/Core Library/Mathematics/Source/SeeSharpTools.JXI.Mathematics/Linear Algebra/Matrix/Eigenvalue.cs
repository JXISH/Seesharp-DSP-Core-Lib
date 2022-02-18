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
        /// 计算特征值
        /// </summary>
        public T[] Eigenvalues()
        {
            T[] eigenvalues = new T[_rowSize];  
            Eigenvalues(eigenvalues, null, false);

            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public T[] Eigenvalues(out T[][] eigenvectors)
        {
            T[] eigenvalues = new T[_rowSize];
            eigenvectors = new T[_rowSize][];
            for (int i = 0; i < _rowSize; i++)
            {
                eigenvectors[i] = new T[_columSize];
            }
            Eigenvalues( eigenvalues, eigenvectors, true);
            return eigenvalues;
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public void Eigenvalues(T[] eigenvalues, T[][] eigenvectors)
        {
            Eigenvalues(eigenvalues, eigenvectors, true);
        }

        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        private void Eigenvalues(T[] eigenvalues, T[][] eigenVectors, bool eigenvectorsOut)
        {
            int error = 0;
            int N = _rowSize;

            #region ---- 判断是否为方阵 ----

            if (!this.IsValid) { throw new ArgumentException("Matrix is not valid."); }

            if (!this.IsSquare) { throw new ArgumentException("Data size is not valid."); }

            if (eigenvalues.Length != N) { throw new ArgumentException("Data size is not valid."); }

            #endregion

            #region ---- 初始化特征值数组 ----
            GCHandle eigenvalues_GC = GCHandle.Alloc(eigenvalues, GCHandleType.Pinned);
            IntPtr eigenvalues_address = eigenvalues_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 初始化特征向量数组 ----
            // 注： 特征向量是按列排列，对应特征值
            JobCompute jobz = eigenvectorsOut ? JobCompute.Compute : JobCompute.NotCompute;
            T[] eigenvectors = eigenvectorsOut ?( new T[N * N]) : null;
            GCHandle eigenvectors_GC = GCHandle.Alloc(eigenvectors, GCHandleType.Pinned);
            IntPtr eigenvectors_address = eigenvectors_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 初始化临时矩阵内存 ----
            // 计算会覆盖矩阵内存空间，需要保留原矩阵的数据
            T[] tempData = new T[_dataRef.Length];
            Vector.ArrayCopy(_dataRef, tempData);
            GCHandle tempData_GC = GCHandle.Alloc(tempData, GCHandleType.Pinned);
            IntPtr tempData_address = tempData_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 调用API ----
            if (tempData is Complex32[])
            {
                if (_isSymmetric)
                {
                    float[] eigenvalues_float = new float[N];
                    Complex32[] eigenvalues_fc32 = eigenvalues as Complex32[];
                    error = MatrixCaller.LAPACKE_cheev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                       N, tempData_address, N, eigenvalues_float);
                    Vector.RealImageToComplex(eigenvalues_float, Vector.ConstInit(N, 0.0f), eigenvalues_fc32);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_cgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                       N, tempData_address, N, eigenvalues_address,
                                                       IntPtr.Zero, N, eigenvectors_address, N);
                }

            }
            else if (tempData is Complex[])
            {
                if (_isSymmetric)
                {
                    double[] eigenvalues_double = new double[N];
                    Complex[] eigenvalues_fc = eigenvalues as Complex[];
                    error = MatrixCaller.LAPACKE_zheev(MatrixLayoutEnum.RowMajor, jobz, MatrixTriangularChar.UpTriangular,
                                                       N, eigenvectors_address, N, eigenvalues_double);
                    Vector.RealImageToComplex(eigenvalues_double, Vector.ConstInit(N, 0.0), eigenvalues_fc);
                }
                else
                {
                    error = MatrixCaller.LAPACKE_zgeev(MatrixLayoutEnum.RowMajor, JobCompute.NotCompute, jobz,
                                                       N, tempData_address, N, eigenvalues_address,
                                                       IntPtr.Zero, N, eigenvectors_address, N);
                }
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region ---- 扫尾处理 ----
            if (_isSymmetric && eigenvectorsOut)
            {
                Vector.ArrayCopy(tempData, eigenvectors_address);
            }

            tempData_GC.Free();
            eigenvalues_GC.Free();
            eigenvectors_GC.Free();
            #endregion

            if (error != 0) { throw new InvalidOperationException(String.Format("Compute error. Error code = {0}", error)); }
            
            #region ---- 整理特征向量数组 ----
            if (eigenvectorsOut)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        eigenVectors[j][i] = eigenvectors[i * N + j];
                    }
                }
            }
            #endregion
        }
    }

    internal partial class MatrixCaller
    {
        #region ---- 一般矩阵 (LAPACK Least Squares and Eigenvalue Problem -> Drive -> LAPACKE_?geev) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvLeft, JobCompute jobvRight,
                                                int n, float[,] a, int lda, float[] wr, float[] wi,
                                                float[,] vl, int ldvl, float[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                int n, double[,] a, int lda, double[] wr, double[] wi,
                                                double[,] vl, int ldvl, double[,] vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                int n, IntPtr a, int lda, IntPtr w,
                                                IntPtr vl, int ldvl, IntPtr vr, int ldvr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgeev(MatrixLayoutEnum matrix_layout, JobCompute jobvl, JobCompute jobvr,
                                                int n, IntPtr a, int lda, IntPtr w,
                                                IntPtr vl, int ldvl, IntPtr vr, int ldvr);
        #endregion

        #region ---- 对称矩阵 (LAPACK Least Squares and Eigenvalue Problem -> Drive -> LAPACKE_?syev/?heev) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_ssyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo, 
                                                 int n, float[,] a, int lda, float[] w);
       
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dsyev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo, 
                                                 int n, double[,] a, int lda, double[] w);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, float[] w);
       
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zheev(MatrixLayoutEnum matrix_layout, JobCompute jobz, MatrixTriangularChar uplo,
                                                 int n, IntPtr a, int lda, double[] w);
        #endregion
    }
}
