using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

/************************************************************************************************
 * 主要以可实例化的类提供基础波形生成，尤其方便连续产生带噪声的测试波形
 * 同时简化提供静态方法
 * 来自 Git\RF\VST\CSharp Source\Public\Digital Signal Processing\Basic Function Generation.cs
*************************************************************************************************/
namespace SeeSharpTools.JXI.SignalProcessing.Generation
{

    #region------------------------- 信号生成静态类 -------------------------

    /// <summary>
    /// 波形生成静态类。
    /// </summary>
    public static class EasyGeneration
    {
        /// <summary>
        /// 生成单一复数正弦波。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="frequency"></param>
        /// <param name="sampleRate"></param>
        /// <param name="signalAmplitude"></param>
        /// <param name="snr"></param>
        /// <param name="initialPhase">in Degree.</param>
        public static void ComplexSine(Complex[] x, double frequency, double sampleRate, double signalAmplitude, double snr = 200, double initialPhase =0)
        {
            // 生成复数正弦信号。
            var generator = new ComplexSineGenerator(sampleRate, frequency, signalAmplitude, initialPhase);
            generator.NoiseType = (snr < 200) ? NoiseType.UniformWhiteNoise : NoiseType.None;
            generator.SNR = snr;
            generator.Generate(x);
        }

        /// <summary>
        /// 生成I/Q交错（Interleave）格式的单一复数正弦波。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="frequency"></param>
        /// <param name="sampleRate"></param>
        /// <param name="signalAmplitude"></param>
        /// <param name="snr"></param>
        /// <param name="initialPhase"></param>
        public static void ComplexSine(short[] x, double frequency, double sampleRate, double signalAmplitude, double snr = 200, double initialPhase = 0)
        {
            // 生成复数正弦信号。
            var generator = new ComplexSineGenerator(sampleRate, frequency, signalAmplitude, initialPhase);
            generator.NoiseType = (snr < 200) ? NoiseType.UniformWhiteNoise : NoiseType.None;
            generator.SNR = snr;
            generator.Generate(x);
        }

        /// <summary>
        /// 生成单一正弦波。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="frequency"></param>
        /// <param name="sampleRate"></param>
        /// <param name="signalAmplitude"></param>
        /// <param name="snr"></param>
        public static void Sine(short[] x, double frequency, double sampleRate, double signalAmplitude, double snr)
        {
            var generator = new SineGenerator(sampleRate, frequency, signalAmplitude);
            generator.NoiseType = (snr < 200) ? NoiseType.UniformWhiteNoise : NoiseType.None;
            generator.SNR = snr;
            generator.Generate(x);

        }

        /// <summary>
        /// 对于给定的信号频率和采样率，计算整周期波形的点数。
        /// </summary>
        /// <param name="signalFrequency">期望信号频率</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="actualFrequency">实际频率</param>
        /// <param name="tolerance">容差</param>
        /// <param name="minLength">最小长度</param>
        /// <returns>最少周期点数</returns>
        public static int CalcCycleQuantum(double signalFrequency, double sampleRate, int maxLength, out double actualFrequency, double tolerance = 1, int minLength = 0)
        {
            double[] inputFreq = new double[1] { signalFrequency };
            double[] outputFreq;
            int waveLength = FunctionGenerator.CalcCycleQuantum(inputFreq, sampleRate, maxLength, out outputFreq, tolerance, minLength);
            actualFrequency = outputFreq[0];
            return waveLength;
        }

    }

    #endregion

    #region------------------------- 可实例化的信号生成类（实数信号生成） -------------------------

    /// <summary>
    /// 基础波形生成基类。
    /// </summary>
    public abstract class FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        /// <summary>
        /// 临时数组，当获取的数据类型不为double[]时，先按照double[]生成并保存在临时此临时数组，再统一做数据类型转换。
        /// </summary>
        private double[] _rawDoubleData;

        /// <summary>
        /// 下一次获取数据时的起始相位，角度梯度, 即[0,1)区间。
        /// 也可以理解为周期为1的信号的起始时间。
        /// </summary>
        protected double _phaseOfNextBlock;

        /// <summary>
        /// 随机数生成器（噪声）。
        /// </summary>
        protected Random _randomGen;

        #endregion

        #region------------------------- 公共属性 -------------------------

        protected double _sampleRate;
        /// <summary>
        /// 采样率。
        /// </summary>
        public double SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }

        protected double _frequency;
        /// <summary>
        /// 频率。
        /// </summary>
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        protected double _amplitude;
        /// <summary>
        /// 幅度。
        /// </summary>
        public double Amplitude
        {
            get { return _amplitude; }
            set { _amplitude = value; }
        }

        protected double _initialPhase;
        /// <summary>
        /// 初始相位，in Degree。
        /// 设置此属性值必然会触发Reset()，即重置历史数据，下一次调用Generate(...)方法时，必然从该初始相位开始生成波形。
        /// </summary>
        public double InitialPhase
        {
            get { return _initialPhase; }
            set
            {
                _initialPhase = value;
                Reset();
            }
        }

        protected NoiseType _noiseType;
        /// <summary>
        /// 噪声类型。
        /// </summary>
        public NoiseType NoiseType
        {
            get { return _noiseType; }
            set { _noiseType = value; }
        }

        protected double _snr;
        /// <summary>
        /// 信噪比，dB。
        /// </summary>
        public double SNR
        {
            get { return _snr; }
            set { _snr = value; }
        }

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public FunctionGenerator() : this(1000, 10, 1, 0, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase"></param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public FunctionGenerator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
        {
            // 设置公共属性的默认值。
            _sampleRate = sampleRate;
            _frequency = frequency;
            _amplitude = amplitude;
            _initialPhase = initialPhase;
            _noiseType = noiseType;
            _snr = snr;

            // 实例化私有成员
            _randomGen = new Random(NoiseGenerator.GetRandomSeed());
        }

        #endregion

        #region------------------------- 公共方法（抽象方法） -------------------------

        /// <summary>
        /// 重置，清空历史数据。
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// 生成信号，double格式。
        /// </summary>
        /// <param name="data"></param>
        public abstract void Generate(double[] data);

        /// <summary>
        /// 生成信号，short格式。
        /// </summary>
        /// <param name="data"></param>
        public virtual void Generate(short[] data)
        {
            // 先生成double[]数据。
            if (_rawDoubleData == null || _rawDoubleData.Length != data.Length) { _rawDoubleData = new double[data.Length]; }
            Generate(_rawDoubleData);

            // 转换为short[]数据。
            for (int i = 0; i < data.Length; i++) { data[i] = (short)_rawDoubleData[i]; }
        }
        #endregion

        #region------------------------- 私有方法 -------------------------

        /// <summary>
        /// 添加噪声
        /// </summary>
        /// <param name="data"></param>
        protected void AddNoise(double[] data)
        {
            if (_noiseType != NoiseType.None)
            {
                double noiseAmplitude = _amplitude * Math.Pow(10, _snr / -20) * 2; // *2 表示将 random 幅度范围在(-0.5 -  +0.5)之间 归一化为 (-1 - +1) 之间
                for (int i = 0; i < data.Length; i++)
                    data[i] += noiseAmplitude * (_randomGen.NextDouble() - 0.5);
            }
        }

        /// <summary>
        /// 获取归一化的相位，当相位不在[0，1)区间时，回归到该区间。
        /// </summary>
        /// <param name="time">待求相位的时刻</param>
        /// <param name="scale">相位归一化因子</param>
        /// <param name="offset">初始相位</param>
        /// <returns></returns>
        public static double GenerateScaledPhase(double time, double scale, double offset = 0)
        {
            double phase = time * scale + offset;    // 获取当前相位。 TODO： 当 i 比较大的时候，会影响精度。
            return (phase - Math.Floor(phase));     // 当前相位回归[0,1)区间，即 mod 1。
        }

        /// <summary>
        /// 对于给定的信号频率和采样率，计算整周期波形的点数。
        /// </summary>
        /// <param name="signalFrequency">期望信号频率</param>
        /// <param name="sampleRate">采样率</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="actualFrequency">实际频率</param>
        /// <param name="tolerance">容差</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="quantumSize">量化步进</param>
        /// <returns>最少周期点数</returns>
        internal static int CalcCycleQuantum(double[] signalFrequency, double sampleRate, int maxLength, out double[] actualFrequency, double tolerance = 1, int minLength = 0, int quantumSize = 8)
        {
            actualFrequency = new double[signalFrequency.Length];

            if (signalFrequency.Length == 0)
                return 0;

            double[] scale = new double[signalFrequency.Length];
            for (int i = 0; i < signalFrequency.Length; i++)
                scale[i] = signalFrequency[i] / sampleRate;

            int samples = Math.Max(minLength, quantumSize);
            double freqError;
            double minFreqError = sampleRate;
            int minErrorSamples = 0;            
            double[] tempFrequency = new double[signalFrequency.Length];            

            do
            {                
                freqError = 0;  // 重置频率误差

                // 计算所有频率的频率误差，求最大值
                for (int i = 0; i < signalFrequency.Length; i++)
                {
                    tempFrequency[i] = sampleRate * Math.Round(scale[i] * samples) / samples;
                    freqError = Math.Max(freqError, Math.Abs(tempFrequency[i] - signalFrequency[i]));
                }

                // 更新最小频率误差对应的点数
                if (minFreqError > freqError)
                {
                    minFreqError = freqError;
                    minErrorSamples = samples;
                    for (int i = 0; i < tempFrequency.Length; i++)
                        actualFrequency[i] = tempFrequency[i];
                }

                // 满足要求，退出循环
                if (freqError <= tolerance)
                    break;

                samples += quantumSize; // 下一帧点数
            }
            while (samples < maxLength);

            // 返回最小的点数
            return minErrorSamples;
        }

        #endregion

    }

    /// <summary>
    /// 单正弦波生成。
    /// </summary>
    public class SineGenerator : FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SineGenerator() : this(10000, 1000, 1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        public SineGenerator(double sampleRate, double frequency, double amplitude, double initialPhase = 0)
            : this(sampleRate, frequency, amplitude, initialPhase, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public SineGenerator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
            : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr)
        {
            // 保存参数至私有成员，本对象没有新增属性，所有参数已在基类的构造函数中存储至私有成员。

            // 重置参数，需要先赋值再调用Reset
            Reset();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 重置下一次生成数据的初始相位为公共属性InitialPhase的值。
        /// </summary>
        public override void Reset()
        {
            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0。
            _phaseOfNextBlock = GenerateScaledPhase(_initialPhase, 1.0 / 360); 
        }

        /// <summary>
        /// 生成数据。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(double[] data)
        {
            double phaseIncrement = _frequency / _sampleRate;               // 相位增量，0-0.5之间，超过0.5则不满足Nyquist定理。
            double scale = Math.PI * 2;                                     // scale,  是将相位从[0,1)区间，转换为 [0,2pi)区间。
            double currentPhase;
            double actualPhase;

            // 生成正弦数据并添加噪声（若开启）。
            for (int i = 0; i < data.Length; i++)
            {
                currentPhase = GenerateScaledPhase(i, phaseIncrement, _phaseOfNextBlock);// 获取当前相位。
                actualPhase = currentPhase * scale;                         // 将相位从[0,1)区间，转换为 [0,2pi)区间，即正弦实际相位。
                data[i] = _amplitude * Math.Sin(actualPhase);               // 幅度因子
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = GenerateScaledPhase(data.Length, phaseIncrement, _phaseOfNextBlock); 

            // 添加噪声
            AddNoise(data);
        }

        #endregion
    }
    /// <summary>
    /// 三角波生成
    /// </summary>
    public class TriangleGenrator : FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public TriangleGenrator() : this(10000, 1000, 1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        public TriangleGenrator(double sampleRate, double frequency, double amplitude, double initialPhase = 0)
            : this(sampleRate, frequency, amplitude, initialPhase, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public TriangleGenrator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
            : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr)
        {
            // 保存参数至私有成员，本对象没有新增属性，所有参数已在基类的构造函数中存储至私有成员。

            // 重置参数，需要先赋值再调用Reset
            Reset();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 重置下一次生成数据的初始相位为公共属性InitialPhase的值。
        /// </summary>
        public override void Reset()
        {
            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0.75，即偏移3pi/2。
            _phaseOfNextBlock = GenerateScaledPhase(_initialPhase, 1.0 / 360, 0.75);
        }
        /// <summary>
        /// 生成数据。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(double[] data)
        {
            double phaseIncrement = _frequency / _sampleRate;  // 相位增量，0-0.5之间，超过0.5则不满足Nyquist定理。
            double scale = 4;                                   // scale, offset1,offset2 是将相位从[0,1)区间，转换为 [-1,1)区间。
            double offset1 = -0.5;                               // 方法是先平移 offset1 得到 [-0.5,0.5) 区间，取Abs 得到 [0,0.5]区间，
            double offset2 = -1;                                 // 乘以scale得到[0,2]区间，再平移 offset2 得到 [-1,1] 区间。
            double currentPhase;
            double normalizedAmplitude;

            // 生成三角波数据。
            for (int i = 0; i < data.Length; i++)
            {
                currentPhase = GenerateScaledPhase(i, phaseIncrement, _phaseOfNextBlock);   // 获取当前相位。
                normalizedAmplitude = (Math.Abs(currentPhase + offset1) * scale + offset2); // 将相位从[0,1)区间，转换为 [-1,1)区间，即三角波。
                data[i] = _amplitude * normalizedAmplitude;                 // 幅度因子
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = GenerateScaledPhase(data.Length, phaseIncrement, _phaseOfNextBlock);

            // 添加噪声
            AddNoise(data);
        }

        #endregion
    }

    /// <summary>
    /// 锯齿波生成
    /// </summary>
    public class SawtoothGenrator : FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SawtoothGenrator() : this(10000, 1000, 1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        public SawtoothGenrator(double sampleRate, double frequency, double amplitude, double initialPhase = 0)
            : this(sampleRate, frequency, amplitude, initialPhase, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public SawtoothGenrator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
            : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr)
        {
            // 保存参数至私有成员，本对象没有新增属性，所有参数已在基类的构造函数中存储至私有成员。

            // 重置参数，需要先赋值再调用Reset
            Reset();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 重置下一次生成数据的初始相位为公共属性InitialPhase的值。
        /// </summary>
        public override void Reset()
        {
            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0。
            _phaseOfNextBlock = GenerateScaledPhase(_initialPhase, 1.0 / 360);
        }
        /// <summary>
        /// 生成数据。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(double[] data)
        {
            double phaseIncrement = _frequency / _sampleRate;   // 相位增量，0-0.5之间，超过0.5则不满足Nyquist定理。
            double scale = 2;                                   // scale, offset 是将相位从[0,1)区间，转换为 [-1,1)区间。
            double offset = -1;                                 // 方法是先乘以scale得到[0,2)区间，再平移 offset 得到 [-1,1) 区间。
            double currentPhase;
            double normalizedAmplitude;

            // 生成锯齿波数据。
            for (int i = 0; i < data.Length; i++)
            {
                currentPhase = GenerateScaledPhase(i, phaseIncrement, _phaseOfNextBlock);      // 获取当前相位。
                normalizedAmplitude = currentPhase * scale + offset;        // 将相位从[0,1)区间，转换为 [-1,1)区间，即锯齿波。
                data[i] = _amplitude * normalizedAmplitude;                 // 幅度因子
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = GenerateScaledPhase(data.Length, phaseIncrement, _phaseOfNextBlock);

            // 添加噪声
            AddNoise(data);
        }

        #endregion
    }

    /// <summary>
    /// 方波生成
    /// </summary>
    public class SquareGenrator : FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        #endregion

        #region------------------------- 公共属性 -------------------------

        private double _dutyCycle;
        /// <summary>
        /// 占空比，%。
        /// </summary>
        public double DutyCycle
        {
            set { _dutyCycle = value; }
            get { return _dutyCycle; }
        }
        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public SquareGenrator() : this(10000, 1000, 1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="dutyCycle"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        public SquareGenrator(double sampleRate, double frequency, double amplitude, double dutyCycle = 50, double initialPhase = 0)
            : this(sampleRate, frequency, amplitude, dutyCycle, initialPhase, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="dutyCycle"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public SquareGenrator(double sampleRate, double frequency, double amplitude, double dutyCycle, double initialPhase, NoiseType noiseType, double snr)
            : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr)
        {
            // 保存参数至私有成员。           
            _dutyCycle = dutyCycle;
            // 重置参数， 需要先赋值再调用Reset
            Reset();
        }

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 重置下一次生成数据的初始相位为公共属性InitialPhase的值。
        /// </summary>
        public override void Reset()
        {
            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0。
            _phaseOfNextBlock = GenerateScaledPhase(_initialPhase, 1.0 / 360);
        }
        /// <summary>
        /// 生成数据。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(double[] data)
        {
            double phaseIncrement = _frequency / _sampleRate;   // 相位增量，0-0.5之间，超过0.5则不满足Nyquist定理。
            double dutyCycle = _dutyCycle / 100;                // % 转换为0 - 1之间。
            double currentPhase;
            double normalizedAmplitude;

            // 生成方波数据。
            for (int i = 0; i < data.Length; i++)
            {
                currentPhase = GenerateScaledPhase(i, phaseIncrement, _phaseOfNextBlock);       // 获取当前相位。
                normalizedAmplitude = currentPhase < dutyCycle ? 1 : -1;   // 相位小于duty cycle 为1,即方波。
                data[i] = _amplitude * normalizedAmplitude;                 // 幅度因子
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = GenerateScaledPhase(data.Length, phaseIncrement, _phaseOfNextBlock);

            // 添加噪声
            AddNoise(data);
        }

        #endregion
    }

    #endregion

    #region------------------------- 可实例化的信号生成类（复数信号生成） -------------------------

    /// <summary>
    /// 基础复数波形生成基类。
    /// </summary>
    public abstract class ComplexFunctionGenerator : FunctionGenerator
    {
        #region------------------------- 私有成员 -------------------------

        /// <summary>
        /// 临时数组，当获取的数据类型不为Complex[]时，先按照Complex[]生成并保存在临时此临时数组，再统一做数据类型转换。
        /// </summary>
        private Complex[] _rawComplexData;

        #endregion

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ComplexFunctionGenerator() : this(10000, 1000, 1, 0, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ComplexFunctionGenerator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
        : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr) {; }

        #endregion

        #region------------------------- 公共方法（抽象方法） -------------------------

        /// <summary>
        /// 生成信号，Complex格式。
        /// </summary>
        /// <param name="data"></param>
        public abstract void Generate(Complex[] data);

        #endregion

        #region------------------------- 公共方法 -------------------------

        /// <summary>
        /// 生成信号，I16 Interleaved IQ格式。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(short[] data)
        {
            // 检查输入参数有效。
            if (data.Length % 2 != 0) { throw new ArgumentException("Array length must be even."); }

            // 先生成Complex[]数据。
            int numberOfSamples = data.Length / 2;
            if (_rawComplexData == null || _rawComplexData.Length != numberOfSamples) { _rawComplexData = new Complex[numberOfSamples]; }
            Generate(_rawComplexData);

            // 转换为short[]数据。
            for (int i = 0; i < numberOfSamples; i++)
            {
                data[i * 2] = (short)_rawComplexData[i].Real;
                data[i * 2 + 1] = (short)_rawComplexData[i].Imaginary;
            }
        }

        /// <summary>
        /// 生成信号，double Interleaved IQ格式。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(double[] data)
        {
            // 检查输入参数有效。
            if (data.Length % 2 != 0) { throw new ArgumentException("Array length must be even."); }

            // 先生成Complex[]数据。
            int numberOfSamples = data.Length / 2;
            if (_rawComplexData == null || _rawComplexData.Length != numberOfSamples) { _rawComplexData = new Complex[numberOfSamples]; }
            Generate(_rawComplexData);

            // 转换为double[]数据。
            for (int i = 0; i < numberOfSamples; i++)
            {
                data[i * 2] = _rawComplexData[i].Real;
                data[i * 2 + 1] = _rawComplexData[i].Imaginary;
            }
        }

        #endregion

        #region------------------------- 私有方法 -------------------------
        
        /// <summary>
        /// 添加噪声
        /// </summary>
        /// <param name="data"></param>
        protected void AddNoise(Complex[] data)
        {
            if (_noiseType != NoiseType.None)
            {
                double noiseAmplitude = _amplitude * Math.Pow(10, _snr / -20) * 2; // *2 表示将 random 幅度范围在(-0.5 -  +0.5)之间 归一化为 (-1 - +1) 之间
                for (int i = 0; i < data.Length; i++)
                    data[i] += new Complex(noiseAmplitude * (_randomGen.NextDouble() - 0.5), noiseAmplitude * (_randomGen.NextDouble() - 0.5));
            }
        }

        #endregion        

    }
    #endregion

    /// <summary>
    /// 复数单正弦波生成。
    /// </summary>
    public class ComplexSineGenerator : ComplexFunctionGenerator
    {

        #region------------------------- 构造函数 -------------------------

        /// <summary>
        /// 构造函数。
        /// </summary>
        public ComplexSineGenerator() : this(10000, 1000, 1) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        public ComplexSineGenerator(double sampleRate, double frequency, double amplitude, double initialPhase = 0)
            : this(sampleRate, frequency, amplitude, initialPhase, NoiseType.None, 200) {; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <param name="frequency"></param>
        /// <param name="amplitude"></param>
        /// <param name="initialPhase">初始相位，in Degree。</param>
        /// <param name="noiseType"></param>
        /// <param name="snr"></param>
        public ComplexSineGenerator(double sampleRate, double frequency, double amplitude, double initialPhase, NoiseType noiseType, double snr)
            : base(sampleRate, frequency, amplitude, initialPhase, noiseType, snr)
        {
            // 保存参数至私有成员，本对象没有新增属性，所有参数已在基类的构造函数中存储至私有成员。

            // 重置参数，需要先赋值再调用Reset
            Reset();
        }

        #endregion

        /// <summary>
        /// 重置下一次生成数据的初始相位为公共属性InitialPhase的值。
        /// </summary>
        public override void Reset()
        {
            // 设置起始相位，Degree -> 角度梯度 ，即[0,1)区间。 其中，角度 0度 对应 角度梯度值为 0。
            _phaseOfNextBlock = GenerateScaledPhase(_initialPhase, 1.0 / 360);
        }

        /// <summary>
        /// 生成信号，Complex格式。
        /// </summary>
        /// <param name="data"></param>
        public override void Generate(Complex[] data)
        {
            double phaseIncrement = _frequency / _sampleRate;               // 相位增量，0-1之间，超过1则不满足Nyquist定理。
            double scale = Math.PI * 2;                                     // scale,  是将相位从[0,1)区间，转换为 [0,2pi)区间。
            double currentPhase;
            double actualPhase;

            // 生成正弦数据并添加噪声（若开启）。
            for (int i = 0; i < data.Length; i++)
            {
                currentPhase = GenerateScaledPhase(i, phaseIncrement, _phaseOfNextBlock);       // 获取当前相位。
                actualPhase = currentPhase * scale;                         // 将相位从[0,1)区间，转换为 [0,2pi)区间，即正弦实际相位。
                data[i] = new Complex(_amplitude * Math.Cos(actualPhase), _amplitude * Math.Sin(actualPhase));               // 幅度因子
            }

            // 计算下一次的起始相位。
            _phaseOfNextBlock = GenerateScaledPhase(data.Length, phaseIncrement, _phaseOfNextBlock);

            // 添加噪声
            AddNoise(data);
        }
    }

    #region------------------------- 公共数据类型 -------------------------

    /// <summary>
    /// 噪声类型。
    /// </summary>
    public enum NoiseType { None, UniformWhiteNoise, }

    /// <summary>
    /// 基础波形信号类型。
    /// </summary>
    public enum FunctionGenSignalType
    {
        /// <summary>
        /// 正弦波。
        /// </summary>
        Sine,

        /// <summary>
        /// 三角波。
        /// </summary>
        Triangle,

        /// <summary>
        /// 锯齿波。
        /// </summary>
        Sawtooth,

        /// <summary>
        /// 方波。
        /// </summary>
        Square
    }

    #endregion

}
