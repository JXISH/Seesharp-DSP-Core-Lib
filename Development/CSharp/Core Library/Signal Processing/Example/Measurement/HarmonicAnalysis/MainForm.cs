/*******************************************
  * HarmonicAnalysis example
  * ToneAnalysis, calculating the frequency, amplitude, and phase of a sinusoidal signal
  * THDAnalysis, calculate THD
  * SINADAnalysis, calculate SINAD
  * *****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeeSharpTools.JXI.SignalProcessing.Measurement;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JY.ArrayUtility;
using System.Numerics;


namespace Winform_HarmonicAnalysis
{
    public partial class MainForm : Form
    {

        #region Private Field
        #endregion

        #region Constructor 
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Start button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            var noise = new double[(int)SampleCount.Value];
            Generation.UniformWhiteNoise(ref noise, (double)Amplitude.Value * 0.05);
            //Generating an input signal
            var sin = new double[(int)SampleCount.Value];
            Generation.SineWave(ref sin, (double)Amplitude.Value, (double)Phase.Value , (double)Frequency.Value,
                (double)SampleRate.Value);
            for (int i = 0; i < sin.Length; i++)//Plus DC
            {
                sin[i] += (double)DCOffset.Value;
            }
            ArrayCalculation.Add(sin, noise, ref sin);//Plus noise
            easyChartWave.Plot(sin, 0, 1.0 / (double)SampleRate.Value);

            //ToneAnalysis, calculate frequency, phase, Amplitude
            double fundamentalFreq = 0;
            double phase = 0;
            double amplitude = 0;
            HarmonicAnalysis.ToneAnalysis(sin, 1 / (double)SampleRate.Value, 
                out fundamentalFreq, out amplitude, out phase);

            //THDAnalysis, calculate THD
            double THD = 0;
            double[] componentsLevel = new double[0];
            HarmonicAnalysis.THDAnalysis(sin, 1 / (double)SampleRate.Value, 
                out fundamentalFreq, out THD, ref componentsLevel);
            THD *= 100; //JXISH fix

            //SINADAnalysis, calculate SINAD
            double SINAD = 0;
            HarmonicAnalysis.SINADAnalysis(sin, 1 / (double)SampleRate.Value,
                out fundamentalFreq, out SINAD, ref componentsLevel);
            SINAD = 10 * Math.Log10(SINAD); //JXISH fix

            //Display
            labelAmplitude.Text = amplitude.ToString();
            labelPhase.Text = phase.ToString();
            labelFrequency.Text = fundamentalFreq.ToString();
            labelTHD.Text = THD.ToString();
            labelSINAD.Text = SINAD.ToString();
            labelFundamentalFreqV2 .Text = componentsLevel[1].ToString();
            labelDCV2 .Text = componentsLevel[0].ToString();
        }
        #endregion

        #region Methods
        #endregion
    }
}
