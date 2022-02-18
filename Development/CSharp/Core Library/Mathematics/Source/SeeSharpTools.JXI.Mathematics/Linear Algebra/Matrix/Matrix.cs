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
        private T[] _dataRef;        
        /// <summary>
        /// 矩阵数据
        /// </summary>
        public T[,] MatrixArray 
        { 
            get 
            {
                T[,] result = new T[_rowSize, _columSize];
                GCHandle result_GC = GCHandle.Alloc(result, GCHandleType.Pinned);
                IntPtr result_address = result_GC.AddrOfPinnedObject();

                Vector.ArrayCopy(_dataRef, result_address);

                result_GC.Free();

                return result;
            } 
        }

        private int _rowSize;
        /// <summary>
        /// 行数
        /// </summary>
        public int Row { get { return _rowSize; } }

        private int _columSize;
        /// <summary>
        /// 列数
        /// </summary>
        public int Colum { get { return _columSize; } }

        private bool _isSymmetric;
        /// <summary>
        /// 是否是(共轭)对称阵
        /// </summary>
        public bool IsSymmetric 
        {
            get { return (_isSymmetric && (_rowSize == _columSize)); }
            set { _isSymmetric = value; }
        }

        /// <summary>
        /// 是否是方阵
        /// </summary>
        public bool IsSquare { get { return (_rowSize == _columSize); } }

        /// <summary>
        /// 是否非空
        /// </summary>
        public bool IsValid { get { return (_dataRef != null && _dataRef.Length > 0 && _rowSize > 0 && _columSize > 0); } }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(int row, int colum)
        {
            _dataRef = new T[row * colum];
            _rowSize = row;
            _columSize = colum;
            _isSymmetric = false;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(T[,] data, bool symmetric = false)
        {
            _dataRef = new T[data.GetLength(0) * data.GetLength(1)];

            GCHandle data_GC = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();

            Vector.ArrayCopy(data_address, _dataRef);

            data_GC.Free();

            _rowSize = data.GetLength(0);
            _columSize = data.GetLength(1);
            _isSymmetric = symmetric;
            return;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Matrix(T[][] data, bool symmetric = false)
        {
            int tempColum = data[0].Length;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Length != tempColum) { throw (new ArgumentException("Invalid data size.")); }
            }

            int elementSize = 0;
            if (data is short[][]) { elementSize = sizeof(short); }
            else if (data is int[][]) { elementSize = sizeof(int); }
            else if (data is float[][]) { elementSize = sizeof(float); }
            else if (data is double[][]) { elementSize = sizeof(double); }
            else if (data is Complex32[][]) { elementSize = sizeof(float) *2; }
            else if (data is Complex[][]) { elementSize = sizeof(double)*2; }
            else { throw new ArgumentException("Data type not supported"); }

            _dataRef = new T[data.Length * tempColum];

            GCHandle dataRef_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr dataRef_address = dataRef_GC.AddrOfPinnedObject();

            for (int i = 0; i < data.Length; i++)
            {                
                Vector.ArrayCopy(data[i], dataRef_address);
                dataRef_address += elementSize * tempColum;
            }

            dataRef_GC.Free();

            _rowSize = data.Length;
            _columSize = tempColum;
            _isSymmetric = symmetric;
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
            if (!VectorVaild(diagonal)) { throw (new ArgumentException("Input data is not valid.")); }

            int N = diagonal.Length;
            _dataRef = new T[N * N];

            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();

            GCHandle diagonal_GC = GCHandle.Alloc(diagonal, GCHandleType.Pinned);
            IntPtr diagonal_address = diagonal_GC.AddrOfPinnedObject();

            MatrixCaller.cblas_scopy(N, diagonal_address, 1, data_address, N + 1);

            _rowSize = N;
            _columSize = N;
            _isSymmetric = true;

            data_GC.Free();
            diagonal_GC.Free();

            return;
        }

        public void CopyFrom(Matrix<T> source)
        {
            _dataRef = new T[source._dataRef.Length];
            Vector.ArrayCopy(source._dataRef, _dataRef);

            _rowSize = source._rowSize;
            _columSize = source._columSize;
            _isSymmetric = source._isSymmetric;
        }

        /// <summary>
        /// 获取矩阵特定阵行
        /// </summary>
        public T[] GetRow(int row)
        {
            #region 判断输入参数合法
            if (row < 0 || row >= _rowSize) { throw (new ArgumentException("Row size is not valid.")); }
            #endregion

            #region 准备
            T[] result = new T[_columSize];            
            GCHandle resultData_GC = GCHandle.Alloc(result, GCHandleType.Pinned);
            IntPtr resultData_address = resultData_GC.AddrOfPinnedObject();            

            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();
            #endregion

            #region 调用API
            if (_dataRef is float[])
            {
                data_address += row * _columSize * sizeof(float);
                MatrixCaller.cblas_scopy(_columSize, data_address, 1, resultData_address, 1);
            }
            else if (_dataRef is double[])
            {
                data_address += row * _columSize * sizeof(double);
                MatrixCaller.cblas_dcopy(_columSize, data_address, 1, resultData_address, 1);
            }
            else if (_dataRef is Complex32[] )
            {
                data_address += row * _columSize * sizeof(float) * 2;
                MatrixCaller.cblas_ccopy(_columSize, data_address, 1, resultData_address, 1);
            }
            else if (_dataRef is Complex[])
            {
                data_address += row * _columSize * sizeof(double) * 2;
                MatrixCaller.cblas_zcopy(_columSize, data_address, 1, resultData_address, 1);
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region 收尾
            resultData_GC.Free();
            data_GC.Free();
            #endregion

            return result;
        }

        /// <summary>
        /// 获取矩阵特定列行
        /// </summary>
        public T[] GetColum(int colum)
        {
            #region 判断输入参数合法
            if (colum < 0 || colum >= _columSize) { throw (new ArgumentException("Row size is not valid.")); }
            #endregion

            #region 准备
            T[] result = new T[_rowSize];
            GCHandle resultData_GC = GCHandle.Alloc(result, GCHandleType.Pinned);
            IntPtr resultData_address = resultData_GC.AddrOfPinnedObject();

            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();
            #endregion

            #region 调用API
            if (_dataRef is float[])
            {
                data_address += colum * sizeof(float);
                MatrixCaller.cblas_scopy(_rowSize, data_address, _columSize, resultData_address, 1);
            }
            else if (_dataRef is double[])
            {
                data_address += colum * sizeof(double);
                MatrixCaller.cblas_dcopy(_rowSize, data_address, _columSize, resultData_address, 1);
            }
            else if (_dataRef is Complex32[])
            {
                data_address += colum * sizeof(float) * 2;
                MatrixCaller.cblas_ccopy(_rowSize, data_address, _columSize, resultData_address, 1);
            }
            else if (_dataRef is Complex[])
            {
                data_address += colum * sizeof(double) * 2;
                MatrixCaller.cblas_zcopy(_rowSize, data_address, _columSize, resultData_address, 1);
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region 收尾
            resultData_GC.Free();
            data_GC.Free();
            #endregion

            return result;
        }

        /// <summary>
        /// 设置矩阵特定阵行
        /// </summary>
        public void SetRow(int row,T[] rowdata)
        {
            #region 判断输入参数合法
            if (row < 0 || row >= _rowSize) { throw (new ArgumentException("Row size is not valid.")); }
            if (rowdata.Length != _columSize) { throw (new ArgumentException("Row data size is not valid.")); }
            #endregion

            #region 准备
            GCHandle input_GC = GCHandle.Alloc(rowdata, GCHandleType.Pinned);
            IntPtr input_address = input_GC.AddrOfPinnedObject();

            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();
            #endregion

            #region 调用API
            if (_dataRef is float[])
            {
                data_address += row * _columSize * sizeof(float);
                MatrixCaller.cblas_scopy(_columSize, input_address, 1, data_address, 1);
            }
            else if (_dataRef is double[])
            {
                data_address += row * _columSize * sizeof(double);
                MatrixCaller.cblas_dcopy(_columSize, input_address, 1, data_address, 1);
            }
            else if (_dataRef is Complex32[])
            {
                data_address += row * _columSize * sizeof(float) * 2;
                MatrixCaller.cblas_ccopy(_columSize, input_address, 1, data_address, 1);
            }
            else if (_dataRef is Complex[])
            {
                data_address += row * _columSize * sizeof(double) * 2;
                MatrixCaller.cblas_zcopy(_columSize, input_address, 1, data_address, 1);
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region 收尾
            input_GC.Free();
            data_GC.Free();
            #endregion
        }

        /// <summary>
        /// 设置矩阵特定列行
        /// </summary>
        public void SetColum(int colum, T[] columdata)
        {
            #region 判断输入参数合法
            if (colum < 0 || colum >= _columSize) { throw (new ArgumentException("Row size is not valid.")); }
            if (columdata.Length != _rowSize) { throw (new ArgumentException("Row data size is not valid.")); }
            #endregion

            #region 准备
            GCHandle input_GC = GCHandle.Alloc(columdata, GCHandleType.Pinned);
            IntPtr input_address = input_GC.AddrOfPinnedObject();

            GCHandle data_GC = GCHandle.Alloc(_dataRef, GCHandleType.Pinned);
            IntPtr data_address = data_GC.AddrOfPinnedObject();
            #endregion

            #region 调用API
            if (_dataRef is float[])
            {
                data_address += colum * sizeof(float);
                MatrixCaller.cblas_scopy(_rowSize, input_address, _columSize, data_address, 1);
            }
            else if (_dataRef is double[])
            {
                data_address += colum * sizeof(double);
                MatrixCaller.cblas_dcopy(_rowSize, input_address, _columSize, data_address, 1);
            }
            else if (_dataRef is Complex32[])
            {
                data_address += colum * sizeof(float) * 2;
                MatrixCaller.cblas_ccopy(_rowSize, input_address, _columSize, data_address, 1);
            }
            else if (_dataRef is Complex[])
            {
                data_address += colum * sizeof(double) * 2;
                MatrixCaller.cblas_zcopy(_rowSize, input_address, _columSize, data_address, 1);
            }
            else { throw new ArgumentException("Data type not supported"); }
            #endregion

            #region 收尾
            input_GC.Free();
            data_GC.Free();
            #endregion
        }

        private static bool VectorVaild(T[] vector)
        {
            return (vector.Length > 0 && vector != null);
        }

        /// <summary>
        /// 单位阵
        /// </summary>
        public static Matrix<T> Identity(int N)
        {
            if (N <= 0) { throw (new ArgumentException("N is not valid.")); }

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
            else { throw new ArgumentException("Data type not supported"); }

            return new Matrix<T>(diagonal);
        }
    }

    internal partial class MatrixCaller
    {
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
