using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.GeneralSpectrum;
using System.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters;
using SeeSharpTools.JXI.SignalProcessing.Window;

namespace FrequencyShiftExample
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            GeneralSpectrumTask _task = new GeneralSpectrumTask();
            double _amplitude = (double)(Amplitude.Value);               // 振幅
            double _dOffset = (double)(DCoffset.Value);                  // 直流偏置
            double _fOffsetReal = (double)(FreqOffset.Value);            // 实信号频率偏移量
            double _fOffsetComplex;                                      // 实信号对应的复信号频率偏移量 
            int _samplingRate = (int)(SamplingRate.Value);               // 采样率
            double _freq = (double)(Frequency.Value);                    // 频率
            double _freq2 = (double)(Frequency2.Value);                  // 频率2
            double _freq3 = (double)(Frequency3.Value);                  // 频率3
            Complex[] Wave = new Complex[2000];                          // 波形
            var spec = new double[(int)_samplingRate / 2];               // 频谱（变换前）
            var spec2 = new double[(int)_samplingRate / 2];              // 频谱（变换后）
            double[] temp1 = new double[2000];
            double[] temp2 = new double[2000];
            double[] WaveReal = new double[2000];                        // 波形实部（变换前）
            double[] WaveReal2 = new double[2000];                       // 波形实部（变换后）
            double[] time = new double[2000];                            // 时间序列


            // 生成时间序列，离散的时间间隔dt = 1 / _samplingRate
            for (int i = 0; i < Wave.Length; i++)
                time[i] = (double)i / (double)_samplingRate;

            // 计算复信号频率搬移量
            // 一个周期内采样1 / _fOffsetComplex个点，1s采_samplingRate个点，_fOffsetReal个周期，
            // 所以 _fOffsetReal / _fOffsetComplex = _samplingRate
            _fOffsetComplex = _fOffsetReal / _samplingRate;


            // 生成对应的复数信号
            Generation.SineWave(ref temp1, _amplitude, 0, _freq, _samplingRate);     // temp1 是信号的实部
            Generation.SineWave(ref temp2, _amplitude, 90, _freq, _samplingRate);    // temp2 是信号的虚部
            for (int i = 0; i < Wave.Length; i++)
            {
                Wave[i] = new Complex(temp1[i], temp2[i]);
            }
            Generation.SineWave(ref temp1, _amplitude, 0, _freq2, _samplingRate);     // temp1 是信号的实部
            Generation.SineWave(ref temp2, _amplitude, 90, _freq2, _samplingRate);    // temp2 是信号的虚部
            for (int i = 0; i < Wave.Length; i++)
            {
                Wave[i] += new Complex(temp1[i], temp2[i]);
            }
            Generation.SineWave(ref temp1, _amplitude, 0, _freq3, _samplingRate);     // temp1 是信号的实部
            Generation.SineWave(ref temp2, _amplitude, 90, _freq3, _samplingRate);    // temp2 是信号的虚部
            for (int i = 0; i < Wave.Length; i++)
            {
                Wave[i] += new Complex(temp1[i], temp2[i]);
            }

            // 加上直流偏置,并取出实部到temp_real当中用来绘图
            for (int i = 0; i < Wave.Length; i++)
            {
                Wave[i] += new Complex(_dOffset, 0);
                WaveReal[i] = Wave[i].Real;
            }


            // 绘制原始时域实信号
            RawWaveForm.Plot(time, WaveReal);



            // 计算频谱图,设置参数
            _task.InputDataType = InputDataType.Real;
            _task.SampleRate = _samplingRate;
            _task.WindowType = WindowType.None;
            _task.Average.Mode = SpectrumAverageMode.NoAveraging;
            _task.Average.WeightingType = SpectrumWeightingType.LinearMoving;
            _task.Average.Size = 10;
            _task.Output.NumberOfLines = (int)_task.SampleRate / 2;
            _task.Unit.Type = SpectrumOutputUnit.V;
            _task.Unit.Impedance = 50;
            _task.Unit.IsPSD = false;

            // 绘制对应的频谱图
            _task.GetSpectrum(WaveReal, ref spec);
            RawSpectrum.Plot(spec, _task.SpectralInfomation.FreqStart, _task.SpectralInfomation.FreqDelta);


            // 频率偏移器构造
            FrequencyShift<Complex> shifter = new FrequencyShift<Complex>(_fOffsetComplex, 0);
            shifter.Process(Wave);
            // 取出Wave的实部
            for (int i = 0; i < Wave.Length; i++)
                WaveReal2[i] = Wave[i].Real;

            // 绘制频率搬移之后的时域图像
            TransformedWaveForm.Plot(time, WaveReal2);

            // 绘制对应的频谱图
            _task.GetSpectrum(WaveReal2, ref spec2);
            TransformedSpectrum.Plot(spec2, _task.SpectralInfomation.FreqStart, _task.SpectralInfomation.FreqDelta);


        }
    }
}
