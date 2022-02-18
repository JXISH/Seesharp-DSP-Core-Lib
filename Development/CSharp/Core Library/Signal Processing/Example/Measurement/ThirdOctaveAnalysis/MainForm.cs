/*******************************************
  * ThirdOctaveAnalysis example
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
using SeeSharpTools.JXI.SignalProcessing.Measurement;
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Conditioning.EasyFilters;
namespace Winform_ThirdOctaveAnalysis
{
    public partial class MainForm : Form
    {
        #region Private Field
        #endregion

        #region Constructor 
        public MainForm()
        {
            InitializeComponent(); 
            SampleRate.Enabled = false;
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            double duration = (int)(CycleCount.Value); //seconds;
            double sampleRate = (double)(SampleRate.Value); //you cannot change this
            int dataLength = (int)(sampleRate* duration);
            double[] sin = new double[dataLength];
            Generation.SineWave(ref sin,(double )Amplitude.Value, 0, (double)Frequency.Value, sampleRate);
            //Initialization
            ThirdOctaveAnalysis analysis = new ThirdOctaveAnalysis();
            //Configuration
            analysis.AverageMode =(TimeAveragingMode)Enum.Parse(typeof(TimeAveragingMode), comboBoxTimeAveragingMode .Text);
            analysis.WeightingFilterType =(WeightingType)Enum.Parse(typeof(WeightingType), comboBoxWeightingFilterType .Text);
            //Analysis
            var result = analysis.Analyze(sin, sampleRate);

            double[] octaveLevels = new double[result.ThirdOctaveLevels.Length];
            double[] nominalFrequencies = new double[result.ThirdOctaveLevels.Length];
            for (int i = 0; i < octaveLevels.Length; i++)
            {
                octaveLevels[i] = 20 * Math.Log10(result.ThirdOctaveLevels[i]) + 100;
                nominalFrequencies[i] = result.NominalFrequencies[i];
            }
            //Display
            //chartResult.Series[0].Points.DataBindXY(nominalFrequencies,octaveLevels);
            chartResult.Series[0].Points.DataBindY(octaveLevels);            easyChartWave.Plot(sin,0,1/ (double)SampleRate.Value);
        }
        #endregion

        #region Methods
        #endregion
    }
}
