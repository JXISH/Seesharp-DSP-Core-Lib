using System;
using System.Reflection;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.SignalProcessing
{
    /// <summary>
    /// Synchronizer class
    /// Simplified Chinese:同步类
    /// </summary>
    public static class Synchronization
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// constructor, execute when first call any method
        /// </summary>
        static Synchronization()
        {
            var bCofficient = Properties.Resource.Coefficient;//内插滤波器系数
            coef = new double[bCofficient.Length/sizeof(double) - 2];
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
        /// <para>Synchronize method that makes all the input channels simultaneous.</para>
        /// <para>This method only applies to bandlimited signal such as sinusoidal waveform.</para>
        /// <para>Note: </para>
        /// <para>1 Array size should be numberOfChannels * SamplesPerChannel.</para>
        /// <para>2 Size of return array will be smaller than input array because of settlepoints truncation.</para>
        /// <para>Simplified Chinese:同步输入信号(仅限带限信号，比如正弦信号)</para>
        /// <para> 注意：1数组的大小必须是通道数*每通道抽样点数</para>
        /// <para>       2由于使用了滤波器使得返回数组的大小小于输入信号</para>
        /// </summary>
        /// <param name="data">
        /// <para>input data  </para>
        /// <para>Simplified Chinese:输入信号</para>
        /// </param>
        /// <returns>
        /// <para>syncdata after resample and filtering  </para>
        /// <para>Simplified Chinese:同步后的信号</para>
        /// </returns>
        public static double[,] SyncWaveform(double[,] data)
        {
            int numOfChannels = data.GetLength(0);
            double dt = 1 / (double)numOfChannels;
            return SyncWaveform(data, dt);
        }

        /// <summary>
        /// <para>Synchronize method that makes all the input channels simultaneous.</para>
        /// <para>This method only applies to bandlimited signal such as sinusoidal waveform.</para>
        /// <para>Note: </para>
        /// <para>1 Array size should be numberOfChannels * SamplesPerChannel.</para>
        /// <para>2 Size of return array will be smaller than input array because of settlepoints truncation.</para>
        /// <para>Simplified Chinese:同步输入信号(仅限带限信号，比如正弦信号)</para>
        /// <para> 注意：1数组的大小必须是通道数*每通道抽样点数</para>
        /// <para>       2由于使用了滤波器使得返回数组的大小小于输入信号</para>
        /// </summary>
        /// <param name="data">
        /// <para>input data  </para>
        /// <para>Simplified Chinese:输入信号</para>
        /// </param>
        /// <param name="ChannelShiftSamples">
        /// <para>Channel to Channel delay in Sample points  </para>
        /// <para>Simplified Chinese:通道之间延迟的抽样点数</para>
        /// </param>
        /// <returns>
        /// <para>syncdata after resample and filtering  </para>
        /// <para>Simplified Chinese:同步后的信号</para>
        /// </returns>
        public static double[,] SyncWaveform(double[,] data, double ChannelShiftSamples)
        {
            if (data.GetLength(1) < 170)
            {
                throw new JXIParamException("Please input more samples");
            }
            int numOfChannels = data.GetLength(0);//通道数
            int samplesPerChannel = data.GetLength(1);//每通道采集的点数
            double dt = ChannelShiftSamples;//需要移位的点数
            int outputLength = samplesPerChannel - settlePoints * 2;//输出的同步信号的长度
            double tPhase = 1 / (double)interpFactor;//相位
            double[,] syncData = new double[numOfChannels, outputLength];//输出信号
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

            for (int chNum = 0; chNum < numOfChannels; chNum++)
            {
                t0 = settlePoints + 1 - chNum * dt;
                i1 = (int)Math.Floor(t0);
                tFromI = t0 - i1;
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
                                Yi[k] += data[chNum, m] * coef[filterIndexStart + l * interpFactor];
                            }
                        }
                        syncData[chNum, outputIndex] = ((Yi[1] - Yi[0]) * subphasePosition + Yi[0]) * filterScale;
                    }
                }
            }
            return syncData;
        }
        #endregion
    }
}
