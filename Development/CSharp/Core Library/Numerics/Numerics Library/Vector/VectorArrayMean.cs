using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        #region ---- Mean ----

        /// <summary>
        /// 对 double 数组求平均
        /// </summary>
        public static double ArrayMean(double[] a)
        {
            double mean;
            ippsMean_64f(a, a.Length, out mean);
            return mean;
        }

        /// <summary>
        /// 对 float 数组求平均
        /// </summary>
        public static float ArrayMean(float[] a)
        {
            float mean;
            ippsMean_32f(a, a.Length, out mean);
            return mean;
        }

        /// <summary>
        /// 对 Complex 数组求平均
        /// </summary>
        public static Complex ArrayMean(Complex[] a)
        {
            Complex mean;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMean_64fc(a_address, a.Length, out mean);

            a_GC.Free();
            return mean;
        }

        /// <summary>
        /// 对 Complex32 数组求平均
        /// </summary>
        public static Complex32 ArrayMean(Complex32[] a)
        {
            Complex32 mean;

            GCHandle a_GC = GCHandle.Alloc(a, GCHandleType.Pinned);
            IntPtr a_address = a_GC.AddrOfPinnedObject();

            ippsMean_32fc(a_address, a.Length, out mean);

            a_GC.Free();
            return mean;
        }

        #endregion

        #region ---- Phase Mean ----

        /// <summary>
        /// 对 float 相位数组求平均
        /// </summary>
        public static float ArrayPhaseMean(float[] phase)
        {
            float mean, weight;

            ArrayPhaseMeanWeight(phase, out mean, out weight);

            return mean;
        }

        /// <summary>
        /// 对 float 相位数组求平均, 向量和
        /// </summary>
        public static void ArrayPhaseMeanWeight(float[] phase, out float mean, out float weight)
        {
            int length = phase.Length;

            Complex32 vectorSum = ArraySum(PolarToComplex(ConstInit(length, 1.0f), phase));
            weight = (float)(vectorSum.Magnitude / length);
            float phaseOffset = (float) vectorSum.Phase;

            float[] tempPhaseArray = GetArrayAdd(phase, -phaseOffset);
            GetPhaseInRange(tempPhaseArray);

            mean = ArrayMean(tempPhaseArray) + phaseOffset;
            GetPhaseInRange(ref mean);
        }

        /// <summary>
        /// 对 double 相位数组求平均
        /// </summary>
        public static double ArrayPhaseMean(double[] phase)
        {
            double mean, weight;

            ArrayPhaseMeanWeight(phase, out mean, out weight);

            return mean;
        }

        /// <summary>
        /// 对 double 相位数组求平均, 向量和
        /// </summary>
        public static void ArrayPhaseMeanWeight(double[] phase,out double mean,out double weight)
        {
            int length = phase.Length;

            Complex vectorSum = ArraySum(PolarToComplex(ConstInit(length, 1.0), phase));
            weight = vectorSum.Magnitude / length;
            double phaseOffset = vectorSum.Phase;

            double[] tempPhaseArray = GetArrayAdd(phase, -phaseOffset);
            GetPhaseInRange(tempPhaseArray);

            mean = ArrayMean(tempPhaseArray) + phaseOffset;
            GetPhaseInRange(ref mean);
        }

        #region ---- Get Phase In Range ----
        private static void GetPhaseInRange(float[] phase)
        {
            for (int i = 0; i < phase.Length; i++)
            {
                GetPhaseInRange(ref phase[i]);
            }
        }

        private static float GetPhaseInRange  (float phase)
        {
            GetPhaseInRange(ref phase);
            return phase;
        }

        private static void GetPhaseInRange(ref float phase)
        {
            float duoPI = (float)(Math.PI * 2);
            phase %= duoPI;
            if (phase > Math.PI) { phase -= duoPI; }
            else if (phase < -Math.PI) { phase += duoPI; }
            return;
        }

        private static void GetPhaseInRange(double[] phase)
        {
            for (int i = 0; i < phase.Length; i++)
            {
                GetPhaseInRange(ref phase[i]);
            }
        }

        private static double GetPhaseInRange(double phase)
        {
            GetPhaseInRange(ref phase);
            return phase;
        }

        private static void GetPhaseInRange(ref double phase)
        {
            double duoPI = Math.PI * 2;
            phase %= duoPI;
            if (phase > Math.PI) { phase -= duoPI; }
            else if (phase < -Math.PI) { phase += duoPI; }
            return;
        }
        #endregion

        #endregion

        #region---- Mean: pMean = Mean(pSrc) ----

        // float
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMean_32f(float[] pSrc, int len, out float pMean, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // complex32
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMean_32fc(IntPtr pSrc, int len, out Complex32 pMean, IppHintAlgorithm hint = IppHintAlgorithm.ippAlgHintNone);

        // double
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMean_64f(double[] pSrc, int len, out double pMean);

        // complex
        [DllImport(ippDllName, CallingConvention = ippCallingConvertion, ExactSpelling = true, SetLastError = false)]
        private static extern int ippsMean_64fc(IntPtr pSrc, int len, out Complex pMean);

        #endregion
    }
}
