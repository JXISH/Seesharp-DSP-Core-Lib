using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Const ----

        /// <summary>
        /// 生成常数 byte 数组
        /// </summary>
        public static byte[] ConstInit(int N, byte constData)
        {
            byte[] byteData = new byte[N];

            ConstInit(N, byteData, constData);

            return byteData;
        }

        /// <summary>
        /// 生成常数 byte 数组
        /// </summary>
        public static void ConstInit(int N, byte[] output, byte constData)
        {
            ippsSet_8u(constData, output, N);
        }

        /// <summary>
        /// 生成常数 short 数组
        /// </summary>
        public static short[] ConstInit(int N, short constData)
        {
            short[] shortData = new short[N];

            ConstInit(N, shortData, constData);

            return shortData;
        }

        /// <summary>
        /// 生成常数 short 数组
        /// </summary>
        public static void ConstInit(int N, short[] output, short constData)
        {
            ippsSet_16s(constData, output, N);
        }

        /// <summary>
        /// 生成常数 int 数组
        /// </summary>
        public static int[] ConstInit(int N, int constData)
        {
            int[] intData = new int[N];

            ConstInit(N, intData, constData);

            return intData;
        }

        /// <summary>
        /// 生成常数 int 数组
        /// </summary>
        public static void ConstInit(int N, int[] output, int constData)
        {
            ippsSet_32s(constData, output, N);
        }

        /// <summary>
        /// 生成常数 double 数组
        /// </summary>
        public static double[] ConstInit(int N, double constData)
        {
            double[] doubleData = new double[N];

            ConstInit(N, doubleData, constData);

            return doubleData;
        }

        /// <summary>
        /// 生成常数 double 数组
        /// </summary>
        public static void ConstInit(int N, double[] output, double constData)
        {
            ippsSet_64f(constData, output, N);
        }

        /// <summary>
        /// 生成常数 float 数组
        /// </summary>
        public static float[] ConstInit(int N, float constData)
        {
            float[] floatData = new float[N];

            ConstInit(N, floatData, constData);

            return floatData;
        }

        /// <summary>
        /// 生成常数 float 数组
        /// </summary>
        public static void ConstInit(int N, float[] output, float constData)
        {
            ippsSet_32f(constData, output, N);
        }

        /// <summary>
        /// 生成常数 Complex 数组
        /// </summary>
        public static Complex[] ConstInit(int N, Complex constData)
        {
            Complex[] complexData = new Complex[N];

            ConstInit(N, complexData, constData);

            return complexData;
        }

        /// <summary>
        /// 生成常数 Complex 数组
        /// </summary>
        public static void ConstInit(int N, Complex[] output, Complex constData)
        {
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSet_64fc(constData, output_address, N);

            output_GC.Free();
        }

        /// <summary>
        /// 生成常数 Complex32 数组
        /// </summary>
        public static Complex32[] ConstInit(int N, Complex32 constData)
        {
            Complex32[] complex32Data = new Complex32[N];

            ConstInit(N, complex32Data, constData);

            return complex32Data;
        }

        /// <summary>
        /// 生成常数 Complex32 数组
        /// </summary>
        public static void ConstInit(int N, Complex32[] output, Complex32 constData)
        {
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsSet_32fc(constData, output_address, N);

            output_GC.Free();
        }

        /// <summary>
        /// 生成常数数组
        /// </summary>
        public static T[] ConstInit<T>(int N, T constData)
        {
            T[] result = new T[N];

            if (constData is byte constData_u8 && result is byte[] result_u8)
            {
                ConstInit(N, result_u8, constData_u8);
            }
            else if (constData is short constData_s16 && result is short[] result_s16)
            {
                ConstInit(N, result_s16, constData_s16);
            }
            else if (constData is int constData_s32 && result is int[] result_s32)
            {
                ConstInit(N, result_s32, constData_s32);
            }
            else if (constData is float constData_f32 && result is float[] result_f32)
            {
                ConstInit(N, result_f32, constData_f32);
            }
            else if (constData is double constData_f64 && result is double[] result_f64)
            {
                ConstInit(N, result_f64, constData_f64);
            }
            else if (constData is Complex32 constData_fc32 && result is Complex32[] result_fc32)
            {
                ConstInit(N, result_fc32, constData_fc32);
            }
            else if (constData is Complex constData_fc64 && result is Complex[] result_fc64)
            {
                ConstInit(N, result_fc64, constData_fc64);
            }
            else { throw new Exception("Data type not supported"); }

            return result;
        }

        /// <summary>
        /// 生成常数数组
        /// </summary>
        public static void ConstInit<T>(int N, T constData, IntPtr outputArray)
        {
            if (constData is byte constData_u8)
            {
                ippsSet_32f(constData_u8, outputArray, N);
            }
            else if (constData is short constData_s16)
            {
                ippsSet_32f(constData_s16, outputArray, N);
            }
            else if (constData is int constData_s32)
            {
                ippsSet_32f(constData_s32, outputArray, N);
            }
            else if (constData is float constData_f32)
            {
                ippsSet_32f(constData_f32, outputArray, N);
            }
            else if (constData is double constData_f64)
            {
                ippsSet_64f(constData_f64, outputArray, N);
            }
            else if (constData is Complex32 constData_fc32)
            {
                ippsSet_32fc(constData_fc32, outputArray, N);
            }
            else if (constData is Complex constData_fc64)
            {
                ippsSet_64fc(constData_fc64, outputArray, N);
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 生成全零数组
        /// </summary>
        public static T[] ZeroInit<T>(int N)
        {
            T[] result = new T[N];

            if (result is byte[] zeroData_u8)
            {
                ippsZero_8u(zeroData_u8, N);
            }
            else if (result is short[] zeroData_s16)
            {
                ippsZero_16s(zeroData_s16, N);
            }
            else if (result is int[] zeroData_s32)
            {
                ippsZero_32s(zeroData_s32, N);
            }
            else if (result is float[] zeroData_f32)
            {
                ippsZero_32f(zeroData_f32, N);
            }
            else if (result is double[] zeroData_f64)
            {
                ippsZero_64f(zeroData_f64, N);
            }
            else if (result is Complex32[] zeroData_fc32)
            {
                GCHandle output_GC = GCHandle.Alloc(zeroData_fc32, GCHandleType.Pinned);
                IntPtr output_address = output_GC.AddrOfPinnedObject();
                ippsZero_32fc(output_address, N);
                output_GC.Free();
            }
            else if (result is Complex[] zeroData_fc64)
            {
                GCHandle output_GC = GCHandle.Alloc(zeroData_fc64, GCHandleType.Pinned);
                IntPtr output_address = output_GC.AddrOfPinnedObject();
                ippsZero_64fc(output_address, N);
                output_GC.Free();
            }
            else { throw new Exception("Data type not supported"); }

            return result;
        }

        /// <summary>
        /// 生成全零数组
        /// </summary>
        public static void ZeroInit<T>(int N, T[] outputArray)
        {
            if (outputArray is byte[] zeroData_u8)
            {
                ippsZero_8u(zeroData_u8, N);
            }
            else if (outputArray is short[] zeroData_s16)
            {
                ippsZero_16s(zeroData_s16, N);
            }
            else if (outputArray is int[] zeroData_s32)
            {
                ippsZero_32s(zeroData_s32, N);
            }
            else if (outputArray is float[] zeroData_f32)
            {
                ippsZero_32f(zeroData_f32, N);
            }
            else if (outputArray is double[] zeroData_f64)
            {
                ippsZero_64f(zeroData_f64, N);
            }
            else if (outputArray is Complex32[] zeroData_fc32)
            {
                GCHandle output_GC = GCHandle.Alloc(zeroData_fc32, GCHandleType.Pinned);
                IntPtr output_address = output_GC.AddrOfPinnedObject();
                ippsZero_32fc(output_address, N);
                output_GC.Free();
            }
            else if (outputArray is Complex[] zeroData_fc64)
            {
                GCHandle output_GC = GCHandle.Alloc(zeroData_fc64, GCHandleType.Pinned);
                IntPtr output_address = output_GC.AddrOfPinnedObject();
                ippsZero_64fc(output_address, N);
                output_GC.Free();
            }
            else { throw new Exception("Data type not supported"); }
        }

        /// <summary>
        /// 生成全零数组
        /// </summary>
        public static void ZeroInit<T>(int N, IntPtr outputArray)
        {
            T[] datatype = new T[1];

            if (datatype is byte[])
            {
                ippsZero_8u(outputArray, N);
            }
            else if (datatype is short[])
            {
                ippsZero_16s(outputArray, N);
            }
            else if (datatype is int[])
            {
                ippsZero_32s(outputArray, N);
            }
            else if (datatype is float[])
            {
                ippsZero_32f(outputArray, N);
            }
            else if (datatype is double[])
            {
                ippsZero_64f(outputArray, N);
            }
            else if (datatype is Complex32[])
            {
                ippsZero_32fc(outputArray, N);
            }
            else if (datatype is Complex[])
            {
                ippsZero_64fc(outputArray, N);
            }
            else { throw new Exception("Data type not supported"); }
        }

        #endregion

        #region ---- Ramp ----

        /// <summary>
        /// 生成一阶线性 double 数组
        /// </summary>
        public static double[] RampInit(int N, double delta, double start = 0.0)
        {
            double[] doubleData = new double[N];

            ippsVectorSlope_64f(doubleData, N, start, delta);

            return doubleData;
        }

        /// <summary>
        /// 生成一阶线性 float 数组
        /// </summary>
        public static float[] RampInit(int N, float delta, float start = 0.0f)
        {
            float[] floatData = new float[N];

            ippsVectorSlope_32f(floatData, N, start, delta);

            return floatData;
        }

        #endregion

        #region ---- Tone ----

        /// <summary>
        /// 生成 Complex 单频数组
        /// </summary>
        public static Complex[] ToneInit(int N, double frequency, double theta = 0.0, double amplitude = 1.0)
        {
            return (ToneInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Complex 单频数组
        /// </summary>
        public static Complex[] ToneInit(int N, double frequency, ref double theta, double amplitude = 1.0)
        {
            Complex[] complexData = new Complex[N];
            ToneInit(N, complexData, frequency, ref theta, amplitude);
            return complexData;
        }

        /// <summary>
        /// 生成 Complex 单频数组
        /// </summary>
        public static void ToneInit(int N, Complex[] output, double frequency, ref double theta, double amplitude = 1.0)
        {
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            // frequency = frequency % 1.0;
            frequency -= Math.Floor(frequency);
            // theta -= ((Math.Floor(theta / (Math.PI * 2.0))) * (Math.PI * 2.0));
            ippsTone_64fc(output_address, N, amplitude, frequency, ref theta, 0);

            output_GC.Free();
        }

        /// <summary>
        /// 生成 Complex32 单频数组
        /// </summary>
        public static Complex32[] ToneInit(int N, float frequency, float theta = 0.0f, float amplitude = 1.0f)
        {
            return (ToneInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Complex32 单频数组
        /// </summary>
        public static Complex32[] ToneInit(int N, float frequency, ref float theta, float amplitude = 1.0f)
        {
            Complex32[] complexData = new Complex32[N];
            ToneInit(N, complexData, frequency, ref theta, amplitude);
            return complexData;
        }

        /// <summary>
        /// 生成 Complex32 单频数组
        /// </summary>
        public static void ToneInit(int N, Complex32[] output, float frequency, ref float theta, float amplitude = 1.0f)
        {
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            // frequency = frequency % 1.0f;
            frequency -= (float)(Math.Floor(frequency));
            ippsTone_32fc(output_address, N, amplitude, frequency, ref theta, 0);

            output_GC.Free();
        }

        /// <summary>
        /// 生成单频数组
        /// </summary>
        public static T[] ToneInit<T>(int N, double frequency, double theta = 0.0, double amplitude = 1.0)
        {
            T[] result = new T[N];
            if (result is Complex32[] result_fc32)
            {
                float theta_float = (float)theta;
                ToneInit(N, result_fc32, (float)frequency, ref theta_float, (float)amplitude);
            }
            else if (result is Complex[] result_fc64)
            {
                ToneInit(N, result_fc64, frequency, ref theta, amplitude);
            }
            else { throw new Exception("Data type not supported"); }

            return result;
        }

        #endregion

        #region ---- Cosine ----

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static double[] CosineInit(int N, double frequency, double theta = 0.0, double amplitude = 1.0)
        {
            return (CosineInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static double[] CosineInit(int N, double frequency, ref double theta, double amplitude = 1.0)
        {
            double[] cosineData = new double[N];
            ippsTone_64f(cosineData, N, amplitude, frequency, ref theta, 0);
            return cosineData;
        }

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static float[] CosineInit(int N, float frequency, float theta = 0.0f, float amplitude = 1.0f)
        {
            return (CosineInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static float[] CosineInit(int N, float frequency, ref float theta, float amplitude = 1.0f)
        {
            float[] cosineData = new float[N];
            ippsTone_32f(cosineData, N, amplitude, frequency, ref theta, 0);
            return cosineData;
        }

        #endregion

        #region ---- Sine ----

        /// <summary>
        /// 生成 Sine 
        /// </summary>
        public static double[] SineInit(int N, double frequency, double theta = 0.0, double amplitude = 1.0)
        {
            return (SineInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Sine 
        /// </summary>
        public static double[] SineInit(int N, double frequency, ref double theta, double amplitude = 1.0)
        {
            double[] cosineData = new double[N];
            theta -= (Math.PI / 2.0);
            theta -= ((Math.Floor(theta / (Math.PI * 2.0))) * (Math.PI * 2.0));
            ippsTone_64f(cosineData, N, amplitude, frequency, ref theta, 0);
            theta += (Math.PI / 2.0);
            return cosineData;
        }

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static float[] SineInit(int N, float frequency, float theta = 0.0f, float amplitude = 1.0f)
        {
            return (SineInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Cosine 
        /// </summary>
        public static float[] SineInit(int N, float frequency, ref float theta, float amplitude = 1.0f)
        {
            float[] cosineData = new float[N];
            double thedaDouble = theta;
            thedaDouble -= (Math.PI / 2.0);
            thedaDouble -= ((Math.Floor(theta / (Math.PI * 2.0))) * (Math.PI * 2.0));
            theta = (float)thedaDouble;

            ippsTone_32f(cosineData, N, amplitude, frequency, ref theta, 0);

            thedaDouble += (Math.PI / 2.0);
            theta = (float)thedaDouble;

            return cosineData;
        }

        #endregion

        #region ---- UniformNoise ----

        /// <summary>
        /// 生成均一白噪声 double 数组
        /// </summary>
        public static double[] UniformNoise(int N, double low = 0.0, double high = 1.0, uint seed = 0xD6BF7DF2)
        {
            double[] doubleData = new double[N];

            int bufferSize = 0;
            ippsRandUniformGetSize_64f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandUniformInit_64f(state_address, low, high, seed);
            ippsRandUniform_64f(doubleData, N, state_address);

            state_GC.Free();

            return doubleData;
        }

        /// <summary>
        /// 生成均一白噪声 float 数组
        /// </summary>
        public static float[] UniformNoise(int N, float low = 0.0f, float high = 1.0f, uint seed = 0xD6BF7DF2)
        {
            float[] floatData = new float[N];

            int bufferSize = 0;
            ippsRandUniformGetSize_32f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandUniformInit_32f(state_address, low, high, seed);
            ippsRandUniform_32f(floatData, N, state_address);

            state_GC.Free();

            return floatData;
        }

        /// <summary>
        /// 生成均一白噪声 Complex 数组
        /// </summary>
        public static Complex[] UniformNoise(int N, Complex center, double range = 0.5, uint seed = 0xD6BF7DF2)
        {
            Complex[] complexData = new Complex[N];

            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            int bufferSize = 0;
            ippsRandUniformGetSize_64f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandUniformInit_64f(state_address, -range, range, seed);
            ippsRandUniform_64f(complex_address, N * 2, state_address);

            ippsAddC_64fc_I(center, complex_address, N);

            state_GC.Free();
            complex_GC.Free();

            return complexData;
        }

        /// <summary>
        /// 生成均一白噪声 Complex32 数组
        /// </summary>
        public static Complex32[] UniformNoise(int N, Complex32 center, float range = 0.5f, uint seed = 0xD6BF7DF2)
        {
            Complex32[] complexData = new Complex32[N];

            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            int bufferSize = 0;
            ippsRandUniformGetSize_32f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandUniformInit_32f(state_address, -range, range, seed);
            ippsRandUniform_32f(complex_address, N * 2, state_address);

            ippsAddC_32fc_I(center, complex_address, N);

            state_GC.Free();
            complex_GC.Free();

            return complexData;
        }

        #endregion

        #region ---- GaussNoise ----

        /// <summary>
        /// 生成高斯白噪声 double 数组
        /// </summary>
        public static double[] GaussNoise(int N, double mean = 0.0, double deviation = 1.0, uint seed = 0xD6BF7DF2)
        {
            double[] doubleData = new double[N];

            int bufferSize = 0;
            ippsRandGaussGetSize_64f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandGaussInit_64f(state_address, mean, deviation, seed);
            ippsRandGauss_64f(doubleData, N, state_address);

            state_GC.Free();

            return doubleData;
        }

        /// <summary>
        /// 生成高斯白噪声 float 数组
        /// </summary>
        public static float[] GaussNoise(int N, float mean = 0.0f, float deviation = 1.0f, uint seed = 0xD6BF7DF2)
        {
            float[] floatData = new float[N];

            int bufferSize = 0;
            ippsRandGaussGetSize_32f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandGaussInit_32f(state_address, mean, deviation, seed);
            ippsRandGauss_32f(floatData, N, state_address);

            state_GC.Free();

            return floatData;
        }

        /// <summary>
        /// 生成高斯白噪声 Complex 数组
        /// </summary>
        public static Complex[] GaussNoise(int N, Complex center, double deviation = 1.0, uint seed = 0xD6BF7DF2)
        {
            Complex[] complexData = new Complex[N];

            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            int bufferSize = 0;
            ippsRandGaussGetSize_64f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandGaussInit_64f(state_address, 0, deviation, seed);
            ippsRandGauss_64f(complex_address, N * 2, state_address);

            ippsAddC_64fc_I(center, complex_address, N);

            state_GC.Free();
            complex_GC.Free();

            return complexData;
        }

        /// <summary>
        /// 生成高斯白噪声 Complex32 数组
        /// </summary>
        public static Complex32[] GaussNoise(int N, Complex32 center, float deviation = 1.0f, uint seed = 0xD6BF7DF2)
        {
            Complex32[] complexData = new Complex32[N];

            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            int bufferSize = 0;
            ippsRandGaussGetSize_32f(ref bufferSize);
            byte[] state = new byte[bufferSize];

            GCHandle state_GC = GCHandle.Alloc(state, GCHandleType.Pinned);
            IntPtr state_address = state_GC.AddrOfPinnedObject();

            ippsRandGaussInit_32f(state_address, 0, deviation, seed);
            ippsRandGauss_32f(complex_address, N * 2, state_address);

            ippsAddC_32fc_I(center, complex_address, N);

            state_GC.Free();
            complex_GC.Free();

            return complexData;
        }

        #endregion

        #region ---- Triangle ----

        /// <summary>
        /// 生成 Triangle 
        /// </summary>
        public static double[] TriangleInit(int N, double frequency, double theta = 0.0, double amplitude = 1.0)
        {
            return (TriangleInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Triangle 
        /// </summary>
        public static double[] TriangleInit(int N, double frequency, ref double theta, double amplitude = 1.0)
        {
            double[] triangleData = new double[N];
            ippsTriangle_64f(triangleData, N, amplitude, frequency, 0, ref theta);
            return triangleData;
        }

        /// <summary>
        /// 生成 Triangle 
        /// </summary>
        public static float[] TriangleInit(int N, float frequency, float theta = 0.0f, float amplitude = 1.0f)
        {
            return (TriangleInit(N, frequency, ref theta, amplitude));
        }

        /// <summary>
        /// 生成 Triangle 
        /// </summary>
        public static float[] TriangleInit(int N, float frequency, ref float theta, float amplitude = 1.0f)
        {
            float[] triangleData = new float[N];
            ippsTriangle_32f(triangleData, N, amplitude, frequency, 0, ref theta);
            return triangleData;
        }

        #endregion


        #region ---- Tone ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTone_32f(float[] pDst, int len, float magn, float rFreq, ref float pPhase, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTone_64f(double[] pDst, int len, double magn, double rFreq, ref double pPhase, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);


        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTone_32fc(IntPtr pDst, int len, float magn, float rFreq, ref float pPhase, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTone_64fc(IntPtr pDst, int len, double magn, double rFreq, ref double pPhase, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        #endregion

        #region ---- Slop ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsVectorSlope_32f(float[] pDst, int len, float offset, float slope);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsVectorSlope_64f(double[] pDst, int len, double offset, double slope);

        #endregion

        #region ---- Triangle ----

        // I16
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_16s(short[] pDst, int len, short magn, float rFreq, float asym, ref float pPhase);

        // I16Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_16sc(IntPtr pDst, int len, short magn, float rFreq, float asym, ref float pPhase);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_32f(float[] pDst, int len, float magn, float rFreq, float asym, ref float pPhase);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_32fc(IntPtr pDst, int len, float magn, float rFreq, float asym, ref float pPhase);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_64f(double[] pDst, int len, double magn, double rFreq, double asym, ref double pPhase);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsTriangle_64fc(IntPtr pDst, int len, double magn, double rFreq, double asym, ref double pPhase);

        #endregion

        #region---- Const ----

        // byte
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_8u(byte val, byte[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_8u(byte val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_8u(byte[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_8u(IntPtr pDst, int len);

        // short
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_16s(short val, short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_16s(short val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_16s(short[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_16s(IntPtr pDst, int len);

        // int
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_32s(int val, int[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_32s(int val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_32s(int[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_32s(IntPtr pDst, int len);

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_32f(float val, float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_32f(float val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_32f(float[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_32f(IntPtr pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_64f(double val, double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_64f(double val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_64f(double[] pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_64f(IntPtr pDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_32fc(Complex32 val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_32fc(IntPtr pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsSet_64fc(Complex val, IntPtr pDst, int len);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsZero_64fc(IntPtr pDst, int len);

        #endregion

        #region---- Uniform Noise ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniformGetSize_32f(ref int pRandUniformStateSize);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniformInit_32f(IntPtr pRandUniState, float low, float high, uint seed);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniform_32f([Out] float[] pDst, int len, IntPtr pRandUniState);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniform_32f(IntPtr pDst, int len, IntPtr pRandUniState);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniformGetSize_64f(ref int pRandUniformStateSize);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniformInit_64f(IntPtr pRandUniState, double low, double high, uint seed);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniform_64f([Out] double[] pDst, int len, IntPtr pRandUniState);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandUniform_64f(IntPtr pDst, int len, IntPtr pRandUniState);

        #endregion

        #region---- Gauss Noise ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGaussGetSize_32f(ref int pRandGaussStateSize);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGaussInit_32f(IntPtr pRandGaussState, float mean, float stdDev, uint seed);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGauss_32f(float[] pDst, int len, IntPtr pRandGaussState);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGauss_32f(IntPtr pDst, int len, IntPtr pRandGaussState);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGaussGetSize_64f(ref int pRandGaussStateSize);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGaussInit_64f(IntPtr pRandGaussState, double mean, double stdDev, uint seed);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGauss_64f(double[] pDst, int len, IntPtr pRandGaussState);

        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRandGauss_64f(IntPtr pDst, int len, IntPtr pRandGaussState);

        #endregion

    }
}
