using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;

using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix
{
    public partial class Matrix<T>
    {
        public T Determinant()
        {
            return Determinant(this);
        }

        public static T Determinant(Matrix<T> input)
        {
            #region ---- 判断 ----
            if (!(input.IsValid && input.IsSquare))
            { throw new System.Exception("The Input Matrix is invalid."); }
            #endregion

            #region ---- 准备 ----
            int row = input.Row;
            int colum = input.Colum;
            int minSide = Math.Min(row, colum);           
            int[] rowInterchanged = new int [minSide];

            // 创建临时内存存放数据.
            // 计算会覆盖矩阵内存空间, 需要保留原矩阵的数据
            T[,] tempMatrix = new T[row, colum];
            Vector.ArrayCopy<T>(input.MatrixArray, tempMatrix);

            GCHandle tempMatrix_GC = GCHandle.Alloc(tempMatrix, GCHandleType.Pinned);
            IntPtr tempMatrix_address = tempMatrix_GC.AddrOfPinnedObject();

            #endregion
            // LU 分解
            LUFactorizationInplace<T>(tempMatrix_address, row, colum, rowInterchanged);

            #region ---- 收尾 ----

            tempMatrix_GC.Free();

            #endregion

            int sign = 1;
            for (int i = 0; i < rowInterchanged.Length; i++)
            {
                sign *= ((rowInterchanged[i] == i) ? 1 : -1);
            }

            // 对角线相乘
            T[] determinant = new T[1];
            if (determinant is float[] determinant_f32)
            {
                determinant_f32[0] = sign;
                float[,] upperTriangular_f32 = tempMatrix as float[,];
                for (int i = 0; i < minSide; i++) { determinant_f32[0] *= upperTriangular_f32[i, i]; }
            }
            else if (determinant is double[] determinant_f64)
            {
                determinant_f64[0] = sign;
                double[,] upperTriangular_f64 = tempMatrix as double[,];
                for (int i = 0; i < minSide; i++) { determinant_f64[0] *= upperTriangular_f64[i, i]; }
            }
            else if (determinant is Complex32[] determinant_fc32)
            {
                determinant_fc32[0] = 1.0;
                Complex32[,] upperTriangular_fc32 = tempMatrix as Complex32[,];
                for (int i = 0; i < minSide; i++) { determinant_fc32[0] *= upperTriangular_fc32[i, i]; }
            }
            else if (determinant is Complex[] determinant_fc64)
            {
                determinant_fc64[0] = 1.0;
                Complex[,] upperTriangular_fc64 = tempMatrix as Complex[,];
                for (int i = 0; i < minSide; i++) { determinant_fc64[0] *= upperTriangular_fc64[i, i]; }
            }
            else { throw new System.Exception("Data type not supported"); }

            return determinant[0];
        }
        
        private static void LUFactorization(T[,] matrixA, out T[,] lowerTriangular, out T[,] upperTriangular, out int[,] permutationMatrix)
        {
            #region ---- 准备 ----
            int rowA = matrixA.GetLength(0);
            int columA = matrixA.GetLength(1);
            int minSide = Math.Min(rowA, columA);

            // 创建临时内存存放数据.
            // 计算会覆盖矩阵内存空间, 需要保留原矩阵的数据
            T[,] tempMatrix = new T[rowA, columA];
            Vector.ArrayCopy<T>(matrixA, tempMatrix);

            GCHandle tempMatrix_GC = GCHandle.Alloc(tempMatrix, GCHandleType.Pinned);
            IntPtr tempMatrix_address = tempMatrix_GC.AddrOfPinnedObject();

            int[] ipiv = new int [minSide];
            #endregion

            // LU 分解
            LUFactorizationInplace<T>(tempMatrix_address, rowA, columA, ipiv);

            #region LU数组赋值

            // 定义下三角矩阵
            lowerTriangular = new T[rowA, minSide];
            // 定义上三角矩阵
            upperTriangular = new T[minSide, columA];
            // 定义置换矩阵
            permutationMatrix = new int[rowA, rowA];

            // L 对角线为 1
            if (lowerTriangular is float[,] L_f32)
            {
                for (int i = 0; i < minSide; i++) { L_f32[i, i] = 1.0f; }
            }
            else if (lowerTriangular is double[,] L_f64)
            {
                for (int i = 0; i < minSide; i++) { L_f64[i, i] = 1.0; }
            }
            else if (lowerTriangular is Complex32[,] L_fc32)
            {
                for (int i = 0; i < minSide; i++) { L_fc32[i, i] = Complex32.One; }
            }
            else if (lowerTriangular is Complex[,] L_fc64)
            {
                for (int i = 0; i < minSide; i++) { L_fc64[i, i] = Complex.One; }
            }
            else { throw new System.Exception("Data type not supported"); }

            // L 其它项赋值
            for (int j = 0; j < minSide; j++)
            {
                for (int i = j + 1; i < rowA; i++)
                {
                    lowerTriangular[i, j] = tempMatrix[i, j];
                }
            }

            // U 赋值
            for (int i = 0; i < minSide; i++)
            {
                for (int j = i; j < columA; j++)
                {
                    upperTriangular[i, j] = tempMatrix[i, j];
                }
            }

            // P 置换赋值
            // 定义每行的标记
            int[] rowMark = new int[rowA];
            for (int i = 0; i < rowA; i++) { rowMark[i] = i; }
            // 置换标记
            for (int i = 0; i < ipiv.Length; i++)
            {
                int temp = rowMark[i];
                rowMark[i] = rowMark[ipiv[i]];
                rowMark[ipiv[i]] = temp;
            }
            // 更新置换矩阵
            for (int i = 0; i < rowA; i++) { permutationMatrix[i, rowMark[i]] = 1; }

            #endregion

            #region ---- 收尾 ----

            tempMatrix_GC.Free();

            #endregion
        }

        /// <summary>
        /// 对矩阵进行LU分解
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="matrixA"> 行排列 </param>
        /// <param name="rowA"></param>
        /// <param name="columA"></param>
        /// <param name="ipiv"></param>
        /// <param name="info"></param>
        private static void LUFactorizationInplace<T>(IntPtr matrixA, int rowA, int columA, int[] ipiv)
        {
            int error = 0;

            #region ---- 准备 ----
            T[] datatype = new T[1];

            int interval = columA;
            int minSide = Math.Min(rowA, columA);
            if (ipiv.Length < minSide) { throw new System.Exception("The size of ipiv is invalid."); };

            #endregion

            #region ---- 调用API ----

            if (datatype is float[])
            {
                error = MatrixCaller.LAPACKE_sgetrf(MatrixLayoutEnum.RowMajor,
                                                   rowA, columA, matrixA, interval,
                                                   ipiv);
            }
            else if (datatype is double[])
            {
                error = MatrixCaller.LAPACKE_dgetrf(MatrixLayoutEnum.RowMajor,
                                                   rowA, columA, matrixA, interval,
                                                   ipiv);
            }
            else if (datatype is Complex32[])
            {
                error = MatrixCaller.LAPACKE_cgetrf(MatrixLayoutEnum.RowMajor,
                                                   rowA, columA, matrixA, interval,
                                                   ipiv);
            }
            else if (datatype is Complex[])
            {
                error = MatrixCaller.LAPACKE_zgetrf(MatrixLayoutEnum.RowMajor,
                                                   rowA, columA, matrixA, interval,
                                                   ipiv);
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion  

            for (int i = 0; i < minSide; i++) { ipiv[i]--; }

            if (error < 0) { throw new System.Exception(String.Format("Compute error. Error code = {0}", error)); }
        }
    }

    internal partial class MatrixCaller
    {


        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgetrf(MatrixLayoutEnum matrix_layout, 
                                                  int m, int n, float[,] a, int lda, 
                                                  int[] ipiv);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_sgetrf(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, IntPtr a, int lda,
                                                  int[] ipiv);


        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgetrf(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, double[,] a, int lda,
                                                  int[] ipiv);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_dgetrf(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, IntPtr a, int lda,
                                                  int[] ipiv);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_cgetrf(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, IntPtr a, int lda, 
                                                  int[] ipiv);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int LAPACKE_zgetrf(MatrixLayoutEnum matrix_layout,
                                                  int m, int n, IntPtr a, int lda,
                                                  int[] ipiv);
    }
}
