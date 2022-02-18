using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region---- Reverse ----

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(short[] inout)
        {
            ippsFlip_16u_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(short[] a, short[] output)
        {
            ippsFlip_16u(a, output, a.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(float[] inout)
        {
            ippsFlip_32f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(float[] a, float[] output)
        {
            ippsFlip_32f(a, output, a.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(double[] inout)
        {
            ippsFlip_64f_I(inout, inout.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(double[] a, double[] output)
        {
            ippsFlip_64f(a, output, a.Length);
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(Complex32[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsFlip_32fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(Complex32[] a, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsFlip_32fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(Complex[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsFlip_64fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        ///  数组反转
        /// </summary>
        public static void ArrayReserve(Complex[] a, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsFlip_64fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region---- Reverse: pDst = Invert (pSrc) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_16u(short[] pSrc, short[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_32f(float[] pSrc, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_64f(double[] pSrc, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_64fc(IntPtr pSrc, IntPtr pDst, int len);

        #endregion

        #region---- Reverse: pSrcDst = Invert (pSrcDst) ----

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_16u_I(short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_32f_I(float[] pSrcDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_64f_I(double[] pSrcDst, int len);


        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_32fc_I(IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsFlip_64fc_I(IntPtr pSrcDst, int len);

        #endregion
    }
}
