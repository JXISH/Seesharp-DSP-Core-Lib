using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Generate Complex ----

        /// <summary>
        /// 由实部虚部生成 Complex 数组
        /// </summary>
        public static void RealImageToComplex(double[] real, double[] image, Complex[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsRealToCplx_64f(real, image, complex_address, real.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由实部虚部生成 Complex 数组
        /// </summary>
        public static Complex[] RealImageToComplex(double[] real, double[] image)
        {
            Complex[] complexData = new Complex[real.Length];
            RealImageToComplex(real, image, complexData);
            return complexData;
        }

        /// <summary>
        /// 由实部虚部生成 Complex32 数组
        /// </summary>
        public static void RealImageToComplex(float[] real, float[] image, Complex32[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsRealToCplx_32f(real, image, complex_address, real.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由实部虚部生成 Complex32 数组
        /// </summary>
        public static Complex32[] RealImageToComplex(float[] real, float[] image)
        {
            Complex32[] complexData = new Complex32[real.Length];
            RealImageToComplex(real, image, complexData);
            return complexData;
        }

        /// <summary>
        /// 由幅度相位生成 Complex 数组
        /// </summary>
        public static void PolarToComplex(double[] magitude, double[] phase, Complex[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsPolarToCart_64fc(magitude, phase, complex_address, magitude.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由幅度相位生成 Complex 数组
        /// </summary>
        public static void PolarToComplex( double[] phase, Complex[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsPolarToCart_64fc(ConstInit(phase.Length, 1.0), phase, complex_address, phase.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由幅度相位生成 Complex 数组
        /// </summary>
        public static Complex[] PolarToComplex(double[] magitude, double[] phase)
        {
            Complex[] complexData = new Complex[magitude.Length];
            PolarToComplex(magitude, phase, complexData);
            return complexData;
        }

        /// <summary>
        /// 由幅度相位生成 Complex 数组
        /// </summary>
        public static Complex[] PolarToComplex( double[] phase)
        {
            Complex[] complexData = new Complex[phase.Length];
            PolarToComplex(phase, complexData);
            return complexData;
        }

        /// <summary>
        /// 由幅度相位生成 Complex32 数组
        /// </summary>
        public static void PolarToComplex(float[] magitude, float[] phase, Complex32[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsPolarToCart_32fc(magitude, phase, complex_address, magitude.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由幅度相位生成 Complex32 数组
        /// </summary>
        public static void PolarToComplex(float[] phase, Complex32[] complexData)
        {
            GCHandle complex_GC = GCHandle.Alloc(complexData, GCHandleType.Pinned);
            IntPtr complex_address = complex_GC.AddrOfPinnedObject();

            ippsPolarToCart_32fc(ConstInit(phase.Length, 1.0f), phase, complex_address, phase.Length);

            complex_GC.Free();
        }

        /// <summary>
        /// 由幅度相位生成 Complex32 数组
        /// </summary>
        public static Complex32[] PolarToComplex(float[] magitude, float[] phase)
        {
            Complex32[] complexData = new Complex32[magitude.Length];
            PolarToComplex(magitude, phase, complexData);
            return complexData;
        }

        /// <summary>
        /// 由幅度相位生成 Complex32 数组
        /// </summary>
        public static Complex32[] PolarToComplex(float[] phase)
        {
            Complex32[] complexData = new Complex32[phase.Length];
            PolarToComplex(phase, complexData);
            return complexData;
        }

        #endregion

        #region ---- Conjugation ----

        /// <summary>
        /// 获取 Complex 共轭
        /// inout = conj (inout)
        /// </summary>
        public static void ComplexConjugate(Complex[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsConj_64fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 共轭
        /// output = conj (a)
        /// </summary>
        public static void ComplexConjugate(Complex[] a, Complex[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsConj_64fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 共轭
        /// renture = conj (a)
        /// </summary>
        public static Complex[] GetComplexConjugation(Complex[] a)
        {
            Complex[] output = new Complex[a.Length];
            ComplexConjugate(a, output);
            return output;
        }

        /// <summary>
        /// 获取 Complex32 共轭
        /// </summary>
        public static void ComplexConjugate(Complex32[] inout)
        {
            GCHandle inout_GC = GCHandle.Alloc(inout, GCHandleType.Pinned);
            IntPtr inout_address = inout_GC.AddrOfPinnedObject();

            ippsConj_32fc_I(inout_address, inout.Length);

            inout_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 共轭
        /// </summary>
        public static void ComplexConjugate(Complex32[] a, Complex32[] output)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();
            GCHandle output_GC = GCHandle.Alloc(output, GCHandleType.Pinned);
            IntPtr output_address = output_GC.AddrOfPinnedObject();

            ippsConj_32fc(a_address, output_address, a.Length);

            a_GC.Free();
            output_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 共轭
        /// renture = conj (a)
        /// </summary>
        public static Complex32[] GetComplexConjugation(Complex32[] a)
        {
            Complex32[] output = new Complex32[a.Length];
            ComplexConjugate(a, output);
            return output;
        }

        #endregion

        #region ---- Real ----

        /// <summary>
        /// 获取 Complex 数组实部
        /// </summary>
        public static void GetComplexReal(Complex[] a,double []real)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsReal_64fc(a_address, real, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组实部
        /// </summary>
        public static double[] GetComplexReal(Complex[] a)
        {
            double[] real = new double[a.Length];
            GetComplexReal(a, real);
            return real;
        }

        /// <summary>
        /// 获取 Complex32 数组实部
        /// </summary>
        public static void GetComplexReal(Complex32[] a, float[] real)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsReal_32fc(a_address, real, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组实部
        /// </summary>
        public static float[] GetComplexReal(Complex32[] a)
        {
            float[] real = new float[a.Length];
            GetComplexReal(a, real);
            return real;
        }

        #endregion

        #region ---- Image ----

        /// <summary>
        /// 获取 Complex 数组虚部
        /// </summary>
        public static void GetComplexImage(Complex[] a, double[] image)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsImag_64fc(a_address, image, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组虚部
        /// </summary>
        public static double[] GetComplexImage(Complex[] a)
        {
            double[] image = new double[a.Length];
            GetComplexImage(a, image);
            return image;
        }

        /// <summary>
        /// 获取 Complex32 数组虚部
        /// </summary>
        public static void GetComplexImage(Complex32[] a, float[] image)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsImag_32fc(a_address, image, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组虚部
        /// </summary>
        public static float[] GetComplexImage(Complex32[] a)
        {
            float[] image = new float[a.Length];
            GetComplexImage(a, image);
            return image;
        }

        #endregion

        #region ---- Real & Image ----

        /// <summary>
        /// 获取 Complex 数组实部虚部
        /// </summary>
        public static void ComplexToRealImage(Complex[] a, double[] real, double[] image)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsCplxToReal_64fc(a_address, real, image, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组实部虚部
        /// </summary>
        public static void ComplexToRealImage(Complex32[] a, float[] real, float[] image)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsCplxToReal_32fc(a_address, real, image, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组实部虚部
        /// </summary>
        public static void ComplexToRealImage(double[] magitude, double[] phase, double[] real, double[] image)
        {
            ippsPolarToCart_64f(magitude, phase, real, image, magitude.Length);
        }

        /// <summary>
        /// 获取 Complex32 数组实部虚部
        /// </summary>
        public static void ComplexToRealImage(float[] magitude, float[] phase, float[] real, float[] image)
        {
            ippsPolarToCart_32f(magitude, phase, real, image, magitude.Length);
        }

        #endregion

        #region ---- Magitude ----

        /// <summary>
        /// 获取 Complex 数组幅度
        /// </summary>
        public static void GetComplexMagnitude(Complex[] a, double[] magitude)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMagnitude_64fc(a_address, magitude, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组幅度
        /// </summary>
        public static double[] GetComplexMagnitude(Complex[] a)
        {
            double[] magitude = new double[a.Length];
            GetComplexMagnitude(a, magitude);
            return magitude;
        }

        /// <summary>
        /// 获取 Complex32 数组幅度
        /// </summary>
        public static void GetComplexMagnitude(Complex32[] a, float[] magitude)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMagnitude_32fc(a_address, magitude, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组幅度
        /// </summary>
        public static float[] GetComplexMagnitude(Complex32[] a)
        {
            float[] magitude = new float[a.Length];
            GetComplexMagnitude(a, magitude);
            return magitude;
        }

        /// <summary>
        /// Complex 数组幅度
        /// </summary>
        public static void GetComplexMagnitude(double[] real, double[] image, double[] magitude)
        {
            ippsMagnitude_64f(real, image, magitude, real.Length);
        }

        /// <summary>
        /// Complex 数组幅度
        /// </summary>
        public static double[] GetComplexMagnitude(double[] real, double[] image)
        {
            double[] magitude = new double[real.Length];
            GetComplexMagnitude(real, image, magitude);
            return magitude;
        }

        /// <summary>
        /// Complex32 数组幅度
        /// </summary>
        public static void GetComplexMagnitude(float[] real, float[] image, float[] magitude)
        {
            ippsMagnitude_32f(real, image, magitude, real.Length);
        }

        /// <summary>
        /// Complex32 数组幅度
        /// </summary>
        public static float[] GetComplexMagnitude(float[] real, float[] image)
        {
            float[] magitude = new float[real.Length];
            GetComplexMagnitude(real, image, magitude);
            return magitude;
        }

        #endregion

        #region ---- Phase ----

        /// <summary>
        /// 获取 Complex 数组相位
        /// </summary>
        public static void GetComplexPhase(Complex[] a, double[] phase)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsPhase_64fc(a_address, phase, a.Length);
            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组相位
        /// </summary>
        public static double[] GetComplexPhase(Complex[] a)
        {
            double[] phase = new double[a.Length];
            GetComplexPhase(a, phase);
            return phase;
        }

        /// <summary>
        /// 获取 Complex 数组相位
        /// </summary>
        public static void GetComplexPhase(Complex32[] a, float[] phase)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsPhase_32fc(a_address, phase, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组相位
        /// </summary>
        public static float[] GetComplexPhase(Complex32[] a)
        {
            float[] phase = new float[a.Length];
            GetComplexPhase(a, phase);
            return phase;
        }

        /// <summary>
        /// 获取 Complex 数组相位
        /// </summary>
        public static void GetComplexPhase(double[] real, double[] image , double[] phase)
        {
            ippsPhase_64f(real, image, phase, real.Length);
        }

        /// <summary>
        /// 获取 Complex 数组相位
        /// </summary>
        public static double[] GetComplexPhase(double[] real, double[] image)
        {
            double[] phase = new double[real.Length];
            GetComplexPhase(real,image, phase);
            return phase;
        }

        /// <summary>
        /// 获取 Complex32 数组相位
        /// </summary>
        public static void GetComplexPhase(float[] real, float[] image, float[] phase)
        {
            ippsPhase_32f(real, image, phase, real.Length);
        }

        /// <summary>
        /// 获取 Complex32 数组相位
        /// </summary>
        public static float[] GetComplexPhase(float[] real, float[] image)
        {
            float[] phase = new float[real.Length];
            GetComplexPhase(real, image, phase);
            return phase;
        }

        #endregion

        #region ---- Magitude & Phase ----

        /// <summary>
        /// 获取 Complex 数组幅度相位
        /// </summary>
        public static void ComplexToMagitudePhase(Complex[] a, double[] magitude, double[] phase)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsCartToPolar_64fc(a_address, magitude, phase, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex32 数组幅度相位
        /// </summary>
        public static void ComplexToMagitudePhase(Complex32[] a, float[] magitude, float[] phase)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsCartToPolar_32fc(a_address, magitude, phase, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// 获取 Complex 数组幅度相位
        /// </summary>
        public static void ComplexToMagitudePhase(double[] real, double[] image, double[] magitude, double[] phase)
        {
            ippsCartToPolar_64f(real, image, magitude, phase, real.Length);
        }

        /// <summary>
        /// 获取 Complex32 数组幅度相位
        /// </summary>
        public static void ComplexToMagitudePhase(float[] real, float[] image, float[] magitude, float[] phase)
        {
            ippsCartToPolar_32f(real, image, magitude, phase, real.Length);
        }

        #endregion

        #region ---- Power ----

        /// <summary>
        /// Complex 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static void GetComplexPower(Complex[] a, double[] power)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsPowerSpectr_64fc(a_address, power, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// Complex 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static double[] GetComplexPower(Complex[] a)
        { 
            double[] power = new double[a.Length];
            GetComplexPower(a, power);
            return power;
        }

        /// <summary>
        /// Complex32 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static void GetComplexPower(Complex32[] a, float[] power)
        {
            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsPowerSpectr_32fc(a_address, power, a.Length);

            a_GC.Free();
        }

        /// <summary>
        /// Complex32 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static float[] GetComplexPower(Complex32[] a)
        {
            float[] power = new float[a.Length];
            GetComplexPower(a, power);
            return power;
        }

        /// <summary>
        /// Complex 数组取模的平方（即功率）
        /// power = real * real + image * image
        /// </summary>
        public static void GetComplexPower(double[] real, double[] image, double[] power)
        {
            ippsPowerSpectr_64f(real, image, power, real.Length);
        }

        /// <summary>
        /// Complex 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static double[] GetComplexPower(double[] real, double[] image)
        {
            double[] power = new double[real.Length];
            GetComplexPower(real, image, power);
            return power;
        }

        /// <summary>
        /// Complex32 数组取模的平方（即功率）
        /// power = real * real + image * image
        /// </summary>
        public static void GetComplexPower(float[] real, float[] image, float[] power)
        {
            ippsPowerSpectr_32f(real, image, power, real.Length);
        }

        /// <summary>
        /// Complex32 数组取模的平方（即功率）
        /// power = a.Re * a.Re + a.Im * a.Im
        /// </summary>
        public static float[] GetComplexPower(float[] real, float[] image)
        {
            float[] power = new float[real.Length];
            GetComplexPower(real, image, power);
            return power;
        }

        #endregion

        #region ---- Average Power ----

        /// <summary>
        /// Complex 数组平均功率
        /// </summary>
        public static double GetComplexAvgPower(Complex[] a)
        {
           return Vector.ArrayMean(GetComplexPower(a));
        }

        /// <summary>
        /// Complex32 数组平均功率
        /// </summary>
        public static float GetComplexAvgPower(Complex32[] a)
        {
            return Vector.ArrayMean(GetComplexPower(a));
        }

        #endregion

        #region---- Get Complex ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRealToCplx_32f(float[] pSrcRe, float[] pSrcIm, IntPtr pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsRealToCplx_64f(double[] pSrcRe, double[] pSrcIm, IntPtr pDst, int len);

        #endregion

        #region---- CartToPolar ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCartToPolar_32f(float[] pSrcRe, float[] pSrcIm, float[] pDstMagn, float[] pDstPhase, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCartToPolar_64f(double[] pSrcRe, double[] pSrcIm, double[] pDstMagn, double[] pDstPhase, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCartToPolar_32fc(IntPtr pSrc, float[] pDstMagn, float[] pDstPhase, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCartToPolar_64fc(IntPtr pSrc, double[] pDstMagn, double[] pDstPhase, int len);

        #endregion

        #region---- PolarToCart ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPolarToCart_32f(float[] pSrcMagn, float[] pSrcPhase, float[] pDstRe, float[] pDstIm, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPolarToCart_64f(double[] pSrcMagn, double[] pSrcPhase, double[] pDstRe, double[] pDstIm, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPolarToCart_32fc(float[] pSrcMagn, float[] pSrcPhase, IntPtr pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPolarToCart_64fc(double[] pSrcMagn, double[] pSrcPhase, IntPtr pDst, int len);

        #endregion

        #region---- Conj: pSrcDst = Conj(pSrc) ----
        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConj_32fc(IntPtr pSrc, IntPtr pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConj_64fc(IntPtr pSrc, IntPtr pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConj_32fc_I(IntPtr pSrcDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsConj_64fc_I(IntPtr pSrcDst, int len);

        #endregion

        #region---- Get Real ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsReal_32fc(IntPtr pSrc, float[] pDstRe, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsReal_64fc(IntPtr pSrc, double[] pDstRe, int len);

        #endregion

        #region---- Get Iamge ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsImag_32fc(IntPtr pSrc, float[] pDstIm, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsImag_64fc(IntPtr pSrc, double[] pDstIm, int len);

        #endregion

        #region---- Get Real & Image ----

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCplxToReal_32fc(IntPtr pSrc, float[] pDstRe, float[] pDstIm, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsCplxToReal_64fc(IntPtr pSrc, double[] pDstRe, double[] pDstIm, int len);

        #endregion

        #region---- Magnitude: pDst = magnitude (pSrc) ----
        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMagnitude_32fc(IntPtr pSrc, float[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMagnitude_32f(float[] pSrcRe, float[] pSrcIm, float[] pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMagnitude_64fc(IntPtr pSrc, double[] pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMagnitude_64f(double[] pSrcRe, double[] pSrcIm, double[] pDst, int len);

        #endregion

        #region---- Phase: pDst = phase(pSrc) ----

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPhase_32fc(IntPtr pSrc, float[] pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPhase_64fc(IntPtr pSrc, double[] pDst, int len);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPhase_32f(float[] pSrcRe, float[] pSrcIm, float[] pDst, int len);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPhase_64f(double[] pSrcRe, double[] pSrcIm, double[] pDst, int len);

        #endregion

        #region---- PowerSpectr: pDst = pSrcRe**2 + pSrcIm**2  ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPowerSpectr_32f(float[] pSrcRe, float[] pSrcIm, float[] pDst, int len);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPowerSpectr_64f(double[] pSrcRe, double[] pSrcIm, double[] pDst, int len);

        // Complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPowerSpectr_64fc(IntPtr pSrc, double[] pDst, int len);

        // Complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsPowerSpectr_32fc(IntPtr pSrc, float[] pDst, int len);

        #endregion
    }
}
