using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;
using System;
using System.Reflection;

namespace SeeSharpTools.JXI.SignalProcessing.Measurement
{
    /// <summary>
    /// 1/3 Octave Analyzer Class
    /// </summary>
    public class ThirdOctaveAnalysis
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 调License Manager
        /// </summary>
        static ThirdOctaveAnalysis()
        {

        }
        #endregion

        #region-------------------------Private Filed--------------------------------
        private int _numOfBands = 31;
        private double[] _waveform;
        private double[] _thirdOctaveLevels = new double[31]; //cannot use numOfBands so put const
        private double[] _NominalFrequencies = { 20.00, 25.00, 31.50, 40.00, 50.00, 63.00, 80.00, 100.00, 125.00, 160.00, 200.00, 250.00, 315.00, 400.00, 500.00, 630.00, 800.00, 1000.00, 1250.00, 1600.00, 2000.00, 2500.00, 3150.00, 4000.00, 5000.00, 6300.00, 8000.00, 10000.00, 12500.00, 16000.00, 20000.00 };
        private TimeAveragingMode _averageMode = TimeAveragingMode.Fast;       
        private WeightingType _weightingFilterType = WeightingType.None;
        private double _waveformSampleRate = 51200;
        private bool _debug = false;

        //all needed filter coefficients
        #region FilterConstants
        //Extra3rd Octave filters (16k, 20kHz)  hi frequency first
        private double[] b0ExtraThirdOctave = { 0.012686029371977, -0.000000000000000, -0.038058088115932, 0.000000000000000, 0.038058088115932, -0.000000000000000, -0.012686029371977 };
        private double[] a0ExtraThirdOctave = { 1.000000000000000, 4.057550044558943, 7.489281268887289, 7.972287142587239, 5.164840099331220, 1.929619219508647, 0.329908890323839 };
        private double[] b1ExtraThirdOctave = { 0.006926627365987, -0.000000000000000, -0.020779882097962, 0.000000000000000, 0.020779882097962, -0.000000000000000, -0.006926627365987 };
        private double[] a1ExtraThirdOctave = { 1.000000000000000, 2.071380599358886, 3.581963749093841, 3.360164569733623, 2.683719187187380, 1.152934076726406, 0.416635149399126 };
        //3rd Octave filters hi frequency first
        private double[] b0ThirdOctave = { 0.003727835058191, 0.000000000000000, -0.011183505174573, -0.000000000000000, 0.011183505174573, 0.000000000000000, -0.003727835058191 };
        private double[] a0ThirdOctave = { 1.000000000000000, -0.015494292130875, 2.314008731552099, -0.024247158392120, 1.844018768104695, -0.009752051687584, 0.500204099247212 };
        private double[] b1ThirdOctave = { 0.001980881062446, 0.000000000000000, -0.005942643187337, -0.000000000000000, 0.005942643187337, 0.000000000000000, -0.001980881062446 };
        private double[] a1ThirdOctave = { 1.000000000000000, -1.765072516523764, 3.497239402348804, -3.120107639752248, 2.915504928427239, -1.223530867852739, 0.577658354674395 };
        private double[] b2ThirdOctave = { 0.001041108353595, 0.000000000000000, -0.003123325060784, -0.000000000000000, 0.003123325060784, 0.000000000000000, -0.001041108353595 };
        private double[] a2ThirdOctave = { 1.000000000000000, -3.083060549860066, 5.743136931494242, -6.397988617545618, 4.968422321940684, -2.306166301563761, 0.647236073281918 };
        //LPF for filterbank decimations
        private double[] bLPFThirdOctave = { 0.002092531553488, 0.008667548183196, 0.022284869934039, 0.040693298720820, 0.057337729008005, 0.064071412169423, 0.057337729008005, 0.040693298720820, 0.022284869934039, 0.008667548183196, 0.002092531553488 };
        private double[] aLPFThirdOctave = { 1.000000000000000, -2.693761931480981, 4.514835697963922, -4.821875521478438, 3.777752391408671, -2.125595053573468, 0.890395829577704, -0.263308927112825, 0.054041554032889, -0.006652210892085, 0.000391538523129 };
        //A-weighting filter
        private double[] bAWeighting = { 0.557027464925462, -2.007175827111765, 2.480449724869454, -0.990453584530497, -0.194991290075031, 0.133258780270000, 0.021884731652377 };
        private double[] aAweighting = { 1.000000000000000, -4.253559171048728, 7.114704841645024, -5.850348663746044, 2.399523688478542, -0.439020614913542, 0.028699924427129 };
        #endregion

        #endregion

        #region---------------------------Public Filed-------------------------------

        /// <summary>
        /// <para>Num of bands</para>
        /// <para>Chinese Simplified：频段数(31)</para>
        /// </summary>
        public int NumOfBands
        {
            get
            {
                return _numOfBands;
            }
        }

        /// <summary>
        /// <para>1/3 Octave Levels</para>
        /// <para>Chinese Simplified：三分之一倍频程分析后的能量</para>
        /// </summary>
        public double[] ThirdOctaveLevels
        {
            get
            {
                return _thirdOctaveLevels;
            }
        }

        /// <summary>
        /// <para>The center frequency of each frequency band</para>
        /// <para>Chinese Simplified：每个频段对应的中心频率</para>
        /// </summary>
        public double[] NominalFrequencies
        {
            get
            {
                return _NominalFrequencies;
            }
        }

        /// <summary>
        /// <para>Num of bands</para>
        /// <para>Chinese Simplified：平均方式</para>
        /// </summary>
        public TimeAveragingMode AverageMode
        {
            get
            {
                return _averageMode;
            }
            set
            {
                _averageMode = value;
            }
        }

        /// <summary>
        /// <para>Num of bands</para>
        /// <para>Chinese Simplified：修正</para>
        /// </summary>
        public bool Debug
        {
            get
            {
                return _debug;
            }

            set
            {
                _debug = value;
            }
        }

        /// <summary>
        /// <para>Type of WeightingFilter </para>
        /// <para>Chinese Simplified：加权滤波器的类型</para>
        /// </summary>
        public WeightingType WeightingFilterType
        {
            get
            {
                return _weightingFilterType;
            }

            set
            {
                _weightingFilterType = value;
            }
        }
        #endregion

        #region-----------------------------Private Methods--------------------------
        ///<summary>
        ///预留备用
        ///</summary>  
        private void ResetAveraging()
        {

        }

        #endregion

        #region-------------------------Public Methods-------------------------------
        /// <summary>
        /// <para>Third Octave Analyze</para>
        /// <para>Chinese Simplified：三分之一倍频程分析</para>
        /// </summary>
        /// <param name="x">
        /// <para>The input signal</para>
        /// <para>Chinese Simplified：输入信号</para> 
        /// </param>
        /// <param name="sampleRate">
        /// <para>Sample rate</para>
        /// <para>Chinese Simplified：采样率</para>
        /// </param>
        /// <returns>
        /// <para>Energy of each band</para>
        /// <para>Chinese Simplified：三分之一倍频程分析后每个频段对应的能量</para>
        /// </returns>
        public ThirdThirdOctaveResult Analyze(double[] x, double sampleRate) 
        {
            _waveformSampleRate = sampleRate;

            if (_waveform == null || _waveform.Length != x.Length)
            {
                _waveform = new double[x.Length];
            }
            Buffer.BlockCopy(x, 0, _waveform, 0, sizeof(double) * x.Length);//将输入信号拷贝到x中

            AveragedIIRFilter Filter = new AveragedIIRFilter();//实例化IIR滤波器
            Filter.Init();//初始化
            IIRFilter LPF = new IIRFilter();
            IIRFilter WeightingFilter = new IIRFilter();
            int length = 0;

            if (_weightingFilterType == WeightingType.AWeighting)
            {
                WeightingFilter.SetCoefficients(bAWeighting, aAweighting);
                WeightingFilter.Filter(_waveform, ref _waveform);
            }
            Filter.AvgMode = AverageMode;
            length = _waveform.Length;
            Filter.SetCoefficients(b0ExtraThirdOctave, a0ExtraThirdOctave);
            _thirdOctaveLevels[_numOfBands - 1] = Filter.Filter(_waveform, length);
            Filter.SetCoefficients(b1ExtraThirdOctave, a1ExtraThirdOctave);
            _thirdOctaveLevels[_numOfBands - 2] = Filter.Filter(_waveform, length);
            Filter.SampleRate = _waveformSampleRate;
            for (int i = 0; i < 10; i++)
            {
                Filter.SetCoefficients(b0ThirdOctave, a0ThirdOctave);
                _thirdOctaveLevels[(9 - i) * 3 + 1] = Filter.Filter(_waveform, length);
                if (i == 9) Filter.DebugLevel = _debug;  //debug code 20170506-level JXISH
                Filter.SetCoefficients(b1ThirdOctave, a1ThirdOctave);
                _thirdOctaveLevels[(9 - i) * 3] = Filter.Filter(_waveform, length);
                Filter.DebugLevel = false;  //debug code 20170506-level JXISH
                if (i < 9)
                {
                    Filter.SetCoefficients(b2ThirdOctave, a2ThirdOctave);
                    _thirdOctaveLevels[(9 - i) * 3 - 1] = Filter.Filter(_waveform, length);
                }
                if (i < 9)
                {
                    //Decimate by 2
                    LPF.SetCoefficients(bLPFThirdOctave, aLPFThirdOctave);
                    double[] newWaveform = new double[length];
                    Array.Copy(_waveform, newWaveform, length);
                    LPF.Filter(newWaveform, ref newWaveform);
                    length = length / 2;
                    for (int j = 0; j < length; j++) //decimate _newWaveform to 1/2
                    {
                        _waveform[j] = newWaveform[j * 2]; //gain ERROR
                    }
                    Filter.SampleRate = Filter.SampleRate / 2;
                }
            }
            ThirdThirdOctaveResult result = new ThirdThirdOctaveResult(_thirdOctaveLevels, _NominalFrequencies);
            return result;
        }
        #endregion
    }

    #region-----------------------ThirdOctaveAnalysis类需要的枚举和类--------------------
    /// <summary>
    /// <para>The type of weighting</para>
    /// <para>Chinese Simplified：加权方式</para>
    /// </summary>
    public enum WeightingType
    {
        /// <summary>
        /// 不加权
        /// </summary>
        None,

        /// <summary>
        /// 加权
        /// </summary>
        AWeighting
    };   
    
    /// <summary>
    /// 分析（Analyze)之后得到的结果类型
    /// </summary>
    public class ThirdThirdOctaveResult
    {
        /// <summary>
        /// <para>1/3 Octave Levels</para>
        /// <para>Chinese Simplified：三分之一倍频程分析后的能量</para>
        /// </summary>
        public double[] ThirdOctaveLevels { get; }

        /// <summary>
        /// <para>The center frequency of each frequency band</para>
        /// <para>Chinese Simplified：每个频段对应的中心频率</para>
        /// </summary>
        public double[] NominalFrequencies { get; }

        /// <summary>
        /// <para>Chinese Simplified：构造函数</para>
        /// </summary>
        /// <param name="thirdOctaveLevels"></param>
        /// <param name="nominalFrequencies"></param>
        public ThirdThirdOctaveResult(double[] thirdOctaveLevels,double[] nominalFrequencies)
        {
            ThirdOctaveLevels = thirdOctaveLevels;
            NominalFrequencies = nominalFrequencies;
        }
    }
    #endregion
}
