using System;
using System.Linq;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public partial class Measurement
    {
        internal static double OccupiedBandwidthV2(double[] spectrumV2, SpectralInfo info, double percent = 99)
        {
            // 求总功率。
            double totalPowerV2 = 0;
            for (int j = 0; j < spectrumV2.Length; j++)
            {
                totalPowerV2 += spectrumV2[j];
            }

            // 占用带宽的上下频点对应的功率累计值为 0.5 * powerInPercent。
            double powerThreshold = totalPowerV2 * 0.5 * (100 - percent) / 100;

            // 从左边开始直至功率和>= 0.5 * powerInPercent，即为frequencyLow。
            int i;
            double powerSumV2 = 0;
            for (i = 0; i < spectrumV2.Length; i++)
            {
                powerSumV2 += spectrumV2[i];
                if (powerSumV2 >= powerThreshold) { break; }
            }
            double lowFreqIndex = i + 1 - (powerSumV2 - powerThreshold) / spectrumV2[i];


            // 从右边开始直至功率和>= 0.5 * powerInPercent，即为frequencyHigh。
            powerSumV2 = 0;
            for (i = spectrumV2.Length - 1; i > 0; i--)
            {
                powerSumV2 += spectrumV2[i];
                if (powerSumV2 >= powerThreshold) { break; }
            }

            double highFreqIndex = i + (powerSumV2 - powerThreshold) / spectrumV2[i];

            // 返回占用带宽。
            return (highFreqIndex - lowFreqIndex) * info.FreqDelta;
        }

        public static double OccupiedBandwidth(double[] spectrum, SpectralInfo info, double percent = 99)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            // 返回占用带宽。
            return OccupiedBandwidthV2(spectrumV2, info, percent);
        }

        public static double OccupiedBandwidth(float[] spectrum, SpectralInfo info, double percent = 99)
        {
            return OccupiedBandwidth(Vector.ConvertToDouble(spectrum), info, percent);
        }



        internal static double OccupiedBandwidthXdbV2(double[] spectrumV2, SpectralInfo info, double xdbValue = 26)
        {
            // 先获取频谱最大值。
            double levelThresholdV2 = spectrumV2.Max() / Math.Pow(10, xdbValue / 10);

            // 从左边开始直至电平大于Peak - xdbValue，即为frequencyLow。
            int i;
            for (i = 0; i < spectrumV2.Length; i++)
            {
                if (spectrumV2[i] >= levelThresholdV2) { break; }
            }
            double lowFreqIndex = (i == 0) ? i : (i - (spectrumV2[i] - levelThresholdV2) / (spectrumV2[i] - spectrumV2[i - 1]));

            // 从右边开始直至电平大于Peak - xdbValue，即为frequencyHigh。
            for (i = spectrumV2.Length - 1; i > 0; i--)
            {
                if (spectrumV2[i] >= levelThresholdV2) { break; }
            }
            double highFreqIndex = (i == spectrumV2.Length - 1) ? i : (i + (spectrumV2[i] - levelThresholdV2) / (spectrumV2[i] - spectrumV2[i + 1]));

            // 返回占用带宽。
            return (highFreqIndex - lowFreqIndex) * info.FreqDelta;
        }

        public static double OccupiedBandwidthXdb(double[] spectrum, SpectralInfo info, double xdbValue = 26)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            // 返回占用带宽。
            return OccupiedBandwidthXdbV2(spectrumV2, info, xdbValue);
        }

        public static double OccupiedBandwidthXdb(float[] spectrum, SpectralInfo info, double xdbValue = 26)
        {
            return OccupiedBandwidthXdb(Vector.ConvertToDouble(spectrum), info, xdbValue);
        }

    }
}
