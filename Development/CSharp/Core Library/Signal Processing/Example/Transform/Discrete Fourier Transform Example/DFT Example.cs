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
using SeeSharpTools.JXI.Numerics;
using SeeSharpTools.JXI.SignalProcessing.Transform;

namespace Discrete_Fourier_Transform_Example
{
    public partial class FormDFTExample : Form
    {
        public FormDFTExample()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //Definitions
            int signalLength = 100;
            double frequency = (double)5 / signalLength; //频率为整数周期，免除使用窗函数，使得逆变换完全重合
            //Generate noisy sine wave, real[]
            Complex[] signal = Vector.ToneInit(signalLength, frequency);
            Random seed = new Random();
            double[] noiseR = Vector.GaussNoise(signalLength, 0, 0.01, (uint)seed.Next(10000));
            double[] noiseI = Vector.GaussNoise(signalLength, 0, 0.01, (uint)seed.Next(10000));
            Complex[] noise = new Complex[signalLength];
            Vector.RealImageToComplex(noiseR, noiseI, noise);
            double[] signalR = new double[signalLength];
            double[] signalI = new double[signalLength];
            Vector.ComplexToRealImage(signal, signalR, signalI);

            //do DFT
            Complex[] dftSignal = new Complex[signalLength];
            DFT.ComputeForwardShifted(signal, dftSignal);
            double[] spectrum = Vector.GetComplexPower(dftSignal);

            //do reversed DFT
            Complex[] reversedDFTSignal = new Complex[signalLength];
            DFT.ComputeBackwardShifted(dftSignal); //原位计算反变换
            //赋值给显示数组
            double[] recoveredSignalR = new double[signalLength];
            double[] recoveredSignalI=new double[signalLength];
            Vector.ComplexToRealImage(dftSignal, recoveredSignalR, recoveredSignalI);

            //plot timewaveform
            double[][] timePlotX = new double[4][];
            double[][] timePlotY=new double[4][];
            for (int i = 0; i < 4; i++)
            {
                //timePlot[i] =new double[signalLength];
                timePlotX[i] = Vector.RampInit(signalLength, (double)1 / signalLength, -0.5);
            }
            timePlotY[0] = signalR;
            timePlotY[1] = signalI;
            timePlotY[2] = recoveredSignalR;
            timePlotY[3]= recoveredSignalI;
            easyChartXTimeWaveform.Plot(timePlotX, timePlotY);
            //plot spectrum
            easyChartXSpectrum.Plot(spectrum);
        }
    }
}
