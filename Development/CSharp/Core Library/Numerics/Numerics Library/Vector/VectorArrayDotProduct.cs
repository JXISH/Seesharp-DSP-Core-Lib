using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Dot Product ----

        /// <summary>
        /// 对 float 数组求点积
        /// </summary>
        public static float ArrayDotProduct(float[] a, float[] b)
        {
            float dotProduct;
            ippsDotProd_32f(a, b, a.Length, out dotProduct);
            return dotProduct;
        }

        /// <summary>
        /// 对 double 数组求点积
        /// </summary>
        public static double ArrayDotProduct(double[] a, double[] b)
        {
            double dotProduct;
            ippsDotProd_64f(a, b, a.Length, out dotProduct);
            return dotProduct;
        }

        /// <summary>
        /// 对 Complex32 数组求点积
        /// </summary>
        public static Complex32 ArrayDotProduct(Complex32[] a, Complex32[] b)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsDotProd_32fc(a_address, b_address, a.Length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组求点积
        /// </summary>
        public static Complex ArrayDotProduct(Complex[] a, Complex[] b)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsDotProd_64fc(a_address, b_address, a.Length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex32 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex32 ArrayDotProductConj(Complex32[] a, Complex32[] b)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            Complex32[] bConj = Vector.GetComplexConjugation(b);
            GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
            IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();

            ippsDotProd_32fc(a_address, bConj_address, a.Length, out dotProduct);

            a_GC.Free();
            bConj_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex ArrayDotProductConj(Complex[] a, Complex[] b)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            Complex[] bConj = Vector.GetComplexConjugation(b);
            GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
            IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();

            ippsDotProd_64fc(a_address, bConj_address, a.Length, out dotProduct);

            a_GC.Free();
            bConj_GC.Free();

            return dotProduct;
        }

        #endregion

        #region ---- Dot Product: pDp = DotProduct(pSrc1, pSrc2) ----


        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32f(float[] pSrc1, float[] pSrc2, int len, out float pDp);
        
         // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64f(double[] pSrc1, double[] pSrc2, int len, out double pDp);
            
        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex32 pDp);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex pDp);

        #endregion
    }
}
