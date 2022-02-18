using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.SignalProcessing.Window
{
    /**************************************************
     * 窗函数
     * **********************************************/
    /// <summary>
    /// 窗函数
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// 获取窗数据
        /// </summary>
        /// <param name="WindowType">窗类型</param>
        /// <param name="windowdata">窗数据</param>
        /// <param name="CG">相干增益</param>
        /// <param name="ENBW">等效噪声带宽</param>
        /// <param name="windowType">对称窗还是周期窗 </param>
        public static void GetWindow(WindowType WindowType, ref double[] windowdata,
                                     out double CG, out double ENBW, WindowProperty windowType = WindowProperty.PeriodicWindow)
        {
            if (windowdata == null || windowdata.Length <= 0)
            {              
                throw new JXIUserBufferException("windowdata length is null!");
            }
            CG = 0;
            ENBW = 0;
            if (WindowType == WindowType.None)
            {
                for (int i = 0; i < windowdata.Length; i++)//产生单位窗函数
                {
                    windowdata[i] = 1.0;
                }
                CG = 1.0; //矩形窗的相干增益:1.0
                ENBW = 1.0;//矩形窗的等效噪声宽度:1.0
                return;
            }
            //else if (windowType == WindowType.Kaiser)
            //{
            //    double fs = windowdata.Length;
            //    double S1 = 0;
            //    double S2 = 0;
            //    for (int i = 0; i < windowdata.Length; i++)
            //    {
            //        windowdata[i] = ZeroOrderModifiedBesselFunction(Math.PI * winParam * Math.Sqrt(1 - Math.Pow((2 * i / windowdata.Length - 1), 2)), 10) /
            //                        ZeroOrderModifiedBesselFunction(Math.PI * winParam, 10);
            //        S1 += windowdata[i];
            //        S2 += Math.Pow(windowdata[i], 2);
            //    }
            //    CG = 1;
            //    ENBW = fs * S2 / Math.Pow(S1, 2);
            //return;
            //}
             
            double[] windowCoe=Window.GeneralizedWindowCoef(WindowType);
            Window.GeneralizedCosineWindow(windowCoe, ref windowdata, out CG, out ENBW, windowType);
        }

        /// <summary>
        /// 窗函数
        /// </summary>
        /// <param name="windowCoe">窗函数的系数</param>
        /// <param name="windowdata">窗数据</param>
        /// <param name="CG">相干增益</param>
        /// <param name="ENBW">等效噪声带宽</param>
        /// <param name="windowType">窗类型，默认周期窗</param>
        private static void GeneralizedCosineWindow(double[] windowCoe, ref double[] windowdata,
                                     out double CG, out double ENBW, WindowProperty windowType = WindowProperty.PeriodicWindow)
        {
            int order = windowCoe.Length;
            int size = windowdata.Length;
            int copyIndex = 0;
            if (windowType == WindowProperty.PeriodicWindow)
            {
                size = windowdata.Length;
                copyIndex = ((size + 1) / 2 + (size + 1) % 2);
            }
            else
            {
                size = windowdata.Length-1;
                copyIndex = ((size) / 2 + (size ) % 2);
            }
            double fs = size;
            double S1 = 0;
            double S2 = 0;
            double A1 = 2.0 * Math.PI / size;
            double A2 = 0;
            int sign = 1;
            for (int i = 0; i < ((size+1)/2+(size+1)%2); i++)//产生窗函数前半部分
            {
                windowdata[i] = 0;
                A2 = A1 * i;
                for (int j = 0; j < order; j++)
                {
                    if (j %2 == 0)
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }
                    windowdata[i] += sign * windowCoe[j] * Math.Cos(A2*j);
                }
            }
            for (int i = copyIndex; i < windowdata.Length; i++)//产生窗函数后半部分
            {
                windowdata[i] = windowdata[size - i];
            }

            for (int i = 0; i < size; i++)
            {
                S1 += windowdata[i];
                S2 += Math.Pow(windowdata[i], 2);
            }
            //CG=DC
            CG = windowCoe[0];
            //ENBW        
            ENBW = fs * S2 / Math.Pow(S1, 2);
        }

        /// <summary>
        /// 生成窗的参数
        /// </summary>
        private static double[] GeneralizedWindowCoef(WindowType windowType)
        {
            double[] windowCoe;
            switch (windowType)
            {
                //Hanning
                case WindowType.Hanning:
                    windowCoe = new double[] { 0.5, 0.5 };
                    break;
                //hamming
                case WindowType.Hamming:
                    windowCoe = new double[] { 0.54, 0.46 };
                    break;
                //Blackman_Harris
                case WindowType.Blackman_Harris:
                    windowCoe = new double[] { 0.42323, 0.49755, 0.07922 };
                    break;
                //Exact_Blackman
                case WindowType.Exact_Blackman:
                    windowCoe = new double[] { 0.42659, 0.49656, 0.076849 };
                    break;
                //Blackman
                case WindowType.Blackman:
                    windowCoe = new double[] { 0.42, 0.5, 0.08 };
                    break;
                //Four_Term_B_Harris
                case WindowType.Four_Term_B_Harris:
                    windowCoe = new double[] { 0.35875, 0.48829, 0.14128, 0.01168 };
                    break;
                case WindowType.Four_Term_Nuttal:
                    windowCoe = new double[] {0.355768,0.487396,0.144232,0.012604 };
                    break;
                case WindowType.Four_Term_Blackman_Nuttal:
                    windowCoe = new double[] { 3.635819267707608e-001, 4.891774371450171e-001, 1.365995139786921e-001, 1.064112210553003e-002};
                    break;
                case WindowType.Five_Term_Least_Sidelobe:
                    windowCoe = new double[] { 3.232153788877343e-001, 4.714921439576260e-001 , 1.755341299601972e-001 , 2.849699010614994e-002,1.261357088292677e-003};
                    break;
                case WindowType.Six_Term_Least_Sidelobe:
                    windowCoe = new double[] { 2.935578950102797e-001, 4.519357723474506e-001 , 2.014164714263962e-001 , 4.792610922105837e-002 ,5.026196426859393e-003,1.375555679558877e-004};
                    break;
                case WindowType.Three_Term_Nuttal:
                    windowCoe = new double[] {0.40897,0.5,0.09103};
                    break;
                case WindowType.Three_Term_Blackman_Nuttal:
                    windowCoe = new double[] {4.243800934609435e-001,4.973406350967378e-001, 7.827927144231873e-002};
                    break;
                case WindowType.Seven_Term_Least_Sidelobe:
                    windowCoe = new double[] { 2.712203605850388e-001, 4.334446123274422e-001, 2.180041228929303e-001, 6.578534329560609e-002, 1.076186730534183e-002, 7.700127105808265e-004, 1.368088305992921e-005};
                    break;
                case WindowType.Flat_Top_90D:
                    windowCoe = new double[] {0.209783021421, 0.407525336544, 0.281175959705,0.0924746634556, 0.00904101887418 };
                    break;
                case WindowType.Flat_Top_95:
                    windowCoe = new double[] {0.213640903311, 0.414108259879 , 0.278698873916 , 0.0860603241582 , 0.00749163873597 };
                    break;
                case WindowType.Six_Term_Flat_Top:
                    windowCoe = new double[] {0.192240452512, 0.37631789481 , 0.284144941765 , 0.122407781678, 0.0236146057221, 0.00127432351161 };
                    break;
                case WindowType.Seven_Term_Flat_Top:
                    windowCoe = new double[] {0.178153071078, 0.350534041444 , 0.281452647671 ,0.144524263157, 0.0402333021358, 0.00494169539905 , 0.000160979115026 };
                    break;
                default:
                    windowCoe= new double[] { 0.5, 0.5 };
                    break;
            }
            return windowCoe;
        }

        /// <summary>
        /// 变形零阶贝塞尔函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static double ZeroOrderModifiedBesselFunction(double x, double k)
        {
            double I0 = 0;

            for (int i = 0; i < k; i++)
            {
                I0 += Math.Pow(Math.Pow(x / 2, i) / factorial(i), 2);
            }
            return I0;
        }

        /// <summary>
        /// 求阶乘
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int factorial(int num)
        {
            if (num <= 1)
            {
                return 1;
            }
            else 
            {
                return num * factorial(num - 1);
            }
        }

        /// <summary>
        /// 窗类型：对称/周期
        /// </summary>
        public enum WindowProperty
        {
            /// <summary>
            /// 对称窗：常用于设计滤波器
            /// </summary>
            SymmetricWindow,

            /// <summary>
            /// 周期窗：常用于频谱计算
            /// </summary>
            PeriodicWindow,
        }
    }

    /// <summary>
    /// <para>window type</para>
    /// <para>Chinese Simplified: 窗类型</para>
    /// </summary>
    public enum WindowType : int //窗函数类型枚举
    {
        /// <summary>
        /// <para>Rectangle</para>
        /// <para>Chinese Simplified: 矩形窗</para>
        /// </summary>
        None,

        /// <summary>
        /// <para>Hanning (2-Term)</para>
        /// <para>Chinese Simplified: 汉宁窗</para>
        /// </summary>
        Hanning,

        /// <summary>
        /// <para>Hamming (2-Term)</para>
        /// <para>Chinese Simplified: 海明窗</para>
        /// </summary>
        Hamming,

        /// <summary>
        /// <para>Blackman Harris (3-Term)</para>
        /// <para>Chinese Simplified: 布莱克曼-哈里斯窗</para>
        /// </summary>
        Blackman_Harris,

        /// <summary>
        /// <para>Exact Blackman  (3-Term)</para>
        /// </summary>
        Exact_Blackman,

        /// <summary>
        /// <para>Blackman  (3-Term)</para>
        /// <para>Chinese Simplified: 布莱克曼窗</para>
        /// </summary>
        Blackman,

        /// <summary>
        /// 3_Term_Nuttal(3-Term with Continous First Derivative /Nuttal3a)
        /// </summary>
        Three_Term_Nuttal,

        /// <summary>
        /// 3_Term_Blackman_Nuttal(3-Term Minimum)
        /// </summary>
        Three_Term_Blackman_Nuttal,

        /// <summary>
        /// <para>4 Term B-Harris</para>
        /// </summary>																																						
        Four_Term_B_Harris,

        /// <summary>
        /// 4_Term_Nuttal(4-Term with Continous First Derivative /Nuttal4b)
        /// </summary>
        Four_Term_Nuttal,

        /// <summary>
        /// 4_Term_Blackman_Nuttal(4-Term Minimum)
        /// </summary>
        Four_Term_Blackman_Nuttal,

        /// <summary>
        /// <para>5-Term Flat Top(HFT95), the highest sidelobe is −95.0dB, located at f = ±7.49 bins. </para>
        /// <para>Chinese Simplified: 平顶窗</para>	
        /// </summary>
        Flat_Top_95,

        /// <summary>
        /// <para>5-Term Flat Top(HFT90D),the highest sidelobe is −90.2dB, located at f = ±5.58 bins</para>
        /// <para>Chinese Simplified: 平顶窗</para>	
        /// </summary>
        Flat_Top_90D,

        /// <summary>
        /// 5_Term_Least_Sidelobe(Low Sidelobe)
        /// </summary>
        Five_Term_Least_Sidelobe,

        /// <summary>
        /// 6_Term_Flat_Top(HFT116D),the highest sidelobe is −116.8dB, located at f = ±7.52 bins
        /// </summary>
        Six_Term_Flat_Top,

        /// <summary>
        /// 6_Term_Least_Sidelobe
        /// </summary>
        Six_Term_Least_Sidelobe,

        /// <summary>
        /// 7_Term_Flat_Top(HFT144D),the highest sidelobe is −144.1dB, located at f = ±7.07 bins.
        /// </summary>
        Seven_Term_Flat_Top,

        /// <summary>
        /// 7_Term_Least_Sidelobe(7-Term B-Harris)
        /// </summary>
        Seven_Term_Least_Sidelobe,


        ///// <summary>
        ///// <para>Kaiser</para>
        ///// <para>Chinese Simplified: 凯塞窗</para>	
        ///// </summary>
        //Kaiser,
    }

}
