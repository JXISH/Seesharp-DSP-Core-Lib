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
        #region 矩阵乘以常数

        /// <summary>
        /// 矩阵乘以常数
        /// </summary>
        public static Matrix<T> operator *(T scale, Matrix<T> matrix)
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
            Multiply(inout, scale);
        }

        /// <summary>
        /// 矩阵乘以常数
        /// </summary>
        public static void Multiply(Matrix<T> inout, T scale)
        {
            #region ---- 判断 ----
            if (!(inout.IsValid)) { throw (new System.Exception("Matrix is not valid.")); }
            #endregion

            Vector.ArrayScale(inout._dataAddress, scale, inout.RefSize);
        }

        #endregion

        #region 向量点乘向量

        /// <summary>
        /// 向量点乘向量
        /// ouput = left * right
        /// </summary>
        public static T Multiply2(T[] left, T[] right)
        {
            #region ---- 判断 ----

            if (left.Length != right.Length)
            { throw (new System.Exception("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----

            GCHandle left_GC = GCHandle.Alloc(left, GCHandleType.Pinned);
            IntPtr left_address = left_GC.AddrOfPinnedObject();

            GCHandle right_GC = GCHandle.Alloc(right, GCHandleType.Pinned);
            IntPtr right_address = right_GC.AddrOfPinnedObject();

            T[] result = new T[1];
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
                MatrixCaller.cblas_cdotu_sub(N, left_address, 1, right_address, 1, out tempdata_fc32);
                result_fc32[0] = tempdata_fc32;
            }
            else if (result is Complex[] result_fc64)
            {
                Complex tempdata_fc64;
                MatrixCaller.cblas_zdotu_sub(N, left_address, 1, right_address, 1, out tempdata_fc64);
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
        /// 向量点乘向量
        /// ouput = conj (left) * right
        /// </summary>
        public static T Multiply(T[] left, T[] right)
        {
            #region ---- 判断 ----

            if (left.Length != right.Length)
            { throw (new System.Exception("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----

            GCHandle left_GC = GCHandle.Alloc(left, GCHandleType.Pinned);
            IntPtr left_address = left_GC.AddrOfPinnedObject();

            GCHandle right_GC = GCHandle.Alloc(right, GCHandleType.Pinned);
            IntPtr right_address = right_GC.AddrOfPinnedObject();

            T[] result = new T[1];
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
                MatrixCaller.cblas_cdotc_sub(N, left_address, 1, right_address, 1, out tempdata_fc32);
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

        #endregion

        #region 矩阵乘以矩阵

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
            { throw (new System.Exception("Matrix is not valid.")); }

            if (a.Colum != b.Row || a.Row != c.Row || b.Colum != c.Colum)
            { throw (new System.Exception("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----
            #endregion

            #region ---- 调用API ----
            if (a.IsSymmetric)
            {
                MultiplySymmetric(a._dataAddress, b._dataAddress, c._dataAddress,
                                  c.Row, c.Colum, true);
            }
            else
            {
                MultiplyGeneral(a._dataAddress, b._dataAddress, c._dataAddress,
                                c.Row, c.Colum, a.Colum);
            }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        /// <summary>
        /// 矩阵乘以矩阵
        /// </summary>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为k </param>
        /// <param name="matrixB"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="matrixC"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="m"> matrixC行数 / matrixA行数 </param>
        /// <param name="n"> matrixC列数 / matrixB列数 </param>
        /// <param name="k"> matrixB行数 / matrixA列数 </param>
        private static void MultiplyGeneral(IntPtr matrixA, IntPtr matrixB, IntPtr matrixC,
                                            int m, int n, int k)
        {
            #region ---- 准备 ----
            T[] datatype = new T[1];

            int rowC = m;
            int columC = n;
            int rowA = m;
            int columA = k;
            int rowB = k;
            int columB = n;

            #endregion

            #region ---- 调用API ----
            if (datatype is float[])
            {
                MatrixCaller.cblas_sgemm(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         rowC, columC, columA,
                                         1.0f, matrixA, columA,
                                         matrixB, columB,
                                         0.0f, matrixC, columC);
            }
            else if (datatype is double[])
            {
                MatrixCaller.cblas_dgemm(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                         rowC, columC, columA,
                                         1.0, matrixA, columA,
                                         matrixB, columB,
                                         0.0, matrixC, columC);
            }
            else if (datatype is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                Complex32 zerofc32 = Complex32.Zero;
                MatrixCaller.cblas_cgemm3m(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                           rowC, columC, columA,
                                           ref onefc32, matrixA, columA,
                                           matrixB, columB,
                                           ref zerofc32, matrixC, columC);
            }
            else if (datatype is Complex[])
            {
                Complex onefc64 = Complex.One;
                Complex zerofc64 = Complex.Zero;
                MatrixCaller.cblas_zgemm3m(MatrixLayoutEnum.RowMajor, MatrixTransposeEnum.NoTranspose, MatrixTransposeEnum.NoTranspose,
                                           rowC, columC, columA,
                                           ref onefc64, matrixA, columA,
                                           matrixB, columB,
                                           ref zerofc64, matrixC, columC);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        /// <summary>
        /// 矩阵乘以矩阵, matrixA是对称矩阵
        /// </summary>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为 aLeft ? m : n </param>
        /// <param name="matrixB"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="matrixC"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="m"> matrixC行数 / aLeft ? matrixA行数 : matrixB行数 </param>
        /// <param name="n"> matrixC列数 / aLeft ? matrixB列数 : matrixA列数 </param>
        /// <param name="aLeft"></param>
        private static void MultiplySymmetric(IntPtr matrixA, IntPtr matrixB, IntPtr matrixC,
                                              int m, int n, bool aLeft)
        {
            #region ---- 准备 ----
            T[] datatype = new T[1];

            CBLAS_SIDE side = aLeft ? CBLAS_SIDE.CblasLeft : CBLAS_SIDE.CblasRight;

            int rowC = m;
            int columC = n;
            int rowA = aLeft ? m : n;
            int columA = rowA;
            int rowB = aLeft ? columA : rowC;
            int columB = aLeft ? columC : rowA;

            #endregion

            #region ---- 调用API ----
            if (datatype is float[])
            {
                MatrixCaller.cblas_ssymm(MatrixLayoutEnum.RowMajor, side, MatrixTriangularEnum.UpTriangular,
                                         rowC, columC,
                                         1.0f, matrixA, columA,
                                         matrixB, columB,
                                         0.0f, matrixC, columC);
            }
            else if (datatype is double[])
            {
                MatrixCaller.cblas_dsymm(MatrixLayoutEnum.RowMajor, side, MatrixTriangularEnum.UpTriangular,
                                         rowC, columC,
                                         1.0, matrixA, columA,
                                         matrixB, columB,
                                         0.0, matrixC, columC);
            }
            else if (datatype is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                Complex32 zerofc32 = Complex32.Zero;
                MatrixCaller.cblas_chemm(MatrixLayoutEnum.RowMajor, side, MatrixTriangularEnum.UpTriangular,
                                         rowC, columC,
                                         ref onefc32, matrixA, columA,
                                         matrixB, columB,
                                         ref zerofc32, matrixC, columC);
            }
            else if (datatype is Complex[])
            {
                Complex onefc64 = Complex.One;
                Complex zerofc64 = Complex.Zero;
                MatrixCaller.cblas_zhemm(MatrixLayoutEnum.RowMajor, side, MatrixTriangularEnum.UpTriangular,
                                         rowC, columC,
                                         ref onefc64, matrixA, columA,
                                         matrixB, columB,
                                         ref zerofc64, matrixC, columC);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        #endregion

        #region 矩阵乘向量

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
        //public static T[] operator *(T[] left, Matrix<T> right)
        //{
        //    T[] result = new T[right.Colum];
        //    Multiply(right, left, result, false);
        //    return result;
        //}

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
        //public static void Multiply(T[] left, Matrix<T> right, T[] result)
        //{
        //    Multiply(right, left, result, false);
        //}

        /// <summary>
        /// 矩阵乘向量
        /// </summary>
        private static void Multiply(Matrix<T> a, T[] b, T[] c, bool rightVector)
        {
            #region ---- 判断 ----

            if (!a.IsValid)
            { throw (new System.Exception("Matrix is not valid.")); }

            if (!(b != null && b.Length > 0 && c != null && c.Length > 0))
            { throw (new System.Exception("Vector is not valid.")); }

            if (a.Colum != b.Length || a.Row != c.Length)
            { throw (new System.Exception("Data size is not valid.")); }

            #endregion

            #region ---- 准备 ----
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            GCHandle c_GC = GCHandle.Alloc(c, GCHandleType.Pinned);
            IntPtr c_address = c_GC.AddrOfPinnedObject();
            #endregion

            #region ---- 调用API ----

            if (a.IsSymmetric)
            {
                MultiplySymmetric(a._dataAddress, b_address, c_address,
                                  a.Row, rightVector);
            }
            else
            {
                MultiplyGeneral(a._dataAddress, b_address, c_address,
                                a.Row, a.Colum, rightVector);
            }

            #endregion

            #region ---- 收尾 ----
            b_GC.Free();
            c_GC.Free();
            #endregion
        }

        /// <summary>
        /// 矩阵乘向量
        /// </summary>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="vectorB"></param>
        /// <param name="vectorC"></param>
        /// <param name="m"> matrixA行数 </param>
        /// <param name="n"> matrixA列数 </param>
        /// <param name="rightVector"></param>
        private static void MultiplyGeneral(IntPtr matrixA, IntPtr vectorB, IntPtr vectorC,
                                            int m, int n, bool rightVector)
        {
            #region ---- 准备 ----

            T[] datatype = new T[1];
            MatrixTransposeEnum traspose = rightVector ? MatrixTransposeEnum.NoTranspose : MatrixTransposeEnum.Transpose;

            int rowA = m;
            int columA = n;

            #endregion

            #region ---- 调用API ----
            if (datatype is float[])
            {
                MatrixCaller.cblas_sgemv(MatrixLayoutEnum.RowMajor, traspose,
                                         rowA, columA,
                                         1.0f, matrixA, columA,
                                         vectorB, 1,
                                         0.0f, vectorC, 1);
            }
            else if (datatype is double[])
            {
                MatrixCaller.cblas_dgemv(MatrixLayoutEnum.RowMajor, traspose,
                                         rowA, columA,
                                         1.0, matrixA, columA,
                                         vectorB, 1,
                                         0.0, vectorC, 1);
            }
            else if (datatype is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                Complex32 zerofc32 = Complex32.Zero;
                MatrixCaller.cblas_cgemv(MatrixLayoutEnum.RowMajor, traspose,
                                         rowA, columA,
                                         ref onefc32, matrixA, columA,
                                         vectorB, 1,
                                         ref zerofc32, vectorC, 1);
            }
            else if (datatype is Complex[])
            {
                Complex onefc64 = Complex.One;
                Complex zerofc64 = Complex.Zero;
                MatrixCaller.cblas_zgemv(MatrixLayoutEnum.RowMajor, traspose,
                                         rowA, columA,
                                         ref onefc64, matrixA, columA,
                                         vectorB, 1,
                                         ref zerofc64, vectorC, 1);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        /// <summary>
        /// 矩阵乘向量, matrixA是对称矩阵
        /// </summary>
        /// <param name="matrixA"> 行排列, 行首元素存储间隔为n </param>
        /// <param name="vectorB"></param>
        /// <param name="vectorC"></param>
        /// <param name="n"> matrixA行数 / matrixA列数 </param>
        /// <param name="rightVector"></param>
        private static void MultiplySymmetric(IntPtr matrixA, IntPtr vectorB, IntPtr vectorC,
                                              int n, bool rightVector)
        {
            #region ---- 准备 ----

            T[] datatype = new T[1];
            MatrixLayoutEnum layout = rightVector ? MatrixLayoutEnum.RowMajor : MatrixLayoutEnum.ColMajor;

            int rowA = n;
            int columA = rowA;
            int orderA = n;

            #endregion

            #region ---- 调用API ----
            if (datatype is float[])
            {
                MatrixCaller.cblas_ssymv(layout, MatrixTriangularEnum.UpTriangular,
                                         orderA,
                                         1.0f, matrixA, columA,
                                         vectorB, 1,
                                         0.0f, vectorC, 1);

            }
            else if (datatype is double[])
            {
                MatrixCaller.cblas_dsymv(layout, MatrixTriangularEnum.UpTriangular,
                                         orderA,
                                         1.0, matrixA, columA,
                                         vectorB, 1,
                                         0.0, vectorC, 1);

            }
            else if (datatype is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                Complex32 zerofc32 = Complex32.Zero;
                MatrixCaller.cblas_chemv(layout, MatrixTriangularEnum.UpTriangular,
                                         orderA,
                                         ref onefc32, matrixA, columA,
                                         vectorB, 1,
                                         ref zerofc32, vectorC, 1);
            }
            else if (datatype is Complex[])
            {
                Complex onefc64 = Complex.One;
                Complex zerofc64 = Complex.Zero;
                MatrixCaller.cblas_zhemv(layout, MatrixTriangularEnum.UpTriangular,
                                         orderA,
                                         ref onefc64, matrixA, columA,
                                         vectorB, 1,
                                         ref zerofc64, vectorC, 1);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            #region ---- 收尾 ----
            #endregion
        }

        #endregion

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
                                                int m, int n,
                                                float alpha, float[,] a, int lda,
                                                float[] x, int incx,
                                                float beta, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n,
                                                float alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                float beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n,
                                                double alpha, double[,] a, int lda,
                                                double[] x, int incx,
                                                double beta, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n,
                                                double alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                double beta, IntPtr y, int incy);

        // alpha, beta is the point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n,
                                                ref Complex32 alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                ref Complex32 beta, IntPtr y, int incy);

        // alpha, beta is the point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemv(MatrixLayoutEnum Layout, MatrixTransposeEnum trans,
                                                int m, int n,
                                                ref Complex alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                ref Complex beta, IntPtr y, int incy);
        #endregion

        #region---- 对称矩阵 Matrix x Vector (BLAS Level 2 cblas_?symv/?hemv) -----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                float alpha, float[,] a, int lda,
                                                float[] x, int incx,
                                                float beta, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                float alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                float beta, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                double alpha, double[,] a, int lda,
                                                double[] x, int incx,
                                                double beta, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                double alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                double beta, IntPtr y, int incy);

        // alpha, beta is the point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_chemv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                ref Complex32 alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                ref Complex32 beta, IntPtr y, int incy);

        // alpha, beta is the point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zhemv(MatrixLayoutEnum Layout, MatrixTriangularEnum uplo,
                                                int n,
                                                ref Complex alpha, IntPtr a, int lda,
                                                IntPtr x, int incx,
                                                ref Complex beta, IntPtr y, int incy);

        #endregion

        #region ---- 一般矩阵 Matrix x Matrix (BLAS Level 3 cblas_?gemm) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                float alpha, float[,] a, int lda,
                                                float[,] b, int ldb,
                                                float beta, float[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_sgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                float alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                float beta, IntPtr c, int ldc);


        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                double alpha, double[,] a, int lda,
                                                double[,] b, int ldb,
                                                double beta, double[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                double alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                double beta, IntPtr c, int ldc);

        // alpha, beta is the point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                ref Complex32 alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                ref Complex32 beta, IntPtr c, int ldc);

        // alpha, beta is the point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemm(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                int m, int n, int k,
                                                ref Complex alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                ref Complex beta, IntPtr c, int ldc);

        #endregion

        #region ---- 对称矩阵 Matrix x Matrix  (BLAS Level 3 cblas_?symm/?hemm) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                float alpha, float[,] a, int lda,
                                                float[,] b, int ldb,
                                                float beta, float[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_ssymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                float alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                float beta, IntPtr c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                double alpha, double[,] a, int lda,
                                                double[,] b, int ldb,
                                                double beta, double[,] c, int ldc);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_dsymm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                double alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                double beta, IntPtr c, int ldc);

        // alpha, beta is the point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_chemm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                ref Complex32 alpha, IntPtr a, int lda,
                                                IntPtr b, int ldb,
                                                ref Complex32 beta, IntPtr c, int ldc);

        // alpha, beta is the point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zhemm(MatrixLayoutEnum Layout, CBLAS_SIDE side, MatrixTriangularEnum uplo,
                                                int m, int n,
                                                ref Complex alpha, IntPtr a,
                                                int lda, IntPtr b, int ldb,
                                                ref Complex beta, IntPtr c, int ldc);

        #endregion

        #region ---- 一般复数矩阵 Matrix x Matrix (BLAS-like Extensions cblas_?gemm3m) ----

        // alpha, beta is the point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_cgemm3m(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                  int m, int n, int k,
                                                  ref Complex32 alpha, IntPtr a, int lda,
                                                  IntPtr b, int ldb,
                                                  ref Complex32 beta, IntPtr c, int ldc);

        // alpha, beta is the point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void cblas_zgemm3m(MatrixLayoutEnum Layout, MatrixTransposeEnum transa, MatrixTransposeEnum transb,
                                                  int m, int n, int k,
                                                  ref Complex alpha, IntPtr a, int lda,
                                                  IntPtr b, int ldb,
                                                  ref Complex beta, IntPtr c, int ldc);

        #endregion
    }
}
