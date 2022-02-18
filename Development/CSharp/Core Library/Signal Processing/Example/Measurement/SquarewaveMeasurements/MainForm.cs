/*******************************************
  * SquarewaveMeasurements example
  * Can measure period, high and low level, pulse number, maximum pulse width, minimum pulse width, duty ratio
  * Can count high and low values, output histogram
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
using SeeSharpTools.JXI.SignalProcessing.Generation;
using SeeSharpTools.JXI.SignalProcessing.Measurement;

namespace Winform_SquarewaveMeasurements
{
    public partial class MainForm : Form
    {
        #region Private Field
        #endregion

        #region Constructor 
        public MainForm()
        {
            InitializeComponent();
            ///If you don't want to reactivate on your client's computer, call the following function;
            ///you must have a license,include  following information:ComputerID, ActiveCode, and ActiveTime;
            ///if your license is active all the time,ActiveTime is "";
            ///if temporary activation, the uint of ActiveTime is second;
            //LicenseBase.Validate("ComputerID", "ActiveCode", "ActiveTime"); 
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

            //Generating signal
            double[] waveformTmp = new double[(int)SamplesCount .Value ];
            double[] waveformRef = new double[(int)SamplesCount.Value];
            Generation.SquareWave(ref waveformTmp, (double)Amplitude.Value, (double)DutyCycle.Value, (double)Frequency.Value, (double)SampleRate.Value);
            Generation.SquareWave(ref waveformRef, (double)Amplitude.Value, (double)DutyCycle.Value, (double)Frequency.Value, (double)SampleRate.Value);
            ArrayCalculation.AddOffset(ref waveformTmp, (double)DCOffset.Value);
            ArrayCalculation.AddOffset(ref waveformRef, (double)DCOffset.Value);

            //Phase difference
            double[] data = new double[(int)(SampleRate.Value/ Frequency.Value*Phase.Value /360)];
            Array.Copy(waveformRef, data, data.Length);
            Array.Copy(waveformRef, 0, waveformRef, data.Length, (waveformRef.Length - data.Length));
            Array.Copy(data, 0, waveformRef, 0, data.Length);
            Phase.Value = data.Length / (SampleRate.Value / Frequency.Value) * 360;

            double highLevel;//High level
            double lowLevel;//Low level
            double period;//Cycle
            double dutycycle;//Duty cycle
            double phase;//Phase
            double pulseCount;//Number of pulses
            double pulseMaxLength;//Maximum pulse width
            double pulseMinLength;//Minimum pulse width
            //Measuring various parameters
            SquarewaveMeasurements.AmplitudeAnalysis(waveformTmp, out highLevel, out lowLevel);
            SquarewaveMeasurements.PeriodAnalysis(waveformTmp, out period, out dutycycle,
                                   out pulseCount, out pulseMaxLength, out pulseMinLength);
            SquarewaveMeasurements.PhaseAnalysis(waveformTmp, waveformRef, out phase);
            //Statistical level histogram
            double[] levelHistogramX = new double[0];
            double[] levelHistogramY = new double[0];
            SquarewaveMeasurements.GetLevelHistogram(waveformTmp, ref levelHistogramX, ref
           levelHistogramY, new SquarewaveMeasurements.Histogram());

            for (int j = 0; j < levelHistogramX.Length; j++)
            {
                Console.WriteLine("{0}:{1}", levelHistogramX[j], levelHistogramY[j]);
            }

            //Display
            double[,] waveform = new double[(int)SamplesCount.Value,2];
            double[,] waveformT = new double[2, (int)SamplesCount.Value];
            ArrayManipulation.Connected_2D_Array(waveformTmp, waveformRef, ref waveform);
            ArrayManipulation.Transpose(waveform, ref waveformT);
            easyChartWave.Plot(waveformT, 0, 1 / (double)SampleRate.Value);

            labelPeriod.Text = period.ToString();
            labelPhase.Text = phase.ToString();
            labelPulseCount.Text = pulseCount.ToString();
            labelMaxWidth .Text = pulseMaxLength.ToString();
            labelMinWidth .Text = pulseMinLength.ToString();
            labelHighLevel.Text = highLevel.ToString();
            labelLowLevel.Text = lowLevel.ToString();
            labeldutyCycle.Text = dutycycle.ToString();
        }
        #endregion

        #region Methods
        #endregion
    }
}
