using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Numerics;

using SeeSharpTools.JXI.Numerics;

namespace SeeSharpTools.JXI.Mathematics.LinearAlgebra.Matrix
{
    public partial class Matrix<T> : IDisposable
    {
        private const int ALIGNMENT = 64;

        /// <summary>
        /// 矩阵占用内存
        /// </summary>
        private T[,] _dataRef = null;
        private IntPtr _dataAddress = IntPtr.Zero;
        private GCHandle _dataGC;
        /// <summary>
        /// 矩阵占用内存大小 (元素个数)
        /// </summary>
        protected int RefSize { get { return _dataRef.Length; } }

        /// <summary>
        /// 矩阵数据
        /// </summary>
        public T[,] MatrixArray
        {
            get
            {
                T[,] result = new T[Row, Colum];
                Vector.ArrayCopy(_dataAddress, result);
                return result;
            }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get { return _dataRef.GetLength(0); } }

        /// <summary>
        /// 列数
        /// </summary>
        public int Colum { get { return _dataRef.GetLength(1); } }

        private bool _isSymmetric;
        /// <summary>
        /// 是否是(共轭)对称阵
        /// </summary>
        public bool IsSymmetric { get { return _isSymmetric; } }

        /// <summary>
        /// 是否是方阵
        /// </summary>
        public bool IsSquare { get { return (Row == Colum); } }

        /// <summary>
        /// 是否非空
        /// </summary>
        public bool IsValid { get { return (_dataRef != null); } }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Matrix()
        {
            Dispose();
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public void Dispose()
        {
            // 释放内存
            if (_dataRef != null)
            {
                _dataGC.Free();
                _dataAddress = IntPtr.Zero;

                _dataRef = null;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(int row, int colum)
        {
            #region 判断输入参数合法
            if (row <= 0 || colum <= 0) { throw (new System.Exception("Matrix size is not valid.")); }
            #endregion
            CreateMatrixBuffer(row, colum);
            _isSymmetric = false;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(T[,] data, bool symmetric = false)
        {
            #region 判断输入参数合法
            if (data.GetLength(0) <= 0 || data.GetLength(1) <= 0) { throw (new System.Exception("Matrix size is not valid.")); }
            #endregion

            // 初始化内存
            CreateMatrixBuffer(data.GetLength(0), data.GetLength(1));
            // 复制数组
            Vector.ArrayCopy(data, _dataRef);

            _isSymmetric = symmetric ? symmetric : CheckSymmetric(_dataRef);
            return;
        }

        private static bool CheckSymmetric(T[,] data)
        {
            int N = data.GetLength(0);
            if (N != data.GetLength(1)) { return false; }

            if (data is float[,] data_f32)
            {
                return CheckSymmetricReal(data_f32);
            }
            else if (data is double[,] data_f64)
            {
                return CheckSymmetricReal(data_f64);
            }
            else if (data is Complex32[,] data_fc32)
            {
                return CheckHermitianComplex(data_fc32);
            }
            else if (data is Complex[,] data_fc64)
            {
                return CheckHermitianComplex(data_fc64);
            }
            else
            { throw new System.Exception("Data type not supported"); }
        }

        private static bool CheckSymmetricReal(double[,] data)
        {
            int N = data.GetLength(0);
            if (N != data.GetLength(1)) { return false; }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (data[i, j] != data[j, i]) { return false; }
                }
            }
            return true;
        }

        private static bool CheckSymmetricReal(float[,] data)
        {
            int N = data.GetLength(0);
            if (N != data.GetLength(1)) { return false; }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (data[i, j] != data[j, i]) { return false; }
                }
            }
            return true;
        }

        private static bool CheckHermitianComplex(Complex[,] data)
        {
            int N = data.GetLength(0);
            if (N != data.GetLength(1)) { return false; }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (data[i, j] != Complex.Conjugate(data[j, i])) { return false; }
                }
            }
            return true;
        }

        private static bool CheckHermitianComplex(Complex32[,] data)
        {
            int N = data.GetLength(0);
            if (N != data.GetLength(1)) { return false; }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (data[i, j] != Complex32.Conjugate(data[j, i])) { return false; }
                }
            }
            return true;
        }


        /// <summary>
        /// 构造, 一组列向量
        /// </summary>
        public Matrix(T[][] data, bool symmetric = false)
        {
            #region 判断输入参数合法
            int colum = data.Length;
            int row = data[0].Length;

            if (row <= 0 || colum <= 0) { throw (new System.Exception("Matrix size is not valid.")); }

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length != row) { throw (new System.Exception("Invalid data size.")); }
            }

            #endregion

            // 初始化内存
            CreateMatrixBuffer(row, colum);

            // 复制数组
            int size = ElementSize<T>();
            IntPtr tempAddress = _dataAddress;

            for (int i = 0; i < data.Length; i++)
            {
                MatrixCaller.ArrayCopy(data[i], tempAddress, colum);
                tempAddress += size;
            }

            _isSymmetric = symmetric ? symmetric : CheckSymmetric(_dataRef);
            return;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(Matrix<T> source)
        {
            this.CopyFrom(source);
        }

        /// <summary>
        /// 构造对角阵
        /// </summary>
        public Matrix(T[] diagonal)
        {
            if (!VectorVaild(diagonal)) { throw (new System.Exception("Input data is not valid.")); }

            // 初始化内存
            int N = diagonal.Length;
            CreateMatrixBuffer(N, N);

            // 复制数组
            MatrixCaller.ArrayCopy(diagonal, _dataAddress, N + 1);
            _isSymmetric = true;

            return;
        }

        /// <summary>
        /// 复制矩阵
        /// </summary>
        public void CopyFrom(Matrix<T> source)
        {
            // 初始化内存
            CreateMatrixBuffer(source.Row, source.Colum);

            // 复制数组
            Vector.ArrayCopy<T>(source._dataRef, _dataRef);

            _isSymmetric = source._isSymmetric;
        }

        /// <summary>
        /// 获取矩阵特定阵行
        /// </summary>
        public T[] GetRow(int row)
        {
            #region 判断输入参数合法
            if (row < 0 || row >= Row) { throw (new System.Exception("Row size is not valid.")); }
            #endregion

            // 创建返回数组
            T[] result = new T[Colum];
            // 返回数据起始位置
            IntPtr tempAddress = _dataAddress + row * Colum;
            // 复制数据
            Vector.ArrayCopy(tempAddress, result);

            return result;
        }

        /// <summary>
        /// 获取矩阵特定列行
        /// </summary>
        public T[] GetColum(int colum)
        {
            #region 判断输入参数合法
            if (colum < 0 || colum >= Colum) { throw (new System.Exception("Row size is not valid.")); }
            #endregion

            // 创建返回数组
            T[] result = new T[Row];
            // 返回数据起始位置
            IntPtr tempAddress = _dataAddress + colum;
            // 复制数据
            MatrixCaller.ArrayCopy(tempAddress, result, Colum);

            return result;
        }

        /// <summary>
        /// 设置矩阵特定阵行
        /// </summary>
        public void SetRow(int row, T[] rowdata)
        {
            #region 判断输入参数合法
            if (row < 0 || row >= Row) { throw (new System.Exception("Row size is not valid.")); }
            if (rowdata.Length != Colum) { throw (new System.Exception("Row data size is not valid.")); }
            #endregion

            IntPtr tempAddress = _dataAddress + row * Colum;
            Vector.ArrayCopy(rowdata, tempAddress);
        }

        /// <summary>
        /// 设置矩阵特定列行
        /// </summary>
        public void SetColum(int colum, T[] columdata)
        {
            #region 判断输入参数合法
            if (colum < 0 || colum >= Colum) { throw (new System.Exception("Row size is not valid.")); }
            if (columdata.Length != Row) { throw (new System.Exception("Row data size is not valid.")); }
            #endregion

            IntPtr tempAddress = _dataAddress + colum;
            MatrixCaller.ArrayCopy(columdata, tempAddress, Colum);
        }

        /// <summary>
        /// 单位阵
        /// </summary>
        public static Matrix<T> Identity(int N)
        {
            if (N <= 0) { throw (new System.Exception("N is not valid.")); }

            T[] diagonal = new T[N];
            if (diagonal is float[] diagonal_f32)
            {
                Vector.ConstInit(N, diagonal_f32, 1.0f);
            }
            else if (diagonal is double[] diagonal_f64)
            {
                Vector.ConstInit(N, diagonal_f64, 1.0);
            }
            else if (diagonal is Complex32[] diagonal_fc32)
            {
                Vector.ConstInit(N, diagonal_fc32, Complex32.One);
            }
            else if (diagonal is Complex[] diagonal_fc64)
            {
                Vector.ConstInit(N, diagonal_fc64, Complex.One);
            }
            else { throw new System.Exception("Data type not supported"); }

            return new Matrix<T>(diagonal);
        }

        private void CreateMatrixBuffer(int row, int colum)
        {
            // 创建临时变量用于初始化内存
            _dataRef = new T[row, colum];

            _dataGC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            _dataAddress = _dataGC.AddrOfPinnedObject();
        }

        private void CreateMatrixBuffer(T[,] data)
        {
            _dataRef = data;
            _dataGC = GCHandle.Alloc(data, GCHandleType.Pinned);
            _dataAddress = _dataGC.AddrOfPinnedObject();
        }

        private static bool VectorVaild(T[] vector)
        {
            return (vector.Length > 0 && vector != null);
        }

        private static int ElementSize<T>()
        {
            T[] dataType = new T[1];
            if (dataType is float[]) { return sizeof(float); }
            else if (dataType is double[]) { return sizeof(double); }
            else if (dataType is Complex32[]) { return sizeof(float) * 2; }
            else if (dataType is Complex[]) { return sizeof(double) * 2; }
            else { throw new System.Exception("Invalid data type."); }
        }
    }

    internal partial class MatrixCaller
    {
        #region---- Array Copy ----

        public static void ArrayCopy<T>(T[] source, int sourceInterval, T[] destination, int destinationInterval, int length)
        {
            //if (length <= 0) { length = Math.Min(source.Length / sourceInterval, destination.Length / destinationInterval); }
            //else { length = Math.Min(length, Math.Min(source.Length / sourceInterval, destination.Length / destinationInterval)); }

            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            if (destination is float[])
            {
                cblas_scopy(length, source_address, sourceInterval, destination_address, destinationInterval);
            }
            else if (destination is double[])
            {
                cblas_dcopy(length, source_address, sourceInterval, destination_address, destinationInterval);
            }
            else if (destination is Complex32[])
            {
                cblas_ccopy(length, source_address, sourceInterval, destination_address, destinationInterval);
            }
            else if (destination is Complex[])
            {
                cblas_zcopy(length, source_address, sourceInterval, destination_address, destinationInterval);
            }
            else { throw new System.Exception("Data type not supported"); }

            source_GC.Free();
            destination_GC.Free();
        }

        public static void ArrayCopy<T>(IntPtr source, T[] destination, int sourceInterval)
        {
            GCHandle destination_GC = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destination_address = destination_GC.AddrOfPinnedObject();

            int size = destination.Length;

            if (destination is float[])
            {
                cblas_scopy(size, source, sourceInterval, destination_address, 1);
            }
            else if (destination is double[])
            {
                cblas_dcopy(size, source, sourceInterval, destination_address, 1);
            }
            else if (destination is Complex32[])
            {
                cblas_ccopy(size, source, sourceInterval, destination_address, 1);
            }
            else if (destination is Complex[])
            {
                cblas_zcopy(size, source, sourceInterval, destination_address, 1);
            }
            else { throw new System.Exception("Data type not supported"); }

            destination_GC.Free();
        }

        public static void ArrayCopy<T>(T[] source, IntPtr destination, int destinationInterval)
        {
            GCHandle source_GC = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr source_address = source_GC.AddrOfPinnedObject();

            int size = source.Length;

            if (source is float[])
            {
                cblas_scopy(size, source_address, 1, destination, destinationInterval);
            }
            else if (source is double[])
            {
                cblas_dcopy(size, source_address, 1, destination, destinationInterval);
            }
            else if (source is Complex32[])
            {
                cblas_ccopy(size, source_address, 1, destination, destinationInterval);
            }
            else if (source is Complex[])
            {
                cblas_zcopy(size, source_address, 1, destination, destinationInterval);
            }
            else { throw new System.Exception("Data type not supported"); }

            source_GC.Free();
        }

        #endregion

        #region---- MKL DLL Caller ----

#if dspLinux
        private const string mklDllName = @"mkl_rt.dll";
#else
        private const string mklDllName = @"jxiMKLcdecl_s.dll";
        //private const string mklDllName = @"jxiMKLcdecl_p.dll";
#endif
        private const CallingConvention MKLCallingConvertion = CallingConvention.Cdecl;
        #endregion

        #region ---- Copy  (BLAS Level 1 -> cblas_?copy) ----
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_scopy(int n, float[] x, int incx, float[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_scopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_dcopy(int n, double[] x, int incx, double[] y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_dcopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_ccopy(int n, IntPtr x, int incx, IntPtr y, int incy);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern int cblas_zcopy(int n, IntPtr x, int incx, IntPtr y, int incy);
        #endregion

        #region ---- Memory Management  (Support Functions -> Memory Management) ---- 

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_free(IntPtr a_ptr);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr MKL_malloc(int alloc_size, int alignment);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr MKL_calloc(int num, int size, int alignment);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern IntPtr MKL_realloc(IntPtr ptr, int size);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        internal static extern void MKL_Free_Buffers();

        #endregion
    }

    #region ---- Enum ---
    internal enum MatrixLayoutEnum
    {
        RowMajor = 101, /* row-major arrays */
        ColMajor = 102, /* column-major arrays */
    }

    internal enum MatrixLayoutChar : byte
    {
        RowMajor = (byte)'R',
        ColMajor = (byte)'C',
    }

    internal enum MatrixTransposeEnum
    {
        NoTranspose = 111,     /* trans='N' */
        Transpose = 112,       /* trans='T' */
        ConjTranspose = 113,   /* trans='C' */
    };

    internal enum MatrixTransposeChar : byte
    {
        NoTranspose = (byte)'N',
        Transpose = (byte)'T',
        ConjTranspose = (byte)'C',
        ConjugatedOnly = (byte)'R',
    }

    internal enum MatrixTriangularEnum
    {
        UpTriangular = 121,        /* uplo ='U' */
        LowTriangular = 122,         /* uplo ='L' */
    };

    internal enum MatrixTriangularChar : byte
    {
        LowTriangular = (byte)'L',
        UpTriangular = (byte)'U',
    }

    internal enum CBLAS_DIAG
    {
        CblasNonUnit = 131,      /* diag ='N' */
        CblasUnit = 132,        /* diag ='U' */
    };

    internal enum CBLAS_SIDE
    {
        CblasLeft = 141,         /* side ='L' */
        CblasRight = 142,        /* side ='R' */
    };

    internal enum JobCompute : byte
    {
        NotCompute = (byte)'N',
        Compute = (byte)'V',
    }

    #endregion
}
