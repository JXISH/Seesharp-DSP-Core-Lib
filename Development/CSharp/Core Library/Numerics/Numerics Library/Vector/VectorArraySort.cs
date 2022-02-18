using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region---- Ascend ----

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(short[] inout)
        {
            ippsSortAscend_16s_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(short[] a, short[] output)
        {
            ArrayCopy(a, output);
            ArrayAscend(output);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(short[] inout, int[] index)
        {
            ippsSortIndexAscend_16s_I(inout, index,inout.Length);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(short[] a, short[] output,int[] index)
        {
            ArrayCopy(a, output);
            ArrayAscendIndex(output,index);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(int[] inout)
        {
            ippsSortAscend_32s_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(int[] a, int[] output)
        {
            ArrayCopy(a, output);
            ArrayAscend(output);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(int[] inout, int[] index)
        {
            ippsSortIndexAscend_32s_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(int[] a, int[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayAscendIndex(output, index);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(float[] inout)
        {
            ippsSortAscend_32f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(float[] a, float[] output)
        {
            ArrayCopy(a, output);
            ArrayAscend(output);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(float[] inout, int[] index)
        {
            ippsSortIndexAscend_32f_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(float[] a, float[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayAscendIndex(output, index);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(double[] inout)
        {
            ippsSortAscend_64f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组升序
        /// </summary>
        public static void ArrayAscend(double[] a, double[] output)
        {
            ArrayCopy(a, output);
            ArrayAscend(output);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(double[] inout, int[] index)
        {
            ippsSortIndexAscend_64f_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组升序，并返回索引位置
        /// </summary>
        public static void ArrayAscendIndex(double[] a, double[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayAscendIndex(output, index);
        }

        #endregion

        #region---- Descend ----

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(short[] inout)
        {
            ippsSortDescend_16s_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(short[] a, short[] output)
        {
            ArrayCopy(a, output);
            ArrayDescend(output);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(short[] inout, int[] index)
        {
            ippsSortIndexDescend_16s_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(short[] a, short[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayDescendIndex(output, index);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(int[] inout)
        {
            ippsSortDescend_32s_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(int[] a, int[] output)
        {
            ArrayCopy(a, output);
            ArrayDescend(output);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(int[] inout, int[] index)
        {
            ippsSortIndexDescend_32s_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(int[] a, int[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayDescendIndex(output, index);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(float[] inout)
        {
            ippsSortDescend_32f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(float[] a, float[] output)
        {
            ArrayCopy(a, output);
            ArrayDescend(output);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(float[] inout, int[] index)
        {
            ippsSortIndexDescend_32f_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(float[] a, float[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayDescendIndex(output, index);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(double[] inout)
        {
            ippsSortDescend_64f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组降序
        /// </summary>
        public static void ArrayDescend(double[] a, double[] output)
        {
            ArrayCopy(a, output);
            ArrayDescend(output);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(double[] inout, int[] index)
        {
            ippsSortIndexDescend_64f_I(inout, index, inout.Length);
        }

        /// <summary>
        ///  数组降序，并返回索引位置
        /// </summary>
        public static void ArrayDescendIndex(double[] a, double[] output, int[] index)
        {
            ArrayCopy(a, output);
            ArrayDescendIndex(output, index);
        }

        #endregion

        #region---- Ascend: pSrcDst = Ascend(pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortAscend_16s_I(short[] pSrcDst, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortAscend_32s_I(int[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortAscend_32f_I(float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortAscend_64f_I(double[] pSrcDst, int len);

        #endregion

        #region---- Descend: pSrcDst = Descend(pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortDescend_16s_I(short[] pSrcDst, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortDescend_32s_I(int[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortDescend_32f_I(float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortDescend_64f_I(double[] pSrcDst, int len);

        #endregion

        #region---- Ascend: pSrcDst = Ascend(pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexAscend_16s_I(short[] pSrcDst, [Out] int[] pDstIdx, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexAscend_32s_I(int[] pSrcDst, [Out]int[] pDstIdx, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexAscend_32f_I(float[] pSrcDst, [Out]int[] pDstIdx, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexAscend_64f_I(double[] pSrcDst, [Out]int[] pDstIdx, int len);

        #endregion

        #region---- Descend: pSrcDst = Descend(pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexDescend_16s_I(short[] pSrcDst, [Out]int[] pDstIdx, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexDescend_32s_I(int[] pSrcDst, [Out]int[] pDstIdx, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexDescend_32f_I(float[] pSrcDst, [Out] int[] pDstIdx, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSortIndexDescend_64f_I(double[] pSrcDst, [Out]int[] pDstIdx, int len);
        #endregion
    }
}
