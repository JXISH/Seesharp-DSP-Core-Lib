using SeeSharpTools.JXI.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JXI.Mathematics.Geometry
{
    #region 笛卡尔坐标
    /// <summary>
    /// 笛卡尔坐标系，坐标和运算
    /// </summary>
    public struct Cartesian
    {
        /// <summary>
        /// 坐标x
        /// </summary>
        public double x;
        /// <summary>
        /// 坐标y
        /// </summary>
        public double y;
        /// <summary>
        /// 坐标z
        /// </summary>
        public double z;

        /// <summary>
        /// 创建0,0,0原点坐标
        /// </summary>
        public static readonly Cartesian Orgin = new Cartesian();
        /// <summary>
        /// 创建坐标，缺省为0,0,0
        /// </summary>
        public Cartesian(double X = 0.0, double Y = 0.0, double Z = 0.0)
        {
            x = X;
            y = Y;
            z = Z;
        }
        /// <summary>
        /// 坐标加法
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Cartesian operator +(Cartesian b, Cartesian c)
        {
            return new Cartesian(b.x + c.x, b.y + c.y, b.z + c.z);
        }
        /// <summary>
        /// 坐标减法
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Cartesian operator -(Cartesian b, Cartesian c)
        {
            return new Cartesian(b.x - c.x, b.y - c.y, b.z - c.z);
        }
        /// <summary>
        /// 三维矢量内积(分量积之和)
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double operator *(Cartesian b, Cartesian c)
        {
            return (b.x * c.x + b.y * c.y + b.z * c.z);
        }
        /// <summary>
        /// 三维矢量标量缩放
        /// </summary>
        /// <param name="b">矢量</param>
        /// <param name="scale">标量</param>
        /// <returns>缩放后的矢量</returns>
        public static Cartesian operator *(Cartesian b, double scale)
        {
            return new Cartesian(b.x * scale, b.y * scale, b.z * scale);
        }
        /// <summary>
        /// 三维向量积，也称叉积、外积 return = b ^ c in vector
        /// 向量积与两个求积向量所在平面垂直，遵守右手法则
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>b^c</returns>
        public static Cartesian CrossProduct(Cartesian b, Cartesian c)
        {
            return new Cartesian(b.y * c.z - b.z * c.y, b.z * c.x - b.x * c.z, b.x * c.y - b.y * c.x);
        }
        /// <summary>
        /// 点积，也称内积
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double DotProduct(Cartesian b, Cartesian c)
        {
            return (b.x * c.x + b.y * c.y + b.z * c.z);
        }
        /// <summary>
        /// 输入笛卡尔坐标转换输出球坐标
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Spherical ConverterToSpherical(Cartesian p)
        {
            Spherical result = new Spherical();
            result.theta = Math.Atan2(p.y, p.x);
            result.fai = Math.Atan2(p.z, Math.Sqrt(p.x * p.x + p.y * p.y));
            result.r = Math.Sqrt(p.x * p.x + p.y * p.y + p.z * p.z);
            return result;
        }
        /// <summary>
        /// 本实例的球坐标
        /// </summary>
        /// <returns></returns>
        public Spherical ConverterToSpherical()
        {
            return Cartesian.ConverterToSpherical(this);
        }
    }
    #endregion

    #region 球坐标
    /// <summary>
    /// 球坐标，按照数学界常用标记习惯，与ISO 31-11不同
    /// </summary>
    public class Spherical
    {
        /// <summary>
        /// 径向距离
        /// </summary>
        public double r;
        /// <summary>
        /// 方位角 逆时针为正, x+轴0, y+轴pi/2
        /// </summary>
        public double theta;    // 方位角 逆时针为正, x+轴0, y+轴pi/2
        /// <summary>
        /// 俯仰角 -pi/2 ~ pi/2, z+轴pi/2
        /// </summary>
        public double fai;      // 俯仰角 -pi/2 ~ pi/2, z+轴pi/2
        /// <summary>
        /// 返回原点
        /// </summary>
        public static readonly Spherical Orgin = new Spherical();
        /// <summary>
        /// 创建球坐标，缺省为原点
        /// </summary>
        /// <param name="R">径向距离</param>
        /// <param name="Theta">方位角 逆时针为正, x+轴0, y+轴pi/2</param>
        /// <param name="Fai">俯仰角 -pi/2 ~ pi/2, z+轴pi/2</param>
        public Spherical(double R = 0.0, double Theta = 0.0, double Fai = 0.0)
        {
            r = R;
            theta = Theta;
            fai = Fai;
        }
        /// <summary>
        /// 将输入球坐标转换到笛卡尔坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Cartesian ConvertToCartesian(Spherical point)
        {
            Cartesian result = new Cartesian();
            result.x = point.r * Math.Cos(point.fai) * Math.Cos(point.theta);
            result.y = point.r * Math.Cos(point.fai) * Math.Sin(point.theta);
            result.z = point.r * Math.Sin(point.fai);
            return result;
        }
        /// <summary>
        /// 将本实例坐标转换到笛卡尔坐标
        /// </summary>
        /// <returns></returns>
        public Cartesian ConvertToCartesian()
        {
            return Spherical.ConvertToCartesian(this);
        }
    }
    #endregion

    #region 余角和弧度角度
    /// <summary>
    /// 简单的Utilities
    /// </summary>
    public static class Scaling
    {
        /// <summary>
        /// 求余角
        /// 顺时针逆时针转换
        /// 顺时针0度为正北(y轴正方向)
        /// 逆时针0度为正东(x轴正方向)
        /// 单位弧度
        /// </summary>
        public static double ClockwiseExchange(double input)
        {
            return Math.PI / 2.0 - input;
        }

        /// <summary>
        /// 求余角，原位
        /// 顺时针逆时针转换
        /// 单位弧度
        /// </summary>
        public static void ClockwiseExchange(double[] inout)
        {
            Vector.ArraySub(Math.PI / 2.0, inout);
        }

        /// <summary>
        /// 求余角
        /// 顺时针逆时针转换
        /// 单位弧度
        /// </summary>
        public static double[] GetClockwiseExchange(double[] input)
        {
            return Vector.GetArraySub(Math.PI / 2.0, input);
        }

        /// <summary>
        /// 求余角
        /// 顺时针逆时针转换
        /// 顺时针0度为正北(y轴正方向)
        /// 逆时针0度为正东(x轴正方向)
        /// 单位角度
        /// </summary>
        public static double ClockwiseExchangeDegree(double input)
        {
            return 90.0 - input;
        }

        /// <summary>
        /// 求余角，原位
        /// 顺时针逆时针转换
        /// 单位角度
        /// </summary>
        public static void ClockwiseExchangeDegree(double[] inout)
        {
            Vector.ArraySub(90.0, inout);
        }

        /// <summary>
        /// 求余角
        /// 顺时针逆时针转换
        /// 单位角度
        /// </summary>
        public static double[] GetClockwiseExchangeDegree(double[] input)
        {
            return Vector.GetArraySub(90.0, input);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static double Rad2Degree(double input)
        {
            return input * (180.0 / Math.PI);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static void Rad2Degree(double[] inout)
        {
            Vector.ArrayScale(inout, 180.0 / Math.PI);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static double[] GetDegree(double[] input)
        {
            return Vector.GetArrayScale(input, 180.0 / Math.PI);
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static double Degree2Rad(double input)
        {
            return input * (Math.PI / 180.0);
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
        public static double[] GetRad(double[] input)
        {
            return Vector.GetArrayScale(input, Math.PI / 180.0);
        }
    }
    #endregion

}
