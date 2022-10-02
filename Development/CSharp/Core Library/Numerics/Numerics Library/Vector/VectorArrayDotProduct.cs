using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Product Sum ----

        /// <summary>
        /// 对 float 数组求点积
        /// </summary>
        public static float ArrayProductSum(float[] a, float[] b)
        {
            float dotProduct;
            ippsDotProd_32f(a, b, a.Length, out dotProduct);
            return dotProduct;
        }

        /// <summary>
        /// 对 float 数组求点积
        /// </summary>
        public static float ArrayProductSum(float[] a, int aStart, float[] b, int bStart, int length)
        {
            float dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(float);

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(float);

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_32f(a_address, b_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 double 数组求点积
        /// </summary>
        public static double ArrayProductSum(double[] a, double[] b)
        {
            double dotProduct;
            ippsDotProd_64f(a, b, a.Length, out dotProduct);
            return dotProduct;
        }

        /// <summary>
        /// 对 double 数组求点积
        /// </summary>
        public static double ArrayProductSum(double[] a, int aStart, double[] b, int bStart, int length)
        {
            double dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(double);

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(double);

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_64f(a_address, b_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex32 数组乘积之和
        /// </summary>
        public static Complex32 ArrayProductSum(Complex32[] a, Complex32[] b)
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
        /// 对 Complex32 数组乘积之和
        /// </summary>
        public static Complex32 ArrayProductSum(Complex32[] a, int aStart, Complex32[] b, int bStart, int length)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(float) * 2;

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(float) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_32fc(a_address, b_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组求点积
        /// </summary>
        public static Complex ArrayProductSum(Complex[] a, Complex[] b)
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
        /// 对 Complex 数组求点积
        /// </summary>
        public static Complex ArrayProductSum(Complex[] a, int aStart, Complex[] b, int bStart, int length)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(double) * 2;

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(double) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_64fc(a_address, b_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        #endregion

        #region ---- Product Sum ----

        /// <summary>
        /// 对 Complex32 数组 和 float 数组 求乘积之和
        /// </summary>
        public static Complex32 ArrayDotProduct(Complex32[] a, float[] b)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsDotProd_32f32fc(b, a_address, a.Length, out dotProduct);

            a_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex32 数组 和 float 数组 求乘积之和
        /// </summary>
        public static Complex32 ArrayProductSum(Complex32[] a, int aStart, float[] b, int bStart, int length)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(float) * 2;

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(float);

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_32f32fc(b_address, a_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组 和 double 数组 求乘积之和
        /// </summary>
        public static Complex ArrayDotProduct(Complex[] a, double[] b)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsDotProd_64f64fc(b, a_address, a.Length, out dotProduct);

            a_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组 和 double 数组 求乘积之和
        /// </summary>
        public static Complex ArrayProductSum(Complex[] a, int aStart, double[] b, int bStart, int length)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(double) * 2;

            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            b_address += bStart * sizeof(double);

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_64f64fc(b_address, a_address, length, out dotProduct);

            a_GC.Free();
            b_GC.Free();

            return dotProduct;
        }

        #endregion

        #region ---- Dot Product ----


        /// <summary>
        /// 对 Complex32 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex32 ArrayDotProduct(Complex32[] a, Complex32[] b)
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
        /// 对 Complex32 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex32 ArrayDotProduct(Complex32[] a, int aStart, Complex32[] b, int bStart, int length)
        {
            Complex32 dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(float) * 2;

            Complex32[] bConj = Vector.GetComplexConjugation(b);
            GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
            IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();
            bConj_address += bStart * sizeof(float) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_32fc(a_address, bConj_address, length, out dotProduct);

            a_GC.Free();
            bConj_GC.Free();

            return dotProduct;
        }

        /// <summary>
        /// 对 Complex 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex ArrayDotProduct(Complex[] a, Complex[] b)
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

        /// <summary>
        /// 对 Complex 数组求点积
        /// return =  a dotProduct b*
        /// </summary>
        public static Complex ArrayDotProduct(Complex[] a, int aStart, Complex[] b, int bStart, int length)
        {
            Complex dotProduct;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            a_address += aStart * sizeof(double) * 2;

            Complex[] bConj = Vector.GetComplexConjugation(b);
            GCHandle bConj_GC = GCHandle.Alloc(bConj, GCHandleType.Pinned);
            IntPtr bConj_address = bConj_GC.AddrOfPinnedObject();
            bConj_address += bStart * sizeof(double) * 2;

            // 确定拷贝长度
            length = GetMinArrayLenth(a.Length - aStart, b.Length - bStart, length);

            ippsDotProd_64fc(a_address, bConj_address, length, out dotProduct);

            a_GC.Free();
            bConj_GC.Free();

            return dotProduct;
        }


        #endregion

        #region ---- Dot Product: pDp = DotProduct(pSrc1, pSrc2) ----


        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32f(float[] pSrc1, float[] pSrc2, int len, out float pDp);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32f(IntPtr pSrc1, IntPtr pSrc2, int len, out float pDp);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64f(double[] pSrc1, double[] pSrc2, int len, out double pDp);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64f(IntPtr pSrc1, IntPtr pSrc2, int len, out double pDp);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex32 pDp);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex pDp);

        // Complex32 dot float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32f32fc(float[] pSrc1, IntPtr pSrc2, int len, out Complex32 pDp);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_32f32fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex32 pDp);

        // Complex dot Double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64f64fc(double[] pSrc1, IntPtr pSrc2, int len, out Complex pDp);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsDotProd_64f64fc(IntPtr pSrc1, IntPtr pSrc2, int len, out Complex pDp);

        #endregion
    }
}
