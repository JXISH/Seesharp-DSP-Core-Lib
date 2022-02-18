using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Generation;

namespace SeeSharpTools.JXI.RFCommunications.Modem
{

    #region------------------------- 信号调制静态类（AM/FM） -------------------------

    /// <summary>
    /// 信号调制静态类。
    /// </summary>
    public static class EasyModem
    {
        /// <summary>
        /// 生成AM调制信号，调制的内容为标准波形（Sine/Triangle/Sawtooth/Square），频率可设。
        /// </summary>
        /// <param name="modulatedSignal"></param>
        /// <param name="sampleRate"></param>
        /// <param name="depth">调制深度，%。</param>
        /// <param name="messageFrequency"></param>
        /// <param name="messageSignalType"></param>
        /// <param name="snr">信噪比。默认为double.MaxValue，即不添加噪声。</param>
        public static void GenerateAM(Complex[] modulatedSignal, double sampleRate, double depth, double messageFrequency = 1000, FunctionGenSignalType messageSignalType = FunctionGenSignalType.Sine, double snr = double.MaxValue)
        {
            var amSource = new AmplitudeModulator();
            FunctionGenerator baseFunction;
            switch (messageSignalType)
            {
                case FunctionGenSignalType.Triangle:
                    {
                        baseFunction = new TriangleGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                case FunctionGenSignalType.Sawtooth:
                    {
                        baseFunction = new SawtoothGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                case FunctionGenSignalType.Square:
                    {
                        baseFunction = new SquareGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                default:
                    {
                        baseFunction = new SineGenerator(sampleRate, messageFrequency, 1);
                        break;
                    }
            }

            // 若指定了有效的snr，则添加噪声。
            if(snr != double.MaxValue && snr > 0)
            {
                baseFunction.NoiseType = NoiseType.UniformWhiteNoise;
                baseFunction.SNR = snr;
            }
            else
            {
                baseFunction.NoiseType = NoiseType.None;
            }

            amSource.Depth = depth;
            amSource.MessageGenerator = baseFunction;
            amSource.Generate(modulatedSignal);
        }

        /// <summary>
        /// 生成FM调制信号，调制的内容为标准波形（Sine/Triangle/Sawtooth/Square），频率可设。
        /// </summary>
        /// <param name="modulatedSignal"></param>
        /// <param name="sampleRate"></param>
        /// <param name="deviation">最大频偏，Hz。</param>
        /// <param name="messageFrequency"></param>
        /// <param name="messageSignalType"></param>
        /// <param name="snr">信噪比。默认为double.MaxValue，即不添加噪声。</param>
        public static void GenerateFM(Complex[] modulatedSignal, double sampleRate, double deviation, double messageFrequency = 1000, FunctionGenSignalType messageSignalType = FunctionGenSignalType.Sine, double snr = double.MaxValue)
        {
            var fmSource = new FrequencyModulator();
            FunctionGenerator baseFunction;
            switch (messageSignalType)
            {
                case FunctionGenSignalType.Triangle:
                    {
                        baseFunction = new TriangleGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                case FunctionGenSignalType.Sawtooth:
                    {
                        baseFunction = new SawtoothGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                case FunctionGenSignalType.Square:
                    {
                        baseFunction = new SquareGenrator(sampleRate, messageFrequency, 1);
                        break;
                    }
                default:
                    {
                        baseFunction = new SineGenerator(sampleRate, messageFrequency, 1);
                        break;
                    }
            }

            // 若指定了有效的snr，则添加噪声。
            if (snr != double.MaxValue && snr > 0)
            {
                baseFunction.NoiseType = NoiseType.UniformWhiteNoise;
                baseFunction.SNR = snr;
            }
            else
            {
                baseFunction.NoiseType = NoiseType.None;
            }

            fmSource.Deviation = deviation;
            fmSource.MessageGenerator = baseFunction;
            fmSource.Generate(modulatedSignal);
        }
    }

    #endregion

    #region------------------------- 可实例化的信号调制类（AM/FM） -------------------------

    /// <summary>
    /// 模拟调制基类。
    /// </summary>
    public abstract class AnalogModulator
    {

        #region------------------------- 公共属性 -------------------------

        protected MessageSourceType _messageSource;
        /// <summary>
        /// 信源类型。
        /// </summary>
        public MessageSourceType MessageSource
        {
            get { return _messageSource; }
            set { _messageSource = value; }
        }

        protected FunctionGenerator _functionGen;
        /// <summary>
        /// 当信源类型为波形发生器（FunctionGen）时的波形发生器实例。
        /// </summary>
        public FunctionGenerator MessageGenerator
        {
            get { return _functionGen; }
            set { _functionGen = value; }
        }

        protected double _sampleRate;
        /// <summary>
        /// 采样率。
        /// </summary>
        public double SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }

        protected double _initialPhase;
        /// <summary>
        /// 初始相位，in Degree。
        /// </summary>
        public double InitialPhase
        {
            get { return _initialPhase; }
            set { _initialPhase = value; }
        }

        protected double _cnr = 200;
        /// <summary>
        /// 载噪比，dB。
        /// </summary>
        public double CNR
        {
            get { return _cnr; }
            set { _cnr = value; }
        }

        #endregion

        #region------------------------- 私有数据 -------------------------

        /// <summary>
        /// 随机数生成器（噪声）。
        /// </summary>
        protected Random _randomGen;

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public AnalogModulator()
        {
            // 实例化SineGenerator，用于当信源类型为波形发生器（FunctionGen）时的波形生成。
            _functionGen = new SineGenerator(25000,1000, 1);

            _sampleRate = _functionGen.SampleRate;

            // 设置公共属性的默认值。
            _messageSource = MessageSourceType.FunctionGen;

            // 实例化私有成员
            _randomGen = new Random(NoiseGenerator.GetRandomSeed());
        }

        #endregion

        #region------------------------- 公共方法（抽象方法） -------------------------

        /// <summary>
        /// 重置调制器，清除历史数据。
        /// </summary>
        public virtual void Reset()
        {
            _functionGen.Reset();
        }

        /// <summary>
        /// 当信源类型为waveform时，对输入的信号进行调制。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="modulatedSignal"></param>
        public abstract void Modulate(double[] message, Complex[] modulatedSignal);

        /// <summary>
        /// 当信源类型为波形发生器（FunctionGen）时，获取调制后的信号。
        /// </summary>
        /// <param name="modulatedSignal"></param>
        public abstract void Generate(Complex[] modulatedSignal);

        /// <summary>
        /// 根据当前设置的调制参数，计算给定信源最高频率所需的最低采样率。
        /// </summary>
        /// <param name="messageFrequency"></param>
        /// <returns></returns>
        public abstract double CalcMinSampleRate(double messageFrequency);

        #endregion

        #region------------------------- 私有方法 -------------------------
       
        #endregion  

    }

    /// <summary>
    /// 幅度调制（AM）类。
    /// </summary>
    public class AmplitudeModulator : AnalogModulator
    {

        #region------------------------- 公共属性 -------------------------

        private double _modulationDepth;
        /// <summary>
        /// 调制深度，%。
        /// </summary>
        public double Depth
        {
            get { return _modulationDepth; }
            set { _modulationDepth = value; }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        public AmplitudeModulator():base ()
        {
            _modulationDepth = 50;
        }

        #endregion

        #region------------------------- 公共方法（重写基类方法） -------------------------

        /// <summary>
        /// 当信源类型为波形发生器（FunctionGen）时，获取调制后的信号。
        /// </summary>
        /// <param name="modulatedSignal"></param>
        public override void Generate(Complex[] modulatedSignal)
        {
            double[] message = new double[modulatedSignal.Length];
            _functionGen.Generate(message);
            this.Modulate(message, modulatedSignal);
        }

        /// <summary>
        /// 当信源类型为waveform时，对输入的信号进行调制。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="modulatedSignal"></param>
        public override void Modulate(double[] message, Complex[] modulatedSignal)
        {
            double modulationDepth = _modulationDepth / 100;                    // 调制度

            double phaseInRad = _initialPhase / 180 * Math.PI;                  // 初始相位            
            double initReal = Math.Cos(phaseInRad);                             // 初始相位对应复数实部
            double initImage = Math.Sin(phaseInRad);                            // 初始相位对应复数虚部

            double amplitude = 1;                                               // 载波幅度
            double noiseAmplitude = amplitude * Math.Pow(10, _cnr / -20) * 2;   // 载波噪声，*2 表示将 random 幅度范围在(-0.5 -  +0.5)之间 归一化为 (-1 - +1) 之间

            double actualAmplitude;                                             // 信号幅度

            for (int i = 0; i < message.Length; i++)
            {
                // 计算当前幅度
                // 信号幅度 =  调制倍率 * 载波幅度 =  调制倍率 * （ 理想载波幅度  +  噪声幅度 ）
                actualAmplitude = (message[i] * modulationDepth + 1) * (amplitude + noiseAmplitude * (_randomGen.NextDouble() - 0.5));
                // 生成复数信号
                modulatedSignal[i] = new Complex(actualAmplitude * initReal, actualAmplitude * initImage);
            }

        }

        /// <summary>
        /// 根据当前设置的调制参数，计算给定信源最高频率所需的最低采样率。
        /// </summary>
        /// <param name="messageFrequency"></param>
        /// <returns></returns>
        public override double CalcMinSampleRate(double messageFrequency) { return messageFrequency * 1.25; }

        #endregion

    }

    /// <summary>
    /// 频率调制（FM）类。
    /// </summary>
    public class FrequencyModulator : AnalogModulator
    {
        #region------------------------- 私有数据 -------------------------
        /// <summary>
        /// 下一次获取数据时的起始相位，角度梯度, 即[0,1)区间。
        /// 也可以理解为周期为1的信号的起始时间。
        /// </summary>
        protected double _phaseOfNextBlock;
        #endregion

        #region------------------------- 公共属性 -------------------------

        private double _freqDeviation;
        /// <summary>
        /// 调频最大频偏，Hz。
        /// </summary>
        public double Deviation
        {
            get { return _freqDeviation; }
            set { _freqDeviation = value; }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------
        /// <summary>
        /// 构造函数，并初始化参数
        /// </summary>
        public FrequencyModulator() : base()
        {
            _freqDeviation = 5000;

            // 重置参数
            Reset();
        }
        #endregion

        #region------------------------- 公共方法（重写基类方法） -------------------------

        /// <summary>
        /// 重置调制器，清除历史数据。
        /// </summary>
        public override void Reset()
        {
            // 调用基类方法。
            base.Reset();

            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0。
            _phaseOfNextBlock = FunctionGenerator.GenerateScaledPhase(_initialPhase, 1.0 / 360);
        }

        /// <summary>
        /// 当信源类型为波形发生器（FunctionGen）时，获取调制后的信号。
        /// </summary>
        /// <param name="modulatedSignal"></param>
        public override void Generate(Complex[] modulatedSignal)
        {
            double[] message = new double[modulatedSignal.Length];
            _functionGen.Generate(message);
            _sampleRate = _functionGen.SampleRate;
            this.Modulate (message, modulatedSignal);
        }

        /// <summary>
        /// 当信源类型为waveform时，对输入的信号进行调制。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="modulatedSignal"></param>
        public override void Modulate(double[] message, Complex[] modulatedSignal)
        {
            double intergratorScale = _freqDeviation / _sampleRate;         // 积分时间常数，对应相位从[0,1)区间            
            double scale = Math.PI * 2;                                     // scale,  是将相位从[0,1)区间，转换为 [0,2pi)区间

            double actualAmplitude;                                         // 输出信号幅度
            double actualPhase;                                             // 输出信号相位

            double currentPhase = _phaseOfNextBlock;                        // 相位累加器，在[0,1)区间

            double amplitude = 1;                                             // 载波幅度
            double noiseAmplitude = amplitude * Math.Pow(10, _cnr / -20) * 2; // 载波噪声，*2 表示将 random 幅度范围在(-0.5 -  +0.5)之间 归一化为 (-1 - +1) 之间

            // FM调制
            for (int i = 0; i < modulatedSignal.Length; i++)
            {
                // 计算当前相位
                actualPhase = currentPhase * scale;                                         // 将相位从[0,1)区间，转换为 [0,2pi)区间，即正弦实际相位。
                // 计算当前幅度
                actualAmplitude = amplitude + noiseAmplitude * (_randomGen.NextDouble() - 0.5);    // 信号幅度 =   理想载波幅度  +  噪声幅度 。
                // 生成复数信号
                modulatedSignal[i] = new Complex(actualAmplitude * Math.Cos(actualPhase), actualAmplitude * Math.Sin(actualPhase)); // 生成信号

                // 相位累加，获取下一时刻相位。
                currentPhase = FunctionGenerator.GenerateScaledPhase(message[i], intergratorScale, currentPhase); // 相位累加在[0,1)区间进行。
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = currentPhase;
        }

        /// <summary>
        /// 根据当前设置的调制参数，计算给定信源最高频率所需的最低采样率。
        /// </summary>
        /// <param name="messageFrequency"></param>
        /// <returns></returns>
        public override double CalcMinSampleRate(double messageFrequency) { return 2 * (messageFrequency + _freqDeviation); }

        #endregion

    }

    #endregion

    #region------------------------- 公共数据类型 -------------------------

    /// <summary>
    /// 模拟调制的信源类型。
    /// </summary>
    public enum MessageSourceType { Waveform, FunctionGen,  }

    #endregion

}
