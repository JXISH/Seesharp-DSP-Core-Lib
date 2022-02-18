/*******************************************
  * Synchronization example
  * Synchronize 2-way waveforms with Re-sampling
  * sinT is the waveform to be synchronized, the size is the number of channels *Sampling points
  * delay is the Sampling points of the delay between channels
  * sinSyn is the synchronized signal
  * *****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JY.ArrayUtility;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.Synchronization;
using SeeSharpTools.JXI.SignalProcessing.Generation;


namespace Synchronization_Winform
{
    public partial class Mainform : Form
    {
        #region Private Field       
        #endregion

        #region Constructor
        public Mainform()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Sync button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSyn_Click(object sender, EventArgs e)
        {
            //Get parameters
            int length = (int)numericUpDownSamples.Value;//Sampling points
            double sampleRate = (double)numericUpDownSampleRate.Value;//Sampling rate
            double frequency = (double)numericUpDownFrequency.Value;//Signal frequency
            double delay = (double)numericUpDownDelay.Value;//Delay points

            //Generate sync signal
            double[] sin1 = new double[length];//Pre-synchronization signal1
            double[] sin2 = new double[length];//Pre-synchronization signal2
            double[,] sin = new double[length,2];//Pre-synchronization signal
            double[,] sinT = new double[2,length ];//Pre-synchronization signal
            Generation.SineWave(ref sin1, 1, 0, frequency, sampleRate);
            Generation.SineWave(ref sin2, 1, delay * 360 * frequency / sampleRate, frequency, sampleRate);
            ArrayManipulation.Connected_2D_Array(sin1, sin2, ref sin);
            ArrayManipulation.Transpose(sin, ref sinT);
            //Synchronize
            var sinSyn=Synchronization.SyncWaveform(sinT, delay);
            //Display
            easyChartAfter.Plot(sinSyn);//After synchronization
            easyChartBefore.Plot(sinT);//Before synchronization
        }
        #endregion

        #region Methods
        #endregion      
    }
}
