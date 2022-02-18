using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Sum ----

        /// <summary>
        /// 对 double 数组求和
        /// </summary>
        public static double ArraySum(double[] a)
        {
            double sum;
            ippsSum_64f(a, a.Length, out sum);
            return sum;
        }

        /// <summary>
        /// 对 float 数组求和
        /// </summary>
        public static float ArraySum(float[] a)
        {
            float sum;
            ippsSum_32f(a, a.Length, out sum);
            return sum;
        }

        /// <summary>
        /// 对 Complex 数组求和
        /// </summary>
        public static Complex ArraySum(Complex[] a)
        {
            Complex sum;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsSum_64fc(a_address, a.Length, out sum);

            a_GC.Free();
            return sum;
        }

        /// <summary>
        /// 对 Complex32 数组求和
        /// </summary>
        public static Complex32 ArraySum(Complex32[] a)
        {
            Complex32 sum;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsSum_32fc(a_address, a.Length, out sum);

            a_GC.Free();
            return sum;
        }

        #endregion

        #region---- Sum ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSum_32f(float[] pSrc, int len, out float pSum, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSum_64f(double[] pSrc, int len, out double pSum);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSum_32fc(IntPtr pSrc, int len, out Complex32 pSum, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSum_64fc(IntPtr pSrc, int len, out Complex pSum);

        #endregion
    }
}
