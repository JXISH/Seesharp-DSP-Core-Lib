using MathNet.Filtering.FIR;
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SeeSharpTools.JXI.RFCommunications.Modem
{
    /// <summary>
    /// 调制器，对输入变频到新的载波
    /// 接收实数和复数，复数包括Complex和Complex32 (单精度复数)
    /// 可以block by block相位连续
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Modulator <T>
    {
        /// <summary>
        /// 变频器内部相位
        /// </summary>
        private double _phaseOfNextBlock;

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

        #endregion


        /// <summary>
        /// 初始化变频器
        /// </summary>
        public Modulator(double phase = 0)
        {
            _initialPhase = phase;
            Reset();
        }

        /// <summary>
        /// 重置变频器
        /// </summary>
        public void Reset()
        {
            // 设置起始相位，Degree -> 弧度。
            _phaseOfNextBlock = _initialPhase * Math.PI / 180;
        }

        public int Process(T[] input, T[] output)
        {
            double freq = _frequency / _sampleRate;

            if (input is Complex32[])
            {
                // 输入数据类型转换
                Complex32[] source_fc32 = input as Complex32[];
                Complex32[] desination = output as Complex32[];
                // 执行对应数据类型的变频器，Complex32
                desination = Vector.ToneInit(source_fc32.Length, (float)freq, (float)_phaseOfNextBlock);
                Vector.ArrayMulti(desination, source_fc32);
            }
            else if (input is Complex[])
            {
                // 输入数据类型转换
                Complex[] source_fc64 = input as Complex[];
                Complex[] desination = output as Complex[];
                // 执行对应数据类型的变频器，Complex
                desination = Vector.ToneInit(source_fc64.Length, freq, _phaseOfNextBlock);
                Vector.ArrayMulti(desination, source_fc64);
            }

            _phaseOfNextBlock += input.Length * freq;
            _phaseOfNextBlock -= Math.Floor(_phaseOfNextBlock / 2 / Math.PI) * 2 * Math.PI;
            return 0;
        }
    }

    /// <summary>
    /// 数字变频器，包括升降采样 (DDC & DUC)
    /// </summary>
    public class DigitalConverter
    {
        MultiRateFIR<Complex> multirateConverter;
        FIR<Complex> converter;
        bool multirate = false;
        bool prepared = false;
        int _upSampleRate = 1;
        int _downSampleRate = 1;
        int _outputLength = 0;
        double _centerFreq = 0;
        /// <summary>
        /// 输出波形有效长度
        /// </summary>
        public int OutputLength
        {
            get
            {
                return _outputLength;
            }
        }
        /// <summary>
        /// 初始化重采样滤波器,自动计算滤波长度
        /// </summary>
        /// <param name="fc">归一化信号中心频率</param>
        /// <param name="bandwidth">归一化信号带宽</param>
        /// <param name="upsampleRate">升采样倍数</param>
        /// <param name="downsampleRate">降采样倍数</param>
        public void Prepare(double fc, double bandwidth, int upsampleRate, int downsampleRate)
        {
            int halforder = 20 * upsampleRate * downsampleRate;
            Prepare(fc, bandwidth, upsampleRate, downsampleRate, halforder);
        }
        /// <summary>
        /// 初始化重采样滤波器
        /// </summary>
        /// <param name="fc">归一化信号中心频率</param>
        /// <param name="bandwidth">归一化信号带宽</param>
        /// <param name="upsampleRate">升采样倍数</param>
        /// <param name="downsampleRate">降采样倍数</param>
        /// <param name="inputFilterHalfOrder">滤波器半长度</param>
        public void Prepare(double fc, double bandwidth, int upsampleRate, int downsampleRate, int inputFilterHalfOrder)
        {
            double inputSampleHighestRate = upsampleRate;
            double[] LPFCoef = FirCoefficients.LowPass(inputSampleHighestRate, 0.5 * bandwidth, inputFilterHalfOrder); //FIR

            multirate = (downsampleRate != 1) || (upsampleRate != 1);
            if (multirate)
            {
                multirateConverter = new MultiRateFIR<Complex>(LPFCoef, upsampleRate, downsampleRate);
                multirateConverter.Reset();
            }
            else
            {
                converter = new FIR<Complex>(LPFCoef);
                converter.Reset();
            }
            _upSampleRate = upsampleRate;
            _downSampleRate = downsampleRate;
            _centerFreq = fc;
            prepared = true;
        }
        /// <summary>
        /// 下变频，可原位计算
        /// </summary>
        /// <param name="inputIQ">输入IQ</param>
        /// <param name="outputIQ">下变频输出基带</param>
        public void FrequencyConvert(Complex[] inputIQ, ref Complex[] outputIQ)
        {
            FrequencyConvert(inputIQ, -_centerFreq, ref outputIQ);
        }
        /// <summary>
        /// 下变频，可原位计算
        /// </summary>
        /// <param name="inputIQ">输入IQ</param>
        /// <param name="outputIQ">下变频输出基带</param>
        /// <param name="shiftFrequency">移动频率+上移-下移</param>
        public void FrequencyConvert(Complex[] inputIQ, double shiftFrequency, ref Complex[] outputIQ)
        {
            //下变频
            Complex c0 = 1;
            Complex cStep = Complex.FromPolarCoordinates(1, Math.PI * 2 * shiftFrequency);
            for (int i = 0; i < inputIQ.Length; i++)
            {
                outputIQ[i] = inputIQ[i] * c0;
                c0 *= cStep;
            }
        }
        /// <summary>
        /// 执行数字变频
        /// </summary>
        /// <param name="inputIQ">输入基带波形</param>
        /// <param name="outputIQ">输出基带波形</param>
        public void DownSample(Complex[] inputIQ, ref Complex[] outputIQ)
        {

            //低通滤波+降采样
            int _demodBlockLength = (int)Math.Floor((double)inputIQ.Length * _upSampleRate / _downSampleRate);
            outputIQ = new Complex[_demodBlockLength];
            if (multirate)
            {
                multirateConverter.Process(inputIQ, outputIQ, out _outputLength);
            }
            else
            {
                converter.Process(inputIQ, outputIQ);
            }
        }
        /// <summary>
        /// 重置降采样滤波器
        /// </summary>
        public void Reset()
        {
            if (multirate)
            {
                multirateConverter.Reset();
            }
            else
            {
                converter.Reset();
            }
        }
    }
}
