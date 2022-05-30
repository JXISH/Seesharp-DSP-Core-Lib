using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.IQFile;
using SeeSharpTools.JXI.Numerics;

namespace IQ_File_Write_Example
{
    public partial class Form1 : Form
    {
        Complex[] _iqWav;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Generate IQ waveform and display on easychartX
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Definitions
            int signalLength = (int)numericUpDownSignalLength.Value;
            double frequency = (double)numericUpDownSignalFreqOffset.Value / (double)numericUpDownSampleRate.Value;
            //Generate noisy sine wave, real[]
            _iqWav = Vector.ToneInit(signalLength, frequency);
            //用随机数产生噪声种子数
            Random seed = new Random();
            double noiseLevel = (double)numericUpDownNoiseLevel.Value;
            double[] noiseR = Vector.GaussNoise(signalLength, 0, noiseLevel, (uint)seed.Next(10000));
            double[] noiseI = Vector.GaussNoise(signalLength, 0, noiseLevel, (uint)seed.Next(10000));
            Complex[] noise = new Complex[signalLength];
            //实部和虚部两个噪声合并生成复数噪声
            Vector.RealImageToComplex(noiseR, noiseI, noise);
            Vector.ArrayAdd(_iqWav, noise); //signal + noise ->signal
            double[] signalR = new double[signalLength];
            double[] signalI = new double[signalLength];
            Vector.ComplexToRealImage(_iqWav, signalR, signalI); //带噪声的IQ信号分给显示变量

            //plot timewaveform
            double[][] timePlotX = new double[2][];
            double[][] timePlotY = new double[2][];
            for (int i = 0; i < 2; i++)
            {
                timePlotX[i] = Vector.RampInit(signalLength, (double)1 / signalLength, -0.5);
            }
            timePlotY[0] = signalR;
            timePlotY[1] = signalI;

            easyChartXTimeWavefrom.Plot(timePlotX, timePlotY);

        }
        /// <summary>
        /// 对话窗输入文件路径，波形存入Vector File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveVector_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Vector files (*.iq)|*.iq|All files (*.*)|*.*";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IQFileInfo iqInfo = new IQFileInfo();
                iqInfo.Signal = new BasebandInfo();
                iqInfo.Signal.CenterFrequency = (double)numericUpDownCenterFreq.Value;
                iqInfo.Signal.IFCenterFrequency = (double)numericUpDownCenterFreq.Value;
                iqInfo.Signal.SampleRate = (double)numericUpDownSampleRate.Value;
                iqInfo.Signal.RFGain = 1;
                IQFile.WriteVectorFile(saveFileDialog1.FileName, _iqWav, iqInfo);
            }
        }
        /// <summary>
        /// 写入I16交织bin文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveBinFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Bin files (*.bin)|*.bin|All files (*.*)|*.*"; //扩展必须为小写bin，不然Read IQ File会报错
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IQFileInfo iqInfo = new IQFileInfo();
                iqInfo.Signal = new BasebandInfo();
                iqInfo.Signal.CenterFrequency = (double)numericUpDownCenterFreq.Value;
                iqInfo.Signal.IFCenterFrequency = (double)numericUpDownCenterFreq.Value;
                iqInfo.Signal.SampleRate = (double)numericUpDownSampleRate.Value;
                iqInfo.Signal.RFGain = 1;
                IQFile.WriteI16IQFile(saveFileDialog1.FileName, _iqWav, iqInfo);
            }
        }
    }
}
