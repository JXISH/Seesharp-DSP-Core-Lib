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
        /// 矩阵乘以常数
        /// </summary>
        public static Matrix<T> operator *( T scale, Matrix<T> matrix)
        {
            return matrix * scale;
        }

        /// <summary>
        /// 矩阵乘以常数
        /// </summary>
        public static Matrix<T> operator *(Matrix<T> matrix, T scale)
        {
            Matrix<T> result = new Matrix<T>(matrix);
            Multiply(scale, result);
            return result;
        }

        /// <summary>
        /// 矩阵乘以常数
        /// </summary>
        public static void Multiply(T scale, Matrix<T> inout)
        {
            #region ---- 判断 ----
            if (!(inout.IsValid)) { throw (new ArgumentException("Matrix is not valid.")); }
            #endregion

            Vector.ArrayScale(inout._dataRef, scale);
        }

        /// <summary>
        /// 向量点乘向量
        /// </summary>
        public static T Multiply(T[] left, T[] right)
        {
            #region ---- 判断 ----

             if (left.Length != right.Length )
            { throw (new ArgumentException("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----

            GCHandle left_GC = GCHandle.Alloc(left, GCHandleType.Pinned);
            IntPtr left_address = left_GC.AddrOfPinnedObject();

            GCHandle right_GC = GCHandle.Alloc(right, GCHandleType.Pinned);
            IntPtr right_address = right_GC.AddrOfPinnedObject();

            T [] result = new T[1];
            int N = left.Length;

            #endregion

            #region ---- 调用API ----
            if (result is float[] result_f32)
            {
                result_f32[0] = MatrixCaller.cblas_sdot(N, left_address, 1, right_address, 1);
            }
            else if (result is double[] result_f64)
            {
                result_f64[0] = MatrixCaller.cblas_ddot(N, left_address, 1, right_address, 1);
            }
            else if (result is Complex32[] result_fc32)
            {
                Complex32 tempdata_fc32;
                MatrixCaller.cblas_cdotc_sub(N, left_address, 1, right_address, 1,out tempdata_fc32);
                result_fc32[0] = tempdata_fc32;
            }
            else if (result is Complex[] result_fc64)
            {
                Complex tempdata_fc64;
                MatrixCaller.cblas_zdotc_sub(N, left_address, 1, right_address, 1, out tempdata_fc64);
                result_fc64[0] = tempdata_fc64;
            }
            #endregion

            #region ---- 收尾 ----
            left_GC.Free();
            right_GC.Free();
            #endregion

            return result[0];
        }

        /// <summary>
        /// 矩阵乘以矩阵
        /// </summary>
        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            Matrix<T> result = new Matrix<T>(left.Row, right.Colum);
            Multiply(left, right, result);
            return result;
        }

        /// <summary>
        /// 矩阵乘以矩阵
        /// </summary>
        public static void Multiply(Matrix<T> a, Matrix<T> b, Matrix<T> c)
        {
            #region ---- 判断 ----

            if (!(a.IsValid && b.IsValid && c.IsValid))
            { throw (new ArgumentException("Matrix is not valid.")); }

            if (a._columSize != b._rowSize || a._rowSize != c._rowSize || b._columSize != c._columSize)
            { throw (new ArgumentException("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----
            GCHandle a_GC = GCHandle.Alloc(a._dataRef, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            GCHandle b_GC = GCHandle.Alloc(b._dataRef, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            GCHandle c_GC = GCHandle.Alloc(c._dataRef, GCHandleType.Pinned);
            IntPtr c_address = c_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 调用API ----
            if (a._dataRef is float[])
            {
                MatrixCaller.cblas_sgemm(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         a.Row, b.Colum, a.Colum, 1.0f, a_address, a.Colum, b_address, b.Colum,
                                         0.0f, c_address, c.Colum);
            }
            else if (a._dataRef is double[])
            {
                MatrixCaller.cblas_dgemm(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         a.Row, b.Colum, a.Colum, 1.0, a_address, a.Colum, b_address, b.Colum,
                                         0.0, c_address, c.Colum);
            }
            else if (a._dataRef is Complex32[])
            {
                MatrixCaller.cblas_cgemm3m(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         a.Row, b.Colum, a.Colum, Complex32.One, a_address, a.Colum, b_address, b.Colum,
                                         Complex32.Zero, c_address, c.Colum);
            }
            else if (a._dataRef is Complex[])
            {
                MatrixCaller.cblas_zgemm3m(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         a.Row, b.Colum, a.Colum, Complex.One, a_address, a.Colum, b_address, b.Colum,
                                         Complex.Zero, c_address, c.Colum);
            }
            #endregion

            #region ---- 收尾 ----
            a_GC.Free();
            b_GC.Free();
            c_GC.Free();
            #endregion
        }

        /// <summary>
        /// 矩阵右乘向量
        /// </summary>
        public static T[] operator *(Matrix<T> left, T[] right)
        {
            T[] result = new T[left.Row];
            Multiply(left, right, result, true);
            return result;
        }

        /// <summary>
        /// 矩阵左乘向量
        /// </summary>
        public static T[] operator *(T[] left, Matrix<T> right)
        {
            T[] result = new T[right.Colum];
            Multiply(right, left, result, false);
            return result;
        }

        /// <summary>
        /// 矩阵右乘向量
        /// </summary>
        public static void Multiply(Matrix<T> left, T[] right, T[] result)
        {
            Multiply(left, right, result, true);
        }

        /// <summary>
        /// 矩阵左乘向量
        /// </summary>
        public static void Multiply( T[] left, Matrix<T> right, T[] result)
        {
            Multiply(right, left, result, false);
        }

        private static void Multiply(Matrix<T> a, T[] b, T[] c, bool rightVector)
        {
            #region ---- 判断 ----

            if (!a.IsValid)
            { throw (new ArgumentException("Matrix is not valid.")); }

            if (!(b != null && b.Length > 0 && c != null && c.Length > 0))
            { throw (new ArgumentException("Vector is not valid.")); }

            if (a._columSize != b.Length || a._rowSize != c.Length)
            { throw (new ArgumentException("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----
            GCHandle a_GC = GCHandle.Alloc(a._dataRef, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            GCHandle c_GC = GCHandle.Alloc(c, GCHandleType.Pinned);
            IntPtr c_address = c_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 调用API ----

            MatrixTransposeEnum traspose;
            MatrixLayoutEnum layout;

            if (a._dataRef is float[])
            {
                if (a.IsSymmetric)
                {
                    MatrixCaller.cblas_ssymv(MatrixLayoutEnum.RowMajor, MatrixTriangularEnum.UpTriangular,
                                             a.Row, 1.0f, a_address, a.Colum, b_address, 1,
                                             0.0f, c_address, 1);
                }
                else
                {
                    traspose = rightVector ? MatrixTransposeEnum.NoTranspose : MatrixTransposeEnum.Transpose;
                    MatrixCaller.cblas_sgemv(MatrixLayoutEnum.RowMajor, traspose,
                                             a.Row, a.Colum, 1.0f, a_address, a.Colum, b_address, 1,
                                             0.0f, c_address, 1);
                }
            }
            else if (a._dataRef is double[])
            {
                if (a.IsSymmetric)
                {
                    MatrixCaller.cblas_dsymv(MatrixLayoutEnum.RowMajor, MatrixTriangularEnum.UpTriangular,
                                             a.Row, 1.0, a_address, a.Colum, b_address, 1,
                                             0.0, c_address, 1);
                }
                else
                {
                    traspose = rightVector ? MatrixTransposeEnum.NoTranspose : MatrixTransposeEnum.Transpose;
                    MatrixCaller.cblas_dgemv(MatrixLayoutEnum.RowMajor, traspose,
                                             a.Row, a.Colum, 1.0, a_address, a.Colum, b_address, 1,
                                             0.0, c_address, 1);
                }
            }
            else if (a._dataRef is Complex32[])
            {
                if (a.IsSymmetric)
                {
                    layout = rightVector ? MatrixLayoutEnum.RowMajor : MatrixLayoutEnum.ColMajor;
                    MatrixCaller.cblas_chemv(layout, MatrixTriangularEnum.UpTriangular,
                                             a.Row, Complex32.One, a_address, a.Colum, b_address, 1,
                                             Complex32.Zero, c_address, 1);
                }
                else
                {
                    traspose = rightVector ? MatrixTransposeEnum.NoTranspose : MatrixTransposeEnum.Transpose;
                    MatrixCaller.cblas_cgemv(MatrixLayoutEnum.RowMajor, traspose,
                                             a.Row, a.Colum, Complex32.One, a_address, a.Colum, b_address, 1,
                                             Complex32.Zero, c_address, 1);
                }
            }
            else if (a._dataRef is Complex[])
            {
                if (a.IsSymmetric)
                {
                    layout = rightVector ? MatrixLayoutEnum.RowMajor : MatrixLayoutEnum.ColMajor;
                    MatrixCaller.cblas_zhemv(layout, MatrixTriangularEnum.UpTriangular,
                                             a.Row, Complex.One, a_address, a.Colum, b_address, 1,
                                             Complex.Zero, c_address, 1);
                }
                else
                {
                    traspose = rightVector ? MatrixTransposeEnum.NoTranspose : MatrixTransposeEnum.Transpose;
                    MatrixCaller.cblas_zgemv(MatrixLayoutEnum.RowMajor, traspose,
                                             a.Row, a.Colum, Complex.One, a_address, a.Colum, b_address, 1,
                                             Complex.Zero, c_address, 1);
                }
            }
            #endregion

            #region ---- 收尾 ----
            a_GC.Free();
            b_GC.Free();
            c_GC.Free();
            #endregion
        }       

    }
    internal partial class MatrixCaller
    {
        #region ---- Vector x Vector (BLAS Level 1 cblas_?dot) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern float cblas_sdot(int n, float[] x, int incx, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern float cblas_sdot(int n, IntPtr x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern double cblas_ddot(int n, double[] x, int incx, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern double cblas_ddot(int n, IntPtr x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cdotc_sub(int n, IntPtr x, int incx, IntPtr y, int incy, out Complex32 dotc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zdotc_sub(int n, IntPtr x, int incx, IntPtr y, int incy, out Complex dotc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cdotu_sub(int n, IntPtr x, int incx, IntPtr y, int incy, out Complex32 dotc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zdotu_sub(int n, IntPtr x, int incx, IntPtr y, int incy, out Complex dotc);

        #endregion

        #region ---- 一般矩阵 Matrix x Vector (BLAS Level 2 cblas_?gemv) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, float alpha, float[,] a, int lda, float[] x, int incx,
                                                float beta, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, float alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                float beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, double alpha, double[,] a, int lda, double[] x, int incx,
                                                double beta, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, double alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                double beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, Complex32 alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                Complex32 beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n, Complex alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                Complex beta, IntPtr y, int incy);
        #endregion

        #region---- 对称矩阵 Matrix x Vector (BLAS Level 2 cblas_?symv/?hemv) -----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, float alpha, float[,] a, int lda, float[] x, int incx,
                                                float beta, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, float alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                float beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, double alpha, double[,] a, int lda, double[] x, int incx,
                                                double beta, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, double alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                double beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_chemv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, Complex32 alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                Complex32 beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zhemv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n, Complex alpha, IntPtr a, int lda, IntPtr x, int incx,
                                                Complex beta, IntPtr y, int incy);

        #endregion

        #region ---- 一般矩阵 Matrix x Matrix (BLAS Level 3 cblas_?gemm) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb, 
                                                int m, int n, int k, float alpha, float[,] a, int lda, float[,] b, int ldb, 
                                                float beta, float[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k, float alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                float beta, IntPtr c, int ldc);


        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k, double alpha, double[,] a, int lda, double[,] b, int ldb, 
                                                double beta, double[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k, double alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                double beta, IntPtr c, int ldc);


        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k, Complex32 alpha, IntPtr a, int lda, IntPtr b, int ldb, 
                                                Complex32 beta, IntPtr c, int ldc);
        
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k, Complex alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                Complex beta, IntPtr c, int ldc);

        #endregion

        #region ---- 对称矩阵 Matrix x Matrix  (BLAS Level 3 cblas_?symm/?hemm) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo, 
                                                int m, int n, float alpha, float[,] a, int lda, float[,] b, int ldb, 
                                                float beta, float[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n, double alpha, double[,] a, int lda, double[,] b, int ldb,
                                                double beta, double[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_chemm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo, 
                                                int m, int n, Complex32 alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                Complex32 beta, IntPtr c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zhemm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n, Complex alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                Complex beta, IntPtr c, int ldc);

        #endregion

        #region ---- 一般复数矩阵 Matrix x Matrix (BLAS-like Extensions cblas_?gemm3m) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemm3m(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb, 
                                                  int m, int n, int k, Complex32 alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                  Complex32 beta, IntPtr c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemm3m(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                  int m, int n, int k, Complex alpha, IntPtr a, int lda, IntPtr b, int ldb,
                                                  Complex beta, IntPtr c, int ldc);

        #endregion
    }
}
