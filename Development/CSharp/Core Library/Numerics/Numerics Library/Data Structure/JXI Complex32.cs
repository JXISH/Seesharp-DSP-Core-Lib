using System;
using System.Numerics;
using System.Runtime;

//Copied from Git\RF\VST\CSharp Source\Public\Digital Signal Processing
//and modified the namespace
namespace SeeSharpTools.JXI.Numerics
{
    /// <summary>
    /// 实部和虚部为float类型的复数
    /// </summary>
    public struct Complex32 : IEquatable<Complex32>, IFormattable
    {
        private float _real;
        private float _imaginary;

        /// <summary>
        /// 复数 0
        /// </summary>
        public static readonly Complex32 Zero = new Complex32(0.0f, 0.0f);

        /// <summary>
        /// 复数 1
        /// </summary>
        public static readonly Complex32 One = new Complex32(1.0f, 0.0f);

        /// <summary>
        /// 复数 0 + i
        /// </summary>
        public static readonly Complex32 ImaginaryOne = new Complex32(0.0f, 1.0f);

        /// <summary>
        /// 构造
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public Complex32(float real, float imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        /// <summary>
        /// 获取虚部
        /// </summary>
        public float Imaginary
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return _imaginary; }
        }

        /// <summary>
        /// 获取实部
        /// </summary>
        public float Real
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return _real; }
        }

        /// <summary>
        /// 获取幅度
        /// </summary>
        public double Magnitude
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return (new Complex(_real,_imaginary).Magnitude); }
        }

        /// <summary>
        /// 获取幅度平方
        /// </summary>
        public float MagnitudeSquared
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return (_real * _real) + (_imaginary * _imaginary); }
        }

        /// <summary>
        /// 获取相位
        /// </summary>
        public double Phase
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return Math.Atan2(_imaginary, _real); }
        }

        /// <summary>
        /// 获取模
        /// </summary>
        public static double Abs(Complex32 value)
        {
            return value.Magnitude;
        }

        /// <summary>
        /// 复数求和
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex32 Add(Complex32 left, Complex32 right)
        {
            return left + right;
        }

        /// <summary>
        /// 复数共轭
        /// </summary>
        public static Complex32 Conjugate(Complex32 value)
        {
            return new Complex32(value._real, 0.0f - value._imaginary);
        }

        /// <summary>
        /// 复数除法
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex32 Divide(Complex32 dividend, Complex32 divisor)
        {
            return dividend / divisor;
        }

        /// <summary>
        ///从点的极坐标创建复数。
        /// </summary>
        public static Complex32 FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex32((float)(magnitude * Math.Cos(phase)), (float)(magnitude * Math.Sin(phase)));
        }

        /// <summary>
        /// 复数乘法。
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex32 Multiply(Complex32 left, Complex32 right)
        {
            return left*right;
        }
        /// <summary>
        /// 复数求逆
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex32 Negate(Complex32 value)
        {
            return -value;
        }

        /// <summary>
        /// 复数减法
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static Complex32 Subtract(Complex32 left, Complex32 right)
        {
            return left - right;
        }

        /// <summary>
        /// 当前实例与指定的复数是否具有相同的值。
        /// </summary>
        public bool Equals(Complex32 value)
        {
            return (_real == value._real) && (_imaginary == value._imaginary);
        }

        /// <summary>
        /// 指示当前实例与指定的对象是否具有相同的值。
        /// </summary>
        public override bool Equals(object obj)
        {
            return (obj is Complex32) && Equals((Complex32)obj);
        }

        /// <summary>
        /// 返回当前对象的哈希代码。
        /// </summary>
        public override int GetHashCode()
        {
            int num = 99999997;
            int num2 = _real.GetHashCode() % num;
            int hashCode = _imaginary.GetHashCode();
            return num2 ^ hashCode;
        }

        /// <summary>
        ///  通过对当前复数的实部和虚部使用指定格式，将它的值转换为其采用笛卡尔形式的等效字符串表示形式。
        /// </summary>
        public string ToString(string format)
        {
            return string.Format(null, "{0}, {1} i", _real.ToString(format), _imaginary.ToString(format));
        }

        /// <summary>
        ///  通过对当前复数的实部和虚部使用指定格式，将它的值转换为其采用笛卡尔形式的等效字符串表示形式。
        /// </summary>
        public string ToString(IFormatProvider provider)
        {
            return string.Format(provider, "{0}, {1} i", _real, _imaginary);
        }

        /// <summary>
        ///  通过对当前复数的实部和虚部使用指定格式，将它的值转换为其采用笛卡尔形式的等效字符串表示形式。
        /// </summary>
        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format(provider, "{0}, {1} i", _real.ToString(format), _imaginary.ToString(format));
        }

        /// <summary>
        ///  通过对当前复数的实部和虚部使用指定格式，将它的值转换为其采用笛卡尔形式的等效字符串表示形式。
        /// </summary>
        public override string ToString()
        {
            return string.Format(null, "{0}, {1} i", _real, _imaginary);
        }

        /// <summary>
        /// 复数加法
        /// </summary>
        public static Complex32 operator +(Complex32 left, Complex32 right)
        {
            return new Complex32(left._real + right._real, left._imaginary + right._imaginary);
        }

        /// <summary>
        /// 复数加法逆元
        /// </summary>
        public static Complex32 operator -(Complex32 value)
        {
            return new Complex32(0.0f - value.Real, 0.0f - value.Imaginary);
        }

        /// <summary>
        /// 复数减法
        /// </summary>
        public static Complex32 operator -(Complex32 left, Complex32 right)
        {
            return new Complex32(left._real - right._real, left._imaginary - right._imaginary);
        }

        /// <summary>
        /// 复数乘法
        /// </summary>
        public static Complex32 operator *(Complex32 left, Complex32 right)
        {
            float real = left._real * right._real - left._imaginary * right._imaginary;
            float imaginary = left._imaginary * right._real + left._real * right._imaginary;
            return new Complex32(real, imaginary);
        }

        /// <summary>
        /// 复数除法
        /// </summary>
        public static Complex32 operator /(Complex32 left, Complex32 right)
        {
            float a = left._real * right._real + left._imaginary * right._imaginary;
            float b = left._imaginary * right._real - left._real * right._imaginary;
            float r2 = right._real * right._real + right._imaginary * right._imaginary;
            return new Complex32(a / r2, b / r2);
        }

        /// <summary>
        /// 复数相等
        /// </summary>
        public static bool operator ==(Complex32 left, Complex32 right)
        {
            return (left._real == right._real) && (left._imaginary == right._imaginary);
        }

        /// <summary>
        /// 复数不等
        /// </summary>
        public static bool operator !=(Complex32 left, Complex32 right)
        {
            if (left._real == right._real)
            {
                return left._imaginary != right._imaginary;
            }
            return true;
        }

        /// <summary>
        /// double 转换复数
        /// </summary>
        public static implicit operator Complex32(double value) { return new Complex32((float)value, 0); }

        /// <summary>
        /// float 转换复数
        /// </summary>
        public static implicit operator Complex32(float value) { return new Complex32(value, 0); }

        /// <summary>
        /// long 转换复数
        /// </summary>
        public static implicit operator Complex32(long value) { return new Complex32(value, 0); }

        /// <summary>
        /// int 转换复数
        /// </summary>
        public static implicit operator Complex32(int value) { return new Complex32(value, 0); }

        /// <summary>
        /// short 转换复数
        /// </summary>
        public static implicit operator Complex32(short value) { return new Complex32(value, 0); }

        /// <summary>
        /// decimal 转换复数
        /// </summary>
        public static explicit operator Complex32(decimal value) { return new Complex32((float)value, 0); }

        /// <summary>
        /// complex 转换复数
        /// </summary>
        public static explicit operator Complex32(Complex value) { return new Complex32((float)value.Real, (float)value.Imaginary); }
    }
}
