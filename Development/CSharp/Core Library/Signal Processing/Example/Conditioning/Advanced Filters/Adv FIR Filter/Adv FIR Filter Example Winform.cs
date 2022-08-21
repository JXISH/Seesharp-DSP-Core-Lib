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
using SeeSharpTools.JXI.SignalProcessing.Conditioning.AdvancedFilters;


namespace Adv_FIR_Filter_Example
{
    public partial class Form1 : Form
    {
        double Frequency;                                       //正弦波的频率
        double Amplitude;                                       //正弦波幅度
        double Phase = 0;                                       //正弦波的初相位
        double[] sinwave = new double[1000];                    //生成的正弦波
        double[] Gaissian_Nosie = new double[1000];             //生成的高斯噪声
        double[] coe = new double[10];                          //滤波系数
        double DC;                                              //直流分量的值
        int i = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void SinwaveGeneration_Click(object sender, EventArgs e)
        {
            //为参数赋值
            Frequency = (double)ValueofFrequency.Value;
            Amplitude = (double)ValueofAmplitude.Value;
            Phase = (double)ValueofPhase.Value;
            DC = (double)ValueofDC.Value;

            //生成正弦波
            Generation.SineWave(ref sinwave, Amplitude, Phase, Frequency, 1000);

            //加上直流分量
            for (i = 0; i < 1000; i++)
            {
                sinwave[i] += DC;
            }

            //绘制函数图像
            fig1.Plot(sinwave);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FIR_Click(object sender, EventArgs e)
        {
            //均值滤波器
            for (i = 0; i < 10; i++)
            {
                coe[i] = 1.0 / 10;
            }
            double[] output1 = new double[1000];
            //滤波操作
            EasyFIR<double>.FIR(sinwave, coe, out output1);
            fig2.Plot(output1);
        }


        private void Add_Noise_Click(object sender, EventArgs e)
        {
            //生成高斯噪声
            Generation.GaussianWhiteNoise(ref Gaissian_Nosie, 0.1);

            //添加高斯噪声
            for (i = 0; i < 1000; i++)
            { sinwave[i] += Gaissian_Nosie[i]; }
            fig1.Plot(sinwave);
        }

        private void UpSampleFIR_Click(object sender, EventArgs e)
        {
            double[] output2 = new double[1000];

            //升采样滤波
            EasyFIR<double>.UpSampleFIR(sinwave, coe, 2, out output2);
            fig2.Plot(output2);
        }

        private void DownSampleFIR_Click(object sender, EventArgs e)
        {
            double[] output3 = new double[1000];

            //降采样滤波
            EasyFIR<double>.DownSampleFIR(sinwave, coe, 2, out output3);
            fig2.Plot(output3);

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //清空easychart控件
            fig1.Clear();
            fig2.Clear();
        }

    }
}