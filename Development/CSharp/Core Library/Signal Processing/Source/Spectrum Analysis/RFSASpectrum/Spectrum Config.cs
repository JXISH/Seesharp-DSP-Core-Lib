using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    /// <summary>
    /// 计算频谱参数配置
    /// </summary>
    public class SpectrumConfig
    {
        internal const int DefaultSpectrumLines = 401;
        internal const double MinFrequencyError = 0.01; // 相对值。

        /// <summary>
        /// 根据中心频率带宽配置窄带频谱参数，一次采集，一个频谱
        /// </summary>
        public static int SingleBandConfig(double centerFrequency, double span, double sampleRate,
            out double actualFrequency, out double actualSpan,
            double rbw = -1, int spectrumLines = -1, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris)
        {
            SpectrumSettings setting = SingleBandConfig(0, span, sampleRate, centerFrequency, rbw, spectrumLines, windowType);
            actualFrequency = GetRelativeCenterFrequency(setting);
            actualSpan = setting.ActualSpan;
            return setting.SpectrumLines;
        }

        /// <summary>
        /// 根据起始终止频率配置宽带频谱参数，多次采集，宽带拼接
        /// </summary>
        public static int[] MultiBandConfig(double startFreq, double stopFreq, double bandwidth, double sampleRate,
            out double[] tuneFrequecy, out double[] actualFrequency, out double[] actualSpan,
            double rbw = -1, int spectrumLines = -1, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris, double frequencyStep = 1)
        {
            SpectrumSettings[] settings = MultiBandConfig(startFreq, stopFreq, bandwidth, sampleRate, out tuneFrequecy, rbw, spectrumLines, windowType);
            actualFrequency = new double[settings.Length];
            actualSpan = new double[settings.Length];
            int[] actualSpectrumLines = new int[settings.Length];

            for (int i = 0; i < settings.Length; i++)
            {
                actualFrequency[i] = GetRelativeCenterFrequency(settings[i]);
                actualSpan[i] = settings[i].ActualSpan;
                actualSpectrumLines[i] = settings[i].SpectrumLines;
            }

            return actualSpectrumLines;
        }

        /// <summary>
        /// 根据中心频率带宽配置窄带频谱参数，一次采集，一个频谱
        /// </summary>
        public static SpectrumSettings SingleBandConfig(double relativeFrequency, double span, double sampleRate, double tuneFrequecy = 0,
            double rbw = -1, int spectrumLines = -1, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris)
        {
            int fftSize;
            double f0;
            int lines;

            if (rbw > 0)
            {
                // 根据 RBW 计算 fftSize，lines 
                fftSize = GenIdealFFTSize(sampleRate, rbw / FFTWindow.GetWindowBW3dB(windowType));
                // span = ( lines - 1 ) * df，当不整除时，向上取整。
                lines = (int)Math.Ceiling(span / (sampleRate / fftSize)) + 1;
                f0 = relativeFrequency - span / 2;
            }
            else
            {
                if (spectrumLines > 0)
                {
                    // 根据 spectrumLines 计算 lines 
                    lines = spectrumLines;
                }
                else
                {
                    // 使用默认参数
                    lines = DefaultSpectrumLines;
                }
                fftSize = GenIdealFFTSize(sampleRate, span / (lines - 1));
                f0 = relativeFrequency - (sampleRate / fftSize) * (lines - 1) / 2;
            }

            // 获取频谱参数设置
            return GenerateSepctrumSettings(sampleRate, fftSize, f0, lines, windowType, tuneFrequecy);
        }

        /// <summary>
        /// 根据起始终止频率配置宽带频谱参数，多次采集，宽带拼接
        /// </summary>
        public static SpectrumSettings[] MultiBandConfig(double startFreq, double stopFreq, double bandwidth, double sampleRate, out double[] tuneFrequecy,
            double rbw = -1, int spectrumLines = -1, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris, double frequencyStep = 1)
        {
            int fftSize;
            if (rbw > 0)
            {
                // 根据 RBW 计算 fftSize 
                fftSize = GenIdealFFTSize(sampleRate, rbw / FFTWindow.GetWindowBW3dB(windowType));
            }
            else
            {
                if (spectrumLines > 0)
                {
                    // 根据 spectrumLines 计算 fftSize 
                    fftSize = GenIdealFFTSize(sampleRate, (stopFreq - startFreq) / (spectrumLines - 1));
                }
                else
                {
                    // 使用默认参数
                    fftSize = GenIdealFFTSize(sampleRate, bandwidth / DefaultSpectrumLines);
                }
            }

            // 计算每一段的 起始频率，频谱线数，Tuner调谐频率。其中相邻两段的频谱，有一个点交叠。
            double[] startFrequencies;
            int[] bandLines;
            tuneFrequecy = GenMultiBandsCenterFrequency(startFreq, stopFreq, bandwidth, sampleRate / fftSize, out startFrequencies, out bandLines, frequencyStep);

            // 就算每一段频谱的参数设置
            SpectrumSettings[] allSettings = new SpectrumSettings[startFrequencies.Length];
            for (int i = 0; i < startFrequencies.Length; i++)
            {
                // 获取每一段频谱参数设置
                allSettings[i] = GenerateSepctrumSettings(sampleRate, fftSize, startFrequencies[i], bandLines[i], windowType, tuneFrequecy[i]);
            }

            return allSettings;
        }

        /// <summary>
        /// 计算相对中心频率
        /// </summary>
        internal static double GetRelativeCenterFrequency(SpectrumSettings settings)
        {
            // CenterFreq =  StartFreq + Bandwidth / 2
            double centerFrequency = settings.StartIndex * settings.DeltaF - settings.frequencyShift + settings.ActualSpan / 2;
            // CenterFreq > SampleRate / 2 , 频率回绕 （备注：默认 CenterFreq < SampleRate，该条件由计算保证 ） 
            if (centerFrequency > settings.SampleRate / 2)
            {
                centerFrequency -= settings.SampleRate;
            }
            return centerFrequency;
        }

        /// <summary>
        /// 频谱扫描分段
        /// </summary>
        private static double[] GenMultiBandsCenterFrequency(double startFreq, double stopFreq, double bandwidth, double df,
            out double[] startArray, out int[] linesArray, double frequencyStep = 1)
        {
            List<double> startList = new List<double>();
            List<double> stopList = new List<double>();
            List<double> centerList = new List<double>();

            double bandStart, bandStop, bandCenter;

            // 从 startFreq 开始，逐个计算每个频段的 开始，结束，中心 频率。保证每个band交叠一个频点。
            bandStart = startFreq;
            bandStop = startFreq;
            bandCenter = startFreq;
            while (bandStop < stopFreq)
            {
                bandStart = bandStop;                                                   // 赋值band起始频率为上一个的终止频率，因此每个band交叠一个频点
                startList.Add(bandStart);                                               // 添加起始频率

                bandCenter = bandStart + bandwidth / 2;                                 // 计算中心频率
                bandCenter = Math.Floor(bandCenter / frequencyStep) * frequencyStep;    // 中心频率对齐frequency step
                centerList.Add(bandCenter);                                             // 添加中心频率

                bandStop = bandCenter * 2 - bandStart;                                  // 计算终止频率
                bandStop = Math.Floor((bandStop - startFreq) / df) * df + startFreq;    // 终止频率对齐 df
                stopList.Add(bandStop);                                                 // 添加终止频率
            }

            // 调整最后一个band的大小
            if (bandStop > stopFreq)
            {                                                                           // 终止频率大于扫描边界
                stopList.Remove(bandStop);                                              // 去除最后一个终止频率
                bandStop = Math.Ceiling((stopFreq - startFreq) / df) * df + startFreq;  // 以扫描边界计算终止频率。(备注：以startFreq为参考，当不整除时，向上取整)
                stopList.Add(bandStop);                                                 // 添加终止频率
            }

            if (bandCenter > stopFreq)
            {                                                                           // 中心频率大于扫描边界
                centerList.Remove(bandCenter);                                          // 去除最后一个中心频率
                bandCenter = Math.Floor(stopFreq / frequencyStep) * frequencyStep;      // 以扫描边界计算中心频率。(备注：向下取整，确保小于stopFreq)
                centerList.Add(bandCenter);                                             // 添加中心频率
            }

            int bandSizes = centerList.Count;                                           // 计算总共分段个数
            double[] centerArray = new double[bandSizes];
            startArray = new double[bandSizes];
            linesArray = new int[bandSizes];

            for (int i = 0; i < bandSizes; i++)
            {
                centerArray[i] = centerList[i];                                         // 变频器的中心频率
                startArray[i] = startList[i] - centerList[i];                           // 每个band 频谱相对起始频率 
                linesArray[i] = (int)Math.Round((stopList[i] - startList[i]) / df) + 1; // 每个band 的频谱线数。(备注：理论上为整数，采用四舍五入取整)
            }
            return centerArray;
        }

        /// <summary>
        /// 计算频谱参数
        /// </summary>
        private static SpectrumSettings GenerateSepctrumSettings(double sampleRate, int fftSize, double f0, int spectrumlines, FFTWindowType windowType, double tuneFrequecy = 0)
        {
            SpectrumSettings settings;

            double df = sampleRate / fftSize;
            settings.SampleRate = sampleRate;
            settings.FFTSize = fftSize;
            settings.DeltaF = df;

            settings.SpectrumLines = spectrumlines;                                 // 计算spectrumlines
            settings.ActualSpan = (spectrumlines - 1) * df;                         // 计算实际Span 

            // fft计算结果的频率排列是 0 - Fs，所以需要计算当前的 f0 对应 fft 结果中的频率 (即 ：fftStart)
            double fftStart = f0 - Math.Floor(f0 / sampleRate) * sampleRate;

            int startIndex = (int)Math.Round(fftStart / df);                        // 计算startIndex (备注：startIndex 为粗调，freqShift 为细调)  
            settings.StartIndex = startIndex;

            double freqShift = startIndex * df - fftStart;                          // 计算frequencyShift，即 startFrequency 和 FFT bin不对齐产生的误差
            settings.frequencyShift = (Math.Abs(freqShift) < (df * MinFrequencyError)) ? 0 : freqShift;     // frequencyShift 足够小则设为0 ，不做处理。

            settings.ActualRBW = df * FFTWindow.GetWindowBW3dB(windowType);
            settings.WindowType = windowType;
            if (FFTWindow.GetWindowGroup(windowType) == FFTWindowGroup.Sinc)
            {
                settings.WindowSize = fftSize * FFTWindow.SINC_OVER_SAMPLE;
            }
            else
            {
                settings.WindowSize = fftSize;
            }
            settings.StartFrequency = f0 + tuneFrequecy;                            // 修改 StartFrequency， 保证频谱起始频点对应实际频点

            return settings;
        }

        private static int GenIdealFFTSize(double sampleRate, double delatF)
        {
            int[] prime = new int[] { 13, 11, 7, 5, 3, 2 };          // 通过质因数分解，确保 FFT size 只包含有限个不同的质数。 
            //int[] prime = new int[] { 43, 41, 37, 31, 29, 23, 19, 17, 13, 11, 7, 5, 3, 2 };

            int idealFFTSize = (int)Math.Round(sampleRate / delatF);
            int quotient;                                           // 每次除法得到的商
            idealFFTSize--;                                         // 对应第一次 while 循环的 +1操作。
            do
            {
                idealFFTSize++;                                     // 如果不能分解，fft size +1
                quotient = idealFFTSize;
                for (int i = 0; i < prime.Length; i++)              // 从大到小遍历所有的质数
                {
                    while ((quotient % prime[i]) == 0)              // 如果能被当前的质数整除，则求商，即拿掉一个当前质数的因子。
                    {
                        quotient = quotient / prime[i];
                    }
                }
            } while (quotient != 1);                                // 如果商数为1，表示能够正常分解，找到理想FFT Size退出。否则商数为大质数的乘积。

            return idealFFTSize;
        }

        /// <summary>
        /// 获取计算FFT的IQ点数
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static int GetAcquireIQSize(SpectrumSettings settings)
        {
            return settings.WindowSize;
        }

        /// <summary>
        /// 获取最终频谱的线数
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static int GetSpectrumLines(SpectrumSettings settings)
        {
            return settings.SpectrumLines;
        }

    }

    /// <summary>
    /// 频谱计算参数
    /// </summary>
    public struct SpectrumSettings
    {
        internal double SampleRate;           // 采样率
        internal int FFTSize;                 // FFT 线数
        internal double DeltaF;               // 频率步进

        internal double StartFrequency;       // 起始频率
        internal int StartIndex;              // 起始频率索引号
        internal int SpectrumLines;           // 频谱线数

        internal double ActualRBW;            // 分辨率带宽
        internal double ActualSpan;           // 频谱带宽

        internal double frequencyShift;       // fft 频率搬移

        internal FFTWindowType WindowType;    // fft 窗类型
        internal int WindowSize;              // fft 窗长度 <=> IQ点数
    }

    /// <summary>
    /// 频谱的数据信息。
    /// </summary>
    public class SpectralInfo
    {
        /// <summary>
        /// 构造
        /// </summary>
        public SpectralInfo() { }

        /// <summary>
        /// 构造，从已知频谱信息
        /// </summary>
        public SpectralInfo(SpectralInfo source)
        {
            this.CopyFrom(source);
        }

        /// <summary>
        /// 频谱的起始频率，即第一根谱线对应频率值。
        /// </summary>
        public double FreqStart { get; internal set; }

        /// <summary>
        /// 频谱的频率步进，即两根谱线之间的频率间隔。
        /// </summary>
        public double FreqDelta { get; internal set; }

        /// <summary>
        /// 频谱单位。
        /// </summary>
        public SpectrumUnit Unit { get; internal set; }

        /// <summary>
        /// 功率谱密度。
        /// </summary>
        public bool PSD { get; internal set; }

        /// <summary>
        /// 窗长度
        /// 每次频谱需要采集IQ数据的长度
        /// </summary>
        public int WindowSize { get; internal set; }

        /// <summary>
        /// FFT点数
        /// </summary>
        public int FFTSize { get; internal set; }

        /// <summary>
        /// 窗类型
        /// </summary>
        public FFTWindowType WindowType { get; internal set; }

        /// <summary>
        /// 频谱线数
        /// </summary>
        public int SpectrumSize { get; internal set; }

        /// <summary>
        /// 复制频谱信息
        /// </summary>
        public void CopyFrom(SpectralInfo source)
        {
            this.FreqStart = source.FreqStart;
            this.FreqDelta = source.FreqDelta;

            this.WindowType = source.WindowType;
            this.WindowSize = source.WindowSize;
            this.FFTSize = source.FFTSize;

            this.SpectrumSize = source.SpectrumSize;

            this.Unit = source.Unit;
            this.PSD = source.PSD;
        }

        /// <summary>
        /// 比较
        /// </summary>
        public bool Equals(SpectralInfo source)
        {
            return ((this.FreqStart == source.FreqStart)
            && (this.FreqDelta == source.FreqDelta)
            && (this.WindowType == source.WindowType)
            && (this.WindowSize == source.WindowSize)
            && (this.FFTSize == source.FFTSize)
            && (this.SpectrumSize == source.SpectrumSize)
            && (this.Unit == source.Unit)
            && (this.PSD == source.PSD));
        }

        /// <summary>
        /// Obsolete 根据频谱设置，换算频谱信息
        /// </summary>
        public void Generate(SpectrumSettings settings, SpectrumUnit unit = SpectrumUnit.V2, bool psd = false)
        {
            this.FreqStart = settings.StartFrequency;
            this.FreqDelta = settings.DeltaF;
            this.Unit = unit;
            this.PSD = psd;
            this.WindowType = settings.WindowType;
            this.WindowSize = settings.WindowSize;
            this.FFTSize = settings.FFTSize;
            this.SpectrumSize = settings.SpectrumLines;
        }

        /// <summary>
        /// Obsolete 根据频谱设置，换算频谱信息
        /// </summary>
        public void Generate(double tunerFrequency, double span, double sampleRate,
          double rbw = -1, int spectrumLines = -1, SpectrumUnit unit = SpectrumUnit.V2, bool psd = false, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris)
        {
            // 默认 tuner 中心频率对准 DDC 直流分量
            Generate(SpectrumConfig.SingleBandConfig(0, span, sampleRate, tunerFrequency, rbw, spectrumLines, windowType), unit, psd);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectralInfo(double centerFrequency, double span, double sampleRate, int spectrumLines,
            SpectrumUnit unit = SpectrumUnit.V2, bool psd = false, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris)
        {
            // 默认 tuner 中心频率对准 DDC 直流分量
            Generate(SpectrumConfig.SingleBandConfig(0, span, sampleRate, centerFrequency, -1, spectrumLines, windowType), unit, psd);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectralInfo(double startFreq, double deltaFreq, int spectrumLines,
            SpectrumUnit unit = SpectrumUnit.V2, bool psd = false, FFTWindowType windowType = FFTWindowType.Seven_Term_B_Harris)
        {
            this.FreqStart = startFreq;
            this.FreqDelta = deltaFreq;
            this.Unit = unit;
            this.PSD = psd;
            this.WindowType = windowType;
            this.WindowSize = spectrumLines;
            this.FFTSize = spectrumLines;
            this.SpectrumSize = spectrumLines;
        }
    }

}
