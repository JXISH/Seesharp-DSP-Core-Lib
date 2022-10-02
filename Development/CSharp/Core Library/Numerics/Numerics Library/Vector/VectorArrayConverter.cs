using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region---- Convert Real ----

        /// <summary>
        /// 将 short 数组转换为 int 数组
        /// </summary>
        internal static void ArrayConvert(short[] input, int[] output)
        {
            ippsConvert_16s32s(input, output, input.Length);
        }

        /// <summary>
        /// 将 short 数组转换为 float 数组
        /// </summary>
        internal static void ArrayConvert(short[] input, float[] output)
        {
            ippsConvert_16s32f(input, output, input.Length);
        }

        /// <summary>
        /// 将 int 数组转换为 float 数组
        /// </summary>
        internal static void ArrayConvert(int[] input, float[] output)
        {
            ippsConvert_32s32f(input, output, input.Length);
        }

        /// <summary>
        /// 将 int 数组转换为 double 数组
        /// </summary>
        internal static void ArrayConvert(int[] input, double[] output)
        {
            ippsConvert_32s64f(input, output, input.Length);
        }

        /// <summary>
        /// 将 float 数组转换为 double 数组
        /// </summary>
        internal static void ArrayConvert(float[] input, double[] output)
        {
            ippsConvert_32f64f(input, output, input.Length);
        }

        /// <summary>
        /// 将 double 数组转换为 float 数组
        /// </summary>
        internal static void ArrayConvert(double[] input, float[] output)
        {
            ippsConvert_64f32f(input, output, input.Length);
        }

        /// <summary>
        /// 将 double 数组转换为 int 数组
        /// </summary>
        internal static void ArrayConvert(double[] input, int[] output)
        {
            ippsConvert_64f32s_Sfs(input, output, input.Length, IppRoundMode.ippRndNear, 0);
        }

        /// <summary>
        /// 将 float 数组转换为 int 数组
        /// </summary>
        internal static void ArrayConvert(float[] input, int[] output)
        {
            ippsConvert_32f32s_Sfs(input, output, input.Length, IppRoundMode.ippRndNear, 0);
        }

        /// <summary>
        /// 将 float 数组转换为 short 数组
        /// </summary>
        internal static void ArrayConvert(float[] input, short[] output)
        {
            ippsConvert_32f16s_Sfs(input, output, input.Length, IppRoundMode.ippRndNear, 0);
        }

        /// <summary>
        /// 将 int 数组转换为 short 数组
        /// </summary>
        internal static void ArrayConvert(int[] input, short[] output)
        {
            ippsConvert_32s16s(input, output, input.Length);
        }

        #endregion

        #region---- Convert Complex ----

        /// <summary>
        /// 将 short 数组转换为 Complex32 数组
        /// </summary>
        public static void ArrayConvert(short[] input, Complex32[] output)
        {
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsConvert_16s32f(input, output_address, input.Length);

            output_GC.Free();
        }

        /// <summary>
        /// 将 Complex32 数组转换为 Complex 数组
        /// </summary>
        public static void ArrayConvert(Complex32[] input, Complex[] output)
        {
            GCHandle input_GC = GCHandle.Alloc(input, GCHandleType.Pinned);
            IntPtr input_address = input_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsConvert_32f64f(input_address, output_address, input.Length * 2);

            input_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// 将 Complex 数组转换为 Complex32 数组
        /// </summary>
        public static void ArrayConvert(Complex[] input, Complex32[] output)
        {
            GCHandle input_GC = GCHandle.Alloc(input, GCHandleType.Pinned);
            IntPtr input_address = input_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsConvert_64f32f(input_address, output_address, input.Length * 2);

            input_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// 将 Complex32 数组转换为short数组
        /// </summary>
        public static void ArrayConvert(Complex32[] input, short[] output)
        {
            GCHandle input_GC = GCHandle.Alloc(input, GCHandleType.Pinned);
            IntPtr input_address = input_GC.AddrOfPinnedObject();

            ippsConvert_32f16s_Sfs(input_address, output, input.Length * 2, IppRoundMode.ippRndNear, 0);

            input_GC.Free();
        }

        #endregion

        #region---- Convert to Short ----

        /// <summary>
        /// 将 I32 数组转换为 I16 数组
        /// </summary>
        public static short[] ConvertToShort(int[] input)
        {
            short[] output = new short[input.Length];
            ConvertToShort(input, output);
            return output;
        }

        /// <summary>
        /// 将 I32 数组转换为 I16 数组
        /// </summary>
        public static void ConvertToShort(int[] input, short[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 float 数组转换为 I16 数组
        /// </summary>
        public static short[] ConvertToShort(float[] input)
        {
            short[] output = new short[input.Length];
            ConvertToShort(input, output);
            return output;
        }

        /// <summary>
        /// 将 float 数组转换为 I16 数组
        /// </summary>
        public static void ConvertToShort(float[] input, short[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 Complex32 数组转换为 I16 数组
        /// </summary>
        public static short[] ConvertToShort(Complex32[] input)
        {
            short[] output = new short[input.Length * 2];
            ConvertToShort(input, output);
            return output;
        }

        /// <summary>
        /// 将 Complex32 数组转换为 I16 数组
        /// </summary>
        public static void ConvertToShort(Complex32[] input, short[] output)
        {
            ArrayConvert(input, output);
        }

        #endregion

        #region---- Convert to I32 ----

        /// <summary>
        /// 将 double 数组转换为 I32 数组
        /// </summary>
        public static int[] ConvertToInt(double[] input)
        {
            int[] output = new int[input.Length];
            ConvertToInt(input, output);
            return output;
        }

        /// <summary>
        /// 将 double 数组转换为 I32 数组
        /// </summary>
        public static void ConvertToInt(double[] input, int[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 float 数组转换为 int 数组
        /// </summary>
        public static int[] ConvertToInt(float[] input)
        {
            int[] output = new int[input.Length];
            ConvertToInt(input, output);
            return output;
        }

        /// <summary>
        /// 将 float 数组转换为 int 数组
        /// </summary>
        public static void ConvertToInt(float[] input, int[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 I16 数组转换为 I32 数组
        /// </summary>
        public static int[] ConvertToInt(short[] input)
        {
            int[] output = new int[input.Length];
            ConvertToInt(input, output);
            return output;
        }

        /// <summary>
        /// 将 I16 数组转换为 float 数组
        /// </summary>
        public static void ConvertToInt(short[] input, int[] output)
        {
            ArrayConvert(input, output);
        }

        #endregion

        #region---- Convert to Float ----

        /// <summary>
        /// 将 I16 数组转换为 float 数组
        /// </summary>
        public static float[] ConvertToFloat(short[] input)
        {
            float[] output = new float[input.Length];
            ConvertToFloat(input, output);
            return output;
        }

        /// <summary>
        /// 将 I16 数组转换为 float 数组
        /// </summary>
        public static void ConvertToFloat(short[] input, float[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 I32 数组转换为 float 数组
        /// </summary>
        public static float[] ConvertToFloat(int[] input)
        {
            float[] output = new float[input.Length];
            ConvertToFloat(input, output);
            return output;
        }

        /// <summary>
        /// 将 I32 数组转换为 float 数组
        /// </summary>
        public static void ConvertToFloat(int[] input, float[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 double 数组转换为 float 数组
        /// </summary>
        public static float[] ConvertToFloat(double[] input)
        {
            float[] output = new float[input.Length];
            ConvertToFloat(input, output);
            return output;
        }

        /// <summary>
        /// 将 double 数组转换为 float 数组
        /// </summary>
        public static void ConvertToFloat(double[] input, float[] output)
        {
            ArrayConvert(input, output);
        }

        #endregion

        #region---- Convert to Double ----

        /// <summary>
        /// 将 I32 数组转换为 double 数组
        /// </summary>
        public static double[] ConvertToDouble(int[] input)
        {
            double[] output = new double[input.Length];
            ConvertToDouble(input, output);
            return output;
        }

        /// <summary>
        /// 将 I32 数组转换为 double 数组
        /// </summary>
        public static void ConvertToDouble(int[] input, double[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 float 数组转换为 double 数组
        /// </summary>
        public static double[] ConvertToDouble(float[] input)
        {
            double[] output = new double[input.Length];
            ArrayConvert(input, output);
            return output;
        }

        /// <summary>
        /// 将 float 数组转换为 double 数组
        /// </summary>
        public static void ConvertToDouble(float[] input, double[] output)
        {
            ArrayConvert(input, output);
        }

        #endregion

        #region---- Convert to Complex ----

        /// <summary>
        /// 将 Complex32 数组转换为 Complex 数组
        /// </summary>
        public static Complex[] ConvertToComplex(Complex32[] input)
        {
            Complex[] output = new Complex[input.Length];
            ConvertToComplex(input, output);
            return output;
        }

        /// <summary>
        /// 将 Complex32 数组转换为 Complex 数组
        /// </summary>
        public static void ConvertToComplex(Complex32[] input, Complex[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 double 数组转换为 Complex 数组
        /// </summary>
        public static Complex[] ConvertToComplex(double[] input)
        {
            Complex[] output = new Complex[input.Length];
            ConvertToComplex(input, output);
            return output;
        }

        /// <summary>
        /// 将 double 数组转换为 Complex 数组
        /// </summary>
        public static void ConvertToComplex(double[] input, Complex[] output)
        {
            Vector.RealImageToComplex(input, Vector.ConstInit(input.Length, 0.0), output);
        }

        #endregion

        #region---- Convert to Complex32 ----

        /// <summary>
        /// 将 Complex 数组转换为 Complex32 数组
        /// </summary>
        public static Complex32[] ConvertToComplex32(Complex[] input)
        {
            Complex32[] output = new Complex32[input.Length];
            ConvertToComplex32(input, output);
            return output;
        }

        /// <summary>
        /// 将 Complex 数组转换为 Complex32 数组
        /// </summary>
        public static void ConvertToComplex32(Complex[] input, Complex32[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 short 数组转换为 Complex32 数组
        /// </summary>
        public static Complex32[] ConvertToComplex32(short[] input)
        {
            Complex32[] output = new Complex32[input.Length / 2];
            ConvertToComplex32(input, output);
            return output;
        }

        /// <summary>
        /// 将 short 数组转换为 Complex32 数组
        /// </summary>
        public static void ConvertToComplex32(short[] input, Complex32[] output)
        {
            ArrayConvert(input, output);
        }

        /// <summary>
        /// 将 float 数组转换为 Complex32 数组
        /// </summary>
        public static Complex32[] ConvertToComplex32(float[] input)
        {
            Complex32[] output = new Complex32[input.Length];
            ConvertToComplex32(input, output);
            return output;
        }

        /// <summary>
        /// 将 float 数组转换为 Complex32 数组
        /// </summary>
        public static void ConvertToComplex32(float[] input, Complex32[] output)
        {
            Vector.RealImageToComplex(input, Vector.ConstInit(input.Length, 0.0f), output);
        }

        #endregion


        #region---- Convert ----

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_16s64f_Sfs([In] short[] pSrc, [Out] double[] pDst, int len, int scaleFactor);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32s64f([In] int[] pSrc, [Out] double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32f64f([In] float[] pSrc, [Out] double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32f64f(IntPtr pSrc, IntPtr pDst, int len);



        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_16s32f([In] short[] pSrc, [Out] float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_16s32f([In] short[] pSrc, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32s32f([In] int[] pSrc, [Out] float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_64f32f([In] double[] pSrc, [Out] float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_64f32f(IntPtr pSrc, IntPtr pDst, int len);


        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_16s32s([In] short[] pSrc, [Out] int[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32f32s_Sfs([In] float[] pSrc, [Out] int[] pDst, int len, IppRoundMode rndMode, int scaleFactor);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_64f32s_Sfs([In] double[] pSrc, [Out] int[] pDst, int len, IppRoundMode rndMode, int scaleFactor);


        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32s16s([In] int[] pSrc, [Out] short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32f16s_Sfs([In] float[] pSrc, [Out] short[] pDst, int len, IppRoundMode rndMode, int scaleFactor);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_32f16s_Sfs(IntPtr pSrc, [Out] short[] pDst, int len, IppRoundMode rndMode, int scaleFactor);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConvert_64f16s_Sfs([In] double[] pSrc, [Out] short[] pDst, int len, IppRoundMode rndMode, int scaleFactor);

        #endregion
    }
}
