using System;
using System.Numerics;
using System.Reflection;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.SignalProcessing
{
    /// <summary>
    /// Resample class
    /// <para>Simplified Chinese:重采样</para>
    /// </summary>
    public static class EasyResample
    {

        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// constructor, execute when first call any method
        /// </summary>
        static EasyResample()
        {
            var bCofficient = Properties.Resource.Coefficient;//内插滤波器系数
            coef = new double[bCofficient.Length / sizeof(double) - 2];
            Buffer.BlockCopy(bCofficient, 2 * sizeof(double), coef, 0, bCofficient.Length - 2 * sizeof(double));//前两个数为什么不要？
            interpFactor = (int)BitConverter.ToDouble(bCofficient, 0);
            settlePoints = (coef.Length - 1) / 2 / interpFactor;//稳定时间
        }
        #endregion

        #region-----------------------Private Fileds-------------------------------------
        /// <summary>
        /// time of interpolation
        /// </summary>
        private static int interpFactor;

        /// <summary>
        /// number of settle points
        /// </summary>
        private static int settlePoints;

        /// <summary>
        /// coef
        /// </summary>
        private static double[] coef;

        #endregion

        #region-----------------------------Public Methods-------------------------------

        /// <summary>
        /// <para>Resample</para>
        /// <para>Simplified Chinese:重采样</para>
        /// </summary>
        /// <param name="data">data contains the input signal for resampling. The sampling interval of data is 1. </param>
        /// <param name="delay">delay in Sample points</param>
        /// <param name="dt"> specifies the sampling interval for outputWave</param>
        /// <returns></returns>
        public static double[] ResampleWaveform(double[] data, double delay, double dt)
        {
            if (data.Length < 170 || dt==0)
            {
                throw new JXIParamException("Please input more samples,make sure dt!=0");
            }
            int numOfChannels = data.GetLength(0);//通道数
            int samplesPerChannel = data.Length;//每通道采集的点数
            int outputLength = (int)((samplesPerChannel - settlePoints * 2)/ dt);//输出的同步信号的长度
            double[] resampleData = new double[outputLength];//输出信号
            //double[] _t = new double[data.Length];
            for (int i =0 ; i < resampleData.Length ; i++)
            {
                var t = i * dt + delay+settlePoints ;
                resampleData[i] = InterpolateX(t, data);
            }
            return resampleData;
        }

        /// <summary>
        /// <para>Resample</para>
        /// <para>Simplified Chinese:重采样</para>
        /// </summary>
        /// <param name="data"> 
        /// <para>input data</para>
        /// <para>Simplified Chinese:输入信号</para>
        /// </param>
        /// <param name="delay">
        /// <para>delay in Sample points</para>
        /// <para>Simplified Chinese:延迟抽样点数</para>
        /// </param>
        /// <returns></returns>
        public static double[] ResampleWaveform(double[] data, double delay)
        {
            if (data.Length < 170)
            {
                throw new JXIParamException("Please input more samples");
            }
            int numOfChannels = data.GetLength(0);//通道数
            int samplesPerChannel = data.Length;//每通道采集的点数
            int outputLength = (int)(samplesPerChannel - settlePoints * 2);//输出的同步信号的长度
            double tPhase = 1 / (double)interpFactor;//相位
            double[] resampleData = new double[outputLength];//输出信号
            int filterIndexForXi = 0; //interpolation filter coef index for X[i]内插滤波器系数
            int filterIndexStart = 0;//滤波器的开始检索值
            int subsamplePhaseJ = 0;//
            int subsamplePhase = 0;//
            double t0 = 0;
            double tFromI = 0;
            int i1 = 0;
            int filterCenterIndex = (coef.Length - 1) / 2;//滤波器的中间检索值
            double filterScale = interpFactor;//滤波比例
            int iStart = 0;
            double[] Yi = new double[2];
            int m;
            double subphasePosition = 0;
            t0 = settlePoints + 1 - delay;
            i1 = (int)Math.Floor(t0);
            tFromI = (t0 - i1);
            subsamplePhaseJ = (int)Math.Floor(tFromI / tPhase);
            subphasePosition = tFromI / tPhase - subsamplePhaseJ;
            for (int i = i1; i < i1 + outputLength; i++)
            {
                int outputIndex = i - i1;
                for (int k = 0; k < 2; k++)
                {
                    subsamplePhase = subsamplePhaseJ + k;
                    filterIndexForXi = filterCenterIndex - subsamplePhase;
                    filterIndexStart = filterIndexForXi % interpFactor;
                    iStart = i - (int)Math.Floor(filterIndexForXi / (double)interpFactor);
                    Yi[k] = 0;
                    for (int l = 0; l < (coef.Length / interpFactor); l++)
                    {
                        m = iStart + l;
                        if (m >= 0 && m < samplesPerChannel)
                        {
                            Yi[k] += data[m] * coef[filterIndexStart + l * interpFactor];
                        }
                    }
                    resampleData[outputIndex] = ((Yi[1] - Yi[0]) * subphasePosition + Yi[0]) * filterScale;
                    //resampleData[outputIndex] = ((Yi[1] - Yi[0]) + Yi[0]) * filterScale;
                }
            }

            return resampleData;
        }

        /// <summary>
        /// <para>Resample</para>
        /// <para>Simplified Chinese:重采样</para>
        /// </summary>
        /// <param name="data">data contains the input signal for resampling. The sampling interval of data is 1. </param>
        /// <param name="delay">delay in Sample points</param>
        /// <param name="dt"> specifies the sampling interval for outputWave</param>
        /// <returns></returns>
        public static Complex [] ResampleWaveform(Complex[] data, double delay, double dt)
        {
            if (data.Length < 170 || dt == 0)
            {
                throw new JXIParamException("Please input more samples,make sure dt!=0");
            }
            int numOfChannels = data.GetLength(0);//通道数
            int samplesPerChannel = data.Length;//每通道采集的点数
            int outputLength = (int)((samplesPerChannel - settlePoints * 2) / dt);//输出的同步信号的长度
            Complex[] resampleData = new Complex[outputLength];//输出信号
            //double[] _t = new double[data.Length];
            for (int i = 0; i < resampleData.Length; i++)
            {
                var t = i * dt + delay + settlePoints;
                resampleData[i] = InterpolateX(t, data);
            }
            return resampleData;
        }

        #endregion

        #region-----------------------------Private Methods-------------------------------
        /// <summary>
        /// 插点
        /// </summary>
        /// <param name="t">时间</param>
        /// <param name="X">带插入的数据</param>
        /// <returns></returns>
        private static double InterpolateX(double t,double [] X)
        {
            double St = 0;
            double tPhase = 1.0 / (double)interpFactor;
            int i = (int)Math.Floor(t);
            double tToI = t - i;
            int j = (int)Math.Floor(tToI / tPhase);
            double subPhasePosition = tToI / tPhase - j;
            int filterCenterIndex = (int)Math.Floor(coef.Length / 2.0);
            int filterIndexForXi = 0;
            int filterIndexStart = 0;
            int i1 = 0, k = 0;
            double[] Xt = new double[2];

            for (int j1 = 0; j1 < 2; j1++)
            {
                filterIndexForXi = filterCenterIndex - (j + j1);
                filterIndexStart = filterIndexForXi % interpFactor;
                i1 = i - (filterIndexForXi / interpFactor);
                k = (coef.Length - filterIndexStart) / interpFactor;
                Xt[j1] = 0;
                for (int m = 0; m < k; m++)
                {
                    if (m >= 0 && i1 + m < X.Length )
                    {                       
                        Xt[j1] += X[i1 + m] * coef[filterIndexStart + m * interpFactor];
                    }
                }
                Xt[j1] *= interpFactor;
            }

            St = (Xt[1] - Xt[0]) * subPhasePosition + Xt[0];
            return (St);
        }

        /// <summary>
        /// 插点
        /// </summary>
        /// <param name="t">时间</param>
        /// <param name="X">带插入的数据</param>
        /// <returns></returns>
        private static Complex InterpolateX(double t, Complex[] X)
        {
            Complex St;
            double StReal = 0;
            double StIm = 0;
            double tPhase = 1.0 / (double)interpFactor;
            int i = (int)Math.Floor(t);
            double tToI = t - i;
            int j = (int)Math.Floor(tToI / tPhase);
            double subPhasePosition = tToI / tPhase - j;
            int filterCenterIndex = (int)Math.Floor(coef.Length / 2.0);
            int filterIndexForXi = 0;
            int filterIndexStart = 0;
            int i1 = 0, k = 0;
            double[] XtReal = new double[2];
            double[] XtIm = new double[2];

            for (int j1 = 0; j1 < 2; j1++)
            {
                filterIndexForXi = filterCenterIndex - (j + j1);
                filterIndexStart = filterIndexForXi % interpFactor;
                i1 = i - (filterIndexForXi / interpFactor);
                k = (coef.Length - filterIndexStart) / interpFactor;
                XtReal[j1] = 0;
                XtIm[j1] = 0;
                for (int m = 0; m < k; m++)
                {
                    if (m >= 0 && i1 + m < X.Length)
                    {
                        XtReal[j1] += X[i1 + m].Real  * coef[filterIndexStart + m * interpFactor];
                        XtIm[j1] += X[i1 + m].Imaginary  * coef[filterIndexStart + m * interpFactor];
                    }
                }
                XtReal[j1] *= interpFactor;
                XtIm[j1] *= interpFactor;
            }

            StReal = (XtReal[1] - XtReal[0]) * subPhasePosition + XtReal[0];
            StIm = (XtIm[1] - XtIm[0]) * subPhasePosition + XtIm[0];
            St = new Complex(StReal, StIm);
            return (St);
        }
        #endregion
    }
}
