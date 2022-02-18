using System;
using System.Reflection;
using SeeSharpTools.JXI.Exception;

namespace SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters
{
    /// <summary>
    /// internal used AveragedIIRFilter
    /// </summary>
    internal class AveragedIIRFilter
    {
        #region-------------------------Private Fields---------------------------------
        private double[] _demoniator;
        private double[] _numerator;
        private double[] _status;
        private int _numOfStatus = 0;
        private ulong _filteredPointns = 0;

        private TimeAveragingMode _avgMode = TimeAveragingMode.Fast;
        private double _timeConstant = 0;
        private double _alpha = 0; //_alpha is the multipling constant in exponential averaging
        private double _sampleRate = 51200; //constant for 1/3 octave analysis
        private double _level = 0;
        private bool _debugLevel = false;  //debug code 20170506-level JXISH
        #endregion

        #region--------------------------Public Fields---------------------------------
        /// <summary>
        ///Filtered Points
        /// Simplified Chinese：已经滤波的点数
        /// </summary>
        public ulong FilteredPointns
        {
            get
            {
                return _filteredPointns;
            }
        }

        /// <summary>
        /// Sample rate
        /// Simplified Chinese：采样率
        /// </summary>
        public double SampleRate
        {
            get
            {
                return _sampleRate;
            }

            set
            {
                _sampleRate = value;
            }
        }

        /// <summary>
        /// Averaging mode
        /// Simplified Chinese：平均方式
        /// </summary>
        public TimeAveragingMode AvgMode
        {
            get
            {
                return _avgMode;
            }

            set
            {
                _avgMode = value;
                switch (_avgMode)
                {
                    case TimeAveragingMode.Fast:
                        _timeConstant = 0.125;
                        break;
                    case TimeAveragingMode.Slow:
                        _timeConstant = 1;
                        break;
                    case TimeAveragingMode.Impulsive:
                        _timeConstant = 0.035;
                        break;
                    default:
                        _timeConstant = 0.125;
                        break;
                }
            }
        }

        /// <summary>
        /// Debug level
        /// Simplified Chinese：修正
        /// </summary>
        public bool DebugLevel
        {
            get
            {
                return _debugLevel;
            }

            set
            {
                _debugLevel = value;
            }
        }
        #endregion

        #region----------------------------Public Methods------------------------------
        /// <summary>
        /// 
        /// Simplified Chinese：初始化
        /// </summary>
        public void Init()
        {
            _level = 0;
            _sampleRate = 51200;
            _avgMode = TimeAveragingMode.Slow;
            _timeConstant = 0.125;
            _filteredPointns = 0;
            for (int i = 0; i < _numOfStatus; i++)
            {
                _status[0] = 0;
            }
        }

        /// <summary>
        /// Set the filter coefficients
        /// Simplified Chinese：设置滤波器系数
        /// </summary>
        /// <param name="numerator">numerator Simplified Chinese：分子</param>
        /// <param name="demoniator">demoniator Simplified Chinese：分母</param>
        public void SetCoefficients(double[] numerator, double[] demoniator)
        {
            _demoniator = new double[demoniator.Length];
            _numerator = new double[numerator.Length];
            for (int i = 0; i < numerator.Length; i++)
            {
                _numerator[i] = numerator[i];
            }
            for (int i = 0; i < demoniator.Length; i++)
            {
                _demoniator[i] = demoniator[i];
            }
            _numOfStatus = Math.Max(demoniator.Length, numerator.Length);
            _status = new double[_numOfStatus];
            for (int i = 0; i < _numOfStatus; i++)
            {
                _status[0] = 0;
            }
            _filteredPointns = 0;
            _level = 0;
        }

        /// <summary>
        /// This method is IIR filter direct form II structure;
        /// where _numerator=numerators and _demoniator=denominators, 
        /// they are sharing the same status registor _status;
        /// Simplified Chinese:使用直接II型IIR滤波器
        /// 所有输入信号共享一个状态寄存器
        /// </summary>
        /// <param name="x">input signal</param>
        /// <param name="length">length of inputsignal</param>
        public double Filter(double[] x, int length)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            int aLength = _demoniator.Length;
            int bLength = _numerator.Length;
            double backwardSum = 0;
            double forwardSum = 0;
            //this part is used for debug of _level output, it defines an array to record the change of level
            //and plot into JXI data viewer
            double[] levelWaveform = new double[length];
            //debug code 20170506-level JXISH

            _level = 0; //need to revise if not single block

            //_alpha is the exp factor for level averaging
            //(1-_alpha)^(_sampleRate*_timeConstant) = e^-1
            _alpha = 1 - Math.Pow(Math.E, (-1 / (_sampleRate * _timeConstant)));
            for (i = 0; i < length; i++)
            {
                backwardSum = 0;
                forwardSum = 0;
                for (j = 0; j < _status.Length; j++) //s1.Length = max (_demoniator.Length, _numerator.Length) set in set _demoniator/_numerator
                {
                    k = _status.Length - 1 - j; //in this loop, k goes from last _status to _status[0]
                    if (k > 0)
                    {
                        _status[k] = _status[k - 1];
                        if (k < aLength) backwardSum -= _demoniator[k] * _status[k];
                        if (k < bLength) forwardSum += _numerator[k] * _status[k];
                    }
                    else
                    {
                        backwardSum += x[i];
                        _status[0] = backwardSum;
                        forwardSum += backwardSum * _numerator[0];
                    }
                }
                _filteredPointns++;
                //_level average MUST in power, as the wveform is almost at tritical sampling rate
                _level = _level + _alpha * (forwardSum * forwardSum - _level);
                //debug code 20170506-level JXISH following 1 line:
                if (_debugLevel) levelWaveform[i] = Math.Sqrt(_level); //debug _level or forwardSum
            }
            _level = Math.Sqrt(_level); // returns level as Voltage instead of power
            return (_level);
        }
#endregion;
    }




    /// <summary>
    /// <para>IIR Filter Class</para>
    /// <para>Simplified Chinese:IIR滤波器类</para>
    /// </summary>
    public class IIRFilter
    {
        #region----------------------------Construct-------------------------------------
        /// <summary>
        /// 构造函数
        /// </summary>
        static IIRFilter()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IIRFilter()
        {
            IsSOS = false;
        }
        #endregion

        #region-------------------------Private Fields---------------------------------
        private double[] _demoniator, _numerator, _status;
        /// <summary>
        /// SOS status
        /// </summary>
        private double[,] _statusSOS;
        /// <summary>
        /// G
        /// </summary>
        private double[] _g;
        /// <summary>
        /// SOS
        /// </summary>
        private double[,] _SOS;

        private int numOfStatus = 0;
        private ulong _filteredPointns = 0;
        #endregion

        #region--------------------------Public Fields---------------------------------
        /// <summary>
        /// <para>Filtered Points</para>
        /// <para>Simplified Chinese：已经滤波的点数</para>
        /// </summary>
        public ulong FilteredPointns
        {
            get
            {
                return _filteredPointns;
            }
        }

        /// <summary>
        /// 是否使用二阶级联方式，默认否
        /// </summary>
        public bool IsSOS { get; set; }
        #endregion;

        #region----------------------------Public Methods------------------------------
        /// <summary>
        /// <para>Clear status registor</para>
        /// <para>Simplified Chinese:清空状态寄存器</para>
        /// </summary>
        public void Reset()
        {
            _filteredPointns = 0;
            if (_status != null)
            {
                for (int i = 0; i < _status.Length; i++)
                {
                    _status[i] = 0;
                }
            }
            if (_statusSOS != null)
            {
                for (int i = 0; i < _statusSOS.GetLength(0); i++)
                {
                    for (int j = 0; j < _statusSOS.GetLength(1); j++)
                    {
                        _statusSOS[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// <para> Set the filter coefficients</para>
        /// <para>Simplified Chinese：设置滤波器系数</para>
        /// </summary>
        /// <param name="numerator">numerator Simplified Chinese：分子</param>
        /// <param name="demoniator">demoniator Simplified Chinese：分母</param>
        public void SetCoefficients(double[] numerator, double[] demoniator)
        {
            if (IsSOS)
            {
                throw new JXIParamException("IsSOS为真时，不能调用此函数");
            }
            _demoniator = new double[demoniator.Length];
            _numerator = new double[numerator.Length];
            for (int i = 0; i < numerator.Length; i++)
            {
                _numerator[i] = numerator[i];
            }
            for (int i = 0; i < demoniator.Length; i++)
            {
                _demoniator[i] = demoniator[i];
            }
            numOfStatus = Math.Max(demoniator.Length, numerator.Length);
            if (_status == null || _status.Length != numOfStatus)
            {
                _status = new double[numOfStatus];
                Reset();
            }
            //_status = new double[numOfStatus];
            //for (int i = 0; i < numOfStatus; i++)
            //{
            //    _status[0] = 0;
            //}
            _filteredPointns = 0;
        }


        /// <summary>
        /// <para> Set the filter coefficients</para>
        /// <para>Simplified Chinese：设置滤波器系数</para>
        /// </summary>
        /// <param name="SOS">numerator Simplified Chinese：二阶矩阵</param>
        /// <param name="G">demoniator Simplified Chinese：增益</param>
        public void SetCoefficients(double[,] SOS, double[] G)
        {
            if (!IsSOS)
            {
                throw new JXIParamException("IsSOS为假时，不能调用此函数");
            }
            _SOS  = new double[ SOS.GetLength(0),SOS.GetLength(1)];
            _g = new double[G.Length ];
            for (int i = 0; i < SOS.GetLength(0); i++)
            {
                for (int j = 0; j < SOS.GetLength(1); j++)
                {
                    _SOS[i, j] = SOS[i, j];
                }
            }
            for (int i = 0; i < _g.Length; i++)
            {
                _g[i] = G[i];
            }
            numOfStatus = Math.Max(SOS.GetLength(1), G.Length);
            if (_statusSOS == null || _statusSOS.GetLength(0)!= numOfStatus)
            {
                _statusSOS = new double[numOfStatus,3];
                Reset();
            }
            _filteredPointns = 0;
        }

        /// <summary>
        /// <para>This method is IIR filter direct form II structure;</para>
        /// <para>where _numerator=numerators and _demoniator=denominators, </para>
        /// <para>they are sharing the same status registor _status;</para>
        /// <para>Simplified Chinese：直接II型IIR滤波器，共享一个状态寄存器</para>
        /// </summary>
        /// <param name="x">input signal  Simplified Chinese：输入信号</param>
        /// <param name="y">output signal same length as input signal  Simplified Chinese：输出信号，长度与输入信号一致 </param>
        public void Filter(double[] x, ref double[] y)
        {
            if (!IsSOS)
            {
                int i = 0;
                int j = 0;
                int k = 0;
                int aLength = _demoniator.Length;
                int bLength = _numerator.Length;
                double backwardSum = 0;
                double forwardSum = 0;

                for (i = 0; i < x.Length; i++)
                {
                    backwardSum = 0;
                    forwardSum = 0;
                    for (j = 0; j < _status.Length; j++) //s1.Length = max (_demoniator.Length, _numerator.Length) set in set _demoniator/_numerator
                    {
                        k = _status.Length - 1 - j; //in this loop, k goes from last _status to _status[0]
                        if (k > 0)
                        {
                            _status[k] = _status[k - 1];
                            if (k < aLength) backwardSum -= _demoniator[k] * _status[k];//分母
                            if (k < bLength) forwardSum += _numerator[k] * _status[k];//分子
                        }
                        else
                        {
                            backwardSum += x[i];
                            _status[0] = backwardSum;
                            forwardSum += backwardSum * _numerator[0];
                        }
                    }
                    y[i] = forwardSum;
                }
            }
            else
            {
                int i = 0;
                int j = 0;
                int k = 0;
                int aLength = 3;
                int bLength = 3;
                double backwardSum = 0;
                double forwardSum = 0;
                double g = 1;
                double tempX = 0;
                for (int m = 0; m < _g.Length; m++)
                {
                    g *= _g[m];
                }
                for (i = 0; i < x.Length; i++)
                {
                    tempX = x[i];
                    for (j = 0; j < _SOS.GetLength(0); j++)//二阶级联
                    {
                        backwardSum = 0;
                        forwardSum = 0;
                        for (int n = 0; n < 3; n++)
                        {
                            k = 3 - 1 - n; //in this loop, k goes from last _status to _status[0]
                            if (k > 0)
                            {
                                _statusSOS[j, k] = _statusSOS[j, k - 1];
                                if (k < aLength) backwardSum -= _SOS[j, k + 3] * _statusSOS[j, k];//分母
                                if (k < bLength) forwardSum += _SOS[j, k] * _statusSOS[j, k];//分子
                            }
                            else
                            {
                                backwardSum += tempX;
                                _statusSOS[j, 0] = backwardSum;
                                forwardSum += backwardSum * _SOS[j, 0];
                            }
                        }
                        tempX = forwardSum;//前一个二阶系统往后一个二阶系统传输的值
                    }
                    y[i] = g * forwardSum;
                }
            }
            _filteredPointns += (ulong)x.Length;
        }
        #endregion
    }

    #region-------------------AveragedIIRFilter类所需要的枚举类型----------------------
    /// <summary>
    /// Weighting Type
    /// </summary>
    internal enum WeightingType { None, AWeighting };

    /// <summary>
    /// Time Averaging Mode
    /// </summary>
    public  enum TimeAveragingMode
    {
        /// <summary>
        /// 
        /// </summary>
        Impulsive,

        /// <summary>
        /// 
        /// </summary>
        Fast,

        /// <summary>
        /// 
        /// </summary>
        Slow
    };
    #endregion
}
