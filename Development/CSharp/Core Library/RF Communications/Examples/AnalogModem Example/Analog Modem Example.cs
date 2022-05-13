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
using SeeSharpTools.JXI.RFCommunications.Modem;
using SeeSharpTools.JXI.SignalProcessing;
using SeeSharpTools.JXI.SignalProcessing.Generation;

namespace SeeSharpTools.JXI.RFCommunications.AnalogModem_Example
{
    public partial class FormAnalogModem : Form
    {
        public FormAnalogModem()
        {
            InitializeComponent();
            listBoxModulationType.SelectedIndex = 0;
            listBoxMessageWaveformType.SelectedIndex = 0;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //define
            int signalLength = 1000;
            Complex[] iqSignal = new Complex[signalLength];
            double sampleRate = 100000;
            double messageFrequency = 1000;
            double snr = 200;
            //generate baseband
            FunctionGenSignalType messageType = new FunctionGenSignalType();            
            switch (listBoxMessageWaveformType.SelectedIndex)
            {
                case 0:
                    messageType = FunctionGenSignalType.Sine;
                    break;
                case 1:
                    messageType = FunctionGenSignalType.Triangle;
                    break;
                default:
                    messageType = FunctionGenSignalType.Square;
                    break;
            }
            if (listBoxModulationType.SelectedIndex==0)
            {
                EasyModem.GenerateAM(iqSignal, sampleRate, 30, messageFrequency, messageType, snr);
            }
            else
            {
                EasyModem.GenerateFM(iqSignal, sampleRate, sampleRate * 0.2, messageFrequency, messageType, snr);
            }

            //demodulate
            double[] demodulatedMessage=null;
            if(listBoxModulationType.SelectedIndex==0)
            {
                AnalogDemodulation.AMDemodulate(iqSignal, ref demodulatedMessage);
            }
            else
            {
                AnalogDemodulation.FMDemodulate(iqSignal, ref demodulatedMessage);
            }
            //display
            double[,] displayIQ = new double[2,signalLength];
            for (int i = 0; i < signalLength; i++)
            {
                displayIQ[0, i] = iqSignal[i].Real;
                displayIQ[1, i] = iqSignal[i].Imaginary;
            }
            easyChartXIQ.Plot(displayIQ,0, (double)1/sampleRate);
            easyChartXMessage.Plot(demodulatedMessage, 0, (double)1/sampleRate);
        }
    }
}
