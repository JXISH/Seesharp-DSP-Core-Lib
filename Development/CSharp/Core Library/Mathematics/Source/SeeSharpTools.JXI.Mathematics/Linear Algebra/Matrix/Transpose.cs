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
        /// 矩阵转置
        /// </summary>
        public void Transpose()
        {
            Transpose(this);
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

        /// <summary>
        /// 矩阵转置
        /// </summary>
        public static T[,] Transpose(T[,] input)
        {
            T[,] result = new T[input.GetLength(1), input.GetLength(0)];

#if _DEBUG_UseMKL
            GCHandle input_GC = GCHandle.Alloc(input, GCHandleType.Pinned);
            IntPtr input_Address = input_GC.AddrOfPinnedObject();

            GCHandle result_GC = GCHandle.Alloc(result, GCHandleType.Pinned);
            IntPtr result_Address = result_GC.AddrOfPinnedObject();

            Transpose<T>(input_Address, input.GetLength(0), input.GetLength(1), result_Address);

            input_GC.Free();
            result_GC.Free();
#else
            if (input is float[,])
            {
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        result[j, i] = input[i, j];
                    }
                }
            }
            else if (input is double[,])
            {
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        result[j, i] = input[i, j];
                    }
                }
            }
            else if (input is Complex32[,] input_fc32 && result is Complex32[,] result_fc32)
            {
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        result_fc32[j, i] = Complex32.Conjugate(input_fc32[i, j]);
                    }
                }
            }
            else if (input is Complex[,] input_fc64 && result is Complex[,] result_fc64)
            {
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        result_fc64[j, i] = Complex.Conjugate(input_fc64[i, j]);
                    }
                }
            }
            else { throw new System.Exception("Data type not supported"); }
#endif
            return result;
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        public static void Transpose(Matrix<T> matrix)
        {
            #region ---- 判断 ----
            if (!matrix.IsValid) { return; }
            #endregion

#if _DEBUG_Inplace
            // yym_Debug
            // 如果使用同一块内存，需要解决二维数组的行列关系
            Transpose<T>(matrix._dataAddress, matrix.Row, matrix.Colum);
#else
            T[,] result = Transpose(matrix._dataRef);

            #region ---- 重置矩阵元素 ----

            matrix.Dispose();
            matrix.CreateMatrixBuffer(result);

            #endregion
#endif
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="colum"></param>
        /// <param name="result"></param>
        /// <param name="transOnly">false 表示对于复数是共轭转置 </param>
        private static void Transpose<T>(IntPtr matrix, int inputRow, int inputColum, bool transOnly = false)
        {
            #region ---- 判断 ----
            #endregion

            #region ---- 准备 ----
            T[] dataType = new T[1];
            MatrixTransposeChar transMode = transOnly ? MatrixTransposeChar.Transpose : MatrixTransposeChar.ConjTranspose;
            #endregion

            #region ---- 调用API ----
            if (dataType is float[])
            {
                MatrixCaller.MKL_Simatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           inputRow, inputColum,
                                           1.0f, matrix, inputColum,
                                           inputRow);
            }
            else if (dataType is double[])
            {
                MatrixCaller.MKL_Dimatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           inputRow, inputColum,
                                           1.0, matrix, inputColum,
                                           inputRow);
            }
            else if (dataType is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                MatrixCaller.MKL_Cimatcopy(MatrixLayoutChar.RowMajor, transMode,
                                           inputRow, inputColum,
                                           onefc32, matrix, inputColum,
                                           inputRow);
            }
            else if (dataType is Complex[])
            {
                Complex onefc64 = Complex.One;
                MatrixCaller.MKL_Zimatcopy(MatrixLayoutChar.RowMajor, transMode,
                                           inputRow, inputColum,
                                           onefc64, matrix, inputColum,
                                           inputRow);
            }
            else { throw new System.Exception("Data type not supported"); }
            #endregion

            return;
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="colum"></param>
        /// <param name="result"></param>
        /// <param name="transOnly">false 表示对于复数是共轭转置 </param>
        private static void Transpose<T>(IntPtr data, int row, int colum, IntPtr result, bool transOnly = false)
        {
            #region ---- 判断 ----
            #endregion

            #region ---- 准备 ----
            T[] dataType = new T[1];
            MatrixTransposeChar transMode = transOnly ? MatrixTransposeChar.Transpose : MatrixTransposeChar.ConjTranspose;
            #endregion

            #region ---- 调用API ----

            if (dataType is float[])
            {
                MatrixCaller.MKL_Somatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                   row, colum,
                                   1.0f, data, colum,
                                   result, row);

            }
            else if (dataType is double[])
            {
                MatrixCaller.MKL_Domatcopy(MatrixLayoutChar.RowMajor, MatrixTransposeChar.Transpose,
                                           row, colum,
                                           1.0, data, colum,
                                           result, row);
            }
            else if (dataType is Complex32[])
            {
                Complex32 onefc32 = Complex32.One;
                MatrixCaller.MKL_Comatcopy(MatrixLayoutChar.RowMajor, transMode,
                                           row, colum,
                                           onefc32, data, colum,
                                           result, row);
            }
            else if (dataType is Complex[])
            {
                Complex onefc64 = Complex.One;
                MatrixCaller.MKL_Zomatcopy(MatrixLayoutChar.RowMajor, transMode,
                                           row, colum,
                                           onefc64, data, colum,
                                           result, row);
            }
            else { throw new System.Exception("Data type not supported"); }

            #endregion
        }
    }
    internal partial class MatrixCaller
    {
        #region ---- Inplace Transpose (BLAS-like Extensions mkl_?imatcopy) ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Simatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  float alpha, float[,] AB, int lda,
                                                  int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Simatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  float alpha, IntPtr AB, int lda,
                                                  int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Dimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  double alpha, double[,] AB, int lda,
                                                  int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Dimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  double alpha, IntPtr AB, int lda,
                                                  int ldb);

        // alpha is a point to Complex32 cont
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Cimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  Complex32 alpha, IntPtr AB, int lda,
                                                  int ldb);

        // alpha is a point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Zimatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  Complex alpha, IntPtr AB, int lda,
                                                  int ldb);

        #endregion

        #region ---- Outplace Transpose (BLAS-like Extensions mkl_?omatcopy) ----    

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Somatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  float alpha, float[] A, int lda,
                                                  float[] B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Somatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  float alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Domatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  double alpha, double[,] A, int lda,
                                                  double[,] B, int ldb);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Domatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  double alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        // alpha is a point to Complex32 const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Comatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  Complex32 alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

        // alpha is a point to Complex const
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Zomatcopy(MatrixLayoutChar ordering, MatrixTransposeChar trans,
                                                  int rows, int cols,
                                                  Complex alpha, IntPtr A, int lda,
                                                  IntPtr B, int ldb);

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