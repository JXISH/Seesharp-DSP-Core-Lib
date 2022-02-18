using System;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using System.Reflection;


namespace SeeSharpTools.JXI.SignalProcessing.Measurement
{
    /// <summary>
    /// <para>Frequency Response Function</para>
    /// <para>Chinese Simplified：频响函数</para>
    /// </summary>
    public  class FrequencyResponseFunction
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数 调License Manager
        /// </summary>
        static FrequencyResponseFunction()
        {
        }

        /// <summary>
        /// Construct
        /// </summary>
        public FrequencyResponseFunction()
        {
            Average = new AverageParam();
            _averageCount = 0;
        }
        #endregion

        #region---------------------------Public Fileds---------------------
        /// <summary>
        /// 平均
        /// </summary>
        public AverageParam Average { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public bool ResetAveraging
        {
            set
            {
                if (value)
                {
                    _averageCount = 0;
                }
            }
        }

        /// <summary>
        /// 平均是否完成
        /// </summary>
        public bool AveragingDone
        {
            get
            {
                return (_averageCount >= Average.Number);
            }
        }
        #endregion

        #region-------------------------Private Fileds----------------------
        private double[] _inputWaveform;
        private double[] _outputWaveform;
        private double[] _mag;
        private double[] _magDB;
        private double[] _phase;
        private double[] _phaseInDegree;
        private double[] _coherent;
        private double[] _Sxx;
        private double[] _Syy;
        private double[] _SxyRe;
        private double[] _SxyIm;
        private int _averageCount;
        #endregion


        #region-----------------------Public Methods------------------------
        /// <summary>
        /// 初始化
        /// </summary>
        public void Reset()
        {
            if (_inputWaveform != null)
            {
                _Sxx = new double[_inputWaveform.Length];
                _SxyIm = new double[_inputWaveform.Length];
                _SxyRe = new double[_inputWaveform.Length];
                _Syy = new double[_inputWaveform.Length];
                ResetAveraging = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inDB"></param>
        /// <returns></returns>
        public double[] GetMagenitude(bool inDB)
        {
            if (_inputWaveform == null || _outputWaveform == null)
            {
                return null;
            }
            Analyze(_inputWaveform, _outputWaveform);
            if (inDB)
            {
                return (_magDB);
            }
            else
            {
                return (_mag);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inDegree"></param>
        /// <returns></returns>
        public double[] GetPhase(bool inDegree)
        {
            if (_inputWaveform == null || _outputWaveform == null)
            {
                return null;
            }
            Analyze(_inputWaveform, _outputWaveform);
            if (inDegree)
            {
                return (_phaseInDegree);
            }
            else
            {
                return (_phase);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double[] GetCoherente()
        {
            if (_inputWaveform == null || _outputWaveform == null)
            {
                return null;
            }
            Analyze(_inputWaveform, _outputWaveform);
            return (_coherent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputWaveform">
        /// <para>The input signal of the system</para>
        /// <para>Chinese Simplified：待测系统的输入信号</para>
        /// </param>
        /// <param name="outputWaveform">
        /// <para>The output signal of the system</para>
        /// <para>Chinese Simplified：经过待测系统的信号</para>
        /// </param>
        public void Analyze(double[] inputWaveform, double[] outputWaveform)
        {
            if (_inputWaveform == null || _inputWaveform.Length != inputWaveform.Length)
            {
                _inputWaveform = new double[inputWaveform.Length];
            }
            Array.Copy(inputWaveform, _inputWaveform, inputWaveform.Length);
            if (_outputWaveform == null || _outputWaveform.Length != outputWaveform.Length)
            {
                _outputWaveform = new double[outputWaveform.Length];
            }
            Array.Copy(outputWaveform, _outputWaveform, outputWaveform.Length);
            if (_Sxx == null || _Sxx.Length != _inputWaveform.Length)
            {
                _Sxx = new double[_inputWaveform.Length];
            }
            if (_SxyIm == null || _SxyIm.Length != _inputWaveform.Length)
            {
                _SxyIm = new double[_inputWaveform.Length];
            }
            if (_SxyRe == null || _SxyRe.Length != _inputWaveform.Length)
            {
                _SxyRe = new double[_inputWaveform.Length];
            }
            if (_Syy == null || _Syy.Length != _inputWaveform.Length)
            {
                _Syy = new double[_inputWaveform.Length];
            }

            GeneralSpectrumTask spectrumAnalysis = new GeneralSpectrumTask();
            if (_inputWaveform.Length == _outputWaveform.Length)
            {
                _mag = new double[_inputWaveform.Length / 2];
                _magDB = new double[_inputWaveform.Length / 2];
                _phase = new double[_inputWaveform.Length / 2];
                _phaseInDegree = new double[_inputWaveform.Length / 2];
                _coherent = new double[_inputWaveform.Length / 2];
                double[] magX = new double[_inputWaveform.Length / 2];
                double[] phaseX = new double[_inputWaveform.Length / 2];
                double[] magY = new double[_inputWaveform.Length / 2];
                double[] phaseY = new double[_inputWaveform.Length / 2];
                double magSxx = 0;
                double magSxy = 0;
                double magSyy = 0;
                double phaseSxy = 0;
                int i = 0;

                spectrumAnalysis.InputDataType = InputDataType.Real;
                spectrumAnalysis.SampleRate = 1;
                spectrumAnalysis.WindowType = Window.WindowType.Hanning; //Hanning has better dynamic range than Hamming-JXISH found 20170514;

                //添加Average属性
                spectrumAnalysis.Average.Mode = SpectrumAverageMode.NoAveraging;
                spectrumAnalysis.Average.WeightingType = SpectrumWeightingType.LinearContinuous;
                spectrumAnalysis.Average.Size = 1;
                spectrumAnalysis.Output.NumberOfLines = _inputWaveform.Length / 2;
                spectrumAnalysis.Unit.Type = SpectrumOutputUnit.V;
                spectrumAnalysis.Unit.Impedance = 50;
                spectrumAnalysis.Unit.IsPSD = false;

                spectrumAnalysis.GetSpectrum(_inputWaveform, ref magX, ref phaseX);
                spectrumAnalysis.GetSpectrum(_outputWaveform, ref magY, ref phaseY);
                // H1=Sxy/Sxx, which reduces the noise from output; and it is the most popular FRF mode
                // options could be:
                // H2=Syy*Sxy/(Sxy*Sxy)
                // H3 ...
                for (i = 0; i < magX.Length; i++)
                {
                    magSxx = magX[i] * magX[i];
                    magSxy = magX[i] * magY[i];
                    magSyy = magY[i] * magY[i];
                    phaseSxy = phaseY[i] - phaseX[i];
                    if (Average.Mode == AverageMode.RMS)
                    {
                        if (_averageCount < Average.Number)
                        {
                            _Sxx[i] = (_Sxx[i] * _averageCount + magSxx) / (_averageCount + 1);
                            _Syy[i] = (_Syy[i] * _averageCount + magSyy) / (_averageCount + 1);
                            _SxyRe[i] = (_SxyRe[i] * _averageCount + magSxy * Math.Cos(phaseSxy)) / (_averageCount + 1);
                            _SxyIm[i] = (_SxyIm[i] * _averageCount + magSxy * Math.Sin(phaseSxy)) / (_averageCount + 1);
                            if (i == magX.Length - 1)
                            {
                                _averageCount++;
                            }
                        }
                        magSxx = _Sxx[i];
                        magSyy = _Syy[i];
                        magSxy = Math.Sqrt(_SxyIm[i] * _SxyIm[i] + _SxyRe[i] * _SxyRe[i]);
                        phaseSxy = Math.Atan2(_SxyIm[i], _SxyRe[i]);
                    }
                    _mag[i] = magSxy / magSxx;
                    _phase[i] = phaseSxy;
                    _coherent[i] = magSxy * magSxy / (magSxx * magSyy);
                    if (_mag[i] < 0)
                    {
                        _mag[i] = -_mag[i];
                        _phase[i] = _phase[i] + Math.PI;
                    }
                    _magDB[i] = 20 * Math.Log10(_mag[i]); //spectrum unit in V
                    // modulo to 2 Pi
                    _phase[i] = _phase[i] - Math.Floor(_phase[i] / 2 / Math.PI) * 2 * Math.PI;
                    _phaseInDegree[i] = _phase[i] * 180 / Math.PI;
                }
                for (i = 0; i < magX.Length; i++)
                {
                    if (Math.Abs(_phaseInDegree[i]) > 180)
                    {
                        _phaseInDegree[i] -= Math.Round(_phaseInDegree[i] / 360) * 360;
                    }
                }
            }
        }
        #endregion
    }

    #region----------------------FrequencyResponseFunction所需要的枚举和类---------------------------
    /// <summary>
    /// Average Mode
    /// </summary>
    public enum AverageMode
    {
        /// <summary>
        /// No Average
        /// </summary>
        None,

        /// <summary>
        /// RMS Average
        /// </summary>
        RMS
    };

    /// <summary>
    /// 平均参数
    /// </summary>
    public class AverageParam
    {
        /// <summary>
        /// 
        /// </summary>
        public AverageMode Mode { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Number { set; get; }

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="num"></param>
        public AverageParam(AverageMode mode = AverageMode.None,int num = 1)
        {
            Mode = mode;
            Number = num;
        }      
    }
    #endregion

}
