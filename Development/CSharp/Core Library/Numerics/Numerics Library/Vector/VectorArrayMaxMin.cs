using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Templete max ----
        /// <summary>
        ///  获取两个数组对应 index 上较大的那个数据 
        ///  inout = max (inout, b)
        /// </summary>
        public static void ArrayMax<T>(T[] inout, T[] b)
        {
            if (inout is short[] inout_i16 && b is short[] b_i16)
            {
                ArrayMax(inout_i16, b_i16);
            }
            else if (inout is int[] inout_i32 && b is int[] b_i32)
            {
                ArrayMax(inout_i32, b_i32);
            }
            else if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArrayMax(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArrayMax(inout_f64, b_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        ///  获取两个数组对应 index 上较大的那个数据 
        ///  output = max (a,b)
        /// </summary>
        public static void ArrayMax<T>(T[] a, T[] b, T[] output)
        {
            if (a is short[] a_i16 && b is short[] b_i16 && output is short[] output_i16)
            {
                ArrayMax(a_i16, b_i16, output_i16);
            }
            else if (a is int[] a_i32 && b is int[] b_i32 && output is int[] output_i32)
            {
                ArrayMax(a_i32, b_i32, output_i32);
            }
            else if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                ArrayMax(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArrayMax(a_f64, b_f64, output_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        ///  获取两个数组对应 index 上较大的那个数据 
        ///  return = max (a,b)
        /// </summary>
        public static T[] GetArrayMax<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArrayMax(a, b, output);
            return output;
        }
        #endregion

        #region ---- max ----

        #region ---- I16 ----
        /// <summary>
        ///  获取两个 I16 数组对应 index 上较大的那个数据 
        ///  inout = max (inout, b)
        /// </summary>
        public static void ArrayMax(short[] inout, short[] b)
        {
            ippsMaxEvery_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 I16 数组对应 index 上较大的那个数据 
        ///  output = max (a,b)
        /// </summary>
        public static void ArrayMax(short[] a, short[] b, short[] output)
        {
            Vector.ArrayCopy(b, output);
            ippsMaxEvery_16s_I(a, output, output.Length);
        }

        /// <summary>
        ///  获取两个 I16 数组对应 index 上较大的那个数据 
        ///  return = max (a,b)
        /// </summary>
        public static short[] GetArrayMax(short[] a, short[] b)
        {
            short[] output = new short[a.Length];
            ArrayMax(a, b, output);
            return output;
        }
        #endregion

        #region ---- I32 ----
        /// <summary>
        ///  获取两个 I32 数组对应 index 上较大的那个数据 
        ///  inout = max (inout, b)
        /// </summary>
        public static void ArrayMax(int[] a, int[] b)
        {
            ippsMaxEvery_32s_I(b, a, a.Length);
        }

        /// <summary>
        ///  获取两个 I32 数组对应 index 上较大的那个数据 
        ///  output = max (a,b)
        /// </summary>
        public static void ArrayMax(int[] a, int[] b, int[] output)
        {
            Vector.ArrayCopy(b, output);
            ippsMaxEvery_32s_I(a, output, output.Length);
        }

        /// <summary>
        ///  获取两个 I32 数组对应 index 上较大的那个数据 
        ///  return = max (a,b)
        /// </summary>
        public static int[] GetArrayMax(int[] a, int[] b)
        {
            int[] output = new int[a.Length];
            ArrayMax(a, b, output);
            return output;
        }
        #endregion

        #region ---- double ----
        /// <summary>
        ///  获取两个 double 数组对应 index 上较大的那个数据 
        ///  inout = max (inout, b)
        /// </summary>
        public static void ArrayMax(double[] inout, double[] b)
        {
            ippsMaxEvery_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 double 数组对应 index 上较大的那个数据 
        ///  output = max (a,b)
        /// </summary>
        public static void ArrayMax(double[] a, double[] b, double[] output)
        {
            ippsMaxEvery_64f(a,b, output, a.Length);
        }

        /// <summary>
        ///  获取两个 double 数组对应 index 上较大的那个数据 
        ///  return = max (a,b)
        /// </summary>
        public static double[] GetArrayMax(double[] a, double[] b)
        {
            double[] output = new double[a.Length];
            ArrayMax(a, b, output);
            return output;
        }
        #endregion

        #region ---- float ----
        /// <summary>
        ///  获取两个 float 数组对应 index 上较大的那个数据 
        ///  inout = max (inout, b)
        /// </summary>
        public static void ArrayMax(float[] a, float[] b)
        {
            ippsMaxEvery_32f_I(b, a, a.Length);
        }

        /// <summary>
        ///  获取两个 float 数组对应 index 上较大的那个数据 
        ///  output = max (a,b)
        /// </summary>
        public static void ArrayMax(float[] a, float[] b, float[] output)
        {
            ippsMaxEvery_32f(a, b, output, a.Length);
        }

        /// <summary>
        ///  获取两个 float 数组对应 index 上较大的那个数据 
        ///  return = max (a,b)
        /// </summary>
        public static float[] GetArrayMax(float[] a, float[] b)
        {
            float[] output = new float[a.Length];
            ArrayMax(a, b, output);
            return output;
        }
        #endregion

        #endregion

        #region ---- Templete min ----
        /// <summary>
        ///  获取两个数组对应 index 上较小的那个数据 
        ///  inout =  min (inout ,b)
        /// </summary>
        public static void ArrayMin<T>(T[] inout, T[] b)
        {
            if (inout is short[] inout_i16 && b is short[] b_i16)
            {
                ArrayMin(inout_i16, b_i16);
            }
            else if (inout is int[] inout_i32 && b is int[] b_i32)
            {
                ArrayMin(inout_i32, b_i32);
            }
            else if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArrayMin(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArrayMin(inout_f64, b_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        ///  获取两个数组对应 index 上较小的那个数据 
        ///  output = min (a, b)
        /// </summary>
        public static void ArrayMin<T>(T[] a, T[] b, T[] output)
        {
            if (a is short[] a_i16 && b is short[] b_i16 && output is short[] output_i16)
            {
                ArrayMin(a_i16, b_i16, output_i16);
            }
            else if (a is int[] a_i32 && b is int[] b_i32 && output is int[] output_i32)
            {
                ArrayMin(a_i32, b_i32, output_i32);
            }
            else if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                ArrayMin(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArrayMin(a_f64, b_f64, output_f64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        ///  获取两个数组对应 index 上较小的那个数据 
        ///  return = min (a, b)
        /// </summary>
        public static T[] GetArrayMin<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArrayMin(a, b, output);
            return output;
        }
        #endregion

        #region ---- min ----

        #region ---- I16 ----
        /// <summary>
        ///  获取两个 I16 数组对应 index 上较小的那个数据 
        ///  inout =  min (inout ,b)
        /// </summary>
        public static void ArrayMin(short[] inout, short[] b)
        {
            ippsMinEvery_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 I16 数组对应 index 上较小的那个数据 
        ///  output = min (a, b)
        /// </summary>
        public static void ArrayMin(short[] a, short[] b, short[] output)
        {
            Vector.ArrayCopy(b, output);
            ippsMinEvery_16s_I(a, output, output.Length);
        }

        /// <summary>
        ///  获取两个 I16 数组对应 index 上较小的那个数据 
        ///  return = min (a, b)
        /// </summary>
        public static short[] GetArrayMin(short[] a, short[] b)
        {
            short[] output = new short[a.Length];
            ArrayMin(a, b, output);
            return output;
        }
        #endregion

        #region ---- I32 ----
        /// <summary>
        ///  获取两个 I32 数组对应 index 上较小的那个数据 
        ///  inout =  min (inout ,b)
        /// </summary>
        public static void ArrayMin(int[] inout, int[] b)
        {
            ippsMinEvery_32s_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 I32 数组对应 index 上较小的那个数据 
        ///  output = min (a, b)
        /// </summary>
        public static void ArrayMin(int[] a, int[] b, int[] output)
        {
            Vector.ArrayCopy(b, output);
            ippsMinEvery_32s_I(a, output, output.Length);
        }

        /// <summary>
        ///  获取两个 I32 数组对应 index 上较小的那个数据 
        ///  return = min (a, b)
        /// </summary>
        public static int[] GetArrayMin(int[] a, int[] b)
        {
            int[] output = new int[a.Length];
            ArrayMin(a, b, output);
            return output;
        }
        #endregion

        #region ---- double ----
        /// <summary>
        ///  获取两个 double 数组对应 index 上较小的那个数据 
        ///  inout =  min (inout ,b)
        /// </summary>
        public static void ArrayMin(double[] inout, double[] b)
        {
            ippsMinEvery_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 double 数组对应 index 上较小的那个数据 
        ///  output = min (a, b)
        /// </summary>
        public static void ArrayMin(double[] a, double[] b,double[] output)
        {
            ippsMinEvery_64f(a, b,output, a.Length);
        }

        /// <summary>
        ///  获取两个 double 数组对应 index 上较小的那个数据 
        ///  return = min (a, b)
        /// </summary>
        public static double[] GetArrayMin(double[] a, double[] b)
        {
            double[] output = new double[a.Length];
            ArrayMin(a, b, output);
            return output;
        }
        #endregion

        #region ---- float ----
        /// <summary>
        ///  获取两个 float 数组对应 index 上较小的那个数据 
        ///  inout = min (inout, b)
        /// </summary>
        public static void ArrayMin(float[] inout, float[] b)
        {
            ippsMinEvery_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        ///  获取两个 float 数组对应 index 上较小的那个数据 
        ///  output = min (a,b)
        /// </summary>
        public static void ArrayMin(float[] a, float[] b, float[] output)
        {
            ippsMinEvery_32f(a, b, output, a.Length);
        }

        /// <summary>
        ///  获取两个 float 数组对应 index 上较小的那个数据 
        ///  return = min (a, b)
        /// </summary>
        public static float[] GetArrayMin(float[] a, float[] b)
        {
            float[] output = new float[a.Length];
            ArrayMin(a, b, output);
            return output;
        }
        #endregion

        #endregion

        #region ---- Find Max ----

        /// <summary>
        ///  获取double 数组最大值 
        /// </summary>
        public static void ArrayFindMax(double[] a, out double max, out int maxIndex)
        {
            ippsMaxIndx_64f(a, a.Length, out max, out maxIndex);
        }

        /// <summary>
        ///  获取 double 数组最大值 
        /// </summary>
        public static double ArrayFindMax(double[] a)
        {
            double max;
            int maxIndex;
            ArrayFindMax(a, out max, out maxIndex);
            return max;
        }

        /// <summary>
        ///  获取float 数组最大值 
        /// </summary>
        public static void ArrayFindMax(float[] a, out float max, out int maxIndex)
        {
            ippsMaxIndx_32f(a, a.Length, out max, out maxIndex);
        }

        /// <summary>
        ///  获取 float 数组最大值 
        /// </summary>
        public static float ArrayFindMax(float[] a)
        {
            float max;
            int maxIndex;
            ArrayFindMax(a, out max, out maxIndex);
            return max;
        }

        /// <summary>
        ///  获取int 数组最大值 
        /// </summary>
        public static void ArrayFindMax(int[] a, out int max, out int maxIndex)
        {
            ippsMaxIndx_32s(a, a.Length, out max, out maxIndex);
        }

        /// <summary>
        ///  获取 int 数组最大值 
        /// </summary>
        public static int ArrayFindMax(int[] a)
        {
            int max;
            int maxIndex;
            ArrayFindMax(a, out max, out maxIndex);
            return max;
        }

        /// <summary>
        ///  获取short 数组最大值 
        /// </summary>
        public static void ArrayFindMax(short[] a, out short max, out int maxIndex)
        {
            ippsMaxIndx_16s(a, a.Length, out max, out maxIndex);
        }

        /// <summary>
        ///  获取 short 数组最大值 
        /// </summary>
        public static short ArrayFindMax(short[] a)
        {
            short max;
            int maxIndex;
            ArrayFindMax(a, out max, out maxIndex);
            return max;
        }

        #endregion

        #region ---- Find Min ----

        /// <summary>
        ///  获取double 数组最小值 
        /// </summary>
        public static void ArrayFindMin(double[] a, out double min, out int minIndex)
        {
            ippsMinIndx_64f(a, a.Length, out min, out minIndex);
        }

        /// <summary>
        ///  获取 double 数组最小值 
        /// </summary>
        public static double ArrayFindMin(double[] a)
        {
            double min;
            int minIndex;
            ArrayFindMin(a, out min, out minIndex);
            return min;
        }

        /// <summary>
        ///  获取float 数组最小值 
        /// </summary>
        public static void ArrayFindMin(float[] a, out float min, out int minIndex)
        {
            ippsMinIndx_32f(a, a.Length, out min, out minIndex);
        }

        /// <summary>
        ///  获取 float 数组最小值 
        /// </summary>
        public static float ArrayFindMin(float[] a)
        {
            float min;
            int minIndex;
            ArrayFindMin(a, out min, out minIndex);
            return min;
        }

        /// <summary>
        ///  获取short 数组最小值 
        /// </summary>
        public static void ArrayFindMin(short[] a, out short min, out int minIndex)
        {
            ippsMinIndx_16s(a, a.Length, out min, out minIndex);
        }

        /// <summary>
        ///  获取 short 数组最小值 
        /// </summary>
        public static short ArrayFindMin(short[] a)
        {
            short min;
            int minIndex;
            ArrayFindMin(a, out min, out minIndex);
            return min;
        }

        /// <summary>
        ///  获取int 数组最小值 
        /// </summary>
        public static void ArrayFindMin(int[] a, out int min, out int minIndex)
        {
            ippsMinIndx_32s(a, a.Length, out min, out minIndex);
        }

        /// <summary>
        ///  获取 int 数组最小值 
        /// </summary>
        public static int ArrayFindMin(int[] a)
        {
            int min;
            int minIndex;
            ArrayFindMin(a, out min, out minIndex);
            return min;
        }

        #endregion

        #region ---- Find MaxMin ----

        /// <summary>
        ///  获取double 数组最大最小值 
        /// </summary>
        public static void ArrayFindMaxMin(double[] a, out double max, out int maxIndex, out double min, out int minIndex)
        {
            ippsMinMaxIndx_64f(a, a.Length, out min, out minIndex, out max, out maxIndex);
        }

        /// <summary>
        ///  获取float 数组最大最小值 
        /// </summary>
        public static void ArrayFindMaxMin(float[] a, out float max, out int maxIndex, out float min, out int minIndex)
        {
            ippsMinMaxIndx_32f(a, a.Length, out min, out minIndex, out max, out maxIndex);
        }

        /// <summary>
        ///  获取int 数组最大最小值 
        /// </summary>
        public static void ArrayFindMaxMin(int[] a, out int max, out int maxIndex, out int min, out int minIndex)
        {
            ippsMinMaxIndx_32s(a, a.Length, out min, out minIndex, out max, out maxIndex);
        }

        /// <summary>
        ///  获取short 数组最大最小值 
        /// </summary>
        public static void ArrayFindMaxMin(short[] a, out short max, out int maxIndex, out short min, out int minIndex)
        {
            ippsMinMaxIndx_16s(a, a.Length, out min, out minIndex, out max, out maxIndex);
        }
        #endregion


        #region---- Max: pSrcDst = max(pSrc,pSrcDst) ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_16s_I(short[] pSrc, short[] pSrcDst, int len);

        // I32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_32s_I(int[]  pSrc, int[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_32f_I(float[] pSrc, float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_64f_I(double[] pSrc, double[] pSrcDst, int len);

        #endregion

        #region---- Max: pDst = max(pSrc1,pSrc2) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxEvery_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);

        #endregion

        #region---- Min: pSrcDst = min(pSrc,pSrcDst) ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_16s_I(short[] pSrc, short[] pSrcDst, int len);
        
        // I32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_32s_I(int[] pSrc, int[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_32f_I(float[] pSrc, float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_64f_I(double[] pSrc, double[] pSrcDst, int len);

        #endregion

        #region---- Min: pDst = min(pSrc1,pSrc2) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinEvery_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);
        #endregion

        #region---- Max: pMax = max(pSrc) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxIndx_16s(short[] pSrc, int len, out short pMax, out int pIndx);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxIndx_32s(int[] pSrc, int len, out int pMax, out int pIndx);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxIndx_32f(float[] pSrc, int len, out float pMax, out int pIndx);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMaxIndx_64f(double[] pSrc, int len, out double pMax, out int pIndx);

        #endregion

        #region---- Min: pMin = min(pSrc) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinIndx_16s(short[] pSrc, int len, out short pMin, out int pIndx);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinIndx_32s(int[] pSrc, int len, out int pMin, out int pIndx);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinIndx_32f(float[] pSrc, int len, out float pMin, out int pIndx);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinIndx_64f(double[] pSrc, int len, out double pMin, out int pIndx);

        #endregion

        #region---- MinMax: pMin = min(pSrc),pMax = max(pSrc) ----
        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinMaxIndx_16s(short[] pSrc, int len, out short pMin, out int pMinIndx, out short pMax, out int pMaxIndx);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinMaxIndx_32s(int[] pSrc, int len, out int pMin, out int pMinIndx, out int pMax, out int pMaxIndx);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinMaxIndx_32f(float[] pSrc, int len, out float pMin, out int pMinIndx, out float pMax, out int pMaxIndx);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMinMaxIndx_64f(double[] pSrc, int len, out double pMin, out int pMinIndx, out double pMax, out int pMaxIndx);

        #endregion

    }
}
