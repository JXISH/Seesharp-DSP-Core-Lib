using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.Mathematics;
using SeeSharpTools.JXI.Mathematics.Interpolation;

namespace SeeSharpTools.JXI.SignalProcessing.SpectrumAnalysis.RFSASpectrum
{
    public partial class Measurement
    {
        #region ---- 私有方法 ----

        private static Complex RectWindowFrequencyResponse(double freq, int winLength)
        {
            double tempA = Math.PI * freq;
            double r = MathExtension.Sinc(tempA * winLength) / MathExtension.Sinc(tempA);
            double theta = (1.0 - winLength) * tempA;
            return new Complex(r * Math.Cos(theta), r * Math.Sin(theta));
        }

        private static double WindowFrequencyResponse(double freq, FFTWindowType windowType, int winLength)
        {
            switch (FFTWindow.GetWindowGroup(windowType))
            {
                // 余弦窗
                case FFTWindowGroup.Cosine:
                    return WindowFrequencyResponseCosine(freq, windowType, winLength);
                // 其它
                default:
                    return WindowFrequencyResponseNormal(freq, windowType, winLength);
            }
        }

        private static double WindowFrequencyResponseNormal(double freq, FFTWindowType windowType, int winLength)
        {
            double[] window = FFTWindow.GetWindow(windowType, winLength);
            Complex response = new Complex();
            double theta = -2 * Math.PI * freq;
            for (int i = 0; i < window.Length; i++)
            {
                response += new Complex(Math.Cos(theta * i), Math.Sin(theta * i)) * window[i];
            }
            return response.Magnitude / winLength;
        }

        private static double WindowFrequencyResponseCosine(double freq, FFTWindowType windowType, int winLength)
        {
            double[] windowCoe = FFTWindow.GeneralizedCosineWindowCoefNormalized(windowType);
            Complex response = new Complex();
            for (int i = 0; i < windowCoe.Length; i++)
            {
                double f1 = freq - (double)i / winLength;
                double f2 = freq + (double)i / winLength;
                response += (0.5 * windowCoe[i]) * (RectWindowFrequencyResponse(f1, winLength) + RectWindowFrequencyResponse(f2, winLength));
            }
            return response.Magnitude;
        }

        private static double WindowFrequencyResponseFirstDerivative(double freq, FFTWindowType windowType, int winLength)
        {
            double delta = Math.Pow(2, -40);
            return (WindowFrequencyResponse(freq + delta, windowType, winLength) - WindowFrequencyResponse(freq, windowType, winLength)) / delta;
        }

        private static double ThreeFingerPeakSearch(double spectrum0, double spectrum1, double spectrum2, FFTWindowType windowType, int winLength, int fftSize, out double peakAmplitude)
        {
            peakAmplitude = spectrum1;
            double peakFreq = 0;

            if (FFTWindow.GetWindowGroup(windowType) == FFTWindowGroup.Sinc) { return peakFreq; }  // Sinc窗不做峰值搜索，直接返回。

            double max = Math.Sqrt(spectrum1);
            double secondMax = Math.Max(Math.Sqrt(spectrum0), Math.Sqrt(spectrum2));

            double df = 1.0 / fftSize;
            double freq = df / 4.0;
            double freqErrorLimit = df * 1E-8;
            double freqError;

            double A1=1, A2, D1, D2;

            for (int i = 0; i < 100; i++)
            {
                A1 = WindowFrequencyResponse(freq, windowType, winLength);
                A2 = WindowFrequencyResponse(df - freq, windowType, winLength);
                D1 = WindowFrequencyResponseFirstDerivative(freq, windowType, winLength);
                D2 = WindowFrequencyResponseFirstDerivative(freq - df, windowType, winLength);
                freqError = (A1 * secondMax - A2 * max) / (D1 * max - D2 * secondMax);
                if (Math.Abs(freqError) < freqErrorLimit) { break; }
                freq -= freqError;
            }

            double index = freq * fftSize;

            if (index > 0 && index < 1)
            {
                peakFreq = index * (spectrum0 > spectrum2 ? -1 : 1);
                peakAmplitude /= (A1 * A1);
            }

            return peakFreq;
        }

        #endregion

        /// <summary>
        /// 在频谱中查找最大的峰值。
        /// </summary> 
        internal static SpectrumPeak PeakSearchV2(double[] spectrumV2, SpectralInfo info)
        {
            SpectrumPeak spectrumPeakV2 = new SpectrumPeak();

            #region 查找最大值
            double maxPowerV2;
            int maxIndex;
            Vector.ArrayFindMax(spectrumV2, out maxPowerV2, out maxIndex);

            spectrumPeakV2.MaxFrequency = info.FreqStart + info.FreqDelta * maxIndex; 
            spectrumPeakV2.MaxLevel = maxPowerV2;
            #endregion

            #region 查找峰值
            if (maxIndex == 0 || maxIndex == spectrumV2.Length - 1)
            {
                spectrumPeakV2.Frequency = spectrumPeakV2.MaxFrequency;
                spectrumPeakV2.Level = spectrumPeakV2.MaxLevel;
            }
            else
            {
                double peakPowerV2;
                double indexOffset = ThreeFingerPeakSearch(spectrumV2[maxIndex - 1], spectrumV2[maxIndex], spectrumV2[maxIndex + 1], info.WindowType, info.WindowSize, info.FFTSize, out peakPowerV2);
                spectrumPeakV2.Frequency = info.FreqStart + info.FreqDelta * (maxIndex + indexOffset);
                spectrumPeakV2.Level = peakPowerV2;
            }
            #endregion

            #region 峰值信号，带宽为1Bin
            spectrumPeakV2.PowerInBand = spectrumPeakV2.Level;
            spectrumPeakV2.Bandwidth = info.FreqDelta;
            #endregion

            return spectrumPeakV2;
        }

        public static SpectrumPeak PeakSearch(double[] spectrum, SpectralInfo info)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            SpectrumPeak spectrumPeakV2 = PeakSearchV2(spectrumV2, info);

            return UnitConversion(spectrumPeakV2, info.Unit);
        }

        public static SpectrumPeak PeakSearch(float[] spectrum, SpectralInfo info)
        {
            return PeakSearch(Vector.ConvertToDouble(spectrum), info);
        }


        /// <summary>
        /// 在频谱中查找峰值。
        /// </summary>
        internal static List<SpectrumPeak> PeakSearchV2(double[] spectrumV2, SpectralInfo info, SpectrumThreshold thresholdV2)
        {
            //创建空列表
            List<SpectrumPeak> spectrumPeaks = new List<SpectrumPeak>();

            int startIndex, stopIndex, peakIndex;
            double totalPower, maxPower, peakPower;

            int currentIndex = 0;
            double currentFrequency = info.FreqStart;

            // 遍历所有频点
            while (currentIndex < spectrumV2.Length)
            {
                #region 查找超过模板的点，即上升沿
                for (; currentIndex < spectrumV2.Length; currentIndex++)
                {
                    if (thresholdV2.IsExceeded(currentFrequency, spectrumV2[currentIndex]))
                    {
                        break;
                    }
                    currentFrequency += info.FreqDelta;
                }
                #endregion

                // 找到上升沿，开始找下降沿
                if (currentIndex != spectrumV2.Length)  
                {
                    startIndex = currentIndex; // 将已经找到上升沿保存
                   
                    // 初始化峰值功率
                    maxPower = spectrumV2[startIndex];
                    peakIndex = startIndex;

                    // 初始化总功率
                    totalPower = spectrumV2[startIndex];

                    currentIndex++;
                    currentFrequency += info.FreqDelta;

                    #region 查找低于模板的点，即下降沿
                    for (; currentIndex < spectrumV2.Length; currentIndex++)
                    {
                        #region 更新功率最大值
                        if (spectrumV2[currentIndex] > maxPower)
                        {
                            maxPower = spectrumV2[currentIndex];
                            peakIndex = currentIndex;
                        }
                        #endregion

                        #region 更新总功率
                        totalPower += spectrumV2[currentIndex];
                        #endregion

                        #region 查找下降沿
                        if (!(thresholdV2.IsExceeded(currentFrequency, spectrumV2[currentIndex])))
                        {
                            break;
                        }
                        #endregion

                        currentFrequency += info.FreqDelta;
                    }
                    #endregion

                    stopIndex = currentIndex;    // 将已经找到下降沿保存。注，如果没有下降沿，则 stopIndex = spectrum.Length，表示上升沿之后所有点

                    // 创建新的对象，添加到List中
                    SpectrumPeak spectrumPeak = new SpectrumPeak();

                    #region 查找最大值
                    spectrumPeak.MaxFrequency = info.FreqStart + info.FreqDelta * peakIndex; 
                    spectrumPeak.MaxLevel = maxPower;
                    #endregion

                    #region 带宽， 总功率
                    spectrumPeak.PowerInBand = totalPower / FFTWindow.GetWindowENBW(info.WindowType);
                    spectrumPeak.Bandwidth = (stopIndex - startIndex) * info.FreqDelta;
                    #endregion

                    #region 查找峰值
                    if (peakIndex == 0 || peakIndex == spectrumV2.Length - 1) // peak 在最左边 或最右边
                    {
                        spectrumPeak.Frequency = spectrumPeak.MaxFrequency;
                        spectrumPeak.Level = spectrumPeak.MaxLevel;
                    }
                    // peak 在 startIndex 和 stopIndex 之间
                    /* 注： 如果peak = startIndex 或则 stopIndex 需要排除某些特殊情况，在这个段内应该没有峰值 */
                    else if (true || (peakIndex > startIndex && peakIndex < stopIndex))
                    {
                        double indexOffset = ThreeFingerPeakSearch(spectrumV2[peakIndex - 1], spectrumV2[peakIndex], spectrumV2[peakIndex + 1], info.WindowType, info.WindowSize, info.FFTSize, out peakPower);
                        
                        spectrumPeak.Frequency = info.FreqStart + info.FreqDelta * (peakIndex + indexOffset); 
                        spectrumPeak.Level = peakPower;                        
                    }
                    #endregion

                    spectrumPeaks.Add(spectrumPeak);
                }
            }
            return spectrumPeaks;
        }

        internal static List<SpectrumPeak> PeakSearchV2(double[] spectrum, SpectralInfo info, double threshold)
        {
            var spectrumThreshold = new SpectrumThreshold(info.FreqStart, threshold, info.FreqStart + spectrum.Length * info.FreqDelta, threshold);
            return PeakSearchV2(spectrum, info, spectrumThreshold);
        }

        public static List<SpectrumPeak> PeakSearch(double[] spectrum, SpectralInfo info, SpectrumThreshold threshold)
        {
            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);
            SpectrumThreshold thresholdV2 = UnitConversion(threshold, SpectrumUnit.V2, info.Unit);

            //创建空列表
            List<SpectrumPeak> spectrumPeaksV2 = PeakSearchV2(spectrumV2, info, thresholdV2);

            return UnitConversion(spectrumPeaksV2, info.Unit);
        }

        public static List<SpectrumPeak> PeakSearch(double[] spectrum, SpectralInfo info, double threshold)
        {
            var spectrumThreshold = new SpectrumThreshold(info.FreqStart, threshold, info.FreqStart + spectrum.Length * info.FreqDelta, threshold);
            return PeakSearch(spectrum, info, spectrumThreshold);
        }

        public static List<SpectrumPeak> PeakSearch(float[] spectrum, SpectralInfo info, SpectrumThreshold threshold)
        {
            return PeakSearch(Vector.ConvertToDouble(spectrum), info, threshold);
        }

        public static List<SpectrumPeak> PeakSearch(float[] spectrum, SpectralInfo info, double threshold)
        {
            var spectrumThreshold = new SpectrumThreshold(info.FreqStart, threshold, info.FreqStart + spectrum.Length * info.FreqDelta, threshold);
            return PeakSearch(spectrum, info, spectrumThreshold);
        }

        /// <summary>
        /// Obselete
        /// </summary>
        public static List<SpectrumPeak> FindPeak(double[] spectrum, SpectralInfo info, SpectrumThreshold threshold)
        {
            //创建空列表
            List<SpectrumPeak> spectrumPeaks = new List<SpectrumPeak>();

            int startIndex, stopIndex, peakIndex;
            double totalPower, maxPower, peakFrequency, peakPower, weightedPower;
            double scale = FFTWindow.GetWindowENBW(info.WindowType);

            // 先将频谱单位转换为 V2
            double[] spectrumV2 = UnitConversionToV2Ref(spectrum, info.Unit);

            int currentIndex = 0;
            double currentFrequency = info.FreqStart;
            while (currentIndex < spectrumV2.Length)
            {
                // 查找超过模板的点，即上升沿
                for (; currentIndex < spectrumV2.Length; currentIndex++)
                {
                    if (threshold.IsExceeded(currentFrequency, spectrum[currentIndex]))
                    {
                        break;
                    }
                    currentFrequency += info.FreqDelta;
                }

                if (currentIndex != spectrumV2.Length)  // 找到上升沿
                {
                    startIndex = currentIndex; // 将已经找到上升沿保存
                    maxPower = spectrumV2[startIndex];
                    peakIndex = startIndex;

                    totalPower = spectrumV2[startIndex];

                    currentIndex++;
                    currentFrequency += info.FreqDelta;

                    // 查找低于模板的点，即下降沿
                    for (; currentIndex < spectrumV2.Length; currentIndex++)
                    {
                        if (spectrumV2[currentIndex] > maxPower)
                        {
                            maxPower = spectrumV2[currentIndex];
                            peakIndex = currentIndex;
                        }

                        totalPower += spectrumV2[startIndex];

                        if (!(threshold.IsExceeded(currentFrequency, spectrum[currentIndex])))
                        {
                            break;
                        }
                        currentFrequency += info.FreqDelta;
                    }

                    stopIndex = currentIndex;    // 将已经找到下降沿保存。注，如果没有下降沿，则 stopIndex = spectrum.Length，表示上升沿之后所有点

                    // 创建新的对象，添加到List中
                    SpectrumPeak spectrumPeak = new SpectrumPeak();

                    spectrumPeak.MaxFrequency = info.FreqStart + info.FreqDelta * peakIndex; 
                    spectrumPeak.MaxLevel = UnitConversion(maxPower, info.Unit);

                    #region 带宽， 总功率
                    spectrumPeak.PowerInBand = UnitConversion(totalPower / scale, info.Unit);
                    spectrumPeak.Bandwidth = (stopIndex - startIndex) * info.FreqDelta;
                    #endregion

                    if (peakIndex == 0 || peakIndex == spectrumV2.Length - 1) // peak 在最左边 或最右边
                    {
                        spectrumPeak.Frequency = spectrumPeak.MaxFrequency;
                        spectrumPeak.Level = spectrumPeak.MaxLevel;
                    }
                    // peak 在 startIndex 和 stopIndex 之间
                    /* 注： 如果peak = startIndex 或则 stopIndex 需要排除某些特殊情况，在这个段内应该没有峰值 */
                    else if (true || (peakIndex > startIndex && peakFrequency < stopIndex))         // 计算N个点的加权平均。
                    {
                        startIndex = Math.Max(0, peakIndex - 2);
                        stopIndex = Math.Min(spectrumV2.Length - 1, peakIndex + 2);
                        peakPower = 0;
                        weightedPower = 0;
                        for (int i = startIndex; i <= stopIndex; i++)
                        {
                            peakPower += spectrumV2[i];
                            weightedPower += spectrumV2[i] * i;
                        }

                        double indexOffset = weightedPower / peakPower;
                        peakFrequency = info.FreqStart + info.FreqDelta * (startIndex + indexOffset);
                        spectrumPeak.Frequency = peakFrequency;
                        spectrumPeak.Level = UnitConversion(peakPower / scale, info.Unit);
                    }
                    spectrumPeaks.Add(spectrumPeak);
                }
            }
            return spectrumPeaks;
        }

    }

    /// <summary>
    /// 频谱门限曲线的一段。
    /// </summary>
    public class SpectrumThreshold
    {
        private List<SpectrumBin> _bins;
        /// <summary>
        /// 频谱模板各节点的频率值和上下限。
        /// </summary>
        public List<SpectrumBin> Bins { get { return _bins; } }

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SpectrumThreshold()
        {
            _bins = new List<SpectrumBin>();
        }

        /// <summary>
        /// 实例化只包含起点和终点的频谱门限曲线。
        /// </summary>
        /// <param name="freqStart"></param>
        /// <param name="thresholdAtStart"></param>
        /// <param name="freqEnd"></param>
        /// <param name="thresholdAtEnd"></param>
        public SpectrumThreshold(double freqStart, double thresholdAtStart, double freqEnd, double thresholdAtEnd)
        {
            _bins = new List<SpectrumBin>();
            _bins.Add(new SpectrumBin(freqStart, thresholdAtStart));
            _bins.Add(new SpectrumBin(freqEnd, thresholdAtEnd));
        }

        /// <summary>
        /// 根据基准曲线和上浮数值实例化新的频谱门限曲线。
        /// </summary>
        /// <param name="frequencies"></param>
        /// <param name="levels"></param>
        /// <param name="delta"></param>
        public SpectrumThreshold(double[] frequencies, double[] levels, double delta)
        {
            // 输入参数有效性检查。
            if (frequencies == null || levels == null) { throw new ArgumentNullException("Input Array"); }
            if (frequencies.Length != levels.Length) { throw new ArgumentException("Array Length Inconsistent."); }

            // 创建空列表。
            _bins = new List<SpectrumBin>();
            for (int i = 0; i < frequencies.Length; i++) { _bins.Add(new SpectrumBin(frequencies[i], levels[i] + delta)); }
        }

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        public SpectrumThreshold(SpectrumThreshold source) : this()
        {
            this.CopyFrom(source);
        }

        #endregion

        #region ------------------- 公共方法 -------------------

        /// <summary>
        /// 获取频谱门限曲线在[frequencyStart, frequencyStop]之间的子集。
        /// </summary>
        /// <param name="frequencyStart"></param>
        /// <param name="frequencyStop"></param>
        /// <returns></returns>
        public SpectrumThreshold GetSubset(double frequencyStart, double frequencyStop)
        {
            int startIndex, endIndex;

            // 如果当前Threshold曲线在设定的频率范围之外，则返回空。
            if (_bins.First().Frequency >= frequencyStop || _bins.Last().Frequency <= frequencyStart) { return null; }
            else
            {
                // 在当前Threshold曲线中查找对应起始频率的点。
                for (startIndex = 0; startIndex < _bins.Count; startIndex++) { if (_bins[startIndex].Frequency >= frequencyStart) break; }

                // 如果对应起始频率的Threshold曲线所在点已经是最后一个点，则取前一个点。
                // （比如当前Threshold曲线只有两个点，分别对应频率0和频率+Infinite。）
                if (startIndex == (_bins.Count - 1)) { startIndex--; }

                // 在当前Threshold曲线中查找对应终止频率的点。
                for (endIndex = startIndex + 1; endIndex < _bins.Count; endIndex++) { if (_bins[endIndex].Frequency >= frequencyStop) break; }

                // endIndex不能越界。
                endIndex = Math.Min(endIndex, _bins.Count - 1);

                // 将选中的门限曲线拷贝至新建列表，并将新列表中各点的实际频率值限制在[frequencyStart, frequencyStop]之间。
                var subset = new SpectrumThreshold();
                var freqRange = new NumericRange(frequencyStart, frequencyStop);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    subset.Bins.Add(new SpectrumBin(freqRange.Coerce(_bins[i].Frequency), _bins[i].Level));
                }

                // 返回新建列表。
                return subset;
            }

        }

        /// <summary>
        /// 判断(frequency, level)是否超过了门限曲线。
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsExceeded(double frequency, double level)
        {
            int index;

            // 如果frequency超出了门限曲线的频率范围，则返回false.
            if (frequency < _bins.First().Frequency || frequency > _bins.Last().Frequency) { return false; }

            // 查找对应frequency的门限曲线点的索引值。
            for (index = 0; index < _bins.Count; index++) { if (_bins[index].Frequency >= frequency) { break; } }

            // 获取frequency对应在门限曲线中的门限值。
            double threshold;
            if (index == 0)
            {
                // 如果frquency对应的是曲线中的第一个点，则直接取出其门限值。
                threshold = _bins[index].Level;
            }
            else
            {
                // 如果frequency对应的不是第一个点，则与上一个点插值计算门限值。
                threshold = Interpolation.LinearInterpolate(_bins[index - 1].Frequency, _bins[index - 1].Level, _bins[index].Frequency, _bins[index].Level, frequency);
            }

            // 返回与输入幅度的比较结果。
            return (threshold < level);
        }

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(SpectrumThreshold source)
        {
            _bins.Clear();
            if (source != null && source.Bins.Count > 0)
            {
                // 依次复制输入列表的每个Bin。
                foreach (var bin in source.Bins) { _bins.Add(new SpectrumBin(bin.Frequency, bin.Level)); }
            }
        }

        #endregion

    }

    /// <summary>
    /// 频谱曲线的一个点。
    /// </summary>
    public class SpectrumBin
    {
        /// <summary>
        /// 频率，Hz。
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// 电平值。
        /// </summary>
        public double Level { get; set; }

        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SpectrumBin() : this(0, 0) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="level"></param>
        public SpectrumBin(double frequency, double level)
        {
            this.Frequency = frequency;
            this.Level = level;
        }

        #endregion

    }

    /// <summary>
    /// 在频谱上检测到的一个峰。
    /// </summary>
    public class SpectrumPeak
    {
        /// <summary>
        /// 峰值中心频率，Hz。
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// 峰的总电平。
        /// </summary>
        public double Level { get; set; }

        /// <summary>
        /// 最大谱线所在的频率，Hz。
        /// </summary>
        public double MaxFrequency { get; set; }

        /// <summary>
        /// 最大谱线的电平。
        /// </summary>
        public double MaxLevel { get; set; }

        /// <summary>
        /// 带宽，Hz。
        /// </summary>
        public double Bandwidth { get; set; }

        /// <summary>
        /// 总功率电平。
        /// </summary>
        public double PowerInBand { get; set; }


        #region ------------------- 构造函数 -------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SpectrumPeak() : this(0, 0, 0, 0,0,0) {; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="level"></param>
        /// <param name="maxFrequency"></param>
        /// <param name="maxLevel"></param>
        public SpectrumPeak(double frequency, double level, double maxFrequency, double maxLevel, double bandwidth, double powerInBand)
        {
            this.Frequency = frequency;
            this.Level = level;
            this.MaxFrequency = maxFrequency;
            this.MaxLevel = maxLevel;
            this.Bandwidth = bandwidth;
            this.PowerInBand = powerInBand;
        }

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        public SpectrumPeak(SpectrumPeak source) : this()
        {
            CopyFrom(source);
        }

        #endregion

        /// <summary>
        /// 复制对象内容。
        /// </summary>
        public void CopyFrom(SpectrumPeak source)
        {
            this.Frequency = source.Frequency;
            this.Level = source.Level;
            this.MaxFrequency = source.MaxFrequency;
            this.MaxLevel = source.MaxLevel;
            this.Bandwidth = source.Bandwidth;
            this.PowerInBand = source.PowerInBand;
        }

    }
}
