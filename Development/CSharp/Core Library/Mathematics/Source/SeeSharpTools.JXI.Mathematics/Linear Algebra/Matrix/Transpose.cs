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
        /// 矩阵转置 ** 永远抛异常不能用 **
        /// </summary>
        public void Transpose()
        {
            #region ---- 判断 ----
            if (!this.IsValid) { return; }
            #endregion

            #region ---- 调用API ----
            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();

            if (_dataRef is float[])
            {
                MatrixCaller.mkl_simatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           _rowSize, _columSize, 1.0f, data_address, _columSize, _rowSize);
            }
            else if (_dataRef is double[])
            {
                MatrixCaller.mkl_dimatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           _rowSize, _columSize, 1.0, data_address, _columSize, _rowSize);
            }
            else if (_dataRef is Complex32[])
            {
                MatrixCaller.mkl_cimatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           _rowSize, _columSize, Complex32.One, data_address, _columSize, _rowSize);
            }
            else if (_dataRef is Complex[])
            {
                MatrixCaller.mkl_zimatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           _rowSize, _columSize, Complex.One, data_address, _columSize, _rowSize);
            }
            else { throw new ArgumentException("Data type not supported"); }

            data_GC.Free();

            #endregion

            #region ---- 重置矩阵大小 ----
            int temp = _rowSize;
            _rowSize = _columSize;
            _columSize = _rowSize;
            #endregion

            return;
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        public static Matrix<T> GetTranspose(Matrix<T> source)
        {
            Matrix<T> result = new Matrix<T>(source);
            result.Transpose();
            return result;
        }

        private static void Transpose(T[] data, int row, int colum, T[] result)
        {
            #region ---- 判断 ----
            if (data.Length <= 0 || data == null) { return; }
            if (result.Length <= 0 || result == null) { return; }
            if (data.Length != row * colum) { throw (new ArgumentException("The data size is invalid.")); }
            if (result.Length != row * colum) { throw (new ArgumentException("The result size is invalid.")); }
            #endregion

            #region ---- 调用API ----
            GCHandle data_GC = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();

            GCHandle result_GC = GCHandle.Alloc(result, GCHandleType.Pinned);
            IntPtr result_address = result_GC.AddrOfPinnedObject();

            if (data is float[])
            {
                MatrixCaller.mkl_somatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           row, colum, 1.0f, data_address, colum,
                                           result_address, row);
            }
            else if (data is double[])
            {
                MatrixCaller.mkl_domatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           row, colum, 1.0, data_address, colum,
                                           result_address, row);
            }
            else if (data is Complex32[])
            {
                MatrixCaller.mkl_comatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           row, colum, Complex32.One, data_address, colum,
                                           result_address, row);
            }
            else if (data is Complex[])
            {
                MatrixCaller.mkl_zomatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           row, colum, Complex.One, data_address, colum,
                                           result_address, row);
            }
            else { throw new ArgumentException("Data type not supported"); }

            data_GC.Free();
            result_GC.Free();
            #endregion
        }
    }

    internal partial class MatrixCaller
    {
        #region ---- Inplace Transpose (BLAS-like Extensions mkl_?imatcopy) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_simatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, float alpha, float[,] AB, int lda, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_simatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                          int rows, int cols, float alpha, IntPtr AB, int lda, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_dimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, double alpha, double[,] AB, int lda, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_dimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                          int rows, int cols, double alpha, IntPtr AB, int lda, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_cimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, Complex32 alpha, IntPtr AB, int lda, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_zimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, Complex alpha, IntPtr AB, int lda, int ldb);

        #endregion

        #region ---- Outplace Transpose (BLAS-like Extensions mkl_?omatcopy) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_somatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, float alpha, float[,] A, int lda,
                                                  float[,] B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_somatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, float alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_domatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, double alpha, double[,] A, int lda,
                                                  double[,] B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_domatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, double alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_comatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, Complex32 alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void mkl_zomatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols, Complex alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        #endregion
    }
}
