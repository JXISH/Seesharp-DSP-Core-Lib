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
using System.Numerics;

namespace BasicFunctionGeneratorExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            FunctionSelect.SelectedIndex = 0;
            NoiseSelect.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void Start_Click(object sender, EventArgs e)
        {
            double _SamplingRate = (double)SamplingRate.Value;            // 采样率
            double _Frequency = (double)Frequency.Value;                  // 频率
            double _Amplitude = (double)Amplitude.Value;                  // 幅度
            double _InitialPhase = (double)Initialphase.Value;            // 初相位
            double _SNR = (double)SNR.Value;                              // 信噪比
            double[] data = new double[2000];                             // 波形数据
            Complex[] complexdata = new Complex[2000];                    // 复数波形数据
            double[] datareal = new double[2000];                         // 复数波形实部数据
            double[] dataimg = new double[2000];                          // 复数波形虚部数据
            double[] time = new double[2000];                             // 时间序列


            // 采样点间隔为 1 / _SamplingRate
            for (int i = 0; i < time.Length; i++)
                time[i] = i / _SamplingRate;

            switch(FunctionSelect.SelectedIndex)
            {
                case (int)FunctionType.SingleComplexSineWave:             // 生成单一复数正弦波
                    {
                        Complex[] x_C = new Complex[2000];
                        EasyGeneration.ComplexSine(x_C, _Frequency, _SamplingRate, _Amplitude);

                        //分别绘制实部虚部函数图像
                        for (int i = 0; i < x_C.Length; i++)
                        {
                            datareal[i] = x_C[i].Real;
                            dataimg[i] = x_C[i].Imaginary;
                        }

                        double[][] time2 = new double[2][] { time, time };
                        double[][] data2 = new double[2][] { datareal, dataimg };

                        fig1.Plot(time2, data2);
                        break;
                    }
                case (int)FunctionType.InterleaveSingleComplexSineWave:   // 生成I/Q交错格式的单一复数正弦波
                    {
                        short[] x_s = new short[4000];
                        EasyGeneration.ComplexSine(x_s, _Frequency, _SamplingRate, _Amplitude);

                        //分别绘制实部虚部函数图像
                        for (int i = 0; i < x_s.Length; i++)
                        {
                            if (i%2 == 0)
                            {
                                datareal[i/2] = x_s[i];
                            }
                            else
                            {
                                dataimg[i/2] = x_s[i];
                            }
                        }

                        double[][] time2 = new double[2][] { time, time };
                        double[][] data2 = new double[2][] { datareal, dataimg };

                        fig1.Plot(time2, data2);

                        break;
                    }
                case (int)FunctionType.SingleSineWave:                    // 生成单一正弦波
                    {
                        short[] x_sh = new short[2000];
                        EasyGeneration.Sine(x_sh, _Frequency, _SamplingRate, _Amplitude, _SNR);

                        for (int i = 0; i < x_sh.Length; i++)
                        {
                            data[i] = x_sh[i];
                        }

                        fig1.Plot(data);

                        break; 
                    }
                case (int)FunctionType.SineGenerator:                     // 正弦波发生器
                    {
                        // 若选择的噪声类型为无噪声
                        if (NoiseSelect.SelectedIndex == (int)NoiseType.None)
                        {
                            SineGenerator s = new SineGenerator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase,NoiseType.None, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        // 若选择的噪声类型为均匀白噪声
                        else if (NoiseSelect.SelectedIndex == (int)NoiseType.UniformWhiteNoise)
                        {
                            SineGenerator s = new SineGenerator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.UniformWhiteNoise, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        break; 
                    }
                case (int)FunctionType.TriangleGenerator:                  // 三角波发生器
                    {
                        // 若选择的噪声类型为无噪声
                        if (NoiseSelect.SelectedIndex == (int)NoiseType.None)
                        {
                            TriangleGenrator s = new TriangleGenrator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.None, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        // 若选择的噪声类型为均匀白噪声
                        else if (NoiseSelect.SelectedIndex == (int)NoiseType.UniformWhiteNoise)
                        {
                            TriangleGenrator s = new TriangleGenrator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.UniformWhiteNoise, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        break; 
                    }
                case (int)FunctionType.SawtoothGenerator:                  // 锯齿波发生器
                    {
                        // 若选择的噪声类型为无噪声
                        if (NoiseSelect.SelectedIndex == (int)NoiseType.None)
                        {
                            SawtoothGenrator s = new SawtoothGenrator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.None, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        // 若选择的噪声类型为均匀白噪声
                        else if (NoiseSelect.SelectedIndex == (int)NoiseType.UniformWhiteNoise)
                        {
                            SawtoothGenrator s = new SawtoothGenrator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.UniformWhiteNoise, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        break; 
                    }
                case (int)FunctionType.SquareGenerator:                   // 方波发生器
                    {
                        // 若选择的噪声类型为无噪声
                        if (NoiseSelect.SelectedIndex == (int)NoiseType.None)
                        {
                            SquareGenrator s = new SquareGenrator(_SamplingRate, _Frequency, _Amplitude, 50, _InitialPhase, NoiseType.None, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        // 若选择的噪声类型为均匀白噪声
                        else if (NoiseSelect.SelectedIndex == (int)NoiseType.UniformWhiteNoise)
                        {
                            SquareGenrator s = new SquareGenrator(_SamplingRate, _Frequency, _Amplitude, 50, _InitialPhase, NoiseType.UniformWhiteNoise, _SNR);
                            s.Generate(data);
                            fig1.Plot(time, data);
                        }
                        break;
                    }
                case (int)FunctionType.ComplexSineGenerator:             // 复数正弦波发生器
                    {
                        // 若选择的噪声类型为无噪声
                        if (NoiseSelect.SelectedIndex == (int)NoiseType.None)
                        {
                            ComplexSineGenerator s = new ComplexSineGenerator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.None, _SNR);
                            s.Generate(complexdata);

                            for (int i = 0; i < complexdata.Length; i++)
                            {
                                datareal[i] = complexdata[i].Real;
                                dataimg[i] = complexdata[i].Imaginary;
                            }

                            double[][] time2 = new double[2][] { time, time };
                            double[][] data2 = new double[2][] { datareal, dataimg };

                            fig1.Plot(time2, data2);
                        }
                        // 若选择的噪声类型为均匀白噪声
                        else if (NoiseSelect.SelectedIndex == (int)NoiseType.UniformWhiteNoise)
                        {
                            ComplexSineGenerator s = new ComplexSineGenerator(_SamplingRate, _Frequency, _Amplitude, _InitialPhase, NoiseType.UniformWhiteNoise, _SNR);
                            s.Generate(complexdata);

                            for (int i = 0; i < complexdata.Length; i++)
                            {
                                datareal[i] = complexdata[i].Real;
                                dataimg[i] = complexdata[i].Imaginary;
                            }

                            double[][] time2 = new double[2][] { time, time };
                            double[][] data2 = new double[2][] { datareal, dataimg };

                            fig1.Plot(time2, data2);

                        }
                        break;
                    }
            }       
        }
        private enum FunctionType
        {
            SingleComplexSineWave,              // 生成单一正弦波
            InterleaveSingleComplexSineWave,    // 生成单一复数正弦波
            SingleSineWave,                     // 生成I/Q交错格式的单一复数正弦波
            SineGenerator,                      // 正弦波发生器
            TriangleGenerator,                  // 三角波发生器
            SawtoothGenerator,                  // 锯齿波发生器
            SquareGenerator,                    // 方波发生器
            ComplexSineGenerator                // 复数正弦波发生器
        }
        
        
    }
}
