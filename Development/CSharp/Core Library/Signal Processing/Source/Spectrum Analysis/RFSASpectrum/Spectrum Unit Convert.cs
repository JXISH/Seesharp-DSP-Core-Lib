using System;
using System.Collections.Generic;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public partial class Measurement
    {
        /// <summary>
        /// 频谱单位转换函数
        /// </summary>
        public static void UnitConversion(double[] spectrum, SpectralInfo info, SpectrumUnit newUnit, bool psd = false)
        {
            if (info.Unit != SpectrumUnit.V2)
            {
                UnitConversionToV2(spectrum, info.Unit);
            }

            if (psd ^ info.PSD)                                                             // 输入输出是否为 PSD 不一致
            {
                double scale = FFTWindow.GetWindowENBW(info.WindowType) * info.FreqDelta;   // 输入为 PSD ，输出非PSD
                if (psd) { scale = 1.0 / scale; }                                           // 输出为 PSD ，输入非PSD
                
                Vector.ArrayScale(spectrum, scale);
            }

            UnitConversionV2ToOther(spectrum, newUnit);

            info.Unit = newUnit;
            info.PSD = psd;
        }

        public static void UnitConversion(float[] spectrum, SpectralInfo info, SpectrumUnit newUnit, bool psd = false)
        {
            if (info.Unit != SpectrumUnit.V2)
            {
                UnitConversionToV2(spectrum, info.Unit);
            }

            if (psd ^ info.PSD)                                                             // 输入输出是否为 PSD 不一致
            {
                double scale = FFTWindow.GetWindowENBW(info.WindowType) * info.FreqDelta;   // 输入为 PSD ，输出非PSD
                if (psd) { scale = 1.0 / scale; }                                           // 输出为 PSD ，输入非PSD

                Vector.ArrayScale(spectrum, scale);
            }

            UnitConversionV2ToOther(spectrum, newUnit);

            info.Unit = newUnit;
            info.PSD = psd;
        }

        public static void UnitConversion(double[] spectrum, SpectralInfo info,  double[] spectrumOut, SpectralInfo infoOut, SpectrumUnit unitOut, bool psd =false)
        {
            Vector.ArrayCopy(spectrum,  spectrumOut);
            infoOut.CopyFrom(info);

            UnitConversion(spectrumOut, infoOut, unitOut, psd);
        }

        public static void UnitConversion(float[] spectrum, SpectralInfo info, float[] spectrumOut, SpectralInfo infoOut, SpectrumUnit unitOut, bool psd = false)
        {
            Vector.ArrayCopy(spectrum,  spectrumOut);
            infoOut.CopyFrom(info);

            UnitConversion(spectrumOut, infoOut, unitOut, psd);
        }

        public static double UnitConversion(double data, SpectrumUnit unitOut, SpectrumUnit unitIn = SpectrumUnit.V2)
        {
            if (unitIn != SpectrumUnit.V2)
            {
                data = UnitConversionToV2(data, unitIn);
            }
            return UnitConversionV2ToOther(data, unitOut);
        }

        public static SpectrumPeak UnitConversion(SpectrumPeak inputPeak, SpectrumUnit unitOut, SpectrumUnit unitIn = SpectrumUnit.V2)
        {
            SpectrumPeak outputPeak = new SpectrumPeak(inputPeak);

            if (unitIn != SpectrumUnit.V2)
            {
                outputPeak.Level = UnitConversionToV2(outputPeak.Level, unitIn);
                outputPeak.MaxLevel = UnitConversionToV2(outputPeak.MaxLevel, unitIn);
            }

            outputPeak.Level = UnitConversionV2ToOther(outputPeak.Level, unitOut);
            outputPeak.MaxLevel = UnitConversionV2ToOther(outputPeak.MaxLevel, unitOut);

            return outputPeak;
        }

        public static List <SpectrumPeak> UnitConversion(List<SpectrumPeak> inputPeaks, SpectrumUnit unitOut, SpectrumUnit unitIn = SpectrumUnit.V2)
        {
            List<SpectrumPeak> outputPeaks = new List<SpectrumPeak>();

            foreach (SpectrumPeak peak in inputPeaks)
            {
                outputPeaks.Add(UnitConversion(peak, unitOut, unitIn));
            }

            return outputPeaks;
        }

        public static SpectrumThreshold UnitConversion(SpectrumThreshold inputThreshold, SpectrumUnit unitOut, SpectrumUnit unitIn = SpectrumUnit.V2)
        {
            SpectrumThreshold outputThreshold = new SpectrumThreshold(inputThreshold);

            int count = outputThreshold.Bins.Count;
            for (int i = 0; i < count; i++)
            {                
                outputThreshold.Bins[i].Level = Measurement.UnitConversion(outputThreshold.Bins[i].Level, unitOut, unitIn);
            }

            return outputThreshold;
        }

        #region ---- 私有方法 ----

        /// <summary>
        /// 将单位转变为V2
        /// </summary>
        private static double UnitConversionToV2(double data, SpectrumUnit unit, double impedance = 50)
        {
            double power = 0;
            switch (unit)
            {
                case SpectrumUnit.V2:
                    power = data;
                    break;
                case SpectrumUnit.dBuV:
                    power = Math.Pow(10.0, (data - 120) / 10);
                    break;
                case SpectrumUnit.dBmV:
                    power = Math.Pow(10.0, (data - 60) / 10);
                    break;
                case SpectrumUnit.dBV:
                    power = Math.Pow(10.0, data / 10);
                    break;
                case SpectrumUnit.V:
                    power = data * data;
                    break;
                case SpectrumUnit.dBm:
                    power = Math.Pow(10.0, (data - 30) / 10) * impedance;
                    break;
                case SpectrumUnit.dBW:
                    power = Math.Pow(10.0, data / 10) * impedance;
                    break;
                case SpectrumUnit.W:
                    power = data * impedance;
                    break;
            }
            return power;
        }

        private static void UnitConversionToV2(double[] data, SpectrumUnit unit, double impedance = 50)
        {
            switch (unit)
            {
                case SpectrumUnit.V2:
                    break;
                case SpectrumUnit.dBuV:                         // y =  10 ** ( ( x - 120 ) / 10 )
                    Vector.ArrayAdd(data, -120);
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.dBmV:                         // y =  10 ** ( ( x - 60 ) / 10 )
                    Vector.ArrayAdd(data, -60);
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.dBV:                          // y =  10 ** ( x / 10 )
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.V:                             // y =  x ** 2
                    Vector.ArraySquare(data);
                    break;
                case SpectrumUnit.dBm:                           // 10 ** ( ( x - 30 ) / 10 ) * impedance  
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    Vector.ArrayScale(data, impedance/1000);
                    break;
                case SpectrumUnit.dBW:                          // 10 ** ( x / 10 ) * impedance  
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    Vector.ArrayScale(data, impedance);
                    break;
                case SpectrumUnit.W:                            // y = x * impendace
                    Vector.ArrayScale(data, impedance); 
                    break;

            }
        }

        private static void UnitConversionToV2(float[] data, SpectrumUnit unit, double impedance = 50)
        {
            switch (unit)
            {
                case SpectrumUnit.V2:
                    break;
                case SpectrumUnit.dBuV:                         // y =  10 ** ( ( x - 120 ) / 10 )
                    Vector.ArrayAdd(data, -120);
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.dBmV:                         // y =  10 ** ( ( x - 60 ) / 10 )
                    Vector.ArrayAdd(data, -60);
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.dBV:                          // y =  10 ** ( x / 10 )
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    break;
                case SpectrumUnit.V:                             // y =  x ** 2
                    Vector.ArraySquare(data);
                    break;
                case SpectrumUnit.dBm:                           // 10 ** ( ( x - 30 ) / 10 ) * impedance  
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    Vector.ArrayScale(data, impedance / 1000);
                    break;
                case SpectrumUnit.dBW:                          // 10 ** ( x / 10 ) * impedance  
                    Vector.ArrayScale(data, 0.1);
                    Vector.ArrayExp10(data);
                    Vector.ArrayScale(data, impedance);
                    break;
                case SpectrumUnit.W:                            // y = x * impendace
                    Vector.ArrayScale(data, impedance);
                    break;

            }
        }

        private static double[] UnitConversionToV2Ref (double[] data, SpectrumUnit unit, double impedance = 50)
        {
            if (unit == SpectrumUnit.V2 )
            {
               return data;
            }
            else
            {
                double[]  spectrumV2 = new double[data.Length];
                Vector.ArrayCopy(data, spectrumV2);
                UnitConversionToV2(spectrumV2, unit, impedance);
                return spectrumV2;
            }
        }

        private static float[] UnitConversionToV2Ref(float[] data, SpectrumUnit unit, double impedance = 50)
        {
            if (unit == SpectrumUnit.V2)
            {
                return data;
            }
            else
            {
                float[] spectrumV2 = new float[data.Length];
                Vector.ArrayCopy(data,  spectrumV2);
                UnitConversionToV2(spectrumV2, unit, impedance);
                return spectrumV2;
            }
        }

        /// <summary>
        /// 将单位V2转变为其他单位
        /// </summary>
        private static double UnitConversionV2ToOther(double data, SpectrumUnit unit, double impedance = 50)
        {
            double power = 0;
            switch (unit)
            {
                case SpectrumUnit.V2:
                    power = data;
                    break;
                case SpectrumUnit.V:
                    power = Math.Sqrt(data);
                    break;
                case SpectrumUnit.dBV:
                    power = 10 * Math.Log10(data);
                    break;
                case SpectrumUnit.dBmV:
                    power = 10 * Math.Log10(data) + 60;
                    break;
                case SpectrumUnit.dBuV:
                    power = 10 * Math.Log10(data) + 120;
                    break;
                case SpectrumUnit.W:
                    power = data / impedance;
                    break;
                case SpectrumUnit.dBW:
                    power = 10 * Math.Log10(data/ impedance);
                    break;
                case SpectrumUnit.dBm:
                    power = 10 * Math.Log10(data /impedance) +30;
                    break;
            }
            return power;
        }

        private static void UnitConversionV2ToOther(double[] data, SpectrumUnit unit, double impedance = 50)
        {
            switch (unit)
            {
                case SpectrumUnit.V2:
                    break;
                case SpectrumUnit.V:                            // y = sqrt (x)
                    Vector.ArrayRoot(data);
                    break;
                case SpectrumUnit.dBV:                          // y = 10 * log10 (x)
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10);
                    break;
                case SpectrumUnit.dBmV:                         // y = 10 * log10 (x) + 60
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10);
                    Vector.ArrayAdd(data,60);
                    break;
                case SpectrumUnit.dBuV:                         // y = 10 * log10 (x) + 120
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10);
                    Vector.ArrayAdd(data, 120);
                    break;
                case SpectrumUnit.W:                            // y = x / impedance
                    Vector.ArrayScale(data, 1 / impedance);
                    break;
                case SpectrumUnit.dBW:                          // y = 10 * log10 ( x / impedance )
                    Vector.ArrayScale(data, 1 / impedance);
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10);
                    break;
                case SpectrumUnit.dBm:                          // y = 10 * log10 ( x / impedance ) + 30
                    Vector.ArrayScale(data, 1000 / impedance);
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10);
                    break;
            }
        }

        private static void UnitConversionV2ToOther(float[] data, SpectrumUnit unit, double impedance = 50)
        {
            switch (unit)
            {
                case SpectrumUnit.V2:
                    break;
                case SpectrumUnit.V:                            // y = sqrt (x)
                    Vector.ArrayRoot(data);
                    break;
                case SpectrumUnit.dBV:                          // y = 10 * log10 (x)
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10.0f);
                    break;
                case SpectrumUnit.dBmV:                         // y = 10 * log10 (x) + 60
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10.0f);
                    Vector.ArrayAdd(data, 60.0f);
                    break;
                case SpectrumUnit.dBuV:                         // y = 10 * log10 (x) + 120
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10.0f);
                    Vector.ArrayAdd(data, 120.0f);
                    break;
                case SpectrumUnit.W:                            // y = x / impedance
                    Vector.ArrayScale(data, 1.0 / impedance);
                    break;
                case SpectrumUnit.dBW:                          // y = 10 * log10 ( x / impedance )
                    Vector.ArrayScale(data, 1.0 / impedance);
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10.0f);
                    break;
                case SpectrumUnit.dBm:                          // y = 10 * log10 ( x / impedance ) + 30
                    Vector.ArrayScale(data, 1000.0 / impedance);
                    Vector.ArrayLog10(data);
                    Vector.ArrayScale(data, 10.0f);
                    break;
            }
        }

        #endregion

    }

    /// <summary>
    /// <para>unit of power spectrum</para>
    /// <para>Chinese Simplified: 频谱的单位</para>
    /// </summary>
    public enum SpectrumUnit : int
    {
        /// <summary>
        /// Voltage
        /// </summary>
        V = 0,

        /// <summary>
        /// V^2
        /// </summary>
        V2,

        /// <summary>
        /// Watt
        /// </summary>
        W,

        /// <summary>
        /// dBm
        /// </summary>
        dBm,

        /// <summary>
        /// dBW
        /// </summary>
        dBW,

        /// <summary>
        /// dBV
        /// </summary>
        dBV,

        /// <summary>
        /// dBmV
        /// </summary>
        dBmV,

        /// <summary>
        /// dBuV
        /// </summary>
        dBuV
    }

    /// <summary>
    /// 频谱单位信息
    /// </summary>
    internal class SpectrumUnitInfo
    {
        public SpectrumUnitInfo()
        {
            Type = SpectrumUnit.V2;
            Impedance = 50;
            IsPSD = false;
        }

        /// <summary>
        /// 单位类型
        /// </summary>
        public SpectrumUnit Type { get; set; }

        /// <summary>
        /// 阻抗,默认值为50
        /// </summary>  
        public double Impedance { get; set; }

        /// <summary>
        /// 幅度或功率谱是否归一化为密度谱(Power Spectral Density)，默认为false。
        /// </summary>
        public bool IsPSD { get; set; }
    }

    /// <summary>
    /// Spectrum peak scaling
    /// </summary>
    internal enum PeakScaling : int
    {
        /// <summary>
        /// Rms
        /// </summary>
        Rms = 0,

        /// <summary>
        /// Peak
        /// </summary>
        Peak = 1,
    }
}
