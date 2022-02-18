using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- scale Templete ----

        /// <summary>
        /// 数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale<T>(T[] inout, T b)
        {
            if (inout is short[] inout_i16 && b is short b_i16)
            {
                ArrayScale(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float b_f32)
            {
                ArrayScale(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double b_f64)
            {
                ArrayScale(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32 b_fc32)
            {
                ArrayScale(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex b_fc64)
            {
                ArrayScale(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale<T>(T[] a, T b, T[] output)
        {
            if (a is short[] a_i16 && b is short b_i16 && output is short[] output_i16)
            {
                ArrayScale(a_i16, b_i16, output_i16);
            }
            else if (a is float[] a_f32 && b is float b_f32 && output is float[] output_f32)
            {
                ArrayScale(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double b_f64 && output is double[] output_f64)
            {
                ArrayScale(a_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32 b_fc32 && output is Complex32[] output_fc32)
            {
                ArrayScale(a_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex b_fc64 && output is Complex[] output_fc64)
            {
                ArrayScale(a_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘以常数
        /// output = a * b
        /// </summary>
        public static T[] GetArrayScale<T>(T[] a, T b)
        {
            T[] output = new T[a.Length];
            ArrayScale(a, b, output);
            return output;
        }

        #endregion

        #region ---- scale Templete ----
        /// <summary>
        /// 数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale<T>(T[] inout, double b)
        {
            if (inout is float[] inout_f32)
            {
                ArrayScale(inout_f32, b);
            }
            else if (inout is double[] inout_f64)
            {
                ArrayScale(inout_f64, b);
            }
            else if (inout is Complex32[] inout_fc32)
            {
                ArrayScale(inout_fc32, b);
            }
            else if (inout is Complex[] inout_fc64)
            {
                ArrayScale(inout_fc64, b);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale<T>(T[] a, double b, T[] output)
        {
            if (a is float[] a_f32 && output is float[] output_f32)
            {
                ArrayScale(a_f32, b, output_f32);
            }
            else if (a is double[] a_f64 && output is double[] output_f64)
            {
                ArrayScale(a_f64, b, output_f64);
            }
            else if (a is Complex32[] a_fc32 && output is Complex32[] output_fc32)
            {
                ArrayScale(a_fc32, b, output_fc32);
            }
            else if (a is Complex[] a_fc64 && output is Complex[] output_fc64)
            {
                ArrayScale(a_fc64, b, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘以常数
        /// output = a * b
        /// </summary>
        public static T[] GetArrayScale<T>(T[] a, double b)
        {
            T[] output = new T[a.Length];
            ArrayScale(a, b, output);
            return output;
        }

        #endregion

        #region ---- scale ----

        /// <summary>
        /// I16 数组乘以常数
        /// inout = inout * b
        /// </summary>
        internal static void ArrayScale(short[] inout, short b)
        {
            ippsMulC_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16 数组乘以常数
        /// output = a * b
        /// </summary>
        internal static void ArrayScale(short[] a, short b, short[] output)
        {
            Vector.ArrayCopy(output, a);
            ippsMulC_16s_I(b, output, output.Length);
        }

        /// <summary>
        /// double数组乘以常数
        /// inout = inout * b
        /// </summary>
        internal static void ArrayScale(double[] inout, double b)
        {
            ippsMulC_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double数组乘以常数
        /// output = a * b
        /// </summary>
        internal static void ArrayScale(double[] a, double b, double[] output)
        {
            ippsMulC_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float数组乘以常数
        /// inout = inout * b
        /// </summary>
        internal static void ArrayScale(float[] inout, float b)
        {
            ippsMulC_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float数组乘以常数
        /// output = a * b
        /// </summary>
        internal static void ArrayScale(float[] a, float b, float[] output)
        {
            ippsMulC_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex数组乘以常数
        /// inout = inout * b
        /// </summary>
        internal static void ArrayScale(Complex[] inout, Complex b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsMulC_64fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex数组乘以常数
        /// output = a * b
        /// </summary>
        internal static void ArrayScale(Complex[] a, Complex b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMulC_64fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Comple32数组乘以常数
        /// inout = inout * b
        /// </summary>
        internal static void ArrayScale(Complex32[] inout, Complex32 b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsMulC_32fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Comple32数组乘以常数
        /// output = a * b
        /// </summary>
        internal static void ArrayScale(Complex32[] a, Complex32 b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMulC_32fc(a_address, b, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- scale float[] * double----

        /// <summary>
        /// float数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale(float[] inout, double b)
        {
            ArrayScale(inout, (float)b);
        }

        /// <summary>
        /// float数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale(float[] a, double b, float[] output)
        {
            ArrayScale(a, (float)b, output);
        }

        /// <summary>
        /// float 数组乘以常数
        /// output = a * b
        /// </summary>
        public static float[] GetArrayScale(float[] a, double b)
        {
            return GetArrayScale(a, (float)b);
        }


        /// <summary>
        /// Comple32数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale(Complex32[] inout, double b)
        {
            ArrayScale(inout, (float)b);
        }

        /// <summary>
        /// Comple32数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale(Complex32[] a, double b, Complex32[] output)
        {
            ArrayScale(a, (float)b, output);
        }

        /// <summary>
        /// Complex32 数组乘以常数
        /// output = a * b
        /// </summary>
        public static Complex32[] GetArrayScale(Complex32[] a, double b)
        {
            return GetArrayScale(a, (float)b);
        }

        #endregion

        #region ---- scale Complex * Real ----

        /// <summary>
        /// Complex数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale(Complex[] inout, double b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsMulC_64f_I(b, inout_address, inout.Length * 2);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale(Complex[] a, double b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMulC_64f(a_address, b, output_address, a.Length * 2);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex 数组乘以常数
        /// output = a * b
        /// </summary>
        public static Complex[] GetArrayScale(Complex[] a, double b)
        {
            Complex[] output = new Complex[a.Length];
            ArrayScale(a, b, output);
            return output;
        }


        /// <summary>
        /// Comple32数组乘以常数
        /// inout = inout * b
        /// </summary>
        public static void ArrayScale(Complex32[] inout, float b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsMulC_32f_I(b, inout_address, inout.Length * 2);

            inout_GC.Free();
        }

        /// <summary>
        /// Comple32数组乘以常数
        /// output = a * b
        /// </summary>
        public static void ArrayScale(Complex32[] a, float b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMulC_32f(a_address, b, output_address, a.Length * 2);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘以常数
        /// output = a * b
        /// </summary>
        public static Complex32[] GetArrayScale(Complex32[] a, float b)
        {
            Complex32[] output = new Complex32[a.Length];
            ArrayScale(a, b, output);
            return output;
        }

        #endregion

        #region ---- multi Templete ----

        /// <summary>
        /// 数组乘法
        /// inout = inout * b
        /// </summary>
        public static void ArrayMulti<T>(T[] inout, T[] b)
        {
            if (inout is short[] inout_i16 && b is short[] b_i16)
            {
                ArrayMulti(inout_i16, b_i16);
            }
            else if (inout is float[] inout_f32 && b is float[] b_f32)
            {
                ArrayMulti(inout_f32, b_f32);
            }
            else if (inout is double[] inout_f64 && b is double[] b_f64)
            {
                ArrayMulti(inout_f64, b_f64);
            }
            else if (inout is Complex32[] inout_fc32 && b is Complex32[] b_fc32)
            {
                ArrayMulti(inout_fc32, b_fc32);
            }
            else if (inout is Complex[] inout_fc64 && b is Complex[] b_fc64)
            {
                ArrayMulti(inout_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘法
        /// output = a * b
        /// </summary>
        public static void ArrayMulti<T>(T[] a, T[] b, T[] output)
        {
            if (a is short[] a_i16 && b is short[] b_i16 && output is short[] output_i16)
            {
                ArrayMulti(a_i16, b_i16, output_i16);
            }
            else if (a is float[] a_f32 && b is float[] b_f32 && output is float[] output_f32)
            {
                ArrayMulti(a_f32, b_f32, output_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64 && output is double[] output_f64)
            {
                ArrayMulti(a_f64, b_f64, output_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32[] b_fc32 && output is Complex32[] output_fc32)
            {
                ArrayMulti(a_fc32, b_fc32, output_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex[] b_fc64 && output is Complex[] output_fc64)
            {
                ArrayMulti(a_fc64, b_fc64, output_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 数组乘法
        /// return = a * b
        /// </summary>
        public static T[] GetArrayMulti<T>(T[] a, T[] b)
        {
            T[] output = new T[a.Length];
            ArrayMulti(a, b, output);
            return output;
        }

        #endregion

        #region ---- multi ----

        /// <summary>
        /// I16 数组乘法
        /// inout = inout * b
        /// </summary>
        internal static void ArrayMulti(short[] inout, short[] b)
        {
            ippsMul_16s_I(b, inout, inout.Length);
        }

        /// <summary>
        /// I16 数组乘法
        /// output = a * b
        /// </summary>
        internal static void ArrayMulti(short[] a, short[] b, short[] output)
        {
            ippsMul_16s(a, b, output, a.Length);
        }

        /// <summary>
        /// double 数组乘法
        /// inout = inout * b
        /// </summary>
        internal static void ArrayMulti(double[] inout, double[] b)
        {
            ippsMul_64f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// double 数组乘法
        /// output = a * b
        /// </summary>
        internal static void ArrayMulti(double[] a, double[] b, double[] output)
        {
            ippsMul_64f(a, b, output, a.Length);
        }

        /// <summary>
        /// float 数组乘法
        /// inout = inout * b
        /// </summary>
        internal static void ArrayMulti(float[] inout, float[] b)
        {
            ippsMul_32f_I(b, inout, inout.Length);
        }

        /// <summary>
        /// float 数组乘法
        /// output = a * b
        /// </summary>
        internal static void ArrayMulti(float[] a, float[] b, float[] output)
        {
            ippsMul_32f(a, b, output, a.Length);
        }

        /// <summary>
        /// Complex 数组乘法
        /// inout = inout * b
        /// </summary>
        internal static void ArrayMulti(Complex[] inout, Complex[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsMul_64fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex 数组乘法
        /// output = a * b
        /// </summary>
        internal static void ArrayMulti(Complex[] a, Complex[] b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMul_64fc(a_address, b_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘法
        /// inout = inout * b
        /// </summary>
        internal static void ArrayMulti(Complex32[] inout, Complex32[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();

            ippsMul_32fc_I(b_address, inout_address, inout.Length);

            inout_GC.Free();
            b_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘法
        /// output = a * b
        /// </summary>
        internal static void ArrayMulti(Complex32[] a, Complex32[] b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle b_GC = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr b_address = b_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMul_32fc(a_address, b_address, output_address, a.Length);

            a_GC.Free();
            b_GC.Free();
            output_GC.Free();
        }

        #endregion

        #region ---- multi Comple Real ----

        /// <summary>
        /// Complex 数组乘以 double 数组
        /// inout = inout * b
        /// </summary>
        public static void ArrayMulti(Complex[] inout, double[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            int length = b.Length;
            int multiLength = length * 2;
            double[] bCopy = new double[multiLength];
            GCHandle bCopy_GC = GCHandle.Alloc(bCopy, GCHandleType.Pinned);
            IntPtr bCopy_address = bCopy_GC.AddrOfPinnedObject();

            ippsRealToCplx_64f(b, b, bCopy_address, length);
            ippsMul_64f_I(bCopy_address, inout_address, multiLength);

            inout_GC.Free();
            bCopy_GC.Free();
        }

        /// <summary>
        /// Complex 数组乘以 double 数组
        /// output = a * b
        /// </summary>
        public static void ArrayMulti(Complex[] a, double[] b, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            int length = b.Length;
            int multiLength = length * 2;
            double[] bCopy = new double[multiLength];
            GCHandle bCopy_GC = GCHandle.Alloc(bCopy, GCHandleType.Pinned);
            IntPtr bCopy_address = bCopy_GC.AddrOfPinnedObject();

            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsRealToCplx_64f(b, b, bCopy_address, length);
            ippsMul_64f(a_address, bCopy_address, output_address, multiLength);

            bCopy_GC.Free();
            a_GC.Free();
        }

        /// <summary>
        /// Complex 数组乘以 double 数组
        /// return = a * b
        /// </summary>
        public static Complex[] GetArrayMulti(Complex[] a, double[] b)
        {
            Complex[] output = new Complex[a.Length];
            ArrayMulti(a, b, output);
            return output;
        }

        /// <summary>
        /// Complex32 数组乘以 float 数组
        /// inout = inout * b
        /// </summary>
        public static void ArrayMulti(Complex32[] inout, float[] b)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsMul_32f32fc_I(b, inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘以 float 数组
        /// output = a * b
        /// </summary>
        public static void ArrayMulti(Complex32[] a, float[] b, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsMul_32f32fc(b, a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘以 float 数组
        /// return = a * b
        /// </summary>
        public static Complex32[] GetArrayMulti(Complex32[] a, float[] b)
        {
            Complex32[] output = new Complex32[a.Length];
            ArrayMulti(a, b, output);
            return output;
        }

        /// <summary>
        /// Complex 数组乘以 double 数组. double 数组已经按照 Complex 形式排列复制一份。
        /// </summary>
        public static void ArrayMulti_double(Complex[] a, IntPtr b)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMul_64f_I(b, a_address, a.Length * 2);

            a_GC.Free();
        }

        /// <summary>
        /// Complex32 数组乘以 float 数组. float 数组已经按照 Complex32 形式排列复制一份。
        /// </summary>
        public static void ArrayMulti_float(Complex32[] a, IntPtr b)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMul_32f_I(b, a_address, a.Length * 2);

            a_GC.Free();
        }

        #endregion

        #region  ---- Scale:  pSrcDst = val * pSrcDst ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_16s_I(short val, short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32f_I(float val, float[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32f_I(float val, IntPtr pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64f_I(double val, double[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64f_I(double val, IntPtr pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32fc_I(Complex32 val, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64fc_I(Complex val, IntPtr pSrcDst, int len);

        #endregion

        #region  ---- Scale:  pDst = val * pSrc ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32f(float[] pSrc, float val, float[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32f(IntPtr pSrc, float val, IntPtr pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64f(double[] pSrc, double val, double[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64f(IntPtr pSrc, double val, IntPtr pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_32fc(IntPtr pSrc, Complex32 val, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMulC_64fc(IntPtr pSrc, Complex val, IntPtr pDst, int len);

        #endregion

        #region ---- Multi : pSrcDst = pSrc * pSrcDst ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_16s_I(short[] pSrc, short[] pSrcDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f_I(float[] pSrc, float[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64f_I(double[] pSrc, double[] pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64f_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f32fc_I(float[] pSrc, IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64fc_I(IntPtr pSrc, IntPtr pSrcDst, int len);

        #endregion

        #region ---- Multi : pDst = pSrc1 * pSrc2 ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_16s(short[] pSrc1, short[] pSrc2, short[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f(float[] pSrc1, float[] pSrc2, float[] pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64f(double[] pSrc1, double[] pSrc2, double[] pDst, int len);

        // double 
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64f(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_32f32fc(float[] pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMul_64fc(IntPtr pSrc1, IntPtr pSrc2, IntPtr pDst, int len);

        #endregion
    }
}
