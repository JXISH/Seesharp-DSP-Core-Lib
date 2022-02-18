using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        /// <summary>
        /// Unwraps the Phase array by eliminating discontinuities whose absolute values exceed either pi or 180.
        /// </summary>
        public static T[] GetPhaseUnwrap<T>(T[] phase)
        {
            T[] result = new T[phase.Length];
            Vector.ArrayCopy(phase, result);
            if (result is float[] result_f32)
            {
                PhaseUnwrap(result_f32);
            }
            else if (result is double[] result_f64)
            {
                PhaseUnwrap(result_f64);
            }
            else { throw new Exception("Data type not supported"); }

            return result;
        }

        /// <summary>
        /// Unwraps the Phase array by eliminating discontinuities whose absolute values exceed either pi or 180.
        /// </summary>
        public static void PhaseUnwrap(double[] phase)
        {
            double prev = phase[0];
            double k = 0;
            for (int i = 1; i < phase.Length; i++)
            {
                k = Math.Floor((phase[i - 1] - phase[i] + Math.PI) / (Math.PI * 2.0));
                phase[i] += Math.PI * 2.0 * k;
            }
        }

        /// <summary>
        /// Unwraps the Phase array by eliminating discontinuities whose absolute values exceed either pi or 180.
        /// </summary>
        public static void PhaseUnwrap(float[] phase)
        {
            float prev = phase[0];
            double k = 0;
            for (int i = 1; i < phase.Length; i++)
            {
                k = Math.Floor((phase[i - 1] - phase[i] + Math.PI) / (Math.PI * 2.0));
                phase[i] +=(float)( Math.PI * 2.0 * k);
            }
        }




        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static double[] GetDegree(double[] radPhase)
        {
            return Vector.GetArrayScale(radPhase, 180.0 / Math.PI);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static void RadToDegree(double[] inout)
        {
            Vector.ArrayScale(inout, 180.0 / Math.PI);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static float[] GetDegree(float[] radPhase)
        {
            return Vector.GetArrayScale(radPhase, 180.0 / Math.PI);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static void RadToDegree(float[] inout)
        {
            Vector.ArrayScale(inout, 180.0 / Math.PI);
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static double[] GetRad(double[] degreePhase)
        {
            return Vector.GetArrayScale(degreePhase, Math.PI/180.0);
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static void Degree2Rad(double[] inout)
        {
            Vector.ArrayScale(inout, Math.PI / 180.0);
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static float[] GetRad(float[] degreePhase)
        {
            return Vector.GetArrayScale(degreePhase, Math.PI / 180.0);
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static void Degree2Rad(float[] inout)
        {
            Vector.ArrayScale(inout, Math.PI / 180.0);
        }

    }
}
