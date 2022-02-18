using System;
using SeeSharpTools.JXI.MKL;

namespace SeeSharpTools.JXI.Mathematics
{
    /// <summary>
    /// 公共函数类
    /// </summary>
    static class FittingCommon
    {

        /// <summary>
        /// 求数组所有元素之和
        /// </summary>
        /// <param name="data">数组</param>
        /// <param name="length">求和的长度</param>
        /// <param name="sum">和</param>
        static public void SumOfArray(double[] data, int length,out double sum)
        {
            sum = 0.0;
            for (int i = 0; i < length; i++)
            {
                sum += data[i];
            }
        }

        /// <summary>
        /// 对比x，y，按照最短一个限制两数组等长
        /// </summary>
        static public void Equilong(ref double[] x, ref double[] y, ref double[] yBestFit)
        {
            //int length;
            if (x.Length != y.Length)
            {
                if (x.Length > y.Length)
                {
                    Array.Resize(ref x, y.Length);
                    Array.Resize(ref yBestFit, y.Length);

                }
                else
                {
                    Array.Resize(ref y, x.Length);
                    Array.Resize(ref yBestFit, y.Length);
                }
            }
        }
        /// <summary>
        /// 对比x，y，按照最短一个限制两数组等长
        /// </summary>
        static public void Equilong(ref double[] x, ref double[] y)
        {
            //int length;
            if (x.Length != y.Length)
            {
                if (x.Length > y.Length)
                {
                    Array.Resize(ref x, y.Length);

                }
                else
                {
                    Array.Resize(ref y, x.Length);
                }
            }
        }
        /// <summary>
        /// 进行数据非零矫正，所有0幅度将通过计算机替换成无限接近0的非零数
        /// </summary>
        /// <param name="y"></param>
        static public double[] ZeroValidate(double[] y)
        {
            double SUBSTITUTE = 0.0000000001;
            Random r = new Random();
            bool polarity = false;
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == 0)
                {
                    if (polarity)
                    {
                        y[i] = SUBSTITUTE * r.Next(1, 2);
                        polarity = false;
                    }
                    else
                    {
                        y[i] = SUBSTITUTE * r.Next(1, 2);
                        polarity = true;
                    }

                }
            }
            return y;
        }

        /// <summary>
        /// 构造线性方程组的矩阵
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="order">阶数</param>
        /// <param name="a">构造后的左边矩阵</param>
        /// <param name="b">构造后的右边矩阵</param>
        static public void GeneratedMatrix(double[] x, double[] y, int order, double[] a, double[] b)
        {
            int _dataLenth = x.Length;
            /// <summary>
            /// 临时变量，幂矩阵
            /// </summary>
            double[] tempPow = new double[_dataLenth];

            /// <summary>
            /// 临时变量，乘积矩阵
            /// </summary>
            double[] tempMul = new double[_dataLenth];
            int _aRow = order + 1;
            int _aColumn = order + 1;
            int _bRow = order + 1;
            int _bColumn = 1;

            for (int i = 0; i < _aRow; i++)
            {
                for (int j = 0; j < _aColumn; j++)
                {
                    VMLNative.vdPowx(_dataLenth, x, Convert.ToDouble(i + j), tempPow);
                    FittingCommon.SumOfArray(tempPow, _dataLenth, out a[i + _aRow * j]);
                    //a[i + _aRow * j] = CBLASNative.cblas_dasum(_dataLenth, tempPow, 1);
                }
            }
            for (int i = 0; i < _bRow; i++)
            {
                for (int j = 0; j < _bColumn; j++)
                {
                    VMLNative.vdPowx(_dataLenth, x, Convert.ToDouble(i + j), tempPow);
                    VMLNative.vdMul(_dataLenth, tempPow, y, tempMul);
                    //b[i + _aRow * j] = CBLASNative.cblas_dasum(_dataLenth, tempMul, 1);
                    FittingCommon.SumOfArray(tempMul, _dataLenth, out b[i + _aRow * j]);
                }
            }

        }

        /// <summary>
        /// 计算某x[i]对应的指数拟合值
        /// </summary>
        /// <param name="independentVariablea">自变量</param>
        /// <param name="coefficient">多项式系数</param>
        /// <returns></returns>
        static public double CalculateY(double independentVariablea, double[] coefficients)
        {
            double Y = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                Y += coefficients[i] * System.Math.Pow(independentVariablea, i);
            }
            return Y;
        }
    }
}
