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

namespace Generation_Example
{
    public partial class Form1 : Form
    {
        //接受输出信号的数组
        double[] outputwaveform = new double[1000];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            fig.Clear();
            //获取幅度
            double A = (double)numericUpDownA.Value;
            //获取初相位
            double phase = (double)numericUpDownPhase.Value;
            //获取正弦波频率
            double f = (double)numericUpDownFrequency.Value;
            //获取采样频率
            double samplingrate = (double)numericUpDownSampleRate.Value;
            // 获取占空比
            double dutycycle = (double)numericUpDownDutyCycle.Value;

            //根据不同的选择产生方波或正弦波
            if (WaveForm.Text == "SineWave")
            {
                Generation.SineWave(ref outputwaveform, A, phase, f, samplingrate);
            }
            else
            {
                Generation.SquareWave(ref outputwaveform, A, dutycycle, f, samplingrate);
            }

            fig.Plot(outputwaveform);
        }

        private void AddGaussianNoise_Click(object sender, EventArgs e)
        {
            double std = (double)numericUpDownNoiseStd.Value;
            double[] noise = new double[1000];
            Generation.GaussianWhiteNoise(ref noise, std);
            for (int i = 0; i < 1000; i++)
            {
                outputwaveform[i] += noise[i];
            }
            fig.Clear();
            fig.Plot(outputwaveform);

        }
    }
}
