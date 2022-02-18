using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharpTools.JXI.Numerics
{
    /// <summary>
    /// 数值类型处理库。
    /// </summary>
    public static class NumericsUtility
    {

        /// <summary>
        /// 将short(I16) interleaved IQ转换为Complex IQ。
        /// </summary>
        /// <param name="source">I/Q交替（Interleaved）存放的IQ。</param>
        /// <param name="scale">转换为Complex时的线性因子，非dB。</param>
        /// <param name="destination"></param>
        public static void ConvertComplexIQ(short[] source, double scale, Complex[] destination)
        {
            // 检查输入参数。
            if (source.Length % 2 != 0) { throw new ArgumentException("Interleaved IQ array length must be even."); }
            if ((source.Length / 2) != destination.Length) { throw new ArgumentException("Source and destination array length not match."); }

            // 逐点转换。
            for (int i = 0; i < source.Length / 2; i++) { destination[i] = new Complex(source[i * 2] * scale, source[i * 2 + 1] * scale); }
        }

        /// <summary>
        /// 将Complex IQ转换为short(I16) interleaved IQ。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="scale"></param>
        /// <param name="destination"></param>
        public static void ConvertComplexIQ(Complex[] source, double scale, short[] destination)
        {
            // 检查输入参数。
            if (destination.Length % 2 != 0) { throw new ArgumentException("Interleaved IQ array length must be even."); }
            if ((destination.Length / 2) != source.Length) { throw new ArgumentException("Source and destination array length not match."); }

            // 逐点转换。
            for (int i = 0; i < source.Length; i++)
            {
                destination[i * 2] = (short)(source[i].Real * scale);
                destination[i * 2 + 1] = (short)(source[i].Imaginary * scale);
            }
        }

    }

    /// <summary>
    /// 数值范围类型，包含一对浮点数值Max/Min，提供Coerce(...)，IsInRange(...)等方法。
    /// </summary>
    public class NumericRange
    {
        #region------------------------- 公共属性 -------------------------

        /// <summary>
        /// 最大值
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Min { get; set; }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        public NumericRange() : this(double.MinValue, double.MaxValue) {; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public NumericRange(double min, double max)
        {
            this.Min = Math.Min(min, max);
            this.Max = Math.Max(min, max);
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 将输入数值限制到[Min, Max]之间
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="coercedValue"></param>
        /// <returns></returns>
        public bool Coerce(double inputValue, out double coercedValue)
        {
            bool isInputValueInRange = true;
            coercedValue = inputValue;

            if (inputValue > this.Max)
            {
                coercedValue = this.Max;
                isInputValueInRange = false;
            }

            if (inputValue < this.Min)
            {
                coercedValue = this.Min;
                isInputValueInRange = false;
            }

            return isInputValueInRange;
        }

        /// <summary>
        /// 将输入数值限制到[Min, Max]之间
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public double Coerce(double inputValue)
        {
            return Math.Max(Math.Min(inputValue, this.Max), this.Min);
        }

        /// <summary>
        /// 判断输入数值是否在[Min, Max]之间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsInRange(double value)
        {
            return (value >= this.Min && value <= this.Max);
        }

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(NumericRange source)
        {
            this.Max = source.Max;
            this.Min = source.Min;
        }

        #endregion

    }

    /// <summary>
    /// Math 扩展
    /// </summary>
    public static class MathExtension  //2022-02-01 来自VST\Digital Signal Processing\Math Utility
    {
        /// <summary>
        /// Sinc
        /// </summary>
        public static double Sinc(double x)
        {
            return (x == 0) ? 1 : (Math.Sin(x) / x);    // 特别的： Sin(0) / 0 = 1 
        }
    }

}

