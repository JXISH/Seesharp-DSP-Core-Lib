using SeeSharpTools.JXI.SignalProcessing.Window;
using System;
using System.Runtime.InteropServices;

namespace SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum
{
    #region  ---------公共枚举定义----------

    /// <summary>
    /// <para>unit of power spectrum</para>
    /// <para>Chinese Simplified: 频谱的单位</para>
    /// </summary>
    public enum SpectrumOutputUnit : int
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
    /// <para>spectrum type</para>
    /// <para>Chinese Simplified: 频谱类型</para>
    /// </summary>
    internal enum SpectrumType : int
    {
        /// <summary>
        /// <para>amplitude spectrum</para>
        /// <para>Chinese Simplified: 幅度谱</para>
        /// </summary>
        Amplitude = 0,

        /// <summary>
        /// <para>power spectrum</para>
        /// <para>Chinese Simplified: 功率谱</para>
        /// </summary>
        Power = 1
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
    
    /// <summary>
    /// <para>spectrum unit convertion settings</para>
    /// <para>Chinese Simplified: 单位转换定义结构体</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct UnitConvSetting
    {
        [MarshalAs(UnmanagedType.I4)]
        public SpectrumOutputUnit Unit;

        [MarshalAs(UnmanagedType.I4)]
        public PeakScaling PeakScaling;

        [MarshalAs(UnmanagedType.R8)]
        public double Impedance; // for converting V to Watt

        [MarshalAs(UnmanagedType.I1)]
        public bool PSD; // whether convert to power spectral density.

        public UnitConvSetting(SpectrumOutputUnit unit = SpectrumOutputUnit.dBV, PeakScaling peakScaling = PeakScaling.Rms,
            double impedance = 50.00, bool psd = false)
        {
            Unit = unit;
            PeakScaling = peakScaling;
            Impedance = impedance;
            PSD = psd;
        }
    }

    /// <summary>
    /// <para>spectral information.</para>
    /// <para>Chinese Simplified: 谱信息结构</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct SpectralInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public int spectralLines;

        [MarshalAs(UnmanagedType.I4)]
        public WindowType windowType;

        [MarshalAs(UnmanagedType.I4)]
        public int windowSize;

        [MarshalAs(UnmanagedType.I4)]
        public int FFTSize;
    }

    #endregion

    #region ---------------------------


    public struct Peak
    {
        /// <summary>
        /// 峰值
        /// </summary>
        public double PeakValue;

        /// <summary>
        /// 频谱中峰值对应的加权频率，也就是实际频率
        /// </summary>
        public double PeakFrequency;

        /// <summary>
        /// 频谱中峰值对应的检索频率
        /// </summary>
        public double PeakIndexFreq;
    }

    /// <summary>
    /// <para>Data type of input signal.</para>
    /// <para>Chinese Simplified: 输入信号的类型。</para>
    /// </summary>
    public enum InputDataType
    {
        /// <summary>
        /// 实信号
        /// </summary>
        Real,

        /// <summary>
        /// 复信号
        /// </summary>
        Complex
    }

    /// <summary>
    /// 定义如何确定频谱线数，默认为ByNumberOfLines
    /// </summary>
    public enum SpectrumOutputType
    {
        /// <summary>
        /// 用户通过NumberOfLines属性来设置所需的频谱线数，Commit()后可以获取实际频谱分辨率。
        /// </summary>
        ByNumberOfLines,

        /// <summary>
        /// 用户通过RBW属性来设置所需的频谱分辨率，Commit()之后可以获取实际的频谱线数。
        /// </summary>
        ByRBW
    }

    /// <summary>
    /// 频谱平均方式定义
    /// </summary>
    public enum SpectrumAverageMode
    {
        /// <summary>
        /// 无平均
        /// </summary>
        NoAveraging,

        /// <summary>
        /// RMS平均
        /// </summary>
        RMSAveraging,

        /// <summary>
        /// 峰值保持
        /// </summary>
        PeakHold,

        /// <summary>
        /// 矢量平均
        /// </summary>
        VectorAveraging
    }

    /// <summary>
    /// 当平均方式为RMSAverage时，设定平均的加权方式
    /// </summary>
    public enum SpectrumWeightingType
    {
        /// <summary>
        /// 线性滑动
        /// </summary>
        LinearMoving,

        /// <summary>
        /// 线性连续
        /// </summary>
        LinearContinuous,

        /// <summary>
        /// 指数平均
        /// </summary>
        Exponential
    }

    /// <summary>
    /// 频谱单位信息
    /// </summary>
    public class SpectrumUnitInfo
    {
        /// <summary>
        /// 单位类型
        /// </summary>
        public SpectrumOutputUnit Type { get; set; }

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
    /// 输出参数
    /// </summary>
    public class SpectrumOutput
    {
        private GeneralSpectrumTask a;
        /// <summary>
        /// 默认构造函数
        ///summary>
        public SpectrumOutput(GeneralSpectrumTask task)
        {
            a = task;
            CenterFrequency = -1;
            Bandwidth = -1;
            NumberOfLines = 10;
        }


        /// <summary>
        /// 默认构造函数
        ///summary>
        public SpectrumOutput()
        {
            CenterFrequency = -1;
            Bandwidth = -1;
            NumberOfLines = 10;
        }

        /// <summary>
        /// 确定频谱线数的方式
        /// </summary>
        public SpectrumOutputType Type { get; set; }

        private int _numberOfLines=10;
        /// <summary>
        /// 谱线数
        /// </summary>
        public int NumberOfLines
        {
            get
            {
                return _numberOfLines;
            }
            set
            {
                _numberOfLines = value;
                var isRealType = InputDataType.Real == a.InputDataType;
                //实数FFT运算的点数=谱线数*2，复数FFT运算的点数=谱线数
                a.SpectralInfomation.FFTSize = isRealType ? 2 * _numberOfLines : _numberOfLines;
                //实数的df=Fs/FFTSize,复数的df=Fs/谱线数
                a.SpectralInfomation.FreqDelta = isRealType ? (a.SampleRate / a.SpectralInfomation.FFTSize) : (a.SampleRate / (int)_numberOfLines);
                a.SpectralInfomation.FreqStart = isRealType ? 0 : -1* a.SampleRate/ a.SpectralInfomation.FFTSize * Math.Floor(a.SpectralInfomation.FFTSize/2.0);
            }
        }
       
        /// <summary>
       // / 频谱分辨率, 暂不可用
       // / </summary>
        public double RBW { get; set; }

        /// <summary>
        /// 频谱的中心频率，默认为-1，暂不可用
        /// </summary>
        public double CenterFrequency { get; }

        /// <summary>
        /// 频谱的带宽，默认为-1，暂不可用
       // / </summary>
        public double Bandwidth { get; }
    }

    /// <summary>
    /// 频谱平均配置
    /// </summary>
    public class SpectrumAverage
    {
        private GeneralSpectrumTask a;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpectrumAverage(GeneralSpectrumTask task)
        {
            a = task;
            Mode = SpectrumAverageMode.NoAveraging;
            WeightingType = SpectrumWeightingType.LinearMoving;
            Size = 1;
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpectrumAverage()
        {
            Mode = SpectrumAverageMode.NoAveraging;
            WeightingType = SpectrumWeightingType.LinearMoving;
            Size = 1;
        }

        private SpectrumAverageMode _mode;
        /// <summary>
        /// 平均方式
        /// </summary>
        public SpectrumAverageMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                a._amplitudeAverageAux.Mode = _mode;
            }
        }

        private SpectrumWeightingType _weightingType;
        /// <summary>
        /// 平均加权方式
        /// </summary>
        /// <returns></returns>
        public SpectrumWeightingType WeightingType
        {
            get
            {
                return _weightingType;
            }
            set
            {
                _weightingType = value;
                a._amplitudeAverageAux.WeightingType = _weightingType;
            }
        }

        private double _size;
        /// <summary>
        /// 当平均方式为RMSAverage时，对于LinearMoving加权设定平均次数，对于Exponential加权设定指数衰减系数
        /// </summary>
        public double Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                a._amplitudeAverageAux.Size = _size;
            }
        }
    }

    /// <summary>
    /// 频谱计算相关信息
    /// </summary>
    public class SpectralInformation
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SpectralInformation()
        {
            FreqStart = 0;
            FreqDelta = 0;
            FFTSize = 0;
            FFTCount = 0;
        }

        /// <summary>
        /// 频谱起始频率f0
        /// </summary>
        public double FreqStart { get; internal set; }

        /// <summary>
        /// 频谱谱线的频率间隔df
        /// </summary>
        public double FreqDelta { get; internal set; }

        /// <summary>
        /// 每次计算FFT时的长度，即每次应输入的时域波形长度。
        /// </summary>
        public int FFTSize { get; internal set; }

        /// <summary>
        /// 当前已计算FFT的次数，该值会被Reset()方法清零。
        /// </summary>
        public int FFTCount { get; internal set; }
    }

    internal enum SpectrumAveragingDataUnit
    {
        Linear,
        dB
    }
    #endregion
}
