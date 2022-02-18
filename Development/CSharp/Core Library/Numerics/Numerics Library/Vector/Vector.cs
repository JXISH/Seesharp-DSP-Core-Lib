//#define vectorMKL

using System;
using System.Numerics;
using System.Runtime.InteropServices;
////using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector<T>
    {
        private T[] _dataRef;
        /// <summary>
        /// 向量数据
        /// </summary>
        public T[] VectorArray
        {
            get
            {
                T[] result = new T[_dataRef.Length];
                Vector.ArrayCopy(_dataRef, result);
                return result;
            }
        }

        /// <summary>
        /// 向量长度
        /// </summary>
        public int Size { get { return _dataRef.Length; } }

        /// <summary>
        /// 是否非空
        /// </summary>
        public bool IsValid { get { return (_dataRef != null && _dataRef.Length > 0); } }

        /// <summary>
        /// 获取向量子集
        /// </summary>
        public T[] GetSubVector(int offset, int length)
        {
            #region 判断输入参数合法
            if (!(offset >= 0 && length > 0 && ((offset + length) <= _dataRef.Length))) { throw (new Exception("S ize is not valid.")); }
            #endregion

            T[] result = new T[length];
            Vector.ArrayCopy(_dataRef, offset, result, 0, length);
            return result;
        }

        /// <summary>
        /// 设置矩阵特定阵行
        /// </summary>
        public void SetSubVector(int offset, T[] subData)
        {
            #region 判断输入参数合法
            if (offset < 0) { throw (new Exception("Offset is not valid.")); }
            if (subData == null || subData.Length + offset > _dataRef.Length) { throw (new Exception("Sub data size is not valid.")); }
            #endregion

            Vector.ArrayCopy(subData, 0, _dataRef, offset, subData.Length);
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Vector(int size)
        {
            _dataRef = new T[size];
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Vector(T[] data)
        {
            _dataRef = new T[data.Length];
            Vector.ArrayCopy(data, _dataRef);
        }

        /// <summary>
        /// 构造
        /// </summary>
        public Vector(Vector<T> source)
        {
            this.CopyFrom(source);
        }

        public void CopyFrom(Vector<T> source)
        {
            _dataRef = new T[source._dataRef.Length];
            Vector.ArrayCopy(source._dataRef, _dataRef);
        }

        /// <summary>
        /// 零向量
        /// </summary>
        public static Vector<T> Zero(int N)
        {
            if (N <= 0) { throw (new Exception("N is not valid.")); }

            T[] result = new T[N];        

            return new Vector<T>(result);
        }

        /// <summary>
        /// 全1向量
        /// </summary>
        public static Vector<T> One(int N)
        {
            if (N <= 0) { throw (new Exception("N is not valid.")); }

            T[] result = new T[N];
            if (result is float[] result_f32)
            {
                Vector.ConstInit(N, result_f32, 1.0f);
            }
            else if (result is double[] result_f64)
            {
                Vector.ConstInit(N, result_f64, 1.0);
            }
            else if (result is Complex32[] result_fc32)
            {
                Vector.ConstInit(N, result_fc32, Complex32.One);
            }
            else if (result is Complex[] result_fc64)
            {
                Vector.ConstInit(N, result_fc64, Complex.One);
            }
            else { throw new Exception("Data type not supported"); }

            return new Vector<T>(result);
        }

        /// <summary>
        /// 单位向量
        /// </summary>
        public static Vector<T> Uint(int N, int index)
        {
            if (N <= 0) { throw (new Exception("N is not valid.")); }
            if (index < 0 || index >= N) { throw (new Exception("index is not valid.")); }

            T[] result = new T[N];
            if (result is float[] result_f32)
            {
                result_f32[index] = 1.0f;
            }
            else if (result is double[] result_f64)
            {
                result_f64[index] = 1.0;
            }
            else if (result is Complex32[] result_fc32)
            {
                result_fc32[index] = Complex32.One;
            }
            else if (result is Complex[] result_fc64)
            {
                result_fc64[index] = Complex.One;
            }
            else { throw new Exception("Data type not supported"); }

            return new Vector<T>(result);
        }
    }


    /// <summary>
    /// 数组运算
    /// </summary>
    public partial class Vector
    {

        #region---- MKL DLL Caller ----

#if dspLinux
        private const string mklDllName = @"mkl_rt.dll";
#else
        private const string mklDllName = @"jxiMKLcdecl_s.dll";
        //private const string mklDllName = @"jxiMKLcdecl_p.dll";
#endif

        private const CallingConvention MKLCallingConvertion = CallingConvention.Cdecl;

        #region  ---- Scale:  x = a * x ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_sscal(int n, float a, float[] x, int incx);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_dscal(int n, double a, double[] x, int incx);

        // comlex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_cscal(int n, Complex32 a, IntPtr x, int incx);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_csscal(int n, float a, IntPtr x, int incx);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_zscal(int n, Complex a, IntPtr x, int incx);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_zdscal(int n, double a, IntPtr x, int incx);

        #endregion

        #region  ---- AddMulti:  y = a * x + y  ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_saxpy(int n, float a, float[] x, int incx, float[] y, int incy);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_daxpy(int n, double a, double[] x, int incx, double[] y, int incy);

        // comlex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_caxpy(int n, Complex32 a, IntPtr x, int incx, IntPtr y, int incy);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void cblas_zaxpy(int n, Complex a, IntPtr x, int incx, IntPtr y, int incy);

        #endregion

        #region ---- Add : y = a + b -----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsAdd(int n, float[] a, float[] b, float[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsAdd(int n, IntPtr a, IntPtr b, IntPtr y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdAdd(int n, double[] a, double[] b, double[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdAdd(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcAdd(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzAdd(int n, IntPtr a, IntPtr b, IntPtr y);

        #endregion

        #region ---- Sub : y = a - b -----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsSub(int n, float[] a, float[] b, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdSub(int n, double[] a, double[] b, double[] y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcSub(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzSub(int n, IntPtr a, IntPtr b, IntPtr y);

        #endregion

        #region ---- Multi : y = a * b -----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsMul(int n, float[] a, float[] b, float[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsMul(int n, IntPtr a, IntPtr b, IntPtr y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdMul(int n, double[] a, double[] b, double[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdMul(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcMul(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzMul(int n, IntPtr a, IntPtr b, IntPtr y);

        #endregion

        #region ---- Division : y = a / b -----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsDiv(int n, float[] a, float[] b, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdDiv(int n, double[] a, double[] b, double[] y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcDiv(int n, IntPtr a, IntPtr b, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzDiv(int n, IntPtr a, IntPtr b, IntPtr y);

        #endregion

        #region---- Square: y = a**2 ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsSqr(int n, float[] a, float[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsSqr(int n, IntPtr a, IntPtr y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdSqr(int n, double[] a, double[] y);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdSqr(int n, IntPtr a, IntPtr y);


        #endregion

        #region---- Abs: y = abs(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsAbs(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdAbs(int n, double[] a, double[] y);

        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vcAbs(int n, IntPtr a, float[] y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vzAbs(int n, IntPtr a, double[] y);

        #endregion

        #region---- Conj: y = conj(a) ----
        // complex32
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vcConj(int n, IntPtr a, IntPtr y);

        // complex
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vzConj(int n, IntPtr a, IntPtr y);

        #endregion

        #region---- Max: y = max(a,b) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsFmax(int n, float[] a, float[] b, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdFmax(int n, double[] a, double[] b, double[] y);

        #endregion

        #region---- Max: y = min(a,b) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsFmin(int n, float[] a, float[] b, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdFmin(int n, double[] a, double[] b, double[] y);

        #endregion

        #region----  root: y = sqrt(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vsSqrt(int n, float[] a, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int vdSqrt(int n, double[] a, double[] y);

        #endregion

        #region----  sum: y = sigma(xi) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern float cblas_sasum(int n, float[] x, int incx);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern double cblas_dasum(int n, double[] x, int incx);

        #endregion

        #region----  sincose: y = sin(a),z= cos(a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsSinCos(int n, float[] a, float[] y, float[] z);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdSinCos(int n, double[] a, double[] y, double[] z);
        #endregion

        #region----  CIS: y = exp(a*j) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcCIS(int n, float[] a, IntPtr y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzCIS(int n, double[] a, IntPtr y);

        #endregion

        #region----  Atan2: y = getTheta (z = a + bj) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vsAtan2(int n, float[] a, float[] b, float[] y);

        // double
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vdAtan2(int n, double[] a, double[] b, double[] y);

        #endregion

        #region----  Arg: y = Arg (a) ----

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vcArg(int n, IntPtr a, float[] y);

        // float
        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern void vzArg(int n, IntPtr a, double[] y);

        #endregion

        #region---- Misc ----

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern uint vmlSetMode(uint mode);

        [DllImport(mklDllName, CallingConvention = MKLCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern uint vmlGetMode();

        private const uint VML_LA = 0x00000001;
        private const uint VML_HA = 0x00000002;
        private const uint VML_EP = 0x00000003;

        private const uint VML_FTZDAZ_ON = 0x00280000;
        private const uint VML_FTZDAZ_OFF = 0x00140000;

        private const uint VML_ERRMODE_IGNORE = 0x00000100;
        private const uint VML_ERRMODE_ERRNO = 0x00000200;
        private const uint VML_ERRMODE_STDERR = 0x00000400;
        private const uint VML_ERRMODE_EXCEPT = 0x00000800;
        private const uint VML_ERRMODE_CALLBACK = 0x00001000;
        private const uint VML_ERRMODE_NOERR = 0x00002000;
        private const uint VML_ERRMODE_DEFAULT = VML_ERRMODE_ERRNO | VML_ERRMODE_CALLBACK | VML_ERRMODE_EXCEPT;

        private const uint VML_ACCURACY_MASK = 0x0000000F;
        private const uint VML_FPUMODE_MASK = 0x000000F0;
        private const uint VML_ERRMODE_MASK = 0x0000FF00;
        private const uint VML_ERRMODE_STDHANDLER_MASK = 0x00002F00;
        private const uint VML_ERRMODE_CALLBACK_MASK = 0x00001000;
        private const uint VML_NUM_THREADS_OMP_MASK = 0x00030000;
        private const uint VML_FTZDAZ_MASK = 0x003C0000;
        private const uint VML_TRAP_EXCEPTIONS_MASK = 0x0F000000;

        #endregion
        #endregion


        #region---- IPP DLL Caller ----

#if dspLinux
        private const string ippDllName = @"ipps.dll";
#else
        private const string ippDllName = @"jxiIPP.dll";
#endif 

        private const CallingConvention ippCallingConvertion = CallingConvention.Winapi;


        #region  ---- AddMulti:  pSrcDst = val * pSrc + pSrcDst;   pSrcDst = pSrc1 * pSrc2 + pSrcDst ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProductC_32f(float[] pSrc, float val, float[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProduct_32f(float[] pSrc1, float[] pSrc2, float[] pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProductC_64f(double[] pSrc, double val, double[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProduct_64f(double[] pSrc1, double[] pSrc2, double[] pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProduct_32fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsAddProduct_64fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pSrcDst, int len);

        #endregion

        #region ---- SampleUp : pDst = pSrc ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleUp_32f([In] float[] pSrc, int srcLen, [Out] float[] pDst, ref int pDstLen, int factor, ref int pPhase);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleUp_64f([In] double[] pSrc, int srcLen, [Out] double[] pDst, ref int pDstLen, int factor, ref int pPhase);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleUp_32fc(IntPtr pSrc, int srcLen, IntPtr pDst, ref int pDstLen, int factor, ref int pPhase);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleUp_64fc(IntPtr pSrc, int srcLen, IntPtr pDst, ref int pDstLen, int factor, ref int pPhase);

        #endregion

        #region ---- SampleDown : pDst = pSrc ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleDown_32f([In] float[] pSrc, int srcLen, [Out] float[] pDst, ref int pDstLen, int factor, ref int pPhase);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleDown_64f([In] double[] pSrc, int srcLen, [Out] double[] pDst, ref int pDstLen, int factor, ref int pPhase);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleDown_32fc(IntPtr pSrc, int srcLen, IntPtr pDst, ref int pDstLen, int factor, ref int pPhase);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSampleDown_64fc(IntPtr pSrc, int srcLen, IntPtr pDst, ref int pDstLen, int factor, ref int pPhase);

        #endregion


        private enum IppRoundMode
        {
            ippRndZero,
            ippRndNear,
            ippRndFinancial,
        }

        private enum IppHintAlgorithm
        {
            ippAlgHintNone,
            ippAlgHintFast,
            ippAlgHintAccurate,
        }
        #endregion
    }
}
