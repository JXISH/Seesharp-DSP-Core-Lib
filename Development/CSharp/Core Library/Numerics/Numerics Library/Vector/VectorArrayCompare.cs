using System;
using System.Numerics;
using System.Runtime.InteropServices;
//using JXI.RF.DSP.Numerics;

namespace SeeSharpTools.JXI.Numerics
{
    public partial class Vector
    {
        private const float Float_Precision = 1.0E-6f;
        private const double Double_Precision = 1.0E-12;

        #region ---- Compare Templete ----

        /// <summary>
        /// 比较 Array 是否相同
        /// </summary>
        public static bool ArrayEqual<T>(T[] a, T[] b)
        {
            if (a is short[] a_i16 && b is short[] b_i16)
            {
                return ArrayEqual(a_i16, b_i16);
            }
            else if (a is int[] a_i32 && b is int[] b_i32)
            {
                return ArrayEqual(a_i32, b_i32);
            }
            else if (a is float[] a_f32 && b is float[] b_f32)
            {
                return ArrayEqual(a_f32, b_f32);
            }
            else if (a is double[] a_f64 && b is double[] b_f64)
            {
                return ArrayEqual(a_f64, b_f64);
            }
            else if (a is Complex32[] a_fc32 && b is Complex32[] b_fc32)
            {
                return ArrayEqual(a_fc32, b_fc32);
            }
            else if (a is Complex[] a_fc64 && b is Complex[] b_fc64)
            {
                return ArrayEqual(a_fc64, b_fc64);
            }
            else { throw new Exception("Data type not supported"); }
        }
        #endregion

        #region ---- Compare ----

        /// <summary>
        /// 比较 short Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(short[] a, short[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较 I32 Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(int[] a, int[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较 float Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(float[] a, float[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > Math.Abs(a[i] * Float_Precision))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较 double Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(double[] a, double[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Abs(a[i] - b[i]) > Math.Abs(a[i] * Double_Precision))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较 Complex32 Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(Complex32[] a, Complex32[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (Complex32.Abs(a[i] - b[i]) > Complex32.Abs(a[i]) * Float_Precision)
                { return false; }
            }
            return true;
        }

        /// <summary>
        /// 比较 Complex Array 是否相同
        /// </summary>
        internal static bool ArrayEqual(Complex[] a, Complex[] b)
        {
            if (a.Length != b.Length) { return false; }

            for (int i = 0; i < a.Length; i++)
            {
                if (Complex.Abs(a[i] - b[i]) > Complex.Abs(a[i]) * Double_Precision)
                { return false; }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// double Array 比固定值小
        /// </summary>
        public static bool ArrayLess(double[] a, double b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= a[i] < b;
            }

            return result;
        }

        /// <summary>
        /// float Array 比固定值小
        /// </summary>
        public static bool ArrayLess(float[] a, float b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= a[i] < b;
            }

            return result;
        }

        /// <summary>
        /// double Array 的模比固定值小
        /// </summary>
        public static bool ArrayAbsLess(double[] a, double b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= Math.Abs(a[i]) < b;
            }

            return result;
        }

        /// <summary>
        /// float Array 的模比固定值小
        /// </summary>
        public static bool ArrayAbsLess(float[] a, float b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= Math.Abs(a[i]) < b;
            }

            return result;
        }

        /// <summary>
        /// float Array 比固定值大
        /// </summary>
        public static bool ArrayGreater(float[] a, float b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= a[i] < b;
            }

            return result;
        }

        /// <summary>
        /// double Array 比固定值大
        /// </summary>
        public static bool ArrayGreater(double[] a, double b)
        {
            bool result = true;
            for (int i = 0; i < a.Length; i++)
            {
                result &= Math.Abs(a[i]) < b;
            }

            return result;
        }

    }
}
