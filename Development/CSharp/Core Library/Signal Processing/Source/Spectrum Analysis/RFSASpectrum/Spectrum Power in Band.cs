using System;
using System.Linq;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public partial class Measurement
    {
        /// <summary>
        /// 计算信号总功率
        /// </summary>
        internal static double TotalPowerV2(double[] spectrumV2, SpectralInfo info)
        {
            double totalpowerV2 = Vector.ArraySum(spectrumV2);

            if (!info.PSD)
            {
                totalpowerV2 /= FFTWindow.GetWindowENBW(info.WindowType);
            }
            else
            {
                totalpowerV2 *= info.FreqDelta;
            }
            return totalpowerV2;
        }

        public static double TotalPower(double[] spectrum, SpectralInfo info)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            return Measurement.UnitConversion(TotalPowerV2 (spectrumV2, info), info.Unit);
        }

        public static double TotalPower(float[] spectrum, SpectralInfo info)
        {
            return TotalPower(Vector.ConvertToDouble(spectrum), info);
        }


        /// <summary>
        /// 计算带内功率
        /// </summary>
        internal static double PowerInBandV2(double[] spectrumV2, SpectralInfo info, double centerFreqeucny, double bandwidth)
        {
            #region ---- 将 df 归一化 ----

            double fc = centerFreqeucny / info.FreqDelta;
            double sp = bandwidth / info.FreqDelta;
            sp = Math.Max(sp, FFTWindow.GetWindowMainLobeWidth(info.WindowType)); // 带宽大于主瓣宽度
            double fstart = info.FreqStart / info.FreqDelta;
            double fend = spectrumV2.Length + fstart;

            #endregion

            #region ---- 计算开始频率的Index ----

            double startIndex = Math.Max(fstart, fc - sp / 2) - fstart;    // index >= 0        
            int nStartIndex = (int)Math.Floor(startIndex);
            double startReserve = startIndex - nStartIndex;

            #endregion

            #region ---- 计算终止频率的Index ----

            double endIndex = Math.Min(fend, fc + sp / 2) - fstart;  // index < array.size
            int nEndIndex = (int)Math.Floor(endIndex);
            double endReserve = endIndex - nEndIndex;
            endReserve = (nEndIndex == endIndex) ? 1 : endReserve;      // 如果找到数组最后一个元素，向内缩进。
            nEndIndex = (nEndIndex == endIndex) ? (nEndIndex - 1) : nEndIndex;

            #endregion

            #region ---- 计算功率和 ----

            double powerV2 = 0;
            for (int i = nStartIndex; i <= nEndIndex; i++)
            {
                powerV2 += spectrumV2[i];
            }
            powerV2 -= spectrumV2[nStartIndex] * startReserve;      // 边界点用线性插值
            powerV2 += spectrumV2[nEndIndex] * (endReserve - 1);

            #endregion

            if (!info.PSD)
            {
                powerV2 /= FFTWindow.GetWindowENBW(info.WindowType); 
            }
            else
            {
                powerV2 *= info.FreqDelta;
            }
            return powerV2;
        }

        public static double PowerInBand(double[] spectrum, SpectralInfo info, double centerFreqeucny, double bandwidth)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            return Measurement.UnitConversion(PowerInBandV2 (spectrumV2,info,centerFreqeucny,bandwidth), info.Unit);
        }

        public static double PowerInBand(float[] spectrum, SpectralInfo info, double centerFreqeucny, double bandwidth)
        {
            return PowerInBand(Vector.ConvertToDouble(spectrum), info, centerFreqeucny,bandwidth);
        }

    }

}
